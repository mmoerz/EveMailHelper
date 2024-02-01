using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Text.RegularExpressions;

using EveMailHelper.ChatLogParser.Models;

namespace EveMailHelper.ChatLogParser
{
    public class ChatLogParser : IChatLogParser
    {
        private const string _SECTION = "Upload";
        private readonly IConfiguration _configuration = null!;
        private readonly ILogger<ChatLogParser> _logger = null!;

        public string GetUploadPath
        {
            get
            {
                if (_configuration == null)
                    throw new Exception("missing configuration");
                IConfigurationSection uploadSection = _configuration.GetSection(_SECTION);
                string? directory = uploadSection
                    .GetValue<string>("Directory");
                if (directory == null)
                    throw new Exception("missing Directory in configuration file");
                
                return (string) directory;
            }
        }

        public ChatLogParser(IConfiguration Configuration, ILogger<ChatLogParser> logger)
        {
            _configuration = Configuration;
            _logger = logger;
        }

        public ChatLogParser()
        {

        }

        //public ParserChat Chat { get; set; } = new();

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

            var path = //cfgSection.GetValue<string>("Directory");
                GetUploadPath;
            if (path == null)
                throw new Exception("missing directory in Upload section in appsettings.json");

            if (!File.Exists(path))
                throw new Exception($"missing directory ${path}");
        }

        private ParserChat ParseHeader(StreamReader txtR)
        {
            ParserChat chat = new();

            bool HeaderComplete = false;
            int[] emptyLines = new int[7] { 1, 2, 3, 4, 6, 12, 13 };

            string? line;

            while (!HeaderComplete && (line = txtR.ReadLine()) != null)
            {
                LineCount++;
                if (emptyLines.Contains(LineCount) && !string.IsNullOrEmpty(line))
                    break;
                Match? match;
                switch (LineCount)
                {
                    case 5:
                        if (!ParserRegexes.dashLine.IsMatch(line))
                            throw new Exception("line 5 was not a dashline");
                        break;

                    case 7:
                        match = ParserRegexes.channelId.Match(line);
                        if (!match.Success)
                            throw new Exception("line 7 didn't contain channelid");
                        chat.ChannelId = match.Groups[1].Value;
                        break;
                    case 8:
                        match = ParserRegexes.channelName.Match(line);
                        if (!match.Success)
                            throw new Exception("line 8 did not contain channel name");
                        chat.ChannelName = match.Groups[1].Value;
                        break;
                    case 9:
                        match = ParserRegexes.Listener.Match(line);
                        if (!match.Success)
                            throw new Exception("line 9 did not contain Listener");
                        chat.Listener = match.Groups[1].Value;
                        break;
                    case 10:
                        match = ParserRegexes.SessionStarted.Match(line);
                        if (!match.Success)
                            throw new Exception("line 10 did not contain session start time");
                        chat.StartedAt = DateTime.Parse(match.Groups[1].Value);
                        break;
                    case 11:
                        if (ParserRegexes.dashLine.IsMatch(line))
                            HeaderComplete = true;
                        else
                            throw new Exception("line 11 did not contain dashline");
                        break;
                    default:
                        break;
                }
            }
            return chat;
        }

        public ParserChat ParseFile(string filename)
        {
            LineCount = 0;
            using var txtR = File.OpenText(filename);

            return ParseStream(txtR);
        }

        public ParserChat ParseStream(StreamReader streamReader)
        { 
            var chat = ParseHeader(streamReader);
            string? line;

            if ((line = streamReader.ReadLine()) == null)
                return chat;
            LineCount++;

            if (string.IsNullOrEmpty(line))
                if ((line = streamReader.ReadLine()) == null)
                    return chat;

            do
            {
                LineCount++;
                
                var match = ParserRegexes.Message.Match(line);
                if (!match.Success)
                    throw new Exception($"unknown message format encountered at line: {LineCount}");
                chat.Messages.Add(new ParserChatMessage()
                {
                    TimeStamp = DateTime.Parse(match.Groups["timestamp"].Value),
                    Author = match.Groups["author"].Value,
                    Message = match.Groups["message"].Value
                });
            } while ((line = streamReader.ReadLine()) != null);
            return chat;
        }
    }
}