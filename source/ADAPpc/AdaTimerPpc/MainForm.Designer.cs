namespace AdaTimerPpc
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu;

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
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuItemStartStop = new System.Windows.Forms.MenuItem();
            this.menuItemMenu = new System.Windows.Forms.MenuItem();
            this.menuItemSetting = new System.Windows.Forms.MenuItem();
            this.menuItemAlarm = new System.Windows.Forms.MenuItem();
            this.timer = new System.Windows.Forms.Timer();
            this.panelTimer = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownMinutes = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCountDown = new System.Windows.Forms.Label();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.panelTimer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.menuItemStartStop);
            this.mainMenu.MenuItems.Add(this.menuItemMenu);
            // 
            // menuItemStartStop
            // 
            this.menuItemStartStop.Text = "Start";
            this.menuItemStartStop.Click += new System.EventHandler(this.menuItemStartStop_Click);
            // 
            // menuItemMenu
            // 
            this.menuItemMenu.MenuItems.Add(this.menuItemSetting);
            this.menuItemMenu.MenuItems.Add(this.menuItemAlarm);
            this.menuItemMenu.MenuItems.Add(this.menuItem4);
            this.menuItemMenu.MenuItems.Add(this.menuItemExit);
            this.menuItemMenu.Text = "Menu";
            // 
            // menuItemSetting
            // 
            this.menuItemSetting.Text = "Setting";
            this.menuItemSetting.Click += new System.EventHandler(this.menuItemSetting_Click);
            // 
            // menuItemAlarm
            // 
            this.menuItemAlarm.Text = "Alarm";
            this.menuItemAlarm.Click += new System.EventHandler(this.menuItemAlarm_Click);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // panelTimer
            // 
            this.panelTimer.Controls.Add(this.label13);
            this.panelTimer.Controls.Add(this.label12);
            this.panelTimer.Controls.Add(this.label11);
            this.panelTimer.Controls.Add(this.label10);
            this.panelTimer.Controls.Add(this.label9);
            this.panelTimer.Controls.Add(this.label8);
            this.panelTimer.Controls.Add(this.label7);
            this.panelTimer.Controls.Add(this.label6);
            this.panelTimer.Controls.Add(this.label2);
            this.panelTimer.Controls.Add(this.label5);
            this.panelTimer.Controls.Add(this.label4);
            this.panelTimer.Controls.Add(this.label3);
            this.panelTimer.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTimer.Location = new System.Drawing.Point(0, 0);
            this.panelTimer.Name = "panelTimer";
            this.panelTimer.Size = new System.Drawing.Size(240, 240);
            this.panelTimer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTimer_MouseDown);
            this.panelTimer.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTimer_Paint);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(169, 15);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(18, 16);
            this.label13.Text = "55";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(208, 57);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 16);
            this.label12.Text = "50";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(211, 163);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 16);
            this.label11.Text = "40";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(171, 211);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 16);
            this.label10.Text = "35";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(52, 211);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 16);
            this.label9.Text = "25";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(10, 171);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 16);
            this.label8.Text = "20";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(55, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 16);
            this.label7.Text = "5";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(17, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 16);
            this.label6.Text = "10";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.Location = new System.Drawing.Point(219, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 15);
            this.label2.Text = "45";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.Location = new System.Drawing.Point(0, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(240, 15);
            this.label5.Text = "0";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(0, 220);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(240, 20);
            this.label4.Text = "30";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(0, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 15);
            this.label3.Text = "15";
            // 
            // numericUpDownMinutes
            // 
            this.numericUpDownMinutes.Location = new System.Drawing.Point(3, 243);
            this.numericUpDownMinutes.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDownMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinutes.Name = "numericUpDownMinutes";
            this.numericUpDownMinutes.Size = new System.Drawing.Size(54, 22);
            this.numericUpDownMinutes.TabIndex = 3;
            this.numericUpDownMinutes.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownMinutes.ValueChanged += new System.EventHandler(this.numericUpDownMinutes_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(63, 247);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 18);
            this.label1.Text = "Minutes";
            // 
            // labelCountDown
            // 
            this.labelCountDown.Location = new System.Drawing.Point(146, 247);
            this.labelCountDown.Name = "labelCountDown";
            this.labelCountDown.Size = new System.Drawing.Size(91, 20);
            this.labelCountDown.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "-";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.labelCountDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownMinutes);
            this.Controls.Add(this.panelTimer);
            this.Menu = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "ADA Timer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.panelTimer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItemStartStop;
        private System.Windows.Forms.MenuItem menuItemMenu;
        private System.Windows.Forms.MenuItem menuItemSetting;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Panel panelTimer;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCountDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MenuItem menuItemAlarm;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItemExit;
    }
}