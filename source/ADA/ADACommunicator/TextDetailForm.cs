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

namespace ADACommunicator
{
    public partial class TextDetailForm : Form
    {
        private const string TEXT_TABLE = "Text";

        public ADACommunicatorDataSet CommunicatorDataSet
        {
            get { return adaCommunicatorDataSet1; }
        }

        public TextDetailForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.BindingContext[adaCommunicatorDataSet1, TEXT_TABLE].EndCurrentEdit();
        }

        private void TextDetailForm_Load(object sender, EventArgs e)
        {
            DataRowView view = this.BindingContext[adaCommunicatorDataSet1, TEXT_TABLE].Current as DataRowView;
            ADACommunicatorDataSet.TextRow textRow = view.Row as ADACommunicatorDataSet.TextRow;

            if (!textRow.IsSymbolIdNull())
            {
                if (!textRow.SymbolRow.IsImageNull())
                {
                    byte[] image = textRow.SymbolRow.Image;
                    this.pictureBoxImage.Image = ImageEngine.Resize(ImageEngine.FromArray(image), this.pictureBoxImage.Size);
                }
            }

            buttonPlaySound.Enabled = (!textRow.IsSymbolIdNull() && !textRow.SymbolRow.IsSoundNull());
        }

        private void buttonChangeSymbol_Click(object sender, EventArgs e)
        {
            DataRowView view = this.BindingContext[adaCommunicatorDataSet1, TEXT_TABLE].Current as DataRowView;
            ADACommunicatorDataSet.TextRow textRow = view.Row as ADACommunicatorDataSet.TextRow;

            SymbolPicker picker = new SymbolPicker();
            SymbolDataSet.LocalizedSymbolRow symbolRow = picker.PickSymbol(this,
                textRow.IsSymbolIdNull() ? -1 : textRow.SymbolId);

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

                this.BindingContext[adaCommunicatorDataSet1, TEXT_TABLE].EndCurrentEdit();

                view.BeginEdit();
                if (textRow.IsNameNull() || textRow.Name.Length == 0)
                {
                    textRow.Name = symbolRow.Name;
                }
                if (textRow.IsDescriptonNull() || textRow.Descripton.Length == 0)
                {
                    textRow.Descripton = textRow.Name;
                }
                textRow.SymbolId = symbolRow.SymbolId;
                view.EndEdit();
            }
        }

        private void buttonPlaySound_Click(object sender, EventArgs e)
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer())
                {
                    DataRowView view = this.BindingContext[adaCommunicatorDataSet1, TEXT_TABLE].Current as DataRowView;
                    ADACommunicatorDataSet.TextRow textRow = view.Row as ADACommunicatorDataSet.TextRow;

                    using (MemoryStream ms = new MemoryStream(textRow.SymbolRow.Sound))
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