namespace Repository
{
    public interface ILogger
    {
        void AddLogToFile(string text, string fileName);
    }
}