namespace SymbolManager
{
    partial class CategoryDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CategoryDetailForm));
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.categoryDetailDataSet = new SymbolManager.CategoryDetailDataSet();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonImportImage = new System.Windows.Forms.Button();
            this.buttonPlaySound = new System.Windows.Forms.Button();
            this.buttonImportSound = new System.Windows.Forms.Button();
            this.dataGridViewName = new System.Windows.Forms.DataGridView();
            this.languageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonPasteImage = new System.Windows.Forms.Button();
            this.buttonCopyImage = new System.Windows.Forms.Button();
            this.buttonExportImage = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonDeleteSound = new System.Windows.Forms.Button();
            this.buttonExportSound = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryDetailDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewName)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.DataBindings.Add(new System.Windows.Forms.Binding("Image", this.categoryDetailDataSet, "Category.Image", true));
            resources.ApplyResources(this.pictureBoxImage, "pictureBoxImage");
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = global::SymbolManager.Properties.Settings.Default.ListImageSize;
            this.pictureBoxImage.TabStop = false;
            // 
            // categoryDetailDataSet
            // 
            this.categoryDetailDataSet.DataSetName = "CategoryDetailDataSet";
            this.categoryDetailDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonImportImage
            // 
            resources.ApplyResources(this.buttonImportImage, "buttonImportImage");
            this.buttonImportImage.Name = "buttonImportImage";
            this.buttonImportImage.UseVisualStyleBackColor = true;
            this.buttonImportImage.Click += new System.EventHandler(this.buttonImportImage_Click);
            // 
            // buttonPlaySound
            // 
            resources.ApplyResources(this.buttonPlaySound, "buttonPlaySound");
            this.buttonPlaySound.Name = "buttonPlaySound";
            this.buttonPlaySound.UseVisualStyleBackColor = true;
            this.buttonPlaySound.Click += new System.EventHandler(this.buttonPlaySound_Click);
            // 
            // buttonImportSound
            // 
            resources.ApplyResources(this.buttonImportSound, "buttonImportSound");
            this.buttonImportSound.Name = "buttonImportSound";
            this.buttonImportSound.UseVisualStyleBackColor = true;
            this.buttonImportSound.Click += new System.EventHandler(this.buttonImportSound_Click);
            // 
            // dataGridViewName
            // 
            this.dataGridViewName.AllowUserToAddRows = false;
            this.dataGridViewName.AllowUserToDeleteRows = false;
            this.dataGridViewName.AutoGenerateColumns = false;
            this.dataGridViewName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewName.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.languageDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn});
            this.dataGridViewName.DataMember = "LocalizedCategoryName";
            this.dataGridViewName.DataSource = this.categoryDetailDataSet;
            resources.ApplyResources(this.dataGridViewName, "dataGridViewName");
            this.dataGridViewName.MultiSelect = false;
            this.dataGridViewName.Name = "dataGridViewName";
            this.dataGridViewName.RowTemplate.Height = 24;
            // 
            // languageDataGridViewTextBoxColumn
            // 
            this.languageDataGridViewTextBoxColumn.DataPropertyName = "Language";
            resources.ApplyResources(this.languageDataGridViewTextBoxColumn, "languageDataGridViewTextBoxColumn");
            this.languageDataGridViewTextBoxColumn.Name = "languageDataGridViewTextBoxColumn";
            this.languageDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            resources.ApplyResources(this.nameDataGridViewTextBoxColumn, "nameDataGridViewTextBoxColumn");
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonPasteImage);
            this.groupBox1.Controls.Add(this.buttonCopyImage);
            this.groupBox1.Controls.Add(this.buttonExportImage);
            this.groupBox1.Controls.Add(this.pictureBoxImage);
            this.groupBox1.Controls.Add(this.buttonImportImage);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // buttonPasteImage
            // 
            resources.ApplyResources(this.buttonPasteImage, "buttonPasteImage");
            this.buttonPasteImage.Name = "buttonPasteImage";
            this.buttonPasteImage.UseVisualStyleBackColor = true;
            this.buttonPasteImage.Click += new System.EventHandler(this.buttonPasteImage_Click);
            // 
            // buttonCopyImage
            // 
            resources.ApplyResources(this.buttonCopyImage, "buttonCopyImage");
            this.buttonCopyImage.Name = "buttonCopyImage";
            this.buttonCopyImage.UseVisualStyleBackColor = true;
            this.buttonCopyImage.Click += new System.EventHandler(this.buttonCopyImage_Click);
            // 
            // buttonExportImage
            // 
            resources.ApplyResources(this.buttonExportImage, "buttonExportImage");
            this.buttonExportImage.Name = "buttonExportImage";
            this.buttonExportImage.UseVisualStyleBackColor = true;
            this.buttonExportImage.Click += new System.EventHandler(this.buttonExportImage_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewName);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonDeleteSound);
            this.groupBox3.Controls.Add(this.buttonExportSound);
            this.groupBox3.Controls.Add(this.buttonImportSound);
            this.groupBox3.Controls.Add(this.buttonPlaySound);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // buttonDeleteSound
            // 
            resources.ApplyResources(this.buttonDeleteSound, "buttonDeleteSound");
            this.buttonDeleteSound.Name = "buttonDeleteSound";
            this.buttonDeleteSound.UseVisualStyleBackColor = true;
            this.buttonDeleteSound.Click += new System.EventHandler(this.buttonDeleteSound_Click);
            // 
            // buttonExportSound
            // 
            resources.ApplyResources(this.buttonExportSound, "buttonExportSound");
            this.buttonExportSound.Name = "buttonExportSound";
            this.buttonExportSound.UseVisualStyleBackColor = true;
            this.buttonExportSound.Click += new System.EventHandler(this.buttonExportSound_Click);
            // 
            // CategoryDetailForm
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CategoryDetailForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.CategoryDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryDetailDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewName)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonImportImage;
        private System.Windows.Forms.Button buttonPlaySound;
        private System.Windows.Forms.Button buttonImportSound;
        private CategoryDetailDataSet categoryDetailDataSet;
        private System.Windows.Forms.DataGridView dataGridViewName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonPasteImage;
        private System.Windows.Forms.Button buttonCopyImage;
        private System.Windows.Forms.Button buttonExportImage;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonExportSound;
        private System.Windows.Forms.DataGridViewTextBoxColumn languageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button buttonDeleteSound;
    }
}