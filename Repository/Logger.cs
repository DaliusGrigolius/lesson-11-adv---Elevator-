using System.IO;

namespace Repository
{
    public class Logger
    {
        public void AddLogToFile(string text)
        {
            string filePath = @"C:\Users\User\Desktop\repos\lesson 11 adv\Logs\logger.txt";
            File.WriteAllText(filePath, text);
        }
    }
}
