using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UtilitiesPpc;
using System.IO;
using OpenNETCF.Media;
using AdaWorkSystemPpc.ADAMobileDataSetTableAdapters;

namespace AdaWorkSystemPpc
{
    public partial class ActivityDetailForm : Form
    {
        private ADAMobileDataSet.ActivityRow _activityRow;

        public ADAMobileDataSet.ActivityRow ActivityRow
        {
            get { return _activityRow; }
            set { _activityRow = value; }
        }

        private bool _isCurrentActivity;

        public bool IsCurrentActivity
        {
            get { return _isCurrentActivity; }
            set { _isCurrentActivity = value; }
        }

        public ActivityDetailForm()
        {
            InitializeComponent();
        }

        private void ActivityDetailForm_Load(object sender, EventArgs e)
        {
            if (_activityRow != null)
            {
                if (!_activityRow.IsSymbolIdNull())
                {
                    if (!_activityRow.SymbolRow.IsImageNull())
                    {
                        Image image = ImageEngine.FromArray(_activityRow.SymbolRow.Image);

                        if (image.Width <= pictureBox1.Width && image.Height <= pictureBox1.Height)
                        {
                            pictureBox1.Image = image;
                        }
                        else
                        {
                            ResizeImage(image);
                        }
                    }

                    if (!_activityRow.SymbolRow.IsSoundNull())
                    {
                        menuItemPlaySound.Enabled = true;
                    }

                }

                this.Text = _activityRow.Name;

                StringBuilder sb = new StringBuilder();

                if (_activityRow.IsExecutionStartNull())
                {
                    sb.Append("Not Started");
                }
                else if (_activityRow.IsExecutionEndNull())
                {
                    sb.Append("Started");
                }
                else
                {
                    sb.Append("Finished");
                }

                labelDescription.Text = sb.ToString();

                if (_isCurrentActivity)
                {
                    menuItemStart.Enabled = true;

                    if (!_activityRow.IsExecutionStartNull())
                    {
                        menuItemStart.Text = AdaWorkSystemPpc.Properties.Resources.MenuStop;
                    }
                }
            }
        }

        private void ResizeImage(Image image)
        {
            int width, height;
            double widthRatio = (double)image.Width / pictureBox1.Width;
            double heightRatio = (double)image.Height / pictureBox1.Height;

            if (widthRatio > heightRatio)
            {
                width = pictureBox1.Width;
                height = (int)(image.Height / widthRatio);
            }
            else
            {
                width = (int)(image.Width / heightRatio);
                height = pictureBox1.Height;
            }

            pictureBox1.Image = ImageEngine.Resize(image, new Size(width, height));
        }

        private void menuItemStart_Click(object sender, EventArgs e)
        {
            ADAMobileDataSet.ScheduleRow scheduleRow = _activityRow.ScheduleRowByFK_Activity_Schedule;

            if (scheduleRow.Type == (int)ScheduleType.WorkSystemModel)
            {
                ADAMobileDataSet.ScheduleDataTable scheduleTable = scheduleRow.Table as ADAMobileDataSet.ScheduleDataTable;
                ADAMobileDataSet.ScheduleRow newScheduleRow = scheduleTable.NewScheduleRow();
                newScheduleRow.Type = (int)ScheduleType.WorkSystemInstance;
                newScheduleRow.SymbolId = scheduleRow.SymbolId;
                newScheduleRow.Name = scheduleRow.Name;
                newScheduleRow.IsActive = true;
                newScheduleRow.rowguid = Guid.NewGuid();
                
                ADAMobileDataSet.UserDataTable userTable = scheduleTable.DataSet.Tables["User"] as ADAMobileDataSet.UserDataTable;
                if (userTable.Count > 0)
                {
                    newScheduleRow.UserId = (int)userTable.Rows[0]["UserId"];
                }

                scheduleTable.AddScheduleRow(newScheduleRow);

                ADAMobileDataSet.ActivityRow[] rows = scheduleRow.GetActivityRowsByFK_Activity_Schedule();
                ADAMobileDataSet.ActivityDataTable activityTable = _activityRow.Table as ADAMobileDataSet.ActivityDataTable;
                foreach (ADAMobileDataSet.ActivityRow row in rows)
                {
                    ADAMobileDataSet.ActivityRow newRow = activityTable.NewActivityRow();
                    newRow.ScheduleId = newScheduleRow.ScheduleId;
                    newRow.Sequence = row.Sequence;
                    newRow.Name = row.Name;
                    newRow.SymbolId = row.SymbolId;
                    newRow.rowguid = Guid.NewGuid();
                    activityTable.AddActivityRow(newRow);

                    if (row == _activityRow)
                    {
                        _activityRow = newRow;
                    }
                }
            }

            if (_activityRow.IsExecutionStartNull())
            {
                _activityRow.ExecutionStart = DateTime.Now;
            }
            else
            {
                _activityRow.ExecutionEnd = DateTime.Now;
            }

            DialogResult = DialogResult.OK;
        }

        private void menuItemPlaySound_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            using (MemoryStream ms = new MemoryStream(_activityRow.SymbolRow.Sound))
            {
                SoundPlayer player = new SoundPlayer(ms);
                player.PlaySync();
                player.Dispose();
            }

            Cursor.Current = Cursors.Default;
        }

    }
}