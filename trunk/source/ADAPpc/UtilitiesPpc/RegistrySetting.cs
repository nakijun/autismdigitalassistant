using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Reflection;
using System.Collections;

namespace UtilitiesPpc
{
    public class RegistrySetting : IDisposable
    {
        private Hashtable settings;
        private RegistryKey localSetting;

        /*
        Registry Settings for Language
        HKEY_LOCAL_MACHINE\SOFTWARE\Inflaton\ADA\AppName
        "Language"="en"
        */
        public RegistryKey LocalSetting
        {
            get { return localSetting; }
        }

        private RegistryKey globalSetting;

        /*
        Registry Settings for Language
        HKEY_LOCAL_MACHINE\SOFTWARE\Inflaton\ADA
        "Language"="en"
        */
        public RegistryKey GlobalSetting
        {
            get { return globalSetting; }
        }

        public RegistryKey this[string appName]
        {
            get
            {
                RegistryKey key = this.settings[appName] as RegistryKey;
             
                if (key == null)
                {
                    key = Registry.LocalMachine.CreateSubKey("SOFTWARE").CreateSubKey("Inflaton").CreateSubKey("ADA").CreateSubKey(appName);
                    this.settings[appName] = key;
                }

                return key;
            }
        }

        public RegistrySetting()
        {
            string appName = AppDomain.CurrentDomain.FriendlyName;
            appName = appName.Replace(".exe", "");

            this.globalSetting = Registry.LocalMachine.CreateSubKey("SOFTWARE").CreateSubKey("Inflaton").CreateSubKey("ADA");
            this.localSetting = Registry.LocalMachine.CreateSubKey("SOFTWARE").CreateSubKey("Inflaton").CreateSubKey("ADA").CreateSubKey(appName);

            this.settings = new Hashtable();
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.globalSetting.Close();
            this.localSetting.Close();

            foreach (RegistryKey key in this.settings.Values)
            {
                if (key != null)
                {
                    key.Close();
                }
            }
        }

        #endregion
    }
}
