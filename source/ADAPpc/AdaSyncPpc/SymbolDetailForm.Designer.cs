namespace AdaSyncPpc
{
    partial class SymbolDetailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItemDone = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemPlay = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem200Percent = new System.Windows.Forms.MenuItem();
            this.menuItem150Percent = new System.Windows.Forms.MenuItem();
            this.menuItem100Percent = new System.Windows.Forms.MenuItem();
            this.menuItem75Percent = new System.Windows.Forms.MenuItem();
            this.pictureBoxSymbol = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.textBoxCategory = new OpenNETCF.Windows.Forms.TextBox2();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemDone);
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItemDone
            // 
            this.menuItemDone.Text = "Done";
            this.menuItemDone.Click += new System.EventHandler(this.menuItemDone_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemPlay);
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.menuItem200Percent);
            this.menuItem1.MenuItems.Add(this.menuItem150Percent);
            this.menuItem1.MenuItems.Add(this.menuItem100Percent);
            this.menuItem1.MenuItems.Add(this.menuItem75Percent);
            this.menuItem1.Text = "Menu";
            // 
            // menuItemPlay
            // 
            this.menuItemPlay.Enabled = false;
            this.menuItemPlay.Text = "Play";
            this.menuItemPlay.Click += new System.EventHandler(this.menuItemPlay_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // menuItem200Percent
            // 
            this.menuItem200Percent.Text = "200%";
            this.menuItem200Percent.Click += new System.EventHandler(this.menuItemZoomPercent_Click);
            // 
            // menuItem150Percent
            // 
            this.menuItem150Percent.Text = "150%";
            this.menuItem150Percent.Click += new System.EventHandler(this.menuItemZoomPercent_Click);
            // 
            // menuItem100Percent
            // 
            this.menuItem100Percent.Text = "100%";
            this.menuItem100Percent.Click += new System.EventHandler(this.menuItemZoomPercent_Click);
            // 
            // menuItem75Percent
            // 
            this.menuItem75Percent.Text = "75%";
            this.menuItem75Percent.Click += new System.EventHandler(this.menuItemZoomPercent_Click);
            // 
            // pictureBoxSymbol
            // 
            this.pictureBoxSymbol.Location = new System.Drawing.Point(20, 3);
            this.pictureBoxSymbol.Name = "pictureBoxSymbol";
            this.pictureBoxSymbol.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxSymbol.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.Text = "Text:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.Text = "Category:";
            // 
            // textBoxText
            // 
            this.textBoxText.Location = new System.Drawing.Point(73, 212);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.ReadOnly = true;
            this.textBoxText.Size = new System.Drawing.Size(164, 21);
            this.textBoxText.TabIndex = 3;
            // 
            // textBoxCategory
            // 
            this.textBoxCategory.Location = new System.Drawing.Point(73, 239);
            this.textBoxCategory.Name = "textBoxCategory";
            this.textBoxCategory.ReadOnly = true;
            this.textBoxCategory.Size = new System.Drawing.Size(164, 20);
            this.textBoxCategory.TabIndex = 4;
            // 
            // SymbolDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.textBoxCategory);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxSymbol);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "SymbolDetailForm";
            this.Text = "Symbol Details";
            this.Load += new System.EventHandler(this.SymbolDetailForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItemDone;
        private System.Windows.Forms.PictureBox pictureBoxSymbol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxText;
        private OpenNETCF.Windows.Forms.TextBox2 textBoxCategory;
        private System.Windows.Forms.MenuItem menuItemPlay;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem200Percent;
        private System.Windows.Forms.MenuItem menuItem150Percent;
        private System.Windows.Forms.MenuItem menuItem100Percent;
        private System.Windows.Forms.MenuItem menuItem75Percent;
    }
}