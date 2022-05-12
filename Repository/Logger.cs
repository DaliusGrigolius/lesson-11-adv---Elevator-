using System.IO;

namespace Repository
{
    public class Logger
    {
        public static void AddLogToFile(string text)
        {
            string filePath = @"..\..\..\..\Logs\logger.txt";
            File.AppendAllText(filePath, text);
        }
    }
}
