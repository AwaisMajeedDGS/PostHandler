namespace PostHandler.Foundation.Configurations
{
    using Helper;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class PostHandlerConfigurationManager : ConfigurationManagerBase
    {
        #region Fields and Properties
        private static volatile PostHandlerConfigurationManager _instance;
        private SMSPortalSettings _smsPortalSettings = new SMSPortalSettings();
        public SMSPortalSettings SMSPortalSettings { get { return _smsPortalSettings; } }
        #endregion

        private PostHandlerConfigurationManager()
        {
            InitilizeConfigurationSections();
        }

        public static PostHandlerConfigurationManager Current
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new PostHandlerConfigurationManager();
                        }
                    }
                }
                return _instance;
            }
        }

        protected override void InitilizeConfigurationSections()
        {
            ConfigurationActionDispatcher = new Dictionary<ConfigType, Action<ConfigType>>();
            ConfigurationActionDispatcher.Add(ConfigType.BrainSettings, LoadSMSPortalSettings);
            foreach (var each in ConfigurationActionDispatcher)
            {
                ConfigurationActionDispatcher[each.Key](each.Key);
            }
        }

        private void LoadSMSPortalSettings(ConfigType type)
        {
            Hashtable smsPortalSettings = null;
            if (EnsureConfigurationSection(type, out smsPortalSettings))
            {
                _smsPortalSettings.WriteConnectionString = SafeConvert.ToString(smsPortalSettings["WriteConnectionString"]);
                _smsPortalSettings.ReadConnectionString = SafeConvert.ToString(smsPortalSettings["ReadConnectionString"]);
            }
        }
    }

    public class APIConfigurationManager : ConfigurationManagerBase
    {
        #region Fields and Properties
        private static volatile APIConfigurationManager _instance;
        private APISettings _apiSettings = new APISettings();
        public APISettings APISettings { get { return _apiSettings; } }
        #endregion

        private APIConfigurationManager()
        {
            InitilizeConfigurationSections();
        }

        public static APIConfigurationManager Current
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new APIConfigurationManager();
                        }
                    }
                }
                return _instance;
            }
        }

        protected override void InitilizeConfigurationSections()
        {
            ConfigurationActionDispatcher = new Dictionary<ConfigType, Action<ConfigType>>();
            ConfigurationActionDispatcher.Add(ConfigType.APISettings, LoadAPISettings);
            foreach (var each in ConfigurationActionDispatcher)
            {
                ConfigurationActionDispatcher[each.Key](each.Key);
            }
        }

        private void LoadAPISettings(ConfigType type)
        {
            Hashtable apiSettings = null;
            if (EnsureConfigurationSection(type, out apiSettings))
            {
                
                _apiSettings.WriteConnectionString = SafeConvert.ToString(apiSettings["WriteConnectionString"]);
                _apiSettings.ReadConnectionString = SafeConvert.ToString(apiSettings["ReadConnectionString"]);
                _apiSettings.SMTPPort = Convert.ToInt32(apiSettings["SMTPPort"]);
                _apiSettings.SMTPHost = SafeConvert.ToString(apiSettings["SMTPHost"]);
                _apiSettings.NCUserName = SafeConvert.ToString(apiSettings["NCUserName"]);
                _apiSettings.NCPassword = SafeConvert.ToString(apiSettings["NCPassword"]);
                _apiSettings.NCDomain = SafeConvert.ToString(apiSettings["NCDomain"]);
                _apiSettings.SMSportal = SafeConvert.ToString(apiSettings["BaseUrl"]);
                _apiSettings.SMSAPI = SafeConvert.ToString(apiSettings["APIURL"]);
                _apiSettings.ProgramId = SafeConvert.ToString(apiSettings["ProgramId"]);
                _apiSettings.NRGTracePath = SafeConvert.ToString(apiSettings["NRGTracePath"]);
                _apiSettings.QueueSyncTime = SafeConvert.ToString(apiSettings["QueueSyncTime"]);
                _apiSettings.NTLogToProcess = SafeConvert.ToString(apiSettings["NTLogToProcess"]);
                _apiSettings.NRGAuthkey = SafeConvert.ToString(apiSettings["NRGAuthkey"]);
               
            }
        }
    }

}
