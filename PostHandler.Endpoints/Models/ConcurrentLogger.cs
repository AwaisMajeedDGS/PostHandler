using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using PostHandler.Foundation.Configurations;

namespace PostHandler.Endpoints.Models
{

    public class QueueLogger
    {
        public QueueLogger()
        {
             
        }

        public QueueLogger(string agentid, string logger)
        {
            AgentId = agentid;
            Logger = logger;
        }

        public string AgentId { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public string Level { get; set; } = "type: Info";
        public object StackTrace { get; set; } = "No more tracing required";
        public string Message { get; set; }
        public string Logger { get; set; }
    }

    public static class ConcurrentLogger
    {

        private static ConcurrentQueue<QueueLogger> _QueueLogger = new ConcurrentQueue<QueueLogger>();
        private static readonly string TracePath = "";
        private static readonly int QueueSyncTime = 20000;
        private static readonly int NTLogToProcess = 1000;

        static ConcurrentLogger()
        {
            TracePath = APIConfigurationManager.Current.APISettings.NRGTracePath;
            QueueSyncTime = Convert.ToInt32(APIConfigurationManager.Current.APISettings.QueueSyncTime);
            NTLogToProcess = Convert.ToInt32(APIConfigurationManager.Current.APISettings.NTLogToProcess);
        }

        public static Task Enqueue(QueueLogger log)
        {
            _QueueLogger.Enqueue(log);
            return Task.FromResult(0);
        }

        public static Task InitiateLogger()
        {
            try
            {
                Timer timer = new Timer(QueueSyncTime);
                var handler = new ElapsedEventHandler(async (sender, e) => await LogMessages());
                timer.Elapsed += handler;
                timer.Start();
            }
            catch (Exception)
            {
            }

            return Task.FromResult(0);
        }

        private async static Task LogMessages()
        {
            List<QueueLogger> logs = new List<QueueLogger>();
            for (var i = 0; i < NTLogToProcess; i++)
            {
                QueueLogger log;
                if (!_QueueLogger.TryDequeue(out log))
                {
                    break;
                }
                logs.Add(log);
            }

            if (logs.Count == 0)
            {
                return;
            }

            await WriteLog(logs: logs, fileName: $"{DateTime.Now.ToString("yyyyMMdd")}", path: TracePath);

        }

        private static async Task WriteLog(List<QueueLogger> logs, string fileName, string path = @"c:\traces\logs.txt")
        {
            try
            {
                
                bool exists = Directory.Exists(path);
                if (!exists)
                {
                    Directory.CreateDirectory(path);
                }
                if (!path.EndsWith(@"\"))
                    path += @"\";

                StringBuilder builder = new StringBuilder();

                foreach (var item in logs)
                {
                    builder.Append($"{item.Time}|{item.AgentId}|{item.Level}|{item.Logger}|{item.Message}|{item.StackTrace}");
                    builder.Append(Environment.NewLine);
                }

                string filePath = path + "Log(" + fileName + ").txt";
                if (!string.IsNullOrEmpty(filePath))
                {
                    using (StreamWriter sw = (File.Exists(filePath)) ? File.AppendText(filePath) : File.CreateText(filePath))
                    {
                        await sw.WriteAsync(builder.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

            }


        }
    }
}