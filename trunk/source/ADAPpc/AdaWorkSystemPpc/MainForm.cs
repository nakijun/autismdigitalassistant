using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UtilitiesPpc;
using AdaWorkSystemPpc.ADAMobileDataSetTableAdapters;
using Microsoft.Win32;

namespace AdaWorkSystemPpc
{
    public partial class MainForm : AdaBaseForm
    {
        private ADAMobileDataSet.ScheduleRow _scheduleRow;
        private string _userName;

        public MainForm()
        {
            InitializeComponent();

            this.symbolListView1.ItemActivated += new EventHandler(symbolListView1_ItemActivated);

            _userName = (string)ReadRegistry(Registry.CurrentUser, "Name", "ControlPanel", "Owner");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadDataSet();
        }

        private object ReadRegistry(RegistryKey rootKey, string name, params string[] subKeys)
        {
            RegistryKey key = rootKey;

            foreach (string subKey in subKeys)
            {
                key = key.CreateSubKey(subKey);
            }

            return key.GetValue(name);
        }

        private void LoadDataSet()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                adaScheduleDataSet1.EnforceConstraints = false;

                UserTableAdapter userAdapter = new UserTableAdapter();
                userAdapter.Fill(adaScheduleDataSet1.User);

                ScheduleTableAdapter scheduleAdapter = new ScheduleTableAdapter();
                scheduleAdapter.Fill(adaScheduleDataSet1.Schedule);

                SymbolTableAdapter symbolAdapter = new SymbolTableAdapter();
                symbolAdapter.Fill(adaScheduleDataSet1.Symbol);

                ActivityTableAdapter activityAdapter = new ActivityTableAdapter();
                activityAdapter.Fill(adaScheduleDataSet1.Activity);

                adaScheduleDataSet1.EnforceConstraints = true;
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }

            RefreshViews();

            Cursor.Current = Cursors.Default;
        }

        private void RefreshViews()
        {
            try
            {
                DataRow[] dataRows = adaScheduleDataSet1.Schedule.Select("IsActive = 1 AND Type = " + (int)ScheduleType.WorkSystemInstance);
                int count = dataRows.Length;

                if (count > 0)
                {
                    ShowWorkSystem(dataRows[0] as ADAMobileDataSet.ScheduleRow);
                }
                else
                {
                    ShowModels();
                }

            }
            catch (Exception ex)
            {
                ReportError(ex);
            }
        }

        private void symbolListView1_ItemActivated(object sender, EventArgs args)
        {
            if (_scheduleRow != null)
            {
                ShowDetail(symbolListView1.SelectedIndex);
            }
            else
            {
                DataRow[] dataRows = adaScheduleDataSet1.Schedule.Select("Type = " + (int)ScheduleType.WorkSystemModel, "Name ASC");
                ADAMobileDataSet.ScheduleRow row = dataRows[symbolListView1.SelectedIndex] as ADAMobileDataSet.ScheduleRow;
                ShowWorkSystem(row);
            }
        }

        private void ShowModels()
        {
            _scheduleRow = null;
            symbolListView1.Items.Clear();

            DataRow[] dataRows = adaScheduleDataSet1.Schedule.Select("Type = " + (int)ScheduleType.WorkSystemModel, "Name ASC");
            int count = dataRows.Length;

            for (int i = 0; i < count; i++)
            {
                ADAMobileDataSet.ScheduleRow row = dataRows[i] as ADAMobileDataSet.ScheduleRow;
                SymbolListView.SymbolListItem item = new SymbolListView.SymbolListItem();

                if (!row.IsSymbolIdNull())
                {
                    if (!row.SymbolRow.IsImageNull())
                    {
                        item.Image = ImageEngine.FromArray(row.SymbolRow.Image);
                    }

                    if (!row.SymbolRow.IsSoundNull())
                    {
                        item.Sound = row.SymbolRow.Sound;
                    }
                }

                item.Text = row.Name;

                symbolListView1.Items.Add(item);
            }

            if (count > 0)
            {
                symbolListView1.SelectedIndex = 0;
            }

            menuItemCurrent.Enabled = (count > 0);
            menuItemCurrent.Text = "View";
            menuItemExit.Text = "Exit";

            this.Text = "Select Work System";
            symbolListView1.Invalidate();
        }

        private void ShowWorkSystem(ADAMobileDataSet.ScheduleRow scheduleRow)
        {
            int scheduleId = scheduleRow.ScheduleId;

            _scheduleRow = scheduleRow;

            symbolListView1.Items.Clear();

            DataRow[] dataRows = adaScheduleDataSet1.Activity.Select("ScheduleId=" + scheduleId, "Sequence ASC");
            int count = dataRows.Length;
            int currentActivity = -1;

            for (int i = 0; i < count; i++)
            {
                ADAMobileDataSet.ActivityRow activityRow = dataRows[i] as ADAMobileDataSet.ActivityRow;
                SymbolListView.SymbolListItem item = new SymbolListView.SymbolListItem();

                bool isExecuted = (!activityRow.IsExecutionStartNull() && !activityRow.IsExecutionEndNull());

                item.Checked = isExecuted;

                if (currentActivity < 0)
                {
                    if (!isExecuted)
                    {
                        currentActivity = i;
                    }
                }

                if (!activityRow.IsSymbolIdNull())
                {
                    if (!activityRow.SymbolRow.IsImageNull())
                    {
                        item.Image = ImageEngine.FromArray(activityRow.SymbolRow.Image);
                    }

                    if (!activityRow.SymbolRow.IsSoundNull())
                    {
                        item.Sound = activityRow.SymbolRow.Sound;
                    }
                }

                item.Text = string.Format("{0}:{1}", activityRow.Sequence, activityRow.Name);

                symbolListView1.Items.Add(item);
            }

            symbolListView1.SelectedIndex = currentActivity;
            symbolListView1.Invalidate();
            menuItemCurrent.Enabled = (currentActivity >= 0);

            menuItemCurrent.Text = "Current";
            menuItemExit.Text = (scheduleRow.Type == (int)ScheduleType.WorkSystemModel ? "Back" : "Exit");

            this.Text = scheduleRow.Name;
        }

        private int GetCurrentActivity()
        {
            DataRow[] dataRows = adaScheduleDataSet1.Activity.Select("ScheduleId=" + _scheduleRow.ScheduleId, "Sequence ASC");

            int currentActivity = -1;
            int count = dataRows.Length;

            for (int i = 0; i < count; i++)
            {
                ADAMobileDataSet.ActivityRow row = dataRows[i] as ADAMobileDataSet.ActivityRow;

                bool isExecuted = (!row.IsExecutionStartNull() && !row.IsExecutionEndNull());

                if (currentActivity < 0)
                {
                    if (!isExecuted)
                    {
                        currentActivity = i;
                        break;
                    }
                }
            }

            return currentActivity;
        }

        private void ShowDetail(int index)
        {
            ActivityDetailForm detailForm = new ActivityDetailForm();
            DataRow[] dataRows = adaScheduleDataSet1.Activity.Select("ScheduleId=" + _scheduleRow.ScheduleId, "Sequence ASC");
            detailForm.ActivityRow = dataRows[index] as ADAMobileDataSet.ActivityRow;
            int currentActivity = GetCurrentActivity();
            detailForm.IsCurrentActivity = (index == currentActivity);

            if (DialogResult.OK == detailForm.ShowDialog())
            {
                if (detailForm.ActivityRow.RowState == DataRowState.Modified)
                {
                    if (UpdateCurrentActivity())
                    {
                        int nextActivity = GetCurrentActivity();

                        if (currentActivity != nextActivity)
                        {
                            RefreshViews();
                        }

                        if (nextActivity >= 0)
                        {
                            ShowDetail(nextActivity);
                        }
                        else
                        {
                            UpdateWorkSystemInstance();
                        }
                    }
                }
                else if (detailForm.ActivityRow.RowState == DataRowState.Added)
                {
                    if (AddWorkSystemInstance(detailForm.ActivityRow))
                    {
                        RefreshViews();

                        int nextActivity = GetCurrentActivity();

                        if (nextActivity >= 0)
                        {
                            ShowDetail(nextActivity);
                        }
                    }
                }
            }
        }

        private bool AddWorkSystemInstance(ADAMobileDataSet.ActivityRow activityRow)
        {
            bool result = true;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                //ADAMobileDataSet.UserDataTable userTable = adaScheduleDataSet1.User;

                //if (userTable.Count > 0 && _userName != null
                //    && _userName.Length > 0 && userTable.Rows[0]["Name"] != _userName)
                //{
                //    userTable.Rows[0]["Name"] = _userName;
                //    UserTableAdapter userAdapter = new UserTableAdapter();
                //    int number = userAdapter.Update(adaScheduleDataSet1.User);
                //}
                adaScheduleDataSet1.EnforceConstraints = false;

                ScheduleTableAdapter scheduleAdapter = new ScheduleTableAdapter();
                scheduleAdapter.Init();
                int ret = scheduleAdapter.Update(adaScheduleDataSet1.Schedule);

                ActivityTableAdapter activityAdapter = new ActivityTableAdapter();
                activityAdapter.Init();
                ret = activityAdapter.Update(adaScheduleDataSet1.Activity);

                adaScheduleDataSet1.EnforceConstraints = true;
            }
            catch (Exception ex)
            {
                ReportError(ex);
                result = false;
            }

            Cursor.Current = Cursors.Default;
            return result;
        }

        private void UpdateWorkSystemInstance()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (_scheduleRow != null)
            {
                try
                {
                    _scheduleRow.IsActive = false;
                    ScheduleTableAdapter scheduleAdapter = new ScheduleTableAdapter();
                    int ret = scheduleAdapter.Update(adaScheduleDataSet1.Schedule);
                    RefreshViews();
                }
                catch (Exception ex)
                {
                    ReportError(ex);
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private bool UpdateCurrentActivity()
        {
            bool result = true;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                ActivityTableAdapter activityAdapter = new ActivityTableAdapter();
                int ret = activityAdapter.Update(adaScheduleDataSet1.Activity);
            }
            catch (Exception ex)
            {
                ReportError(ex);
                result = false;
            }

            Cursor.Current = Cursors.Default;
            return result;
        }

        private void ReportError(Exception ex)
        {
            string statusMessage = ex.Message.ToString() + "\r\n" + ex.StackTrace;

            MessageBox.Show(statusMessage, this.Text);
        }

        private void menuItemCurrent_Click(object sender, EventArgs e)
        {
            if (_scheduleRow == null)
            {
                DataRow[] dataRows = adaScheduleDataSet1.Schedule.Select("Type = " + (int)ScheduleType.WorkSystemModel, "Name ASC");
                ADAMobileDataSet.ScheduleRow row = dataRows[symbolListView1.SelectedIndex] as ADAMobileDataSet.ScheduleRow;
                ShowWorkSystem(row);
            }
            else if (symbolListView1.SelectedIndex >= 0)
            {
                symbolListView1.SelectedIndex = GetCurrentActivity();
                ShowDetail(symbolListView1.SelectedIndex);
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            if (_scheduleRow == null || _scheduleRow.Type == (int)ScheduleType.WorkSystemInstance)
            {
                Close();
            }
            else
            {
                ShowModels();
            }
        }
    }
}