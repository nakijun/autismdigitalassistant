namespace ADAControlCentre
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
            this.deviceTableAdapter1 = new ADAControlCentre.ADAUserDataSetTableAdapters.DeviceTableAdapter();
            this.userTableAdapter1 = new ADAControlCentre.ADAUserDataSetTableAdapters.UserTableAdapter();
            this.adaUserDataSet1 = new ADAControlCentre.ADAUserDataSet();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.connectStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonRefreshCurrentUser = new System.Windows.Forms.Button();
            this.labelCurrentUser = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.buttonWriteServerSetting = new System.Windows.Forms.Button();
            this.buttonReadServerSetting = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxServerSetting = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonAnalyzer = new System.Windows.Forms.Button();
            this.buttonWorkSystem = new System.Windows.Forms.Button();
            this.buttonSymbolManager = new System.Windows.Forms.Button();
            this.labelCurrentPDA = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.adaUserDataSet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.groupBox1.Text = "PDA Connection Status";
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
            this.label1.Text = "Current PDA:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelCurrentPDA);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.buttonRefreshCurrentUser);
            this.groupBox2.Controls.Add(this.labelCurrentUser);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(430, 67);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PDA <--> User";
            // 
            // buttonRefreshCurrentUser
            // 
            this.buttonRefreshCurrentUser.Enabled = false;
            this.buttonRefreshCurrentUser.Location = new System.Drawing.Point(349, 35);
            this.buttonRefreshCurrentUser.Name = "buttonRefreshCurrentUser";
            this.buttonRefreshCurrentUser.Size = new System.Drawing.Size(75, 23);
            this.buttonRefreshCurrentUser.TabIndex = 13;
            this.buttonRefreshCurrentUser.Text = "Refresh";
            this.buttonRefreshCurrentUser.UseVisualStyleBackColor = true;
            this.buttonRefreshCurrentUser.Click += new System.EventHandler(this.buttonRefreshCurrentUser_Click);
            // 
            // labelCurrentUser
            // 
            this.labelCurrentUser.Location = new System.Drawing.Point(81, 40);
            this.labelCurrentUser.Name = "labelCurrentUser";
            this.labelCurrentUser.Size = new System.Drawing.Size(236, 13);
            this.labelCurrentUser.TabIndex = 11;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.Controls.Add(this.buttonWriteServerSetting);
            this.groupBox6.Controls.Add(this.buttonReadServerSetting);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.textBoxServerSetting);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.textBoxServerName);
            this.groupBox6.Location = new System.Drawing.Point(6, 123);
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
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.buttonAnalyzer);
            this.groupBox3.Controls.Add(this.buttonWorkSystem);
            this.groupBox3.Controls.Add(this.buttonSymbolManager);
            this.groupBox3.Location = new System.Drawing.Point(224, 123);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(212, 106);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Applications";
            // 
            // buttonAnalyzer
            // 
            this.buttonAnalyzer.Location = new System.Drawing.Point(6, 75);
            this.buttonAnalyzer.Name = "buttonAnalyzer";
            this.buttonAnalyzer.Size = new System.Drawing.Size(200, 23);
            this.buttonAnalyzer.TabIndex = 2;
            this.buttonAnalyzer.Text = "Analyzer";
            this.buttonAnalyzer.UseVisualStyleBackColor = true;
            this.buttonAnalyzer.Click += new System.EventHandler(this.buttonAnalyzer_Click);
            // 
            // buttonWorkSystem
            // 
            this.buttonWorkSystem.Location = new System.Drawing.Point(6, 49);
            this.buttonWorkSystem.Name = "buttonWorkSystem";
            this.buttonWorkSystem.Size = new System.Drawing.Size(200, 23);
            this.buttonWorkSystem.TabIndex = 1;
            this.buttonWorkSystem.Text = "Work System";
            this.buttonWorkSystem.UseVisualStyleBackColor = true;
            this.buttonWorkSystem.Click += new System.EventHandler(this.buttonWorkSystem_Click);
            // 
            // buttonSymbolManager
            // 
            this.buttonSymbolManager.Location = new System.Drawing.Point(6, 23);
            this.buttonSymbolManager.Name = "buttonSymbolManager";
            this.buttonSymbolManager.Size = new System.Drawing.Size(200, 23);
            this.buttonSymbolManager.TabIndex = 0;
            this.buttonSymbolManager.Text = "Symbol Manager";
            this.buttonSymbolManager.UseVisualStyleBackColor = true;
            this.buttonSymbolManager.Click += new System.EventHandler(this.buttonSymbolManager_Click);
            // 
            // labelCurrentPDA
            // 
            this.labelCurrentPDA.Location = new System.Drawing.Point(81, 16);
            this.labelCurrentPDA.Name = "labelCurrentPDA";
            this.labelCurrentPDA.Size = new System.Drawing.Size(343, 13);
            this.labelCurrentPDA.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Current User:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 242);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "ADA Control Centre";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.adaUserDataSet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ADAUserDataSet adaUserDataSet1;
        private ADAControlCentre.ADAUserDataSetTableAdapters.DeviceTableAdapter deviceTableAdapter1;
        private ADAControlCentre.ADAUserDataSetTableAdapters.UserTableAdapter userTableAdapter1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label connectStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonRefreshCurrentUser;
        private System.Windows.Forms.Label labelCurrentUser;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button buttonWriteServerSetting;
        private System.Windows.Forms.Button buttonReadServerSetting;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxServerSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxServerName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonSymbolManager;
        private System.Windows.Forms.Button buttonWorkSystem;
        private System.Windows.Forms.Button buttonAnalyzer;
        private System.Windows.Forms.Label labelCurrentPDA;
        private System.Windows.Forms.Label label5;
    }
}