using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utilities;
using ADASymbolPicker;
using ADADataAccess;
using System.Media;
using System.IO;

namespace ADASchedule
{
    public partial class ActivityDetailForm : Form
    {
        private const string ACTIVITY_TABLE = "Activity";

        private TimeSpan _alarmTimeSpan = new TimeSpan(0, -15, 0);

        public ADAScheduleDataSet ScheduleDataSet
        {
            get { return adaScheduleDataSet1; }
        }

        public ActivityDetailForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.BindingContext[adaScheduleDataSet1, ACTIVITY_TABLE].EndCurrentEdit();

            DataRowView view = this.BindingContext[adaScheduleDataSet1, ACTIVITY_TABLE].Current as DataRowView;
            ADAScheduleDataSet.ActivityRow activityRow = view.Row as ADAScheduleDataSet.ActivityRow;

            DataRow[] rows = adaScheduleDataSet1.Activity.Select(
                "ActivityId <> " + activityRow.ActivityId + " AND ScheduleId = " + activityRow.ScheduleId,
                "Sequence DESC");

            foreach (ADAScheduleDataSet.ActivityRow row in rows)
            {
                if (row.Sequence < activityRow.Sequence)
                {
                    break;
                }

                row.Sequence++;
            }

            rows = adaScheduleDataSet1.Activity.Select("ScheduleId = " + activityRow.ScheduleId,
                "Sequence ASC");

            int i = 1;
            foreach (ADAScheduleDataSet.ActivityRow row in rows)
            {
                row.Sequence = i++;
            }

            ADAScheduleDataSet.Activity_ReminderRow[] activityReminderRows = activityRow.GetActivity_ReminderRows();

            foreach (ADAScheduleDataSet.Activity_ReminderRow activityReminderRow in activityReminderRows)
            {
                if (!activityReminderRow.IsTimeNull() &&
                    activityReminderRow.ReminderId == ADADataAccess.Constants.ALARM_REMINDER_ID)
                {
                    activityReminderRow.Time = this.dateTimePickerAlarm.Value;
                }
            }
        }

        private void ActivityDetailForm_Load(object sender, EventArgs e)
        {
            DataRowView view = this.BindingContext[adaScheduleDataSet1, ACTIVITY_TABLE].Current as DataRowView;
            ADAScheduleDataSet.ActivityRow activityRow = view.Row as ADAScheduleDataSet.ActivityRow;

            if (!activityRow.IsSymbolIdNull())
            {
                if (!activityRow.SymbolRow.IsImageNull())
                {
                    byte[] image = activityRow.SymbolRow.Image;
                    this.pictureBoxImage.Image = ImageEngine.Resize(ImageEngine.FromArray(image), this.pictureBoxImage.Size);
                }
            }

            ADAScheduleDataSet.Activity_ReminderRow[] activityReminderRows = activityRow.GetActivity_ReminderRows();

            foreach (ADAScheduleDataSet.Activity_ReminderRow activityReminderRow in activityReminderRows)
            {
                if (!activityReminderRow.IsTimeNull() &&
                    activityReminderRow.ReminderId == ADADataAccess.Constants.ALARM_REMINDER_ID)
                {
                    this.dateTimePickerAlarm.Value = activityReminderRow.Time;
                    _alarmTimeSpan = activityReminderRow.Time - activityRow.EndTime;
                }
            }

            buttonPlaySound.Enabled = (!activityRow.IsSymbolIdNull() && !activityRow.SymbolRow.IsSoundNull());
        }

        private void buttonChangeSymbol_Click(object sender, EventArgs e)
        {
            DataRowView view = this.BindingContext[adaScheduleDataSet1, ACTIVITY_TABLE].Current as DataRowView;
            ADAScheduleDataSet.ActivityRow activityRow = view.Row as ADAScheduleDataSet.ActivityRow;

            SymbolPicker picker = new SymbolPicker();
            SymbolDataSet.LocalizedSymbolRow symbolRow = picker.PickSymbol(this,
                activityRow.IsSymbolIdNull() ? -1 : activityRow.SymbolId);

            if (symbolRow != null)
            {
                if (adaScheduleDataSet1.Symbol.FindBySymbolId(symbolRow.SymbolId) == null)
                {
                    ADAScheduleDataSet.SymbolRow newRow = adaScheduleDataSet1.Symbol.NewSymbolRow();
                    newRow.SymbolId = symbolRow.SymbolId;

                    if (!symbolRow.IsSoundNull())
                    {
                        newRow.Sound = symbolRow.Sound;
                    }

                    if (!symbolRow.IsImageNull())
                    {
                        newRow.Image = symbolRow.Image;
                    }

                    adaScheduleDataSet1.Symbol.AddSymbolRow(newRow);
                }

                byte[] image = symbolRow.Image;
                if (image != null)
                {
                    this.pictureBoxImage.Image = ImageEngine.Resize(ImageEngine.FromArray(image), this.pictureBoxImage.Size);
                }

                buttonPlaySound.Enabled = !symbolRow.IsSoundNull();

                this.BindingContext[adaScheduleDataSet1, ACTIVITY_TABLE].EndCurrentEdit();

                view.BeginEdit();
                activityRow.Name = symbolRow.Name;
                activityRow.SymbolId = symbolRow.SymbolId;
                activityRow.Image = image;
                view.EndEdit();
            }
        }

        private void buttonPlaySound_Click(object sender, EventArgs e)
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer())
                {
                    DataRowView view = this.BindingContext[adaScheduleDataSet1, ACTIVITY_TABLE].Current as DataRowView;
                    ADAScheduleDataSet.ActivityRow activityRow = view.Row as ADAScheduleDataSet.ActivityRow;

                    using (MemoryStream ms = new MemoryStream(activityRow.SymbolRow.Sound))
                    {
                        player.Stream = ms;
                        player.Play();
                    }
                }
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

        private void dateTimePickerEndTime_ValueChanged(object sender, EventArgs e)
        {
            this.dateTimePickerAlarm.Value = this.dateTimePickerEndTime.Value.Add(_alarmTimeSpan);
        }

        private void dateTimePickerAlarm_ValueChanged(object sender, EventArgs e)
        {
            _alarmTimeSpan = this.dateTimePickerAlarm.Value - this.dateTimePickerEndTime.Value;
        }

    }
}