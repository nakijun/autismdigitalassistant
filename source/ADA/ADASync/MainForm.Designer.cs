namespace ADASync
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            this.rapi.Dispose();

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
            this.deviceTableAdapter1 = new ADASync.ADAUserDataSetTableAdapters.DeviceTableAdapter();
            this.userTableAdapter1 = new ADASync.ADAUserDataSetTableAdapters.UserTableAdapter();
            this.adaUserDataSet1 = new ADASync.ADAUserDataSet();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.connectStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonRefreshCurrentUser = new System.Windows.Forms.Button();
            this.buttonManageUser = new System.Windows.Forms.Button();
            this.labelCurrentUser = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonRestoreDB = new System.Windows.Forms.Button();
            this.buttonBackupDB = new System.Windows.Forms.Button();
            this.buttonRunSync = new System.Windows.Forms.Button();
            this.textBoxSyncApp = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.buttonWriteServerSetting = new System.Windows.Forms.Button();
            this.buttonReadServerSetting = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxServerSetting = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.adaUserDataSet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // deviceTableAdapter1
            // 
            this.deviceTableAdapter1.ClearBeforeFill = true;
            // 
            // userTableAdapter1
            // 
            this.userTableAdapter1.ClearBeforeFill = true;
            // 
            // adaUserDataSet1
            // 
            this.adaUserDataSet1.DataSetName = "ADAUserDataSet";
            this.adaUserDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.connectStatus);
            this.groupBox1.Location = new System.Drawing.Point(6, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(430, 42);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // connectStatus
            // 
            this.connectStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectStatus.Location = new System.Drawing.Point(3, 16);
            this.connectStatus.Name = "connectStatus";
            this.connectStatus.Size = new System.Drawing.Size(424, 23);
            this.connectStatus.TabIndex = 4;
            this.connectStatus.Text = "Not Connected";
            this.connectStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Current User:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonRefreshCurrentUser);
            this.groupBox2.Controls.Add(this.buttonManageUser);
            this.groupBox2.Controls.Add(this.labelCurrentUser);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(430, 76);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "User";
            // 
            // buttonRefreshCurrentUser
            // 
            this.buttonRefreshCurrentUser.Enabled = false;
            this.buttonRefreshCurrentUser.Location = new System.Drawing.Point(323, 11);
            this.buttonRefreshCurrentUser.Name = "buttonRefreshCurrentUser";
            this.buttonRefreshCurrentUser.Size = new System.Drawing.Size(75, 23);
            this.buttonRefreshCurrentUser.TabIndex = 13;
            this.buttonRefreshCurrentUser.Text = "Refresh";
            this.buttonRefreshCurrentUser.UseVisualStyleBackColor = true;
            this.buttonRefreshCurrentUser.Click += new System.EventHandler(this.buttonRefreshCurrentUser_Click);
            // 
            // buttonManageUser
            // 
            this.buttonManageUser.Location = new System.Drawing.Point(125, 47);
            this.buttonManageUser.Name = "buttonManageUser";
            this.buttonManageUser.Size = new System.Drawing.Size(166, 23);
            this.buttonManageUser.TabIndex = 12;
            this.buttonManageUser.Text = "Manage User";
            this.buttonManageUser.UseVisualStyleBackColor = true;
            this.buttonManageUser.Click += new System.EventHandler(this.buttonManageUser_Click);
            // 
            // labelCurrentUser
            // 
            this.labelCurrentUser.Location = new System.Drawing.Point(81, 16);
            this.labelCurrentUser.Name = "labelCurrentUser";
            this.labelCurrentUser.Size = new System.Drawing.Size(236, 13);
            this.labelCurrentUser.TabIndex = 11;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.buttonRun);
            this.groupBox7.Controls.Add(this.buttonRestoreDB);
            this.groupBox7.Controls.Add(this.buttonBackupDB);
            this.groupBox7.Controls.Add(this.buttonRunSync);
            this.groupBox7.Controls.Add(this.textBoxSyncApp);
            this.groupBox7.Location = new System.Drawing.Point(224, 132);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(212, 106);
            this.groupBox7.TabIndex = 14;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "ADA Sync PPC";
            // 
            // buttonRun
            // 
            this.buttonRun.Enabled = false;
            this.buttonRun.Location = new System.Drawing.Point(18, 50);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(88, 23);
            this.buttonRun.TabIndex = 7;
            this.buttonRun.Text = "Run";
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonRestoreDB
            // 
            this.buttonRestoreDB.Enabled = false;
            this.buttonRestoreDB.Location = new System.Drawing.Point(106, 79);
            this.buttonRestoreDB.Name = "buttonRestoreDB";
            this.buttonRestoreDB.Size = new System.Drawing.Size(88, 23);
            this.buttonRestoreDB.TabIndex = 6;
            this.buttonRestoreDB.Text = "Restore DB";
            this.buttonRestoreDB.Click += new System.EventHandler(this.buttonRestoreDB_Click);
            // 
            // buttonBackupDB
            // 
            this.buttonBackupDB.Enabled = false;
            this.buttonBackupDB.Location = new System.Drawing.Point(18, 77);
            this.buttonBackupDB.Name = "buttonBackupDB";
            this.buttonBackupDB.Size = new System.Drawing.Size(88, 23);
            this.buttonBackupDB.TabIndex = 5;
            this.buttonBackupDB.Text = "Backup DB";
            this.buttonBackupDB.Click += new System.EventHandler(this.buttonBackupDB_Click);
            // 
            // buttonRunSync
            // 
            this.buttonRunSync.Enabled = false;
            this.buttonRunSync.Location = new System.Drawing.Point(106, 50);
            this.buttonRunSync.Name = "buttonRunSync";
            this.buttonRunSync.Size = new System.Drawing.Size(88, 23);
            this.buttonRunSync.TabIndex = 4;
            this.buttonRunSync.Text = "Sync and Exit";
            this.buttonRunSync.Click += new System.EventHandler(this.buttonRunSync_Click);
            // 
            // textBoxSyncApp
            // 
            this.textBoxSyncApp.Location = new System.Drawing.Point(4, 24);
            this.textBoxSyncApp.Name = "textBoxSyncApp";
            this.textBoxSyncApp.Size = new System.Drawing.Size(202, 20);
            this.textBoxSyncApp.TabIndex = 0;
            this.textBoxSyncApp.Text = "\\Storage Card\\AdaPpc\\ADASyncPpc.exe";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.buttonWriteServerSetting);
            this.groupBox6.Controls.Add(this.buttonReadServerSetting);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.textBoxServerSetting);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.textBoxServerName);
            this.groupBox6.Location = new System.Drawing.Point(6, 130);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(212, 108);
            this.groupBox6.TabIndex = 15;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Database Server Setting";
            // 
            // buttonWriteServerSetting
            // 
            this.buttonWriteServerSetting.Enabled = false;
            this.buttonWriteServerSetting.Location = new System.Drawing.Point(108, 79);
            this.buttonWriteServerSetting.Name = "buttonWriteServerSetting";
            this.buttonWriteServerSetting.Size = new System.Drawing.Size(88, 23);
            this.buttonWriteServerSetting.TabIndex = 5;
            this.buttonWriteServerSetting.Text = "Write";
            this.buttonWriteServerSetting.UseVisualStyleBackColor = true;
            this.buttonWriteServerSetting.Click += new System.EventHandler(this.buttonWriteServerSetting_Click);
            // 
            // buttonReadServerSetting
            // 
            this.buttonReadServerSetting.Enabled = false;
            this.buttonReadServerSetting.Location = new System.Drawing.Point(20, 79);
            this.buttonReadServerSetting.Name = "buttonReadServerSetting";
            this.buttonReadServerSetting.Size = new System.Drawing.Size(88, 23);
            this.buttonReadServerSetting.TabIndex = 4;
            this.buttonReadServerSetting.Text = "Read";
            this.buttonReadServerSetting.UseVisualStyleBackColor = true;
            this.buttonReadServerSetting.Click += new System.EventHandler(this.buttonReadServerSetting_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Setting on Device:";
            // 
            // textBoxServerSetting
            // 
            this.textBoxServerSetting.Location = new System.Drawing.Point(118, 46);
            this.textBoxServerSetting.Name = "textBoxServerSetting";
            this.textBoxServerSetting.ReadOnly = true;
            this.textBoxServerSetting.Size = new System.Drawing.Size(86, 20);
            this.textBoxServerSetting.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Server Name:";
            // 
            // textBoxServerName
            // 
            this.textBoxServerName.Location = new System.Drawing.Point(118, 23);
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.ReadOnly = true;
            this.textBoxServerName.Size = new System.Drawing.Size(86, 20);
            this.textBoxServerName.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 244);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "MainForm";
            this.Text = "ADA Sync";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.adaUserDataSet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ADAUserDataSet adaUserDataSet1;
        private ADASync.ADAUserDataSetTableAdapters.DeviceTableAdapter deviceTableAdapter1;
        private ADASync.ADAUserDataSetTableAdapters.UserTableAdapter userTableAdapter1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label connectStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonRefreshCurrentUser;
        private System.Windows.Forms.Button buttonManageUser;
        private System.Windows.Forms.Label labelCurrentUser;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonRestoreDB;
        private System.Windows.Forms.Button buttonBackupDB;
        private System.Windows.Forms.Button buttonRunSync;
        private System.Windows.Forms.TextBox textBoxSyncApp;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button buttonWriteServerSetting;
        private System.Windows.Forms.Button buttonReadServerSetting;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxServerSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxServerName;
    }
}