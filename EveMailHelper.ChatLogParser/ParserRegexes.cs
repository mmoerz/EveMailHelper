using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EveMailHelper.ChatLogParser
{
    public static class ParserRegexes
    {
        public static Regex dashLine = 
            new Regex(@"^\s+(?<dashes>-+)$", RegexOptions.Compiled);
        public static Regex channelId = 
            new Regex(@"^\s+Channel ID:\s+(?<channelid>[A-Za-z0-9]+_[a-f0-9]+$)", RegexOptions.Compiled);
        public static Regex channelName = 
            new Regex(@"^\s+Channel Name:\s+(?<channelname>/[A-Za-z0-9]+)$", RegexOptions.Compiled);
        public static Regex Listener = 
            new Regex(@"^\s+Listener:\s+(?<listener>[A-Za-z0-9_'\s]+)$", RegexOptions.Compiled);
        public static Regex SessionStarted = 
            new Regex(@"^\s+Session started:\s+(?<sessionstart>[0-9:.\s]+)$", RegexOptions.Compiled);

        public static Regex Message =
            new Regex(@"\s*\[\s+(?<timestamp>[0-9:.\s]+)\s\]\s+(?<author>[A-Za-z0-9'`""._\s-]+)\s+\>\s+(?<message>.*)$",
                RegexOptions.Compiled);

        public static Regex Timestamp =
            new Regex(@"\s*\[\s+(?<timestamp>[0-9:\.\s]+)\s\]");
    }
}
