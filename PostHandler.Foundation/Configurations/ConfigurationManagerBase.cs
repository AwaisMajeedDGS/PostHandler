namespace PostHandler.Foundation.Configurations
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;

    public abstract class ConfigurationManagerBase
    {

        #region Fields and Properties        
        protected static object _syncRoot = new Object();
        protected static Dictionary<ConfigType, Action<ConfigType>> ConfigurationActionDispatcher;
        #endregion

        protected enum ConfigType
        {
            DialerAppSettings,
            InsightAppSettings,
            AdminAppSettings,
            ResourceApiSettings,
            AuthorizationServerSettings,
            UrlSettings,
            LogsSettings,
            TelephonySettings,
            DBSettings,
            APISettings,
            BrainSettings
        }

        protected abstract void InitilizeConfigurationSections();

        protected static bool EnsureConfigurationSection(ConfigType type, out Hashtable configrations)
        {
            configrations = new Hashtable();
            var sectionName = type.ToString();
            try
            {
                configrations = ConfigurationManager.GetSection(type.ToString()) as Hashtable;
            }
            catch
            {
                throw new Exception(string.Format("{0} section is not defined in App.Config.", sectionName));
            }
            return true;
        }
    }
}

