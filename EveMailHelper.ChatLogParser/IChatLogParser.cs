using EveMailHelper.ChatLogParser.Models;

namespace EveMailHelper.ChatLogParser
{
    public interface IChatLogParser
    {
        string GetUploadPath { get; }

        ParserChat ParseFile(string filename);
        ParserChat ParseStream(StreamReader streamReader);
        void PreFlightCheck();
    }
}