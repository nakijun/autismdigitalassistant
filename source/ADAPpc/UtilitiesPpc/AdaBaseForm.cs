using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.WindowsMobile.Status;
using System.Reflection;
using System.IO;

namespace UtilitiesPpc
{
    public partial class AdaBaseForm : Form
    {
        public const string ENGLISH_CULTURE = "en";
        public const string SIMPLIFIED_CHINESE_CULTURE = "zh-CHS";
        public const string TRADITIONAL_CHINESE_CULTURE = "zh-CHT";

        private const string REGISTRY_LANGUAGE = "Language";
        private const string REGISTRY_IS_DEPLOYED = "IsDeployed";
        private const string REGISTRY_IS_BIG_MEMORY = "IsBigMemory";

        private RegistrySetting _setting;

        private RegistryState _languageRegistryState;

        public RegistrySetting Setting
        {
            get { return _setting; }
            set { _setting = value; }
        }

        private string _cultureName;

        public string CultureName
        {
            get { return _cultureName; }
        }

        private bool _isDeployed;

        public bool IsDeployed
        {
            get { return _isDeployed; }
        }

        private bool _isBigMemory;

        public bool IsBigMemory
        {
            get { return _isBigMemory; }
        }

        public AdaBaseForm()
        {
            InitializeComponent();

            this._setting = new RegistrySetting();

            this._languageRegistryState = new RegistryState(@"HKEY_LOCAL_MACHINE\SOFTWARE\Inflaton\ADA", REGISTRY_LANGUAGE);
            this._languageRegistryState.Changed += new ChangeEventHandler(languageRegistryState_Changed);
            
            this._isDeployed = false;

            object o = this._setting.GlobalSetting.GetValue(REGISTRY_IS_DEPLOYED);
            if (o != null)
            {
                this._isDeployed = Convert.ToBoolean(o);
            }

            o = this._setting.GlobalSetting.GetValue(REGISTRY_IS_BIG_MEMORY);
            if (o != null)
            {
                this._isBigMemory = Convert.ToBoolean(o);
            }
        }

        void languageRegistryState_Changed(object sender, ChangeEventArgs args)
        {
            string cultureName = _languageRegistryState.CurrentValue as string;
            this.ChangeCulture(cultureName);
        }

        private void AdaBaseForm_Load(object sender, EventArgs e)
        {
            this.InitCulture();

            if (!IsBigMemory && ControlBox && MinimizeBox)
            {
                MinimizeBox = false;
            }
        }

        private void AdaBaseForm_Closed(object sender, EventArgs e)
        {
            if (this.IsDeployed && this.GetType().Name == "MainForm")
            {
                string mainWindowName = "ADA Main";

                if (this.Text != mainWindowName)
                {
                    string application = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
                    string mainApp = Path.GetDirectoryName(application) + "\\AdaMainPpc.exe";

                    FormEngine.BringWindowToTop(mainWindowName, mainApp);
                }
            }
        }

        private void InitCulture()
        {
            string cultureName = ENGLISH_CULTURE;// CultureInfo.CurrentCulture.Parent.Name;
            cultureName = (string)this._setting.GlobalSetting.GetValue(REGISTRY_LANGUAGE, cultureName);

            if (!FormLanguageSwitchSingleton.Instance.IsCultureSupported(cultureName))
            {
                this._cultureName = ENGLISH_CULTURE;
            }

            this.ChangeCulture(cultureName);
        }

        protected bool ChangeCulture(string cultureName)
        {
            if (cultureName != this._cultureName)
            {
                this._cultureName = cultureName;

                CultureInfo newCulture = new CultureInfo(cultureName);
                FormLanguageSwitchSingleton.Instance.ChangeCurrentCulture(newCulture);
                FormLanguageSwitchSingleton.Instance.ChangeLanguage(this);

                this._setting.GlobalSetting.SetValue(REGISTRY_LANGUAGE, cultureName);

                this.OnCultureChanged(newCulture);
                return true;
            }

            return false;
        }

        protected virtual void OnCultureChanged(CultureInfo newCulture)
        {
        }

        private void AdaBaseForm_GotFocus(object sender, EventArgs e)
        {
            if (this.IsDeployed)
            {
                FormEngine.SetFullScreen(this);
            }
        }
    }
}