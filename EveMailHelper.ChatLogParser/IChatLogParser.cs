namespace EveMailHelper.ChatLogParser
{
    public interface IChatLogParser
    {
        string GetUploadPath { get; }

        void ParseFile(string filename);
        void PreFlightCheck();
    }
}