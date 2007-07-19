namespace AdaTimerPpc
{
    partial class SettingForm
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
            this.menuItemCancel = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxAlarmEnabled = new System.Windows.Forms.CheckBox();
            this.numericUpDownAlarmPeriod = new System.Windows.Forms.NumericUpDown();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.menuItemCancel);
            // 
            // menuItemCancel
            // 
            this.menuItemCancel.Text = "Cancel";
            this.menuItemCancel.Click += new System.EventHandler(this.menuItemCancel_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(80, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 20);
            this.label1.Text = "minutes before time is up";
            // 
            // checkBoxAlarmEnabled
            // 
            this.checkBoxAlarmEnabled.Location = new System.Drawing.Point(0, 3);
            this.checkBoxAlarmEnabled.Name = "checkBoxAlarmEnabled";
            this.checkBoxAlarmEnabled.Size = new System.Drawing.Size(103, 20);
            this.checkBoxAlarmEnabled.TabIndex = 1;
            this.checkBoxAlarmEnabled.Text = "Warn me at:";
            this.checkBoxAlarmEnabled.CheckStateChanged += new System.EventHandler(this.checkBoxAlarmEnabled_CheckStateChanged);
            // 
            // numericUpDownAlarmPeriod
            // 
            this.numericUpDownAlarmPeriod.Location = new System.Drawing.Point(3, 29);
            this.numericUpDownAlarmPeriod.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownAlarmPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAlarmPeriod.Name = "numericUpDownAlarmPeriod";
            this.numericUpDownAlarmPeriod.Size = new System.Drawing.Size(52, 22);
            this.numericUpDownAlarmPeriod.TabIndex = 2;
            this.numericUpDownAlarmPeriod.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.numericUpDownAlarmPeriod);
            this.Controls.Add(this.checkBoxAlarmEnabled);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.Text = "Setting";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItemCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxAlarmEnabled;
        private System.Windows.Forms.NumericUpDown numericUpDownAlarmPeriod;
    }
}