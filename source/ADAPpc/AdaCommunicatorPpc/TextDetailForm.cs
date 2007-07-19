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
using AdaCommunicatorPpc.ADAMobileDataSetTableAdapters;

namespace AdaCommunicatorPpc
{
    public partial class TextDetailForm : Form
    {
        private ADAMobileDataSet.TextRow _textRow;
        private FliteTTS _tts;

        public ADAMobileDataSet.TextRow TextRow
        {
            get { return _textRow; }
            set { _textRow = value; }
        }

        public TextDetailForm()
        {
            InitializeComponent();
            _tts = new FliteTTS();
        }

        private void TextDetailForm_Load(object sender, EventArgs e)
        {
            if (_textRow != null)
            {
                if (!_textRow.IsSymbolIdNull())
                {
                    if (!_textRow.SymbolRow.IsImageNull())
                    {
                        Image image = ImageEngine.FromArray(_textRow.SymbolRow.Image);

                        if (image.Width <= pictureBox1.Width && image.Height <= pictureBox1.Height)
                        {
                            pictureBox1.Image = image;
                        }
                        else
                        {
                            ResizeImage(image);
                        }
                    }

                    if (!_textRow.SymbolRow.IsSoundNull())
                    {
                        menuItemPlaySound.Enabled = true;
                    }

                }

                this.Text = _textRow.Name;

                labelDescription.Text = _textRow.Descripton;

                menuItemSpeak.Enabled = (_textRow.Descripton.Length > 0);
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
            Cursor.Current = Cursors.WaitCursor;
            _tts.SayIt(_textRow.Descripton);
            Cursor.Current = Cursors.Default;
        }

        private void menuItemPlaySound_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            using (MemoryStream ms = new MemoryStream(_textRow.SymbolRow.Sound))
            {
                SoundPlayer player = new SoundPlayer(ms);
                player.PlaySync();
                player.Dispose();
            }

            Cursor.Current = Cursors.Default;
        }

    }
}