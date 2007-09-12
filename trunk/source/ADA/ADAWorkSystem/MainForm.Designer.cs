namespace ADAWorkSystem
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.adaWorkSystemDataSet1 = new ADAWorkSystem.ADAWorkSystemDataSet();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAddActivity = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listViewWorkSystem = new System.Windows.Forms.ListView();
            this.imageListWorkSystem = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAddWorkSystem = new System.Windows.Forms.Button();
            this.buttonEditWorkSystem = new System.Windows.Forms.Button();
            this.buttonDeleteWorkSystem = new System.Windows.Forms.Button();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simplifiedChineseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traditionalChineseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Sequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActivityName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Image = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adaWorkSystemDataSet1)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(605, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(594, 650);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Activities";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sequence,
            this.ActivityName,
            this.Image});
            this.dataGridView1.DataMember = "Schedule.FK_Activity_Schedule";
            this.dataGridView1.DataSource = this.adaWorkSystemDataSet1;
            this.dataGridView1.Location = new System.Drawing.Point(8, 23);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(568, 584);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // adaWorkSystemDataSet1
            // 
            this.adaWorkSystemDataSet1.DataSetName = "ADAWorkSystemDataSet";
            this.adaWorkSystemDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 7;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.buttonDelete, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonAddActivity, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonEdit, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 610);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(586, 36);
            this.tableLayoutPanel3.TabIndex = 14;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDelete.Location = new System.Drawing.Point(408, 4);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(125, 28);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAddActivity
            // 
            this.buttonAddActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAddActivity.Location = new System.Drawing.Point(50, 4);
            this.buttonAddActivity.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddActivity.Name = "buttonAddActivity";
            this.buttonAddActivity.Size = new System.Drawing.Size(125, 28);
            this.buttonAddActivity.TabIndex = 1;
            this.buttonAddActivity.Text = "Add";
            this.buttonAddActivity.UseVisualStyleBackColor = true;
            this.buttonAddActivity.Click += new System.EventHandler(this.buttonAddActivity_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonEdit.Location = new System.Drawing.Point(229, 4);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(125, 28);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listViewWorkSystem);
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(4, 4);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(593, 650);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Work Systems";
            // 
            // listViewWorkSystem
            // 
            this.listViewWorkSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewWorkSystem.HideSelection = false;
            this.listViewWorkSystem.LargeImageList = this.imageListWorkSystem;
            this.listViewWorkSystem.Location = new System.Drawing.Point(4, 19);
            this.listViewWorkSystem.Margin = new System.Windows.Forms.Padding(4);
            this.listViewWorkSystem.MultiSelect = false;
            this.listViewWorkSystem.Name = "listViewWorkSystem";
            this.listViewWorkSystem.Size = new System.Drawing.Size(585, 591);
            this.listViewWorkSystem.TabIndex = 13;
            this.listViewWorkSystem.UseCompatibleStateImageBehavior = false;
            this.listViewWorkSystem.ItemActivate += new System.EventHandler(this.listViewWorkSystem_ItemActivate);
            this.listViewWorkSystem.SelectedIndexChanged += new System.EventHandler(this.listViewWorkSystem_SelectedIndexChanged);
            // 
            // imageListWorkSystem
            // 
            this.imageListWorkSystem.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageListWorkSystem.ImageSize = new System.Drawing.Size(100, 100);
            this.imageListWorkSystem.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.buttonAddWorkSystem, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonEditWorkSystem, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonDeleteWorkSystem, 5, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 610);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(585, 36);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // buttonAddWorkSystem
            // 
            this.buttonAddWorkSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAddWorkSystem.Location = new System.Drawing.Point(50, 4);
            this.buttonAddWorkSystem.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddWorkSystem.Name = "buttonAddWorkSystem";
            this.buttonAddWorkSystem.Size = new System.Drawing.Size(125, 28);
            this.buttonAddWorkSystem.TabIndex = 4;
            this.buttonAddWorkSystem.Text = "Add";
            this.buttonAddWorkSystem.UseVisualStyleBackColor = true;
            this.buttonAddWorkSystem.Click += new System.EventHandler(this.buttonAddWorkSystem_Click);
            // 
            // buttonEditWorkSystem
            // 
            this.buttonEditWorkSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonEditWorkSystem.Location = new System.Drawing.Point(229, 4);
            this.buttonEditWorkSystem.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEditWorkSystem.Name = "buttonEditWorkSystem";
            this.buttonEditWorkSystem.Size = new System.Drawing.Size(125, 28);
            this.buttonEditWorkSystem.TabIndex = 4;
            this.buttonEditWorkSystem.Text = "Edit";
            this.buttonEditWorkSystem.UseVisualStyleBackColor = true;
            this.buttonEditWorkSystem.Click += new System.EventHandler(this.buttonEditWorkSystem_Click);
            // 
            // buttonDeleteWorkSystem
            // 
            this.buttonDeleteWorkSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDeleteWorkSystem.Location = new System.Drawing.Point(408, 4);
            this.buttonDeleteWorkSystem.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDeleteWorkSystem.Name = "buttonDeleteWorkSystem";
            this.buttonDeleteWorkSystem.Size = new System.Drawing.Size(125, 28);
            this.buttonDeleteWorkSystem.TabIndex = 12;
            this.buttonDeleteWorkSystem.Text = "Delete";
            this.buttonDeleteWorkSystem.UseVisualStyleBackColor = true;
            this.buttonDeleteWorkSystem.Click += new System.EventHandler(this.buttonDeleteWorkSystem_Click);
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.languageToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStripMain.Size = new System.Drawing.Size(1203, 24);
            this.menuStripMain.TabIndex = 11;
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.systemToolStripMenuItem.Text = "System";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(95, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.simplifiedChineseToolStripMenuItem,
            this.traditionalChineseToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // simplifiedChineseToolStripMenuItem
            // 
            this.simplifiedChineseToolStripMenuItem.Name = "simplifiedChineseToolStripMenuItem";
            this.simplifiedChineseToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.simplifiedChineseToolStripMenuItem.Text = "Simplified Chinese";
            this.simplifiedChineseToolStripMenuItem.Click += new System.EventHandler(this.simplifiedChineseToolStripMenuItem_Click);
            // 
            // traditionalChineseToolStripMenuItem
            // 
            this.traditionalChineseToolStripMenuItem.Name = "traditionalChineseToolStripMenuItem";
            this.traditionalChineseToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.traditionalChineseToolStripMenuItem.Text = "Traditional Chinese";
            this.traditionalChineseToolStripMenuItem.Click += new System.EventHandler(this.traditionalChineseToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1203, 658);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // Sequence
            // 
            this.Sequence.DataPropertyName = "Sequence";
            this.Sequence.HeaderText = "Sequence";
            this.Sequence.Name = "Sequence";
            this.Sequence.ReadOnly = true;
            this.Sequence.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Sequence.Width = 76;
            // 
            // ActivityName
            // 
            this.ActivityName.DataPropertyName = "Name";
            this.ActivityName.HeaderText = "Activity";
            this.ActivityName.Name = "ActivityName";
            this.ActivityName.ReadOnly = true;
            this.ActivityName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ActivityName.Width = 56;
            // 
            // Image
            // 
            this.Image.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Image.DataPropertyName = "Image";
            this.Image.HeaderText = "Image";
            this.Image.Name = "Image";
            this.Image.ReadOnly = true;
            this.Image.Width = 52;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 682);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStripMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStripMain;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "ADA Work System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adaWorkSystemDataSet1)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private ADAWorkSystemDataSet adaWorkSystemDataSet1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonAddActivity;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonDeleteWorkSystem;
        private System.Windows.Forms.Button buttonAddWorkSystem;
        private System.Windows.Forms.Button buttonEditWorkSystem;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ListView listViewWorkSystem;
        private System.Windows.Forms.ImageList imageListWorkSystem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simplifiedChineseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem traditionalChineseToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sequence;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActivityName;
        private System.Windows.Forms.DataGridViewImageColumn Image;

    }
}

