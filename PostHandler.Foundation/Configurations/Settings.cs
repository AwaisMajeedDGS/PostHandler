namespace PostHandler.Foundation.Configurations
{

    public abstract class Settings
    {
        public string ReadConnectionString { get; set; }
        public string WriteConnectionString { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPHost { get; set; }
        public string NCUserName { get; set; }
        public string NCPassword { get; set; }
        public string NCDomain { get; set; }
    }
    public class SMSPortalSettings : Settings
    {

    }
    public class DBSettings
    {
        public string ReadConnectionString { get; set; }
        public string WriteConnectionString { get; set; }
    }

    public class APISettings : Settings
    {
        //public string ElasticSearchServerIP { get; set; }
        public string SMSportal { get; set; }
        public string SMSAPI { get; set; }
        public string ProgramId { get; set; }
        public string NRGTracePath { get; set; }
        public string QueueSyncTime { get; set; }
        public string NTLogToProcess { get; set; }
        public string NRGAuthkey { get; set; }
                                           // public string BugReportEmailCredential { get; set; }
    }
}
