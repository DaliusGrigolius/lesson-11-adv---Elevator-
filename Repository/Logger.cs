using System.IO;

namespace Repository
{
    public class Logger
    {
        public static void AddLogToFile(string text)
        {
            string filePath = @"..\..\..\..\Logs\logger.txt";

            if (Directory.Exists(@$"..\..\..\..\Logs\"))
            {
                File.AppendAllText(filePath, text);
            }
            else
            {
                Directory.CreateDirectory(@$"..\..\..\..\Logs\");
                File.AppendAllText(filePath, text);
            }
        }
    }
}
