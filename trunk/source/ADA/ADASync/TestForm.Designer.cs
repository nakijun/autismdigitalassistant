namespace ADASync
{
    partial class TestForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.btnProfileStats = new System.Windows.Forms.Button();
            this.btnProfileDisable = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnProfileEnable = new System.Windows.Forms.Button();
            this.txtProfilePath = new System.Windows.Forms.TextBox();
            this.readShortcut = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.status = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.createShortcut = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.copyFrom = new System.Windows.Forms.Button();
            this.copyTo = new System.Windows.Forms.Button();
            this.connectStatus = new System.Windows.Forms.Label();
            this.connectSync = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.disconnect = new System.Windows.Forms.Button();
            this.connectAsync = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.buttonWriteServerSetting = new System.Windows.Forms.Button();
            this.buttonReadServerSetting = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxServerSetting = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonRestoreDB = new System.Windows.Forms.Button();
            this.buttonBackupDB = new System.Windows.Forms.Button();
            this.buttonRunSync = new System.Windows.Forms.Button();
            this.textBoxSyncApp = new System.Windows.Forms.TextBox();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Name:";
            // 
            // textBoxServerName
            // 
            this.textBoxServerName.Location = new System.Drawing.Point(118, 23);
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.ReadOnly = true;
            this.textBoxServerName.Size = new System.Drawing.Size(86, 20);
            this.textBoxServerName.TabIndex = 1;
            // 
            // btnProfileStats
            // 
            this.btnProfileStats.Location = new System.Drawing.Point(140, 40);
            this.btnProfileStats.Name = "btnProfileStats";
            this.btnProfileStats.Size = new System.Drawing.Size(68, 20);
            this.btnProfileStats.TabIndex = 3;
            this.btnProfileStats.Text = "get stats";
            this.btnProfileStats.Click += new System.EventHandler(this.btnProfileStats_Click);
            // 
            // btnProfileDisable
            // 
            this.btnProfileDisable.Location = new System.Drawing.Point(72, 40);
            this.btnProfileDisable.Name = "btnProfileDisable";
            this.btnProfileDisable.Size = new System.Drawing.Size(68, 20);
            this.btnProfileDisable.TabIndex = 2;
            this.btnProfileDisable.Text = "disable";
            this.btnProfileDisable.Click += new System.EventHandler(this.btnProfileDisable_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(172, 16);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(32, 20);
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "run";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnRun);
            this.groupBox4.Controls.Add(this.btnProfileStats);
            this.groupBox4.Controls.Add(this.btnProfileDisable);
            this.groupBox4.Controls.Add(this.btnProfileEnable);
            this.groupBox4.Controls.Add(this.txtProfilePath);
            this.groupBox4.Location = new System.Drawing.Point(228, 56);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(212, 64);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Profile App";
            // 
            // btnProfileEnable
            // 
            this.btnProfileEnable.Location = new System.Drawing.Point(4, 40);
            this.btnProfileEnable.Name = "btnProfileEnable";
            this.btnProfileEnable.Size = new System.Drawing.Size(68, 20);
            this.btnProfileEnable.TabIndex = 1;
            this.btnProfileEnable.Text = "enable";
            this.btnProfileEnable.Click += new System.EventHandler(this.btnProfileEnable_Click);
            // 
            // txtProfilePath
            // 
            this.txtProfilePath.Location = new System.Drawing.Point(4, 16);
            this.txtProfilePath.Name = "txtProfilePath";
            this.txtProfilePath.Size = new System.Drawing.Size(164, 20);
            this.txtProfilePath.TabIndex = 0;
            // 
            // readShortcut
            // 
            this.readShortcut.Location = new System.Drawing.Point(108, 20);
            this.readShortcut.Name = "readShortcut";
            this.readShortcut.Size = new System.Drawing.Size(88, 28);
            this.readShortcut.TabIndex = 5;
            this.readShortcut.Text = "Read Test";
            this.readShortcut.Click += new System.EventHandler(this.readShortcut_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.status);
            this.groupBox3.Location = new System.Drawing.Point(228, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(212, 40);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ActiveSync Status";
            // 
            // status
            // 
            this.status.Location = new System.Drawing.Point(24, 20);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(168, 16);
            this.status.TabIndex = 0;
            this.status.Text = "unknown";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.readShortcut);
            this.groupBox5.Controls.Add(this.createShortcut);
            this.groupBox5.Location = new System.Drawing.Point(228, 124);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(212, 56);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Shortcut";
            // 
            // createShortcut
            // 
            this.createShortcut.Location = new System.Drawing.Point(20, 20);
            this.createShortcut.Name = "createShortcut";
            this.createShortcut.Size = new System.Drawing.Size(88, 28);
            this.createShortcut.TabIndex = 4;
            this.createShortcut.Text = "Create Test";
            this.createShortcut.Click += new System.EventHandler(this.createShortcut_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.copyFrom);
            this.groupBox2.Controls.Add(this.copyTo);
            this.groupBox2.Location = new System.Drawing.Point(12, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 56);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Copy File";
            // 
            // copyFrom
            // 
            this.copyFrom.Location = new System.Drawing.Point(108, 20);
            this.copyFrom.Name = "copyFrom";
            this.copyFrom.Size = new System.Drawing.Size(88, 28);
            this.copyFrom.TabIndex = 5;
            this.copyFrom.Text = "From Device";
            this.copyFrom.Click += new System.EventHandler(this.copyFrom_Click);
            // 
            // copyTo
            // 
            this.copyTo.Location = new System.Drawing.Point(20, 20);
            this.copyTo.Name = "copyTo";
            this.copyTo.Size = new System.Drawing.Size(88, 28);
            this.copyTo.TabIndex = 4;
            this.copyTo.Text = "To Device";
            this.copyTo.Click += new System.EventHandler(this.copyTo_Click);
            // 
            // connectStatus
            // 
            this.connectStatus.Location = new System.Drawing.Point(12, 84);
            this.connectStatus.Name = "connectStatus";
            this.connectStatus.Size = new System.Drawing.Size(192, 20);
            this.connectStatus.TabIndex = 4;
            this.connectStatus.Text = "Not Connected";
            this.connectStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // connectSync
            // 
            this.connectSync.Location = new System.Drawing.Point(108, 20);
            this.connectSync.Name = "connectSync";
            this.connectSync.Size = new System.Drawing.Size(96, 28);
            this.connectSync.TabIndex = 3;
            this.connectSync.Text = "Synchronous";
            this.connectSync.Click += new System.EventHandler(this.connectSync_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.disconnect);
            this.groupBox1.Controls.Add(this.connectStatus);
            this.groupBox1.Controls.Add(this.connectSync);
            this.groupBox1.Controls.Add(this.connectAsync);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 108);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect";
            // 
            // disconnect
            // 
            this.disconnect.Location = new System.Drawing.Point(12, 52);
            this.disconnect.Name = "disconnect";
            this.disconnect.Size = new System.Drawing.Size(192, 24);
            this.disconnect.TabIndex = 5;
            this.disconnect.Text = "Disconnect";
            this.disconnect.Click += new System.EventHandler(this.disconnect_Click);
            // 
            // connectAsync
            // 
            this.connectAsync.Location = new System.Drawing.Point(12, 20);
            this.connectAsync.Name = "connectAsync";
            this.connectAsync.Size = new System.Drawing.Size(96, 28);
            this.connectAsync.TabIndex = 2;
            this.connectAsync.Text = "Asynchronous";
            this.connectAsync.Click += new System.EventHandler(this.connectAsync_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.buttonWriteServerSetting);
            this.groupBox6.Controls.Add(this.buttonReadServerSetting);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.textBoxServerSetting);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.textBoxServerName);
            this.groupBox6.Location = new System.Drawing.Point(12, 186);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(212, 108);
            this.groupBox6.TabIndex = 13;
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
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.buttonRun);
            this.groupBox7.Controls.Add(this.buttonRestoreDB);
            this.groupBox7.Controls.Add(this.buttonBackupDB);
            this.groupBox7.Controls.Add(this.buttonRunSync);
            this.groupBox7.Controls.Add(this.textBoxSyncApp);
            this.groupBox7.Location = new System.Drawing.Point(230, 188);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(212, 106);
            this.groupBox7.TabIndex = 12;
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
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 316);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "TestForm";
            this.Text = "ADA Sync";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxServerName;
        private System.Windows.Forms.Button btnProfileStats;
        private System.Windows.Forms.Button btnProfileDisable;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnProfileEnable;
        private System.Windows.Forms.TextBox txtProfilePath;
        private System.Windows.Forms.Button readShortcut;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button createShortcut;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button copyFrom;
        private System.Windows.Forms.Button copyTo;
        private System.Windows.Forms.Label connectStatus;
        private System.Windows.Forms.Button connectSync;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button disconnect;
        private System.Windows.Forms.Button connectAsync;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button buttonReadServerSetting;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxServerSetting;
        private System.Windows.Forms.Button buttonWriteServerSetting;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button buttonRunSync;
        private System.Windows.Forms.TextBox textBoxSyncApp;
        private System.Windows.Forms.Button buttonBackupDB;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonRestoreDB;
    }
}

