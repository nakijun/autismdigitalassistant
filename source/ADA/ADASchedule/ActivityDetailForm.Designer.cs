namespace ADASchedule
{
    partial class ActivityDetailForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.adaScheduleDataSet1 = new ADASchedule.ADAScheduleDataSet();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.numericUpDownSequence = new System.Windows.Forms.NumericUpDown();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
            this.numericUpDownScore = new System.Windows.Forms.NumericUpDown();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonPlaySound = new System.Windows.Forms.Button();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.buttonChangeSymbol = new System.Windows.Forms.Button();
            this.dateTimePickerAlarm = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.adaScheduleDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSequence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScore)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(289, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sequence:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 47);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(289, 77);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Description:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(290, 107);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Start Time:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(289, 137);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "End Time:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(290, 196);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Score:";
            // 
            // dateTimePickerEndTime
            // 
            this.dateTimePickerEndTime.CustomFormat = "HH:mm";
            this.dateTimePickerEndTime.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.adaScheduleDataSet1, "Activity.EndTime", true));
            this.dateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEndTime.Location = new System.Drawing.Point(370, 134);
            this.dateTimePickerEndTime.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            this.dateTimePickerEndTime.ShowUpDown = true;
            this.dateTimePickerEndTime.Size = new System.Drawing.Size(276, 22);
            this.dateTimePickerEndTime.TabIndex = 9;
            this.dateTimePickerEndTime.ValueChanged += new System.EventHandler(this.dateTimePickerEndTime_ValueChanged);
            // 
            // adaScheduleDataSet1
            // 
            this.adaScheduleDataSet1.DataSetName = "ADAScheduleDataSet";
            this.adaScheduleDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adaScheduleDataSet1, "Activity.Descripton", true));
            this.textBoxDescription.Location = new System.Drawing.Point(370, 74);
            this.textBoxDescription.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(276, 22);
            this.textBoxDescription.TabIndex = 5;
            // 
            // numericUpDownSequence
            // 
            this.numericUpDownSequence.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.adaScheduleDataSet1, "Activity.Sequence", true));
            this.numericUpDownSequence.Location = new System.Drawing.Point(370, 14);
            this.numericUpDownSequence.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownSequence.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSequence.Name = "numericUpDownSequence";
            this.numericUpDownSequence.Size = new System.Drawing.Size(276, 22);
            this.numericUpDownSequence.TabIndex = 1;
            this.numericUpDownSequence.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textBoxName
            // 
            this.textBoxName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adaScheduleDataSet1, "Activity.Name", true));
            this.textBoxName.Location = new System.Drawing.Point(370, 44);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(276, 22);
            this.textBoxName.TabIndex = 3;
            // 
            // dateTimePickerStartTime
            // 
            this.dateTimePickerStartTime.CustomFormat = "HH:mm";
            this.dateTimePickerStartTime.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.adaScheduleDataSet1, "Activity.StartTime", true));
            this.dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStartTime.Location = new System.Drawing.Point(370, 104);
            this.dateTimePickerStartTime.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            this.dateTimePickerStartTime.ShowUpDown = true;
            this.dateTimePickerStartTime.Size = new System.Drawing.Size(276, 22);
            this.dateTimePickerStartTime.TabIndex = 7;
            // 
            // numericUpDownScore
            // 
            this.numericUpDownScore.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.adaScheduleDataSet1, "Activity.Score", true));
            this.numericUpDownScore.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownScore.Location = new System.Drawing.Point(370, 194);
            this.numericUpDownScore.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownScore.Name = "numericUpDownScore";
            this.numericUpDownScore.Size = new System.Drawing.Size(276, 22);
            this.numericUpDownScore.TabIndex = 11;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(380, 289);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 28);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(488, 289);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 28);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonPlaySound);
            this.groupBox1.Controls.Add(this.pictureBoxImage);
            this.groupBox1.Controls.Add(this.buttonChangeSymbol);
            this.groupBox1.Location = new System.Drawing.Point(14, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(262, 314);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Symbol";
            // 
            // buttonPlaySound
            // 
            this.buttonPlaySound.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonPlaySound.Location = new System.Drawing.Point(150, 275);
            this.buttonPlaySound.Margin = new System.Windows.Forms.Padding(5);
            this.buttonPlaySound.Name = "buttonPlaySound";
            this.buttonPlaySound.Size = new System.Drawing.Size(100, 28);
            this.buttonPlaySound.TabIndex = 17;
            this.buttonPlaySound.Text = "Play Sound";
            this.buttonPlaySound.UseVisualStyleBackColor = true;
            this.buttonPlaySound.Click += new System.EventHandler(this.buttonPlaySound_Click);
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBoxImage.Location = new System.Drawing.Point(10, 25);
            this.pictureBoxImage.Margin = new System.Windows.Forms.Padding(5);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(240, 240);
            this.pictureBoxImage.TabIndex = 7;
            this.pictureBoxImage.TabStop = false;
            // 
            // buttonChangeSymbol
            // 
            this.buttonChangeSymbol.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonChangeSymbol.Location = new System.Drawing.Point(10, 275);
            this.buttonChangeSymbol.Margin = new System.Windows.Forms.Padding(5);
            this.buttonChangeSymbol.Name = "buttonChangeSymbol";
            this.buttonChangeSymbol.Size = new System.Drawing.Size(100, 28);
            this.buttonChangeSymbol.TabIndex = 16;
            this.buttonChangeSymbol.Text = "Change";
            this.buttonChangeSymbol.UseVisualStyleBackColor = true;
            this.buttonChangeSymbol.Click += new System.EventHandler(this.buttonChangeSymbol_Click);
            // 
            // dateTimePickerAlarm
            // 
            this.dateTimePickerAlarm.CustomFormat = "HH:mm";
            this.dateTimePickerAlarm.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerAlarm.Location = new System.Drawing.Point(370, 164);
            this.dateTimePickerAlarm.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePickerAlarm.Name = "dateTimePickerAlarm";
            this.dateTimePickerAlarm.ShowUpDown = true;
            this.dateTimePickerAlarm.Size = new System.Drawing.Size(276, 22);
            this.dateTimePickerAlarm.TabIndex = 23;
            this.dateTimePickerAlarm.ValueChanged += new System.EventHandler(this.dateTimePickerAlarm_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(289, 167);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 22;
            this.label7.Text = "Alarm Time:";
            // 
            // ActivityDetailForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(659, 345);
            this.ControlBox = false;
            this.Controls.Add(this.dateTimePickerAlarm);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.dateTimePickerEndTime);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownSequence);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.dateTimePickerStartTime);
            this.Controls.Add(this.numericUpDownScore);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ActivityDetailForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Activity Detail";
            this.Load += new System.EventHandler(this.ActivityDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.adaScheduleDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSequence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScore)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.NumericUpDown numericUpDownSequence;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime;
        private ADAScheduleDataSet adaScheduleDataSet1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.NumericUpDown numericUpDownScore;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonPlaySound;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.Button buttonChangeSymbol;
        private System.Windows.Forms.DateTimePicker dateTimePickerAlarm;
        private System.Windows.Forms.Label label7;
    }
}