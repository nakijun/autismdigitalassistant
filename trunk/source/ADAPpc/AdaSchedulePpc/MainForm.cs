using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UtilitiesPpc;
using AdaSchedulePpc.ADAMobileDataSetTableAdapters;
using Microsoft.Win32;

namespace AdaSchedulePpc
{
    public partial class MainForm : AdaBaseForm
    {
        private const string REGISTRY_SYMBOL_VIEW = "SymbolView";

        private bool _isShowingSymbolView;
        private DataRow[] _activityRows;

        public MainForm()
        {
            InitializeComponent();

            this.symbolListView1.ItemActivated += new EventHandler(symbolListView1_ItemActivated);
            this.symbolListView1.SelectedIndexChanged += new EventHandler(symbolListView1_SelectedIndexChanged);
        }

        void symbolListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (symbolListView1.SelectedIndex < 0)
            {
                foreach (int index in this.listView1.SelectedIndices)
                {
                    this.listView1.Items[index].Selected = false;
                }
            }
            else
            {
                this.listView1.Items[symbolListView1.SelectedIndex].Selected = true;
                this.listView1.Items[symbolListView1.SelectedIndex].Focused = true;
                this.listView1.EnsureVisible(symbolListView1.SelectedIndex);
            }
        }

        private void symbolListView1_ItemActivated(object sender, EventArgs args)
        {
            ShowDetail(symbolListView1.SelectedIndex);
        }

        private void ShowDetail(int index)
        {
            ActivityDetailForm detailForm = new ActivityDetailForm();
            detailForm.ActivityRow = _activityRows[index] as ADAMobileDataSet.ActivityRow;
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
                            UpdateSchedule();
                        }
                    }
                }
            }
        }

        private void UpdateSchedule()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (adaScheduleDataSet1.Schedule.Rows.Count > 0)
            {
                try
                {
                    ADAMobileDataSet.ScheduleRow scheduleRow = adaScheduleDataSet1.Schedule.Rows[0] as ADAMobileDataSet.ScheduleRow;
                    scheduleRow.IsActive = false;
                    ScheduleTableAdapter scheduleAdapter = new ScheduleTableAdapter();
                    int ret = scheduleAdapter.Update(adaScheduleDataSet1.Schedule);
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

        private T GetLocalSetting<T>(string name, T defaultValue)
        {
            object o = Setting.LocalSetting.GetValue(REGISTRY_SYMBOL_VIEW, null);
            if (o == null)
            {
                return defaultValue;
            }

            return (T)o;
        }

        private bool GetLocalSetting(string name, bool defaultValue)
        {
            object o = Setting.LocalSetting.GetValue(REGISTRY_SYMBOL_VIEW, null);
            if (o == null)
            {
                return defaultValue;
            }

            return bool.TrueString.CompareTo(o) == 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _isShowingSymbolView = GetLocalSetting(REGISTRY_SYMBOL_VIEW, true);
            ShowSymbolView(_isShowingSymbolView);

            LoadDataSet();
        }

        private void ShowSymbolView(bool isShowingSymbolView)
        {
            symbolListView1.Visible = isShowingSymbolView;
            listView1.Visible = !isShowingSymbolView;

            menuItemSymbolView.Checked = isShowingSymbolView;
            menuItemDetailView.Checked = !isShowingSymbolView; ;

            if (isShowingSymbolView)
            {
                symbolListView1.Focus();
            }
            else
            {
                listView1.Focus();
            }

            this._isShowingSymbolView = isShowingSymbolView;
            Setting.LocalSetting.SetValue(REGISTRY_SYMBOL_VIEW, isShowingSymbolView);
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

            DateTime date = DateTime.Now.Date;
            string userName = (string)ReadRegistry(Registry.CurrentUser, "Name", "ControlPanel", "Owner");

            try
            {
                adaScheduleDataSet1.EnforceConstraints = false;

                UserTableAdapter userAdapter = new UserTableAdapter();
                userAdapter.Fill(adaScheduleDataSet1.User, userName);

                ScheduleTableAdapter scheduleAdapter = new ScheduleTableAdapter();
                scheduleAdapter.Fill(adaScheduleDataSet1.Schedule, date, userName);

                SymbolTableAdapter symbolAdapter = new SymbolTableAdapter();
                symbolAdapter.Fill(adaScheduleDataSet1.Symbol, date, userName);

                ActivityTableAdapter activityAdapter = new ActivityTableAdapter();
                activityAdapter.Fill(adaScheduleDataSet1.Activity, date, userName);

                adaScheduleDataSet1.EnforceConstraints = true;
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }

            _activityRows = adaScheduleDataSet1.Activity.Select("", "Sequence ASC");

            RefreshViews();

            Cursor.Current = Cursors.Default;
        }

        private void RefreshViews()
        {
            try
            {
                int currentActivity = -1;
                int count = _activityRows.Length;

                symbolListView1.Items.Clear();
                listView1.Items.Clear();

                for (int i = 0; i < count; i++)
                {
                    AdaSchedulePpc.ADAMobileDataSet.ActivityRow row = _activityRows[i] as AdaSchedulePpc.ADAMobileDataSet.ActivityRow;
                    SymbolListView.SymbolListItem item = new SymbolListView.SymbolListItem();

                    bool isExecuted = (!row.IsExecutionStartNull() && !row.IsExecutionEndNull());

                    item.Checked = isExecuted;

                    if (currentActivity < 0)
                    {
                        if (!isExecuted)
                        {
                            currentActivity = i;
                        }
                    }

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

                    item.Text = string.Format("{0}:{1}", row.Sequence, row.Name);

                    symbolListView1.Items.Add(item);

                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = row.Name;

                    if (!row.IsStartTimeNull())
                    {
                        lvItem.SubItems.Add(row.StartTime.ToString("hh:mm"));
                    }

                    if (!row.IsEndTimeNull())
                    {
                        lvItem.SubItems.Add(row.EndTime.ToString("hh:mm"));
                    }

                    lvItem.Tag = isExecuted;
                    lvItem.Checked = isExecuted;
                    listView1.Items.Add(lvItem);
                }

                symbolListView1.SelectedIndex = currentActivity;
                menuItemCurrent.Enabled = (currentActivity >= 0);
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }
        }

        private int GetCurrentActivity()
        {
            _activityRows = adaScheduleDataSet1.Activity.Select("", "Sequence ASC");

            int currentActivity = -1;
            int count = _activityRows.Length;

            for (int i = 0; i < count; i++)
            {
                AdaSchedulePpc.ADAMobileDataSet.ActivityRow row = _activityRows[i] as AdaSchedulePpc.ADAMobileDataSet.ActivityRow;

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

        private void ReportError(Exception ex)
        {
            string statusMessage = ex.Message.ToString() + "\r\n" + ex.StackTrace;

            MessageBox.Show(statusMessage, this.Text);
        }

        private void menuItemCurrent_Click(object sender, EventArgs e)
        {
            symbolListView1.SelectedIndex = GetCurrentActivity();

            if (listView1.SelectedIndices.Count > 0)
            {
                ShowDetail(listView1.SelectedIndices[0]);
            }
        }

        private void menuItemSymbolView_Click(object sender, EventArgs e)
        {
            ShowSymbolView(true);
        }

        private void menuItemDetailView_Click(object sender, EventArgs e)
        {
            ShowSymbolView(false);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                symbolListView1.SelectedIndex = listView1.SelectedIndices[0];
            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                ShowDetail(listView1.SelectedIndices[0]);
            }
        }

        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            e.NewValue = (bool)listView1.Items[e.Index].Tag ? CheckState.Checked : CheckState.Unchecked;
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}