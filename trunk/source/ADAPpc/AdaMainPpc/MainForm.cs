using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using UtilitiesPpc;
using System.Globalization;
using Microsoft.Win32;
using Microsoft.WindowsMobile.Status;

namespace AdaMainPpc
{
    public partial class MainForm : AdaBaseForm
    {
        private string[] ADA_APPLICATIONS = new string[] 
        {
            "AdaSchedulePpc.exe", 
            "AdaWorkSystemPpc.exe", 
            "AdaCommunicatorPpc.exe", 
            "AdaTimerPpc.exe", 
            "AdaMoneyPpc.exe" 
        };

        private string _appDir;

        private SystemState _connectionsCount;

        private SystemState _connectionsCount2;

        public MainForm()
        {
            InitializeComponent();
            
            this.listBox2App.Items.Add(this.listBox2ItemSchedule);
            this.listBox2App.Items.Add(this.listBox2ItemWorkSystem);
            this.listBox2App.Items.Add(this.listBox2ItemCommunicator);
            this.listBox2App.Items.Add(this.listBox2ItemTimer);
            this.listBox2App.Items.Add(this.listBox2ItemMoney);

            this.listBox2App.SelectedIndex = 0;

            string application = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
            this._appDir = Path.GetDirectoryName(application) + "\\";

            _connectionsCount = new SystemState(SystemProperty.ConnectionsCount);

            _connectionsCount.ComparisonType = StatusComparisonType.Equal;
            _connectionsCount.ComparisonValue = 0;

            string fullyQualifiedName = (System.Reflection.Assembly.GetExecutingAssembly().GetModules())[0].FullyQualifiedName;

            if (IsDeployed)
            {
                _connectionsCount.EnableApplicationLauncher("ADASync", fullyQualifiedName, "-EVENT");
            }
            else
            {
                _connectionsCount.DisableApplicationLauncher();
            }

            _connectionsCount2 = new SystemState(SystemProperty.ConnectionsCount);
            _connectionsCount2.Changed += new ChangeEventHandler(connectionsCount_Changed);
        }

        protected override void OnCultureChanged(CultureInfo newCulture)
        {
            global::AdaMainPpc.Properties.Resources.Culture = newCulture;

            this.listBox2ItemSchedule.Text = global::AdaMainPpc.Properties.Resources.Schedule;
            this.listBox2ItemWorkSystem.Text = global::AdaMainPpc.Properties.Resources.WorkSystem;
            this.listBox2ItemCommunicator.Text = global::AdaMainPpc.Properties.Resources.Communicator;
            this.listBox2ItemTimer.Text = global::AdaMainPpc.Properties.Resources.Timer;
            this.listBox2ItemMoney.Text = global::AdaMainPpc.Properties.Resources.Money;
        }

        void connectionsCount_Changed(object sender, ChangeEventArgs args)
        {
            if (SystemState.ConnectionsCount == 0)
            {
                this.BringToFront();
            }
        }

        private void OpenApplication(string applicationName)
        {
            string arguments = "";

            try
            {
                Process.Start(this._appDir + applicationName, arguments);

                if (!IsBigMemory)
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void listBox2App_Click(object sender, EventArgs e)
        {
            this.RunSelectedApplication();
        }

        private void listBox2App_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.RunSelectedApplication();
            }
        }

        private void RunSelectedApplication()
        {
            int selectedAppIndex = this.listBox2App.SelectedIndex;

            if (selectedAppIndex >= 0 && selectedAppIndex < ADA_APPLICATIONS.Length)
            {
                this.OpenApplication(ADA_APPLICATIONS[selectedAppIndex]);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.menuItemSimplifiedChinese.Enabled = FormLanguageSwitchSingleton.Instance.IsCultureSupported(SIMPLIFIED_CHINESE_CULTURE);
            this.menuItemTraditionalChinese.Enabled = FormLanguageSwitchSingleton.Instance.IsCultureSupported(TRADITIONAL_CHINESE_CULTURE);

            this.CultureChanged();
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
        }

        private void menuItemRun_Click(object sender, EventArgs e)
        {
            this.RunSelectedApplication();
            //FormEngine.SetFullScreen(this);
        }

        private void CultureChanged()
        {
            if (this.CultureName == SIMPLIFIED_CHINESE_CULTURE)
            {
                this.menuItemEnglish.Checked = false;
                this.menuItemSimplifiedChinese.Checked = true;
                this.menuItemTraditionalChinese.Checked = false;
            }
            else if (this.CultureName == TRADITIONAL_CHINESE_CULTURE)
            {
                this.menuItemEnglish.Checked = false;
                this.menuItemSimplifiedChinese.Checked = false;
                this.menuItemTraditionalChinese.Checked = true;
            }
            else
            {
                this.menuItemEnglish.Checked = true;
                this.menuItemSimplifiedChinese.Checked = false;
                this.menuItemTraditionalChinese.Checked = false;
            }
        }

        private void menuItemEnglish_Click(object sender, EventArgs e)
        {
            this.ChangeCulture(ENGLISH_CULTURE);
            this.CultureChanged();
        }

        private void menuItemSimplifiedChinese_Click(object sender, EventArgs e)
        {
            this.ChangeCulture(SIMPLIFIED_CHINESE_CULTURE);
            this.CultureChanged();
        }

        private void menuItemTraditionalChinese_Click(object sender, EventArgs e)
        {
            this.ChangeCulture(TRADITIONAL_CHINESE_CULTURE);
            this.CultureChanged();
        }

        private void menuItemAdvanced_Click(object sender, EventArgs e)
        {
            this.OpenApplication("AdaSyncPpc.exe");
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}