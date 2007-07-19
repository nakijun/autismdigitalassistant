using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ADASchedule.ADAScheduleDataSetTableAdapters;
using ADADataAccess;
using System.Data.SqlClient;
using System.Threading;
using System.Globalization;

namespace ADASchedule
{
    public partial class MainForm : Form
    {
        private const string ENGLISH_CULTURE = "en";
        private const string SIMPLIFIED_CHINESE_CULTURE = "zh-CHS";
        private const string TRADITIONAL_CHINESE_CULTURE = "zh-CHT";

        private string _cultureName;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.adaScheduleDataSet1.DefaultViewManager.DataViewSettings["Activity"].Sort = "Sequence ASC";
            ChangeCulture(Properties.Settings.Default.CultureName);
        }

        private void LoadDataSet()
        {
            Cursor.Current = Cursors.WaitCursor;

            LoadDataSet(adaScheduleDataSet1, monthCalendar1.SelectionStart, true);
            EnableEditButtons();

            Cursor.Current = Cursors.Default;
        }

        private void LoadDataSet(ADAScheduleDataSet dataSet, DateTime date, bool loadSymbol)
        {
            try
            {
                dataSet.EnforceConstraints = false;

                UserTableAdapter userAdapter = new UserTableAdapter();
                userAdapter.Fill(dataSet.User);

                ScheduleTableAdapter scheduleAdapter = new ScheduleTableAdapter();
                scheduleAdapter.Fill(dataSet.Schedule, date);

                if (loadSymbol)
                {
                    SymbolTableAdapter symbolAdapter = new SymbolTableAdapter();
                    symbolAdapter.Fill(dataSet.Symbol, date);
                }

                ActivityTableAdapter activityAdapter = new ActivityTableAdapter();
                activityAdapter.Fill(dataSet.Activity, date);

                if (loadSymbol)
                {
                    dataSet.EnforceConstraints = true;

                    foreach (ADAScheduleDataSet.ActivityRow activityRow in dataSet.Activity.Rows)
                    {
                        if (!activityRow.IsSymbolIdNull() && !activityRow.SymbolRow.IsImageNull())
                        {
                            activityRow.Image = activityRow.SymbolRow.Image;
                        }
                    }
                }

                dataSet.AcceptChanges();
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }
        }

        private void ReportError(Exception ex)
        {
            string statusMessage = ex.Message.ToString() + "\r\n" + ex.StackTrace;

            // ...post the caller's message to the status bar.
            MessageBox.Show(statusMessage, this.Text,
               MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonAddActivity_Click(object sender, EventArgs e)
        {
            ActivityDetailForm f = new ActivityDetailForm();
            f.ScheduleDataSet.Merge(adaScheduleDataSet1);

            DataRowView view = this.BindingContext[adaScheduleDataSet1, "User"].Current as DataRowView;
            ADAScheduleDataSet.UserRow userRow = view.Row as ADAScheduleDataSet.UserRow;

            DataRow[] scheduleRows = f.ScheduleDataSet.Schedule.Select("UserId=" + userRow.UserId);
            ADAScheduleDataSet.ScheduleRow currentScheduleRow = null;
            foreach (ADAScheduleDataSet.ScheduleRow scheduleRow in scheduleRows)
            {
                if (scheduleRow.Type == (int)ScheduleType.Normal)
                {
                    currentScheduleRow = scheduleRow;
                    break;
                }
            }

            if (currentScheduleRow == null)
            {
                currentScheduleRow = f.ScheduleDataSet.Schedule.NewScheduleRow();
                currentScheduleRow.UserId = userRow.UserId;
                currentScheduleRow.Type = (int)ScheduleType.Normal;
                currentScheduleRow.Date = monthCalendar1.SelectionStart;
                currentScheduleRow.IsActive = true;
                f.ScheduleDataSet.Schedule.AddScheduleRow(currentScheduleRow);
            }

            int sequenceMax = 0;
            DateTime startTimeMax = currentScheduleRow.Date;
            DateTime endTimeMax = currentScheduleRow.Date;

            ADAScheduleDataSet.ActivityRow lastActivityRow = null;

            foreach (ADAScheduleDataSet.ActivityRow activityRow in
                currentScheduleRow.GetActivityRowsByFK_Activity_Schedule())
            {
                if (sequenceMax < activityRow.Sequence)
                {
                    sequenceMax = activityRow.Sequence;
                    lastActivityRow = activityRow;

                    if (!activityRow.IsStartTimeNull() && startTimeMax.CompareTo(activityRow.StartTime) < 0)
                    {
                        startTimeMax = activityRow.StartTime;
                    }

                    if (!activityRow.IsEndTimeNull() && endTimeMax.CompareTo(activityRow.EndTime) < 0)
                    {
                        endTimeMax = activityRow.EndTime;
                    }
                }
            }

            ADAScheduleDataSet.ActivityRow currentActivityRow = f.ScheduleDataSet.Activity.NewActivityRow();
            currentActivityRow.ScheduleId = currentScheduleRow.ScheduleId;
            currentActivityRow.Sequence = sequenceMax + 1;
            currentActivityRow.StartTime = endTimeMax;
            currentActivityRow.EndTime = endTimeMax + endTimeMax.Subtract(startTimeMax);
            currentActivityRow.Name = string.Format("Step {0}", currentActivityRow.Sequence);

            if (lastActivityRow != null)
            {
                currentActivityRow.SymbolId = lastActivityRow.SymbolId;
                currentActivityRow.Image = lastActivityRow.Image;
            }

            f.ScheduleDataSet.Activity.AddActivityRow(currentActivityRow);

            f.ScheduleDataSet.DefaultViewManager.DataViewSettings["Activity"].RowFilter = "ActivityId=" + currentActivityRow.ActivityId;

            if (f.ShowDialog() == DialogResult.OK)
            {
                adaScheduleDataSet1.Merge(f.ScheduleDataSet);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            DataRowView view = this.BindingContext[adaScheduleDataSet1, "User.FK_Schedule_User.FK_Activity_Schedule"].Current as DataRowView;
            ADAScheduleDataSet.ActivityRow row = view.Row as ADAScheduleDataSet.ActivityRow;

            ActivityDetailForm f = new ActivityDetailForm();
            f.ScheduleDataSet.Merge(adaScheduleDataSet1);

            f.ScheduleDataSet.DefaultViewManager.DataViewSettings["Activity"].RowFilter = "ActivityId=" + row.ActivityId;

            if (f.ShowDialog() == DialogResult.OK)
            {
                adaScheduleDataSet1.Merge(f.ScheduleDataSet);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DataRowView view = this.BindingContext[adaScheduleDataSet1, "User.FK_Schedule_User.FK_Activity_Schedule"].Current as DataRowView;
            ADAScheduleDataSet.ActivityRow row = view.Row as ADAScheduleDataSet.ActivityRow;
            int scheduleId = row.ScheduleId;
            row.Delete();

            DataRow[] rows = adaScheduleDataSet1.Activity.Select("ScheduleId = " + scheduleId,
                "Sequence ASC");

            int i = 1;
            foreach (ADAScheduleDataSet.ActivityRow activityRow in rows)
            {
                activityRow.Sequence = i++;
            }
        }

        private void SaveDataSet()
        {
            Cursor.Current = Cursors.WaitCursor;

            DateTime date = monthCalendar1.SelectionStart;

            try
            {
                adaScheduleDataSet1.EnforceConstraints = false;

                ScheduleTableAdapter scheduleAdapter = new ScheduleTableAdapter();
                scheduleAdapter.Update(adaScheduleDataSet1);

                ActivityTableAdapter activityAdapter = new ActivityTableAdapter();
                activityAdapter.Update(adaScheduleDataSet1);

                ReminderTableAdapter reminder = new ReminderTableAdapter();
                reminder.Update(adaScheduleDataSet1);

                Activity_ReminderTableAdapter arAdapter = new Activity_ReminderTableAdapter();
                arAdapter.Update(adaScheduleDataSet1);

                adaScheduleDataSet1.EnforceConstraints = true;
                adaScheduleDataSet1.AcceptChanges();
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }

            Cursor.Current = Cursors.Default;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            EnableEditButtons();
        }

        private void EnableEditButtons()
        {
            bool isEnabled = (this.BindingContext[adaScheduleDataSet1, "User.FK_Schedule_User.FK_Activity_Schedule"].Position >= 0);
            buttonEdit.Enabled = isEnabled;
            buttonDelete.Enabled = isEnabled;

            buttonAddActivity.Enabled = (this.BindingContext[adaScheduleDataSet1, "User"].Position >= 0);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();

            if (adaScheduleDataSet1.HasChanges())
            {
                string statusMessage = "Data has been changed. Do you want to save it?";

                DialogResult result = MessageBox.Show(statusMessage, this.Text,
                   MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveDataSet();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (adaScheduleDataSet1.HasChanges())
            {
                string statusMessage = "Data has been changed. Do you want to save it?";

                if (MessageBox.Show(statusMessage, this.Text,
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveDataSet();
                }
            }

            LoadDataSet();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDataSet();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDataSet();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectDateForm selectDateForm = new SelectDateForm();

            if (selectDateForm.ShowDialog(this) == DialogResult.OK)
            {
                for (DateTime copyToDate = selectDateForm.SelectionRange.Start;
                    copyToDate.CompareTo(selectDateForm.SelectionRange.End) <= 0;
                    copyToDate = copyToDate.AddDays(1))
                {
                    Cursor.Current = Cursors.WaitCursor;
                    ADAScheduleDataSet dataSet = new ADAScheduleDataSet();

                    try
                    {
                        LoadDataSet(dataSet, copyToDate, false);

                        DataRow[] scheduleRows = dataSet.Schedule.Select("Type <= " + 1);

                        foreach (ADAScheduleDataSet.ScheduleRow scheduleRow in scheduleRows)
                        {
                            scheduleRow.Delete();
                        }

                        ScheduleTableAdapter scheduleAdapter = new ScheduleTableAdapter();
                        ActivityTableAdapter activityAdapter = new ActivityTableAdapter();

                        activityAdapter.Update(dataSet);
                        scheduleAdapter.Update(dataSet);

                        scheduleRows = adaScheduleDataSet1.Schedule.Select("Type <= " + 1);

                        foreach (ADAScheduleDataSet.ScheduleRow scheduleRow in scheduleRows)
                        {
                            ADAScheduleDataSet.ScheduleRow newScheduleRow;
                            newScheduleRow = dataSet.Schedule.NewScheduleRow();
                            newScheduleRow.Date = copyToDate;
                            newScheduleRow.UserId = scheduleRow.UserId;
                            newScheduleRow.Type = scheduleRow.Type;
                            newScheduleRow.IsActive = true;
                            dataSet.Schedule.AddScheduleRow(newScheduleRow);

                            scheduleAdapter.Update(dataSet);

                            foreach (ADAScheduleDataSet.ActivityRow activityRow in scheduleRow.GetActivityRowsByFK_Activity_Schedule())
                            {
                                ADAScheduleDataSet.ActivityRow newActivityRow;

                                newActivityRow = dataSet.Activity.NewActivityRow();
                                newActivityRow.ScheduleId = newScheduleRow.ScheduleId;

                                if (!activityRow.IsStartTimeNull())
                                {
                                    TimeSpan timeSpan = copyToDate.Subtract(activityRow.StartTime.Date);
                                    newActivityRow.StartTime = activityRow.StartTime.AddDays(timeSpan.Days);
                                }

                                if (!activityRow.IsEndTimeNull())
                                {
                                    TimeSpan timeSpan = copyToDate.Subtract(activityRow.EndTime.Date);
                                    newActivityRow.EndTime = activityRow.EndTime.AddDays(timeSpan.Days);
                                }

                                if (!activityRow.IsSymbolIdNull())
                                {
                                    newActivityRow.SymbolId = activityRow.SymbolId;
                                }

                                if (!activityRow.IsNameNull())
                                {
                                    newActivityRow.Name = activityRow.Name;
                                }

                                if (!activityRow.IsSequenceNull())
                                {
                                    newActivityRow.Sequence = activityRow.Sequence;
                                }

                                dataSet.Activity.AddActivityRow(newActivityRow);
                                activityAdapter.Update(dataSet);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ReportError(ex);
                    }
                }

                Cursor.Current = Cursors.Default;

                monthCalendar1.SetDate(selectDateForm.SelectionRange.Start);
                LoadDataSet();
            }
        }

        private void cleanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectDateForm selectDateForm = new SelectDateForm();

            selectDateForm.SelectionRange.Start = monthCalendar1.SelectionStart.AddDays(-1);
            selectDateForm.SelectionRange.End = selectDateForm.SelectionRange.Start;

            if (selectDateForm.ShowDialog(this) == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;

                try
                {
                    ScheduleTableAdapter scheduleAdapter = new ScheduleTableAdapter();
                    scheduleAdapter.Connection.Open();

                    SqlCommand cmd = scheduleAdapter.Connection.CreateCommand();
                    cmd.CommandText = "update [Schedule] set [IsActive]='false' where [Date] < @Date";
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime);
                    cmd.Parameters[0].Value = DateTime.Now.Date;
                    cmd.ExecuteNonQuery();

                    scheduleAdapter.Connection.Close();
                }
                catch (Exception ex)
                {
                    ReportError(ex);
                }

                Cursor.Current = Cursors.Default;

                LoadDataSet();
            }
        }

        private void ChangeCulture(string cultureName)
        {
            if (cultureName != this._cultureName)
            {
                this._cultureName = cultureName;
                Properties.Settings.Default.CultureName = cultureName;

                if (cultureName == ENGLISH_CULTURE)
                {
                    this.englishToolStripMenuItem.Checked = true;
                    this.simplifiedChineseToolStripMenuItem.Checked = false;
                    this.traditionalChineseToolStripMenuItem.Checked = false;
                }
                else if (cultureName == SIMPLIFIED_CHINESE_CULTURE)
                {
                    this.englishToolStripMenuItem.Checked = false;
                    this.simplifiedChineseToolStripMenuItem.Checked = true;
                    this.traditionalChineseToolStripMenuItem.Checked = false;
                }
                else if (cultureName == TRADITIONAL_CHINESE_CULTURE)
                {
                    this.englishToolStripMenuItem.Checked = false;
                    this.simplifiedChineseToolStripMenuItem.Checked = false;
                    this.traditionalChineseToolStripMenuItem.Checked = true;
                }

                if (Thread.CurrentThread.CurrentUICulture.Name != cultureName)
                {
                    CultureInfo newCulture = new CultureInfo(cultureName);
                    FormLanguageSwitchSingleton.Instance.ChangeCurrentThreadUICulture(newCulture);
                    FormLanguageSwitchSingleton.Instance.ChangeLanguage(this);
                }

                this.LoadDataSet();
            }
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCulture(ENGLISH_CULTURE);
        }

        private void simplifiedChineseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCulture(SIMPLIFIED_CHINESE_CULTURE);
        }

        private void traditionalChineseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCulture(TRADITIONAL_CHINESE_CULTURE);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            buttonEdit_Click(sender, e);
        }
    }
}