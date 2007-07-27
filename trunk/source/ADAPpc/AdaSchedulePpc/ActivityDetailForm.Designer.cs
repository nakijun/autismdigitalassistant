namespace AdaSchedulePpc
{
    partial class ActivityDetailForm
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
            this.menuItemStart = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemPlaySound = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemNowThen = new System.Windows.Forms.MenuItem();
            this.menuItemTimer = new System.Windows.Forms.MenuItem();
            this.menuItemWorkSystem = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemStart);
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItemStart
            // 
            this.menuItemStart.Enabled = false;
            this.menuItemStart.Text = "Start";
            this.menuItemStart.Click += new System.EventHandler(this.menuItemStart_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemPlaySound);
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.menuItemTimer);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItemWorkSystem);
            this.menuItem1.MenuItems.Add(this.menuItemNowThen);
            this.menuItem1.Text = "Menu";
            // 
            // menuItemPlaySound
            // 
            this.menuItemPlaySound.Enabled = false;
            this.menuItemPlaySound.Text = "Play Sound";
            this.menuItemPlaySound.Click += new System.EventHandler(this.menuItemPlaySound_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // menuItemNowThen
            // 
            this.menuItemNowThen.Enabled = false;
            this.menuItemNowThen.Text = "Now/Then";
            // 
            // menuItemTimer
            // 
            this.menuItemTimer.Enabled = false;
            this.menuItemTimer.Text = "Timer";
            // 
            // menuItemWorkSystem
            // 
            this.menuItemWorkSystem.Enabled = false;
            this.menuItemWorkSystem.Text = "Work System";
            // 
            // menuItem3
            // 
            this.menuItem3.Enabled = false;
            this.menuItem3.Text = "Reminder";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 246);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // labelDescription
            // 
            this.labelDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelDescription.Location = new System.Drawing.Point(0, 248);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(240, 20);
            this.labelDescription.Text = "10:30-12:00";
            // 
            // ActivityDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "ActivityDetailForm";
            this.Text = "Activity Details";
            this.Load += new System.EventHandler(this.ActivityDetailForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItemStart;
        private System.Windows.Forms.MenuItem menuItemPlaySound;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItemNowThen;
        private System.Windows.Forms.MenuItem menuItemTimer;
        private System.Windows.Forms.MenuItem menuItemWorkSystem;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.Label labelDescription;
    }
}