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

namespace AdaSchedulePpc
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
                
                if (!_activityRow.IsStartTimeNull() && !_activityRow.IsEndTimeNull())
                {
                    sb.Append(_activityRow.StartTime.ToString("hh:mm"));
                    sb.Append(" - ");
                    sb.Append(_activityRow.EndTime.ToString("hh:mm"));
                    sb.Append("  ");
                }

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
                        menuItemStart.Text = AdaSchedulePpc.Properties.Resources.MenuStop;
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