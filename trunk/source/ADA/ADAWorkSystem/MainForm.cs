using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ADAWorkSystem.ADAWorkSystemDataSetTableAdapters;
using ADADataAccess;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Threading;

namespace ADAWorkSystem
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
            this.adaWorkSystemDataSet1.DefaultViewManager.DataViewSettings["Schedule"].Sort = "Name ASC";
            this.adaWorkSystemDataSet1.DefaultViewManager.DataViewSettings["Activity"].Sort = "Sequence ASC";
            ChangeCulture(Properties.Settings.Default.CultureName);
        }

        private void RefreshWorkSystemListView()
        {
            object currentSelection = null;

            if (this.listViewWorkSystem.SelectedItems.Count > 0)
            {
                currentSelection = this.listViewWorkSystem.SelectedItems[0].Tag;
            }

            ListViewItem selectedItem = null;

            this.listViewWorkSystem.Clear();
            this.imageListWorkSystem.Images.Clear();

            foreach (ADAWorkSystemDataSet.ScheduleRow row in this.adaWorkSystemDataSet1.Schedule.Select(@"IsActive='true' AND Type = " + (int)ScheduleType.WorkSystemModel, "[Name] ASC"))
            {
                string name = row.Name;
                ListViewItem item;

                if (!row.IsSymbolIdNull() && !row.SymbolRow.IsImageNull())
                {
                    byte[] image = row.SymbolRow.Image;

                    using (MemoryStream ms = new MemoryStream(image))
                    {
                        item = this.listViewWorkSystem.Items.Add(name, this.imageListWorkSystem.Images.Count);
                        this.imageListWorkSystem.Images.Add(new Bitmap(ms));
                    }
                }
                else
                {
                    item = this.listViewWorkSystem.Items.Add(name);
                }

                item.Tag = row;

                if (row == currentSelection)
                {
                    selectedItem = item;
                }

                EnableEditButtons();
            }

            if (listViewWorkSystem.Items.Count > 0)
            {
                if (selectedItem == null)
                {
                    selectedItem = listViewWorkSystem.Items[0];
                }

                selectedItem.Selected = true;
                selectedItem.Focused = true;
            }
        }

        private void LoadDataSet()
        {
            Cursor.Current = Cursors.WaitCursor;

            LoadDataSet(adaWorkSystemDataSet1, true);
            EnableEditButtons();
            RefreshWorkSystemListView();

            Cursor.Current = Cursors.Default;
        }

        private void LoadDataSet(ADAWorkSystemDataSet dataSet, bool loadSymbol)
        {
            try
            {
                dataSet.EnforceConstraints = false;

                ScheduleTableAdapter scheduleAdapter = new ScheduleTableAdapter();
                scheduleAdapter.Fill(dataSet.Schedule);

                if (loadSymbol)
                {
                    SymbolTableAdapter symbolAdapter = new SymbolTableAdapter();
                    symbolAdapter.Fill(dataSet.Symbol);
                }

                ActivityTableAdapter activityAdapter = new ActivityTableAdapter();
                activityAdapter.Fill(dataSet.Activity);

                if (loadSymbol)
                {
                    dataSet.EnforceConstraints = true;

                    foreach (ADAWorkSystemDataSet.ActivityRow activityRow in dataSet.Activity.Rows)
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
            f.WorkSystemDataSet.Merge(adaWorkSystemDataSet1);

            DataRowView view = this.BindingContext[adaWorkSystemDataSet1, "Schedule"].Current as DataRowView;
            ADAWorkSystemDataSet.ScheduleRow currentScheduleRow = view.Row as ADAWorkSystemDataSet.ScheduleRow;

            int sequenceMax = 0;
            ADAWorkSystemDataSet.ActivityRow lastActivityRow = null;

            foreach (ADAWorkSystemDataSet.ActivityRow activityRow in
                currentScheduleRow.GetActivityRows())
            {
                if (sequenceMax < activityRow.Sequence)
                {
                    sequenceMax = activityRow.Sequence;
                    lastActivityRow = activityRow;
                }
            }

            ADAWorkSystemDataSet.ActivityRow currentActivityRow = f.WorkSystemDataSet.Activity.NewActivityRow();
            currentActivityRow.ScheduleId = currentScheduleRow.ScheduleId;
            currentActivityRow.Sequence = sequenceMax + 1;
            currentActivityRow.Name = string.Format("Step {0}", currentActivityRow.Sequence);

            if (lastActivityRow != null && !lastActivityRow.IsSymbolIdNull())
            {
                currentActivityRow.SymbolId = lastActivityRow.SymbolId;
                currentActivityRow.Image = lastActivityRow.Image;
            }

            f.WorkSystemDataSet.Activity.AddActivityRow(currentActivityRow);

            f.WorkSystemDataSet.DefaultViewManager.DataViewSettings["Activity"].RowFilter = "ActivityId=" + currentActivityRow.ActivityId;

            if (f.ShowDialog() == DialogResult.OK)
            {
                adaWorkSystemDataSet1.Merge(f.WorkSystemDataSet);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            DataRowView view = this.BindingContext[adaWorkSystemDataSet1, "Schedule.FK_Activity_Schedule"].Current as DataRowView;
            ADAWorkSystemDataSet.ActivityRow row = view.Row as ADAWorkSystemDataSet.ActivityRow;

            ActivityDetailForm f = new ActivityDetailForm();
            f.WorkSystemDataSet.Merge(adaWorkSystemDataSet1);

            f.WorkSystemDataSet.DefaultViewManager.DataViewSettings["Activity"].RowFilter = "ActivityId=" + row.ActivityId;

            if (f.ShowDialog() == DialogResult.OK)
            {
                adaWorkSystemDataSet1.Merge(f.WorkSystemDataSet);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DataRowView view = this.BindingContext[adaWorkSystemDataSet1, "Schedule.FK_Activity_Schedule"].Current as DataRowView;
            ADAWorkSystemDataSet.ActivityRow row = view.Row as ADAWorkSystemDataSet.ActivityRow;
            int scheduleId = row.ScheduleId;
            row.Delete();

            DataRow[] rows = adaWorkSystemDataSet1.Activity.Select("ScheduleId = " + scheduleId,
                "Sequence ASC");

            int i = 1;
            foreach (ADAWorkSystemDataSet.ActivityRow activityRow in rows)
            {
                activityRow.Sequence = i++;
            }
        }

        private void SaveDataSet()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                adaWorkSystemDataSet1.EnforceConstraints = false;

                ScheduleTableAdapter scheduleAdapter = new ScheduleTableAdapter();
                scheduleAdapter.Update(adaWorkSystemDataSet1);

                ActivityTableAdapter activityAdapter = new ActivityTableAdapter();
                activityAdapter.Update(adaWorkSystemDataSet1);

                adaWorkSystemDataSet1.EnforceConstraints = true;
                adaWorkSystemDataSet1.AcceptChanges();
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
            bool isEnabled = (this.BindingContext[adaWorkSystemDataSet1, "Schedule.FK_Activity_Schedule"].Position >= 0);
            buttonEdit.Enabled = isEnabled;
            buttonDelete.Enabled = isEnabled;

            buttonAddActivity.Enabled = (this.BindingContext[adaWorkSystemDataSet1, "Schedule"].Position >= 0);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();

            if (adaWorkSystemDataSet1.HasChanges())
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

        private void buttonAddWorkSystem_Click(object sender, EventArgs e)
        {
            WorkSystemDetailForm f = new WorkSystemDetailForm();
            f.WorkSystemDataSet.Merge(adaWorkSystemDataSet1);

            ADAWorkSystemDataSet.ScheduleRow newScheduleRow = f.WorkSystemDataSet.Schedule.NewScheduleRow();
            newScheduleRow.Name = "Work System " + (f.WorkSystemDataSet.Schedule.Count + 1);
            newScheduleRow.Type = (int)ScheduleType.WorkSystemModel;
            newScheduleRow.IsActive = true;
            f.WorkSystemDataSet.Schedule.AddScheduleRow(newScheduleRow);

            f.WorkSystemDataSet.DefaultViewManager.DataViewSettings["Schedule"].RowFilter = "ScheduleId=" + newScheduleRow.ScheduleId;

            if (f.ShowDialog() == DialogResult.OK)
            {
                adaWorkSystemDataSet1.Merge(f.WorkSystemDataSet);
                RefreshWorkSystemListView();
            }
        }

        private void buttonEditWorkSystem_Click(object sender, EventArgs e)
        {
            DataRowView view = this.BindingContext[adaWorkSystemDataSet1, "Schedule"].Current as DataRowView;
            ADAWorkSystemDataSet.ScheduleRow row = view.Row as ADAWorkSystemDataSet.ScheduleRow;

            WorkSystemDetailForm f = new WorkSystemDetailForm();
            f.WorkSystemDataSet.Merge(adaWorkSystemDataSet1);

            f.WorkSystemDataSet.DefaultViewManager.DataViewSettings["Schedule"].RowFilter = "ScheduleId=" + row.ScheduleId;

            if (f.ShowDialog() == DialogResult.OK)
            {
                adaWorkSystemDataSet1.Merge(f.WorkSystemDataSet);
                RefreshWorkSystemListView();
            }
        }

        private void buttonDeleteWorkSystem_Click(object sender, EventArgs e)
        {
            DataRowView view = this.BindingContext[adaWorkSystemDataSet1, "Schedule"].Current as DataRowView;
            ADAWorkSystemDataSet.ScheduleRow row = view.Row as ADAWorkSystemDataSet.ScheduleRow;

            if (MessageBox.Show("Are sure to delete " + row.Name + "?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                row.IsActive = false;
                RefreshWorkSystemListView();
            }
        }

        private void listViewWorkSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewWorkSystem.SelectedIndices.Count > 0)
            {
                for (int i = 0; i < this.BindingContext[adaWorkSystemDataSet1, "Schedule"].Count; i++)
                {
                    this.BindingContext[adaWorkSystemDataSet1, "Schedule"].Position = i;

                    DataRowView view = this.BindingContext[adaWorkSystemDataSet1, "Schedule"].Current as DataRowView;
                    if (view.Row == listViewWorkSystem.SelectedItems[0].Tag)
                    {
                        break;
                    }
                }
            }
        }

        private void listViewWorkSystem_ItemActivate(object sender, EventArgs e)
        {
            buttonEditWorkSystem_Click(sender, e);
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