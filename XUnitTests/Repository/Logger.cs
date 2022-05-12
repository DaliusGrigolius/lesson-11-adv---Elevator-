using System.IO;

namespace Repository
{
    public class Logger : ILogger
    {
        public void AddLogToFile(string text, string fileName)
        {
            string filePath = @$"..\..\..\..\Logs\{fileName}.txt";

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
