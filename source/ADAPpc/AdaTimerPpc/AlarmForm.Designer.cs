namespace AdaTimerPpc
{
    partial class AlarmForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuItemSetAlarm = new System.Windows.Forms.MenuItem();
            this.menuItemRemoveAlarm = new System.Windows.Forms.MenuItem();
            this.dateTimePickerAlarmTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerAlarmDate = new System.Windows.Forms.DateTimePicker();
            this.listViewFromNow = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.menuItemSetAlarm);
            this.mainMenu.MenuItems.Add(this.menuItemRemoveAlarm);
            // 
            // menuItemSetAlarm
            // 
            this.menuItemSetAlarm.Text = "Set";
            this.menuItemSetAlarm.Click += new System.EventHandler(this.menuItemSetAlarm_Click);
            // 
            // menuItemRemoveAlarm
            // 
            this.menuItemRemoveAlarm.Text = "Remove";
            this.menuItemRemoveAlarm.Click += new System.EventHandler(this.menuItemRemoveAlarm_Click);
            // 
            // dateTimePickerAlarmTime
            // 
            this.dateTimePickerAlarmTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerAlarmTime.Location = new System.Drawing.Point(136, 243);
            this.dateTimePickerAlarmTime.Name = "dateTimePickerAlarmTime";
            this.dateTimePickerAlarmTime.ShowUpDown = true;
            this.dateTimePickerAlarmTime.Size = new System.Drawing.Size(101, 22);
            this.dateTimePickerAlarmTime.TabIndex = 2;
            // 
            // dateTimePickerAlarmDate
            // 
            this.dateTimePickerAlarmDate.Location = new System.Drawing.Point(3, 243);
            this.dateTimePickerAlarmDate.Name = "dateTimePickerAlarmDate";
            this.dateTimePickerAlarmDate.Size = new System.Drawing.Size(127, 22);
            this.dateTimePickerAlarmDate.TabIndex = 1;
            // 
            // listViewFromNow
            // 
            this.listViewFromNow.CheckBoxes = true;
            this.listViewFromNow.Columns.Add(this.columnHeader1);
            this.listViewFromNow.Columns.Add(this.columnHeader2);
            this.listViewFromNow.FullRowSelect = true;
            this.listViewFromNow.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem1.Tag = "0";
            listViewItem1.Text = "Other";
            listViewItem2.Tag = "15";
            listViewItem2.Text = "15 Minnutes";
            listViewItem2.SubItems.Add("From Now");
            listViewItem3.Tag = "30";
            listViewItem3.Text = "30 Minutes";
            listViewItem3.SubItems.Add("From Now");
            listViewItem4.Tag = "45";
            listViewItem4.Text = "45 Minutes";
            listViewItem4.SubItems.Add("From Now");
            listViewItem5.Tag = "60";
            listViewItem5.Text = "1 Hour";
            listViewItem5.SubItems.Add("From Now");
            listViewItem6.Tag = "120";
            listViewItem6.Text = "2 Hour";
            listViewItem6.SubItems.Add("From Now");
            this.listViewFromNow.Items.Add(listViewItem1);
            this.listViewFromNow.Items.Add(listViewItem2);
            this.listViewFromNow.Items.Add(listViewItem3);
            this.listViewFromNow.Items.Add(listViewItem4);
            this.listViewFromNow.Items.Add(listViewItem5);
            this.listViewFromNow.Items.Add(listViewItem6);
            this.listViewFromNow.Location = new System.Drawing.Point(3, 3);
            this.listViewFromNow.Name = "listViewFromNow";
            this.listViewFromNow.Size = new System.Drawing.Size(234, 234);
            this.listViewFromNow.TabIndex = 0;
            this.listViewFromNow.View = System.Windows.Forms.View.Details;
            this.listViewFromNow.SelectedIndexChanged += new System.EventHandler(this.listViewFromNow_SelectedIndexChanged);
            this.listViewFromNow.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewFromNow_ItemCheck);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Type";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 80;
            // 
            // AlarmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.listViewFromNow);
            this.Controls.Add(this.dateTimePickerAlarmDate);
            this.Controls.Add(this.dateTimePickerAlarmTime);
            this.Menu = this.mainMenu;
            this.Name = "AlarmForm";
            this.Text = "Alarm";
            this.Load += new System.EventHandler(this.AlarmForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.AlarmForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerAlarmTime;
        private System.Windows.Forms.MenuItem menuItemRemoveAlarm;
        private System.Windows.Forms.DateTimePicker dateTimePickerAlarmDate;
        private System.Windows.Forms.ListView listViewFromNow;
        private System.Windows.Forms.MenuItem menuItemSetAlarm;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}