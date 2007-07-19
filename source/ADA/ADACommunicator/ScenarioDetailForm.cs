using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utilities;
using ADASymbolPicker;
using System.Media;
using System.IO;
using ADADataAccess;

namespace ADACommunicator
{
    public partial class ScenarioDetailForm : Form
    {
        private const string SCENARIO_TABLE = "Scenario";

        public ADACommunicatorDataSet CommunicatorDataSet
        {
            get { return adaCommunicatorDataSet1; }
        }

        public ScenarioDetailForm()
        {
            InitializeComponent();
        }

        private void ScenarioDetailForm_Load(object sender, EventArgs e)
        {
            DataRowView view = this.BindingContext[adaCommunicatorDataSet1, SCENARIO_TABLE].Current as DataRowView;
            ADACommunicatorDataSet.ScenarioRow scenarioRow = view.Row as ADACommunicatorDataSet.ScenarioRow;

            if (!scenarioRow.IsSymbolIdNull())
            {
                if (!scenarioRow.SymbolRow.IsImageNull())
                {
                    byte[] image = scenarioRow.SymbolRow.Image;
                    this.pictureBoxImage.Image = ImageEngine.Resize(ImageEngine.FromArray(image), this.pictureBoxImage.Size);
                }
            }

            buttonPlaySound.Enabled = (!scenarioRow.IsSymbolIdNull() && !scenarioRow.SymbolRow.IsSoundNull());
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.BindingContext[adaCommunicatorDataSet1, SCENARIO_TABLE].EndCurrentEdit();
        }

        private void buttonChangeSymbol_Click(object sender, EventArgs e)
        {
            DataRowView view = this.BindingContext[adaCommunicatorDataSet1, SCENARIO_TABLE].Current as DataRowView;
            ADACommunicatorDataSet.ScenarioRow scenarioRow = view.Row as ADACommunicatorDataSet.ScenarioRow;

            SymbolPicker picker = new SymbolPicker();
            SymbolDataSet.LocalizedSymbolRow symbolRow = picker.PickSymbol(this,
                scenarioRow.IsSymbolIdNull() ? -1 : scenarioRow.SymbolId);

            if (symbolRow != null)
            {
                if (adaCommunicatorDataSet1.Symbol.FindBySymbolId(symbolRow.SymbolId) == null)
                {
                    ADACommunicatorDataSet.SymbolRow newRow = adaCommunicatorDataSet1.Symbol.NewSymbolRow();
                    newRow.SymbolId = symbolRow.SymbolId;

                    if (!symbolRow.IsSoundNull())
                    {
                        newRow.Sound = symbolRow.Sound;
                    }

                    if (!symbolRow.IsImageNull())
                    {
                        newRow.Image = symbolRow.Image;
                    }

                    adaCommunicatorDataSet1.Symbol.AddSymbolRow(newRow);
                    adaCommunicatorDataSet1.Symbol.AcceptChanges();
                }

                byte[] image = symbolRow.Image;
                if (image != null)
                {
                    this.pictureBoxImage.Image = ImageEngine.Resize(ImageEngine.FromArray(image), this.pictureBoxImage.Size);
                }

                buttonPlaySound.Enabled = !symbolRow.IsSoundNull();

                this.BindingContext[adaCommunicatorDataSet1, SCENARIO_TABLE].EndCurrentEdit();

                view.BeginEdit();
                scenarioRow.SymbolId = symbolRow.SymbolId;
                if (scenarioRow.IsNameNull() || scenarioRow.Name.Length == 0)
                {
                    scenarioRow.Name = symbolRow.Name;
                }

                view.EndEdit();
            }
        }

        private void buttonPlaySound_Click(object sender, EventArgs e)
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer())
                {
                    DataRowView view = this.BindingContext[adaCommunicatorDataSet1, SCENARIO_TABLE].Current as DataRowView;
                    ADACommunicatorDataSet.ScenarioRow scenarioRow = view.Row as ADACommunicatorDataSet.ScenarioRow;

                    using (MemoryStream ms = new MemoryStream(scenarioRow.SymbolRow.Sound))
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