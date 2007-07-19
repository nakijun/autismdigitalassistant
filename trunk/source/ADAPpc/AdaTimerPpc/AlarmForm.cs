using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UtilitiesPpc;

namespace AdaTimerPpc
{
    public partial class AlarmForm : AdaBaseForm
    {
        private bool isAlarmSet;

        public bool IsAlarmSet
        {
            get { return isAlarmSet; }
            set { isAlarmSet = value; }
        }

        private DateTime alarmDateTime;

        public DateTime AlarmDateTime
        {
            get { return alarmDateTime; }
            set { alarmDateTime = value; }
        }

        public AlarmForm()
        {
            InitializeComponent();
            this.alarmDateTime = DateTime.Now;
        }

        private void AlarmForm_Load(object sender, EventArgs e)
        {
            this.SetDisplayDateTime(0);

            this.listViewFromNow.Items[0].Selected = true;
            this.listViewFromNow.Items[0].Focused = true;

            if (this.isAlarmSet)
            {
                this.listViewFromNow.Items[0].Checked = true;
            }
        }

        private void SetDisplayDateTime(int fromNowMinutes)
        {
            bool enabled;
            DateTime dateTime;

            if (fromNowMinutes > 0)
            {
                dateTime = DateTime.Now.AddMinutes(fromNowMinutes);
                enabled = false;
            }
            else
            {
                dateTime = this.alarmDateTime;
                enabled = true;
            }

            this.dateTimePickerAlarmDate.Value = dateTime;
            this.dateTimePickerAlarmTime.Value = dateTime;

            this.dateTimePickerAlarmDate.Enabled = enabled;
            this.dateTimePickerAlarmTime.Enabled = enabled;
        }

        private void menuItemSetAlarm_Click(object sender, EventArgs e)
        {
            if (this.listViewFromNow.FocusedItem != null)
            {
                this.listViewFromNow.FocusedItem.Checked = true;
            }
        }

        private void menuItemRemoveAlarm_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listViewFromNow.Items.Count; i++)
            {
                this.listViewFromNow.Items[i].Checked=false;
            }
        }

        private void listViewFromNow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listViewFromNow.FocusedItem != null)
            {
                int fromNowMinutes = Convert.ToInt32(this.listViewFromNow.FocusedItem.Tag);

                this.SetDisplayDateTime(fromNowMinutes);
            }
        }

        private void listViewFromNow_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                if (!this.listViewFromNow.SelectedIndices.Contains(e.Index))
                {
                    e.NewValue = e.CurrentValue;
                }
                else
                {
                    for (int i = 0; i < this.listViewFromNow.Items.Count; i++)
                    {
                        if (i != e.Index)
                        {
                            this.listViewFromNow.Items[i].Checked = false;
                        }
                    }
                }
            }
        }

        private void AlarmForm_Closing(object sender, CancelEventArgs e)
        {
            this.isAlarmSet = false;

            for (int i = 0; i < this.listViewFromNow.Items.Count; i++)
            {
                if (this.listViewFromNow.Items[i].Checked)
                {
                    this.isAlarmSet = true;
                    this.listViewFromNow.Items[i].Focused = true;
                    this.listViewFromNow.Items[i].Selected = true;
                    break;
                }
            }

            if (this.isAlarmSet)
            {
                DateTime date = this.dateTimePickerAlarmDate.Value.Date;
                DateTime time = this.dateTimePickerAlarmTime.Value;

                this.alarmDateTime = date.Add(time.TimeOfDay);
            }
        }
    }
}