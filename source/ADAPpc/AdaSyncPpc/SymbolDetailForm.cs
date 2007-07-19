using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.Media;
using System.IO;
using System.Drawing.Imaging;
using UtilitiesPpc;

namespace AdaSyncPpc
{
    public partial class SymbolDetailForm : AdaBaseForm
    {
        private SymbolInfo symbol;

        public SymbolInfo Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public SymbolDetailForm()
        {
            InitializeComponent();
        }

        private void menuItemDone_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void SymbolDetailForm_Load(object sender, EventArgs e)
        {
            if (this.symbol != null)
            {
                this.pictureBoxSymbol.Image = this.symbol.Image;
                this.textBoxText.Text = this.symbol.Text;
                this.textBoxCategory.Text = this.symbol.Category.Text;
                this.menuItem100Percent.Checked = true;

                if (this.symbol.Sound != null)
                {
                    this.menuItemPlay.Enabled = true;
                }
            }
        }

        private void menuItemPlay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            using (MemoryStream ms = new MemoryStream(this.symbol.Sound))
            {
                SoundPlayer player = new SoundPlayer(ms);
                player.PlaySync();
                player.Dispose();
            }

            Cursor.Current = Cursors.Default;
        }

        private void menuItemZoomPercent_Click(object sender, EventArgs e)
        {
            if (sender == this.menuItem100Percent)
            {
                this.pictureBoxSymbol.Image = this.symbol.Image;
                this.menuItem75Percent.Checked = false;
                this.menuItem100Percent.Checked = true;
                this.menuItem150Percent.Checked = false;
                this.menuItem200Percent.Checked = false;
            }
            else
            {
                Size size;

                if (sender == this.menuItem75Percent)
                {
                    size = new Size(75, 75);
                    this.menuItem75Percent.Checked = true;
                    this.menuItem100Percent.Checked = false;
                    this.menuItem150Percent.Checked = false;
                    this.menuItem200Percent.Checked = false;
                }
                else if (sender == this.menuItem150Percent)
                {
                    size = new Size(150, 150);
                    this.menuItem75Percent.Checked = false;
                    this.menuItem100Percent.Checked = false;
                    this.menuItem150Percent.Checked = true;
                    this.menuItem200Percent.Checked = false;
                }
                else
                {
                    size = new Size(200, 200);
                    this.menuItem75Percent.Checked = false;
                    this.menuItem100Percent.Checked = false;
                    this.menuItem150Percent.Checked = false;
                    this.menuItem200Percent.Checked = true;
                }

                this.pictureBoxSymbol.Image = ResizeImage(this.symbol.Image, size);
            }
        }

        private Image ResizeImage(Image image, Size size)
        {
            //Create a new Bitmap object
            Image canvas = new Bitmap(size.Width, size.Height, PixelFormat.Format16bppRgb555);

            //create an object that will do the drawing operations
            Graphics artist = Graphics.FromImage(canvas);

            // Create rectangle for displaying image.
            Rectangle destRect = new Rectangle(0, 0, size.Width, size.Height);

            // Create rectangle for source image.
            Rectangle srcRect = new Rectangle(0, 0, image.Width, image.Height);

            // Draw image to Graphics.
            artist.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);

            //now the drawing is done, we can discard the artist object
            artist.Dispose();

            //return the picture
            return canvas;
        }
    }
}