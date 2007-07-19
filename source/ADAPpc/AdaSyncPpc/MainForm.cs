using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Data.SqlServerCe;
using UtilitiesPpc;
using System.IO;
using System.Reflection;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.WindowsMobile.Status;
using AdaSyncPpc.ADAMobileDataSetTableAdapters;
using Microsoft.Win32;

namespace AdaSyncPpc
{
    public partial class MainForm : AdaBaseForm
    {
        private const string REGISTRY_SYMBOL_LIBRARY_SYNC_TIME = "SymbolSync";
        private const string REGISTRY_SCHEDULE_SYNC_TIME = "ScheduleSync";
        private const string REGISTRY_COMMUNICATOR_SYNC_TIME = "CommunicatorSync";
        private const string REGISTRY_DEVICE_ID = "DeviceID";

        private string deviceID;
        private string databaseFileName;

        private SystemState cradlePresent;

        private SystemState cradlePresent2;

        public MainForm(bool autoSync)
        {
            InitializeComponent();

            string fullyQualifiedName = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
            string strAppDir = Path.GetDirectoryName(fullyQualifiedName);
            databaseFileName = strAppDir + "\\ADAMobile.sdf";

            FileInfo fileInfo = new FileInfo(databaseFileName);
            if (fileInfo.Exists)
            {

                DateTime d = fileInfo.LastWriteTime;
                this.labelModifiedTime.Text = d.ToShortDateString() + " " + d.ToShortTimeString();
            }

            cradlePresent = new SystemState(SystemProperty.CradlePresent);

            cradlePresent.ComparisonType = StatusComparisonType.Equal;
            cradlePresent.ComparisonValue = 1;

            if (IsDeployed)
            {
                cradlePresent.EnableApplicationLauncher("ADASync", fullyQualifiedName, "-EVENT");
            }
            else
            {
                cradlePresent.DisableApplicationLauncher();
            }

            cradlePresent2 = new SystemState(SystemProperty.CradlePresent);
            cradlePresent2.Changed += new ChangeEventHandler(cradlePresent_Changed);

            if (autoSync)
            {
                this.timerAutoSync.Enabled = true;
            }

            object syncTimeValue = this.Setting.LocalSetting.GetValue(REGISTRY_SYMBOL_LIBRARY_SYNC_TIME, null) as string;
            if (syncTimeValue != null)
            {
                DateTime d = Convert.ToDateTime(syncTimeValue);
            }

            syncTimeValue = this.Setting.LocalSetting.GetValue(REGISTRY_SCHEDULE_SYNC_TIME, null) as string;
            if (syncTimeValue != null)
            {
                DateTime d = Convert.ToDateTime(syncTimeValue);
            }

            syncTimeValue = this.Setting.LocalSetting.GetValue(REGISTRY_COMMUNICATOR_SYNC_TIME, null) as string;
            if (syncTimeValue != null)
            {
                DateTime d = Convert.ToDateTime(syncTimeValue);
                this.labelSyncTime.Text = d.ToShortDateString() + " " + d.ToShortTimeString();
            }

            this.deviceID = this.Setting.GlobalSetting.GetValue(REGISTRY_DEVICE_ID, null) as string;

            if (this.deviceID == null)
            {
                try
                {
                    this.deviceID = DeviceID.GetDeviceID();
                    this.Setting.GlobalSetting.SetValue(REGISTRY_DEVICE_ID, this.deviceID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists(databaseFileName))
            {
                menuItemSymbolExplorer.Enabled = false;
            }

            cradlePresent_Changed(sender, null);
        }

        void cradlePresent_Changed(object sender, ChangeEventArgs args)
        {
            this.menuItemSync.Enabled = SystemState.CradlePresent;
            this.menuItemReinitialize.Enabled = SystemState.CradlePresent;

            this.textBoxStatus.Text = (SystemState.CradlePresent ? "Connected" : "Disconnected");

            if (args != null && SystemState.CradlePresent)
            {
                this.Synchronize();
            }

            if (args != null && !SystemState.CradlePresent)
            {
                this.Close();
            }
        }

        private bool Synchronize()
        {
            return Synchronize(false);
        }

        private bool SynchronizeSubscription(string subsriptionName, bool reinitializeSubscription, string registryKey, Label label)
        {
            bool result = false;

            Cursor.Current = Cursors.WaitCursor;

            SqlCeReplication repl = null;
            try
            {
                PerformanceSampling.StartSample(0, "Synchronization time");
                repl = new SqlCeReplication();

                repl.Publisher = (string)Setting.LocalSetting.GetValue("Server", "hdhibm");
                repl.InternetUrl = string.Format("http://{0}/ADA/sqlcesa30.dll", repl.Publisher);

                this.textBoxStatus.Text += "Synchronizing " + subsriptionName + " data with " + repl.Publisher + " ...\r\n";
                this.textBoxStatus.Update();

                repl.PublisherDatabase = @"ADA";
                repl.PublisherSecurityMode = SecurityType.DBAuthentication;
                repl.PublisherLogin = @"ada";
                repl.PublisherPassword = @"p@ssw0rd";

                repl.Publication = subsriptionName;
                repl.Subscriber = subsriptionName;
                repl.SubscriberConnectionString = ("Data Source ="
                        + (databaseFileName)
                        + (";Password =" + "\"\";"));
                repl.HostName = deviceID;

                if (!File.Exists(databaseFileName))
                {
                    repl.AddSubscription(AddOption.CreateDatabase);
                }

                if (reinitializeSubscription)
                {
                    repl.ReinitializeSubscription(false);
                }

                repl.Synchronize();

                PerformanceSampling.StopSample(0);

                DateTime now = System.DateTime.Now;
                label.Text = now.ToShortDateString() + " " + now.ToShortTimeString();
                label.Update();
                this.Setting.LocalSetting.SetValue(registryKey, now);

                this.textBoxStatus.Text += "Successful!\r\n" + PerformanceSampling.GetSampleDurationText(0) + "\r\n";
                result = true;
            }
            catch (SqlCeException sqlex)
            {
                this.textBoxStatus.Text += "Failed! Error messages:\r\n";
                foreach (SqlCeError sqlError in sqlex.Errors)
                {
                    this.textBoxStatus.Text += sqlError.Message + "\r\n";
                }
            }
            catch (Exception ex)
            {
                this.textBoxStatus.Text += "Failed! Error messages:\r\n" + ex.Message + "\r\n";
            }
            finally
            {
                if (repl != null)
                {
                    repl.Dispose();
                }
            }

            Cursor.Current = Cursors.Default;

            return result;
        }

        private void menuItemSync_Click(object sender, EventArgs e)
        {
            this.Synchronize();
        }

        private void timerAutoSync_Tick(object sender, EventArgs e)
        {
            if (SystemState.CradlePresent)
            {
                if (this.Synchronize())
                {
                    this.Update();
                    //Thread.Sleep(2000);
                    //this.Close();
                }
            }

            this.timerAutoSync.Enabled = false;
        }

        private void menuItemSymbolExplorer_Click(object sender, EventArgs e)
        {
            try
            {
                SymbolExplorer symbolExploerer = new SymbolExplorer(this.CultureName);
                SymbolInfo selectedSymbol = symbolExploerer.BrowseSymbol();

                if (selectedSymbol != null)
                {
                    SymbolDetailForm symbolDetailForm = new SymbolDetailForm();
                    symbolDetailForm.Symbol = selectedSymbol;
                    symbolDetailForm.ShowDialog();
                }
            }
            catch (SqlCeException sqlex)
            {
                this.textBoxStatus.Text = "Failed! Error messages:\r\n";
                foreach (SqlCeError sqlError in sqlex.Errors)
                {
                    this.textBoxStatus.Text += sqlError.Message;
                }
            }
            catch (Exception ex)
            {
                this.textBoxStatus.Text = "Failed! Error messages:\n" + ex.Message;
            }
        }

        private void menuItemReinitialize_Click(object sender, EventArgs e)
        {
            Synchronize(true);
        }

        private bool Synchronize(bool reinitializeSubscription)
        {
            this.textBoxStatus.Text = "";

            if (SynchronizeSubscription("Symbol", reinitializeSubscription, REGISTRY_SYMBOL_LIBRARY_SYNC_TIME, this.labelSyncTime))
            {
                if (SynchronizeSubscription("Schedule", reinitializeSubscription, REGISTRY_SCHEDULE_SYNC_TIME, this.labelSyncTime))
                {
                    return SynchronizeSubscription("Communicator", reinitializeSubscription, REGISTRY_COMMUNICATOR_SYNC_TIME, this.labelSyncTime);
                }
            }

            return false;
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}