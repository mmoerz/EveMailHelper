using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Text.RegularExpressions;

using EveMailHelper.ChatLogParser.Models;

namespace EveMailHelper.ChatLogParser
{
    public class LogParser : IChatLogParser
    {
        private const string _SECTION = "Upload";
        private readonly IConfiguration _configuration = null!;
        private readonly ILogger<LogParser> _logger = null!;

        public string GetUploadPath
        {
            get
            {
                return _configuration
                    .GetSection(_SECTION)
                    .GetValue<string>("Directory");
            }
        }

        public LogParser(IConfiguration Configuration, ILogger<LogParser> logger)
        {
            _configuration = Configuration;
            _logger = logger;
        }

        public LogParser()
        {

        }

        public ParserChat Chat { get; set; } = new();

        public int LineCount { get; set; } = 0;

        /// <summary>
        /// checks that the configuration file contains the
        /// necessary section and configuration values and
        /// that the necessary upload directory exists.
        /// </summary>
        public void PreFlightCheck()
        {
            // get upload directory from configuration
            var cfgSection = _configuration.GetSection(_SECTION);
            if (cfgSection == null)
            {
                _logger.LogError("missing Upload Section in appsettings.json");
                throw new Exception("missing Upload Section in appsettings.json");
            }

            var path = cfgSection.GetValue<string>("Directory");
            if (path == null)
                throw new Exception("missing directory in Upload section in appsettings.json");

            if (!File.Exists(path))
                throw new Exception($"missing directory ${path}");
        }

        private bool ParseHeader(TextReader txtR)
        {
            bool HeaderComplete = false;
            int[] emptyLines = new int[7] { 1, 2, 3, 4, 6, 12, 13 };
            

            string? line;
            
            while ((line = txtR.ReadLine()) != null && !HeaderComplete)
            {
                LineCount++;
                if (emptyLines.Contains(LineCount) && !string.IsNullOrEmpty(line))
                    break;
                Match? match; 
                switch (LineCount)
                {
                    case 5:
                        if (!ParserRegexes.dashLine.IsMatch(line))
                            return false;
                        break;

                    case 7:
                        match = ParserRegexes.channelId.Match(line);
                        if (!match.Success)
                            return false;
                        Chat.ChannelId = match.Groups[1].Value;
                        break;
                    case 8:
                        match = ParserRegexes.channelName.Match(line);
                        if (!match.Success)
                            return false;
                        Chat.ChannelName = match.Groups[1].Value;
                        break;
                    case 9:
                        match = ParserRegexes.Listener.Match(line);
                        if (!match.Success)
                            return false;
                        Chat.Listener = match.Groups[1].Value;
                        break;
                    case 10:
                        match = ParserRegexes.SessionStarted.Match(line);
                        if (!match.Success)
                            return false;
                        Chat.StartedAt = DateTime.Parse(match.Groups[1].Value);
                        break;
                    case 11:
                        if (ParserRegexes.dashLine.IsMatch(line))
                            HeaderComplete = true;
                        break;
                    default:
                        break;
                }
            }
            return HeaderComplete;
        }

        public void ParseFile(string filename)
        {
            LineCount = 0;
            using var txtR = File.OpenText(filename);

            var headerResult = ParseHeader(txtR);

            string? line;
            while ((line = txtR.ReadLine()) != null)
            {
                LineCount++;
                var match = ParserRegexes.Message.Match(line);
                if (!match.Success)
                    throw new Exception($"unknown message format encountered in file {filename} at line: {LineCount}");
                Chat.Messages.Add(new ParserChatMessage()
                {
                    TimeStamp = DateTime.Parse(match.Groups["timestamp"].Value),
                    Author = match.Groups["author"].Value,
                    Message = match.Groups["message"].Value
                });
            }
        }
    }
}