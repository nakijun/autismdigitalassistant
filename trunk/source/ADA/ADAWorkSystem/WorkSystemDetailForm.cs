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
    public partial class WorkSystemDetailForm : Form
    {
        private const string SCHEDULE_TABLE = "Schedule";

        public ADAWorkSystemDataSet WorkSystemDataSet
        {
            get { return adaWorkSystemDataSet1; }
        }

        public WorkSystemDetailForm()
        {
            InitializeComponent();
        }

        private void WorkSystemDetail_Load(object sender, EventArgs e)
        {
            DataRowView view = this.BindingContext[adaWorkSystemDataSet1, SCHEDULE_TABLE].Current as DataRowView;
            ADAWorkSystemDataSet.ScheduleRow scheduleRow = view.Row as ADAWorkSystemDataSet.ScheduleRow;

            if (!scheduleRow.IsSymbolIdNull())
            {
                if (!scheduleRow.SymbolRow.IsImageNull())
                {
                    byte[] image = scheduleRow.SymbolRow.Image;
                    this.pictureBoxImage.Image = ImageEngine.Resize(ImageEngine.FromArray(image), this.pictureBoxImage.Size);
                }
            }

            buttonPlaySound.Enabled = (!scheduleRow.IsSymbolIdNull() && !scheduleRow.SymbolRow.IsSoundNull());
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.BindingContext[adaWorkSystemDataSet1, SCHEDULE_TABLE].EndCurrentEdit();
        }

        private void buttonChangeSymbol_Click(object sender, EventArgs e)
        {
            DataRowView view = this.BindingContext[adaWorkSystemDataSet1, SCHEDULE_TABLE].Current as DataRowView;
            ADAWorkSystemDataSet.ScheduleRow scheduleRow = view.Row as ADAWorkSystemDataSet.ScheduleRow;

            SymbolPicker picker = new SymbolPicker();
            SymbolDataSet.LocalizedSymbolRow symbolRow = picker.PickSymbol(this,
                scheduleRow.IsSymbolIdNull() ? -1 : scheduleRow.SymbolId);

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

                this.BindingContext[adaWorkSystemDataSet1, SCHEDULE_TABLE].EndCurrentEdit();

                view.BeginEdit();
                scheduleRow.SymbolId = symbolRow.SymbolId;
                scheduleRow.Name = symbolRow.Name;
                view.EndEdit();
            }
        }

        private void buttonPlaySound_Click(object sender, EventArgs e)
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer())
                {
                    DataRowView view = this.BindingContext[adaWorkSystemDataSet1, SCHEDULE_TABLE].Current as DataRowView;
                    ADAWorkSystemDataSet.ScheduleRow scheduleRow = view.Row as ADAWorkSystemDataSet.ScheduleRow;

                    using (MemoryStream ms = new MemoryStream(scheduleRow.SymbolRow.Sound))
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