namespace PostHandler.Foundation.Helper
{
    using System;
    using System.IO;

    public static class TextWriter
    {
        public static async void WriteTextFileLogs(string text, string fileName, string directoryName)
        {
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            fileName = directoryName + fileName + ".txt";
            if (!File.Exists(fileName))
            {
                using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    var tw = new StreamWriter(fs);
                    tw.WriteLine("-----------------------------------------------------------------------------");
                    tw.WriteLine("--------------------------------  Server Time -------------------------------");
                    tw.WriteLine("--------------------------------" + DateTime.Now + "--------------------------");
                    tw.Close();
                }
            }
            if (!File.Exists(fileName)) return;
            using (var sw = new StreamWriter(fileName, true))
            {
                await sw.WriteLineAsync(Environment.NewLine);
                await sw.WriteLineAsync(DateTime.Now + " : " + text);
                sw.Close();
            }
        }
    }
}
