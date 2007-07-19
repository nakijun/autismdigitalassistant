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

namespace ADAWorkSystem
{
    public partial class ActivityDetailForm : Form
    {
        private const string ACTIVITY_TABLE = "Activity";

        public ADAWorkSystemDataSet WorkSystemDataSet
        {
            get { return adaWorkSystemDataSet1; }
        }

        public ActivityDetailForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.BindingContext[adaWorkSystemDataSet1, ACTIVITY_TABLE].EndCurrentEdit();

            DataRowView view = this.BindingContext[adaWorkSystemDataSet1, ACTIVITY_TABLE].Current as DataRowView;
            ADAWorkSystemDataSet.ActivityRow activityRow = view.Row as ADAWorkSystemDataSet.ActivityRow;

            DataRow[] rows = adaWorkSystemDataSet1.Activity.Select(
                "ActivityId <> " + activityRow.ActivityId + " AND ScheduleId = " + activityRow.ScheduleId,
                "Sequence DESC");

            foreach (ADAWorkSystemDataSet.ActivityRow row in rows)
            {
                if (row.Sequence < activityRow.Sequence)
                {
                    break;
                }

                row.Sequence++;
            }

            rows = adaWorkSystemDataSet1.Activity.Select("ScheduleId = " + activityRow.ScheduleId,
                "Sequence ASC");

            int i = 1;
            foreach (ADAWorkSystemDataSet.ActivityRow row in rows)
            {
                row.Sequence = i++;
            }
        }

        private void ActivityDetailForm_Load(object sender, EventArgs e)
        {
            DataRowView view = this.BindingContext[adaWorkSystemDataSet1, ACTIVITY_TABLE].Current as DataRowView;
            ADAWorkSystemDataSet.ActivityRow activityRow = view.Row as ADAWorkSystemDataSet.ActivityRow;

            if (!activityRow.IsSymbolIdNull())
            {
                if (!activityRow.SymbolRow.IsImageNull())
                {
                    byte[] image = activityRow.SymbolRow.Image;
                    this.pictureBoxImage.Image = ImageEngine.Resize(ImageEngine.FromArray(image), this.pictureBoxImage.Size);
                }
            }

            buttonPlaySound.Enabled = (!activityRow.IsSymbolIdNull() && !activityRow.SymbolRow.IsSoundNull());
        }

        private void buttonChangeSymbol_Click(object sender, EventArgs e)
        {
            DataRowView view = this.BindingContext[adaWorkSystemDataSet1, ACTIVITY_TABLE].Current as DataRowView;
            ADAWorkSystemDataSet.ActivityRow activityRow = view.Row as ADAWorkSystemDataSet.ActivityRow;

            SymbolPicker picker = new SymbolPicker();
            SymbolDataSet.LocalizedSymbolRow symbolRow = picker.PickSymbol(this,
                activityRow.IsSymbolIdNull() ? -1 : activityRow.SymbolId);

            if (symbolRow != null)
            {
                if (adaWorkSystemDataSet1.Symbol.FindBySymbolId(symbolRow.SymbolId) == null)
                {
                    ADAWorkSystemDataSet.SymbolRow newRow = adaWorkSystemDataSet1.Symbol.NewSymbolRow();
                    newRow.SymbolId = symbolRow.SymbolId;

                    if (!symbolRow.IsSoundNull())
                    {
                        newRow.Sound = symbolRow.Sound;
                    }

                    if (!symbolRow.IsImageNull())
                    {
                        newRow.Image = symbolRow.Image;
                    }

                    adaWorkSystemDataSet1.Symbol.AddSymbolRow(newRow);
                }

                byte[] image = symbolRow.Image;
                if (image != null)
                {
                    this.pictureBoxImage.Image = ImageEngine.Resize(ImageEngine.FromArray(image), this.pictureBoxImage.Size);
                }

                buttonPlaySound.Enabled = !symbolRow.IsSoundNull();

                this.BindingContext[adaWorkSystemDataSet1, ACTIVITY_TABLE].EndCurrentEdit();

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
                    DataRowView view = this.BindingContext[adaWorkSystemDataSet1, ACTIVITY_TABLE].Current as DataRowView;
                    ADAWorkSystemDataSet.ActivityRow activityRow = view.Row as ADAWorkSystemDataSet.ActivityRow;

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

    }
}