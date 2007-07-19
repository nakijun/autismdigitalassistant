namespace ADAWorkSystem
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
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.adaWorkSystemDataSet1 = new ADAWorkSystem.ADAWorkSystemDataSet();
            this.numericUpDownSequence = new System.Windows.Forms.NumericUpDown();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonPlaySound = new System.Windows.Forms.Button();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.buttonChangeSymbol = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.adaWorkSystemDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSequence)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(284, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sequence:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(284, 76);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Description:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adaWorkSystemDataSet1, "Activity.Descripton", true));
            this.textBoxDescription.Location = new System.Drawing.Point(365, 74);
            this.textBoxDescription.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(231, 22);
            this.textBoxDescription.TabIndex = 5;
            // 
            // adaWorkSystemDataSet1
            // 
            this.adaWorkSystemDataSet1.DataSetName = "ADAWorkSystemDataSet";
            this.adaWorkSystemDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // numericUpDownSequence
            // 
            this.numericUpDownSequence.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.adaWorkSystemDataSet1, "Activity.Sequence", true));
            this.numericUpDownSequence.Location = new System.Drawing.Point(365, 14);
            this.numericUpDownSequence.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownSequence.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSequence.Name = "numericUpDownSequence";
            this.numericUpDownSequence.Size = new System.Drawing.Size(231, 22);
            this.numericUpDownSequence.TabIndex = 1;
            this.numericUpDownSequence.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textBoxName
            // 
            this.textBoxName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adaWorkSystemDataSet1, "Activity.Name", true));
            this.textBoxName.Location = new System.Drawing.Point(365, 44);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(231, 22);
            this.textBoxName.TabIndex = 3;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(365, 289);
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
            this.buttonCancel.Location = new System.Drawing.Point(496, 289);
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
            this.groupBox1.Size = new System.Drawing.Size(261, 311);
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
            // ActivityDetailForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(616, 335);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numericUpDownSequence);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ActivityDetailForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Activity Detail";
            this.Load += new System.EventHandler(this.ActivityDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.adaWorkSystemDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSequence)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.NumericUpDown numericUpDownSequence;
        private System.Windows.Forms.TextBox textBoxName;
        private ADAWorkSystemDataSet adaWorkSystemDataSet1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonPlaySound;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.Button buttonChangeSymbol;
    }
}