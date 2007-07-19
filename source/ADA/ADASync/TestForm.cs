using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using OpenNETCF.Desktop.Communication;
using System.IO;
using System.Diagnostics;
using ADASync.ADAUserDataSetTableAdapters;

namespace ADASync
{
    public partial class TestForm : Form
    {
        internal class TextArgs : EventArgs
        {
            public TextArgs(Control target, string text)
            {
                Target = target;
                Text = text;
            }

            public Control Target;
            public string Text;
        }

        internal class EnableArgs : EventArgs
        {
            public EnableArgs(Control target, bool enabled)
            {
                Target = target;
                Enabled = enabled;
            }

            public Control Target;
            public bool Enabled;
        }

        // global RAPI object
        RAPI rapi;

        private EventHandler textUpdate;
        private EventHandler enableUpdate;
        private CERegistryKey adaSyncRegKey;

        public TestForm()
        {
            InitializeComponent();

            // create our global RAPI object
            this.rapi = new RAPI();

            // wire in some ActiveSync events
            this.rapi.ActiveSync.Active += new ActiveHandler(ActiveSync_Active);
            this.rapi.ActiveSync.Disconnect += new DisconnectHandler(ActiveSync_Disconnect);
            this.rapi.ActiveSync.Listen += new ListenHandler(ActiveSync_Listen);
            this.rapi.ActiveSync.Answer += new AnswerHandler(ActiveSync_Answer);

            textUpdate = new EventHandler(TextMarshaler);
            enableUpdate = new EventHandler(EnabledMarshaler);
        }

        private void TextMarshaler(object o, EventArgs args)
        {
            TextArgs arg = (TextArgs)args;
            arg.Target.Text = arg.Text;
        }

        private void EnabledMarshaler(object o, EventArgs args)
        {
            EnableArgs arg = (EnableArgs)args;
            arg.Target.Enabled = arg.Enabled;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            using (UserTableAdapter symbolAdapter = new UserTableAdapter())
            {
                using (SqlConnection connection = symbolAdapter.Connection)
                {
                    connection.Open();
                    this.textBoxServerName.Text = connection.WorkstationId;
                }
            }

            connectAsync_Click(sender, e);
        }

        private void connectAsync_Click(object sender, System.EventArgs e)
        {
            this.rapi.RAPIConnected += new RAPIConnectedHandler(rapi_RAPIConnected);
            this.rapi.RAPIDisconnected += new RAPIConnectedHandler(rapi_RAPIDisconnected);
            this.rapi.Connect(false, -1);
        }

        private void rapi_RAPIConnected()
        {
            this.Invoke(textUpdate, new object[] { this, new TextArgs(connectStatus, "Connected") });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(connectAsync, false) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(connectSync, false) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonReadServerSetting, true) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonWriteServerSetting, true) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonRunSync, true) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonRun, true) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonBackupDB, true) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonRestoreDB, true) });
        }

        private void rapi_RAPIDisconnected()
        {
            this.Invoke(textUpdate, new object[] { this, new TextArgs(connectStatus, "Not Connected") });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(connectAsync, false) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(connectSync, false) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonReadServerSetting, false) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonWriteServerSetting, false) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonRunSync, false) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonRun, false) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonBackupDB, false) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonRestoreDB, false) });
        }

        private void disconnect_Click(object sender, System.EventArgs e)
        {
            this.rapi.Disconnect();
            connectStatus.Text = "Not Connected";
            connectAsync.Enabled = true;
            connectSync.Enabled = true;
            this.buttonReadServerSetting.Enabled = false;
            this.buttonWriteServerSetting.Enabled = false;
            this.buttonRunSync.Enabled = false;
            this.buttonRun.Enabled = false;
            this.buttonRestoreDB.Enabled = false;
            this.buttonBackupDB.Enabled = false;
        }

        private void connectSync_Click(object sender, System.EventArgs e)
        {
            this.rapi.RAPIConnected += new RAPIConnectedHandler(rapi_RAPIConnected);
            this.rapi.RAPIDisconnected += new RAPIConnectedHandler(rapi_RAPIDisconnected);
            this.rapi.Connect(true);
        }

        private void copyTo_Click(object sender, System.EventArgs e)
        {
            // make sure we're connected
            if (!this.rapi.Connected)
            {
                MessageBox.Show("Not connected!");
                return;
            }

            // create a local file
            byte[] buffer = new byte[14];

            for (int c = 0; c < buffer.Length - 1; c++)
            {
                buffer[c] = BitConverter.GetBytes(("OpenNETCF.org").Substring(c, 1)[0])[0];
            }

            FileStream fs = File.Create("C:\\rapitest.txt");
            fs.Write(buffer, 0, buffer.Length);
            fs.Close();

            // copy it to the device
            this.rapi.CopyFileToDevice("C:\\rapitest.txt", "\\devicerapifile.txt", true);

        }

        private void copyFrom_Click(object sender, System.EventArgs e)
        {
            // make sure we're connected
            if (!this.rapi.Connected)
            {
                MessageBox.Show("Not connected!");
                return;
            }

            this.rapi.CopyFileFromDevice("C:\\filefromdevice.txt", "\\devicerapifile.txt", true);

            StreamReader sr = File.OpenText("C:\\filefromdevice.txt");
            MessageBox.Show(sr.ReadToEnd(), "File contents");
        }

        private void ActiveSync_Active()
        {
            this.Invoke(textUpdate, new object[] { this, new TextArgs(status, "Connected") });
        }

        private void ActiveSync_Disconnect()
        {
            this.Invoke(textUpdate, new object[] { this, new TextArgs(status, "Disconnected") });
        }

        private void ActiveSync_Listen()
        {
            this.Invoke(textUpdate, new object[] { this, new TextArgs(status, "Not Connected") });
        }

        private void ActiveSync_Answer()
        {
            this.Invoke(textUpdate, new object[] { this, new TextArgs(status, "Connecting...") });
        }

        private void btnProfileEnable_Click(object sender, System.EventArgs e)
        {
            this.rapi.CFPerformanceMonitor.EnableProfiling();
        }

        private void btnProfileDisable_Click(object sender, System.EventArgs e)
        {
            this.rapi.CFPerformanceMonitor.DisableProfiling();
        }

        private void btnProfileStats_Click(object sender, System.EventArgs e)
        {
            PerformanceStatistics stats = this.rapi.CFPerformanceMonitor.GetCurrentStatistics();
            MessageBox.Show("Total stat count: " + stats.Count.ToString());
        }

        private void btnRun_Click(object sender, System.EventArgs e)
        {
            this.rapi.CreateProcess(txtProfilePath.Text, "");
        }

        private void createShortcut_Click(object sender, System.EventArgs e)
        {
            // create a shortcut to the control panel on the desktop
            this.rapi.CreateDeviceShortcut("\\Windows\\Desktop\\MyShortcut.lnk", "\\windows\\control.exe");
        }

        private void readShortcut_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(this.rapi.GetDeviceShortcutTarget("\\Windows\\Desktop\\MyShortcut.lnk"));
        }

        private void buttonReadServerSetting_Click(object sender, EventArgs e)
        {
            this.OpenRegistryKey();
            this.textBoxServerSetting.Text = this.adaSyncRegKey.GetValue("Server", "Not set").ToString();
        }

        private void OpenRegistryKey()
        {
            this.adaSyncRegKey = CERegistry.LocalMachine.CreateSubKey("Software").CreateSubKey("Inflaton").CreateSubKey("ADA").CreateSubKey("AdaSyncPpc");
        }

        private void buttonWriteServerSetting_Click(object sender, EventArgs e)
        {
            this.OpenRegistryKey();
            this.adaSyncRegKey.SetValue("Server", this.textBoxServerName.Text);

            this.buttonReadServerSetting_Click(sender, e);

            try
            {
                this.rapi.DeleteDeviceFile(@"\Storage Card\AdaPpc\ADAMobile.sdf");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            try
            {
                this.rapi.CreateProcess(this.textBoxSyncApp.Text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        private void buttonRunSync_Click(object sender, EventArgs e)
        {
            try
            {
                this.rapi.CreateProcess(this.textBoxSyncApp.Text, "--EVENT");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void buttonBackupDB_Click(object sender, EventArgs e)
        {
            try
            {
                this.rapi.CopyFileOnDevice(@"\Storage Card\AdaPpc\ADAMobile.sdf", @"\Storage Card\AdaPpc\ADAMobile-" + this.textBoxServerSetting.Text + ".sdf", true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        private void buttonRestoreDB_Click(object sender, EventArgs e)
        {
            try
            {
                this.rapi.CopyFileOnDevice(@"\Storage Card\AdaPpc\ADAMobile-" + this.textBoxServerName.Text + ".sdf", @"\Storage Card\AdaPpc\ADAMobile.sdf");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.adaSyncRegKey != null)
            {
                try
                {
                    this.adaSyncRegKey.Close();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
    }
}