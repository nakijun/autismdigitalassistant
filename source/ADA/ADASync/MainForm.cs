using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.Desktop.Communication;
using ADASync.ADAUserDataSetTableAdapters;
using System.Diagnostics;
using System.Data.SqlClient;

namespace ADASync
{
    public partial class MainForm : Form
    {
        const int OWNER_INFO_LENGTH = 640;
        const int OWNER_NOTES_LENGTH = 388;

        private CERegistryKey _adaSyncRegKey;

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

        private RAPI rapi;
        private EventHandler textUpdate;
        private EventHandler enableUpdate;
        private ADAUserDataSet.DeviceRow currentDeviceRow;

        public MainForm()
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

            using (UserTableAdapter symbolAdapter = new UserTableAdapter())
            {
                using (SqlConnection connection = symbolAdapter.Connection)
                {
                    if (connection.DataSource == "(local)")
                    {
                        this.textBoxServerName.Text = connection.WorkstationId;
                    }
                    else
                    {
                        this.textBoxServerName.Text = connection.DataSource;
                    }
                }
            }
        }

        private void TextMarshaler(object o, EventArgs args)
        {
            TextArgs arg = (TextArgs)args;
            arg.Target.Text = arg.Text;

            if (this.rapi.Connected)
            {
                this.ReadDeviceInfo();
            }
        }

        private void EnabledMarshaler(object o, EventArgs args)
        {
            EnableArgs arg = (EnableArgs)args;
            arg.Target.Enabled = arg.Enabled;
        }

        private object ReadRegistry(CERegistryKey rootKey, string name, params string[] subKeys)
        {
            CERegistryKey key = rootKey;

            foreach (string subKey in subKeys)
            {
                key = key.CreateSubKey(subKey);
            }

            return key.GetValue(name);
        }

        private void ReadDeviceInfo()
        {
            string userName = (string)ReadRegistry(CERegistry.CurrentUser, "Name", "ControlPanel", "Owner");

            if (userName == null || userName.Length == 0)
            {
                MessageBox.Show(this, "Please update the owner information of the Pockete PC!", "Error");
                return;
            }

            string deviceId = (string)ReadRegistry(CERegistry.LocalMachine, "DeviceID", "Software", "Inflaton", "ADA");
            string deviceName = (string)ReadRegistry(CERegistry.LocalMachine, "Name", "Ident");

            try
            {
                DataRow[] userRows = this.adaUserDataSet1.User.Select("Name='" + userName + "'");

                ADAUserDataSet.UserRow userRow = null;
                if (userRows.Length > 0)
                {
                    userRow = userRows[0] as ADAUserDataSet.UserRow;
                }
                else
                {
                    userRow = this.adaUserDataSet1.User.NewUserRow();
                    userRow.Name = userName;

                    this.adaUserDataSet1.User.AddUserRow(userRow);
                }

                byte[] ownerInfo = (byte[])ReadRegistry(CERegistry.CurrentUser, "Owner", "ControlPanel", "Owner");
                userRow.Company = System.Text.Encoding.Unicode.GetString(ownerInfo, 72, 72).TrimEnd('\0');
                userRow.Address = System.Text.Encoding.Unicode.GetString(ownerInfo, 144, 372).TrimEnd('\0');
                userRow.Telephone = System.Text.Encoding.Unicode.GetString(ownerInfo, 516, 50).TrimEnd('\0');
                userRow.Email = System.Text.Encoding.Unicode.GetString(ownerInfo, 566, 74).TrimEnd('\0');

                byte[] ownerNotes = (byte[])ReadRegistry(CERegistry.CurrentUser, "Owner Notes", "ControlPanel", "Owner");
                if (ownerNotes != null)
                {
                    userRow.Notes = System.Text.Encoding.Unicode.GetString(ownerNotes).TrimEnd('\0');
                }

                userRow.IsActive = true;
                UserTableAdapter uta = new UserTableAdapter();
                uta.Update(this.adaUserDataSet1.User);

                currentDeviceRow = this.adaUserDataSet1.Device.FindByDeviceId(deviceId);

                if (currentDeviceRow == null)
                {
                    currentDeviceRow = this.adaUserDataSet1.Device.NewDeviceRow();
                    currentDeviceRow.DeviceId = deviceId;
                    this.adaUserDataSet1.Device.AddDeviceRow(currentDeviceRow);
                }

                currentDeviceRow.Name = deviceName;
                currentDeviceRow.UserId = userRow.UserId;
                DeviceTableAdapter dta = new DeviceTableAdapter();
                dta.Update(this.adaUserDataSet1.Device);

                UpdateCurrentUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void UpdateCurrentUser()
        {
            this.labelCurrentUser.Text = currentDeviceRow.UserRow.Name;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.userTableAdapter1.Fill(this.adaUserDataSet1.User);
                this.deviceTableAdapter1.Fill(this.adaUserDataSet1.Device);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            this.rapi.RAPIConnected += new RAPIConnectedHandler(rapi_RAPIConnected);
            this.rapi.RAPIDisconnected += new RAPIConnectedHandler(rapi_RAPIDisconnected);
            this.rapi.Connect(false, -1);
        }

        private void ActiveSync_Active()
        {
            this.rapi.Connect(false, -1);
        }

        private void ActiveSync_Disconnect()
        {
        }

        private void ActiveSync_Listen()
        {
        }

        private void ActiveSync_Answer()
        {
        }

        private void rapi_RAPIConnected()
        {
            this.Invoke(textUpdate, new object[] { this, new TextArgs(connectStatus, "Connected") });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(buttonRefreshCurrentUser, true) });
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
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(buttonRefreshCurrentUser, false) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonReadServerSetting, false) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonWriteServerSetting, false) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonRunSync, false) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonRun, false) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonBackupDB, false) });
            this.Invoke(enableUpdate, new object[] { this, new EnableArgs(this.buttonRestoreDB, false) });
        }

        private void buttonRefreshCurrentUser_Click(object sender, EventArgs e)
        {
            this.ReadDeviceInfo();
            //SelectUserForm f = new SelectUserForm();
            //f.UserDataSet.Merge(adaUserDataSet1);
            //if (DialogResult.OK == f.ShowDialog(this))
            //{
            //    try
            //    {
            //        adaUserDataSet1.Merge(f.UserDataSet);

            //        DeviceTableAdapter dta = new DeviceTableAdapter();
            //        dta.Update(this.adaUserDataSet1.Device);

            //        UpdateDeviceInfo();
            //        UpdateCurrentUser();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //}
        }

        private void WriteRegistry(CERegistryKey rootKey, string name, object value, params string[] subKeys)
        {
            CERegistryKey key = rootKey;

            foreach (string subKey in subKeys)
            {
                key = key.CreateSubKey(subKey);
            }

            key.SetValue(name, value);
        }

        private void EncodeStringToBytesArray(string s, ref byte[] a, int position)
        {
            System.Text.Encoding.Unicode.GetBytes(s, 0, s.Length, a, position);
        }

        private void UpdateDeviceInfo()
        {
            WriteRegistry(CERegistry.CurrentUser, "Name", currentDeviceRow.UserRow.Name, "ControlPanel", "Owner");

            byte[] ownerInfo = new byte[OWNER_INFO_LENGTH];
            EncodeStringToBytesArray(currentDeviceRow.UserRow.Name, ref ownerInfo, 0);
            EncodeStringToBytesArray(currentDeviceRow.UserRow.Company, ref ownerInfo, 72);
            EncodeStringToBytesArray(currentDeviceRow.UserRow.Address, ref ownerInfo, 144);
            EncodeStringToBytesArray(currentDeviceRow.UserRow.Telephone, ref ownerInfo, 516);
            EncodeStringToBytesArray(currentDeviceRow.UserRow.Email, ref ownerInfo, 566);

            WriteRegistry(CERegistry.CurrentUser, "Owner", ownerInfo, "ControlPanel", "Owner");

            byte[] ownerNotes = System.Text.Encoding.Unicode.GetBytes(currentDeviceRow.UserRow.Notes);
            WriteRegistry(CERegistry.CurrentUser, "Owner Notes", ownerNotes, "ControlPanel", "Owner");
        }

        private void buttonManageUser_Click(object sender, EventArgs e)
        {

        }

        private void buttonReadServerSetting_Click(object sender, EventArgs e)
        {
            this.OpenRegistryKey();
            this.textBoxServerSetting.Text = this._adaSyncRegKey.GetValue("Server", "Not set").ToString();
        }

        private void OpenRegistryKey()
        {
            this._adaSyncRegKey = CERegistry.LocalMachine.CreateSubKey("Software").CreateSubKey("Inflaton").CreateSubKey("ADA").CreateSubKey("AdaSyncPpc");
        }

        private void buttonWriteServerSetting_Click(object sender, EventArgs e)
        {
            this.OpenRegistryKey();
            this._adaSyncRegKey.SetValue("Server", this.textBoxServerName.Text);

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
            if (this._adaSyncRegKey != null)
            {
                try
                {
                    this._adaSyncRegKey.Close();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
    }
}