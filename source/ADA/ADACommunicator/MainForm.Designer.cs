namespace ADACommunicator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            this.imageListScenario = new System.Windows.Forms.ImageList(this.components);
            this.imageListText = new System.Windows.Forms.ImageList(this.components);
            this.statusStripInfo = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxSymbol = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.listViewText = new System.Windows.Forms.ListView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonDeleteText = new System.Windows.Forms.Button();
            this.buttonEditText = new System.Windows.Forms.Button();
            this.buttonAddText = new System.Windows.Forms.Button();
            this.groupBoxCategory = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.listViewScenario = new System.Windows.Forms.ListView();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAddSenario = new System.Windows.Forms.Button();
            this.buttonEditSenario = new System.Windows.Forms.Button();
            this.buttonDeleteSenario = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.communicatorDataSet = new ADACommunicator.ADACommunicatorDataSet();
            this.menuStripMain.SuspendLayout();
            this.statusStripInfo.SuspendLayout();
            this.groupBoxSymbol.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBoxCategory.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.communicatorDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.languageToolStripMenuItem});
            resources.ApplyResources(this.menuStripMain, "menuStripMain");
            this.menuStripMain.Name = "menuStripMain";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            resources.ApplyResources(this.systemToolStripMenuItem, "systemToolStripMenuItem");
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            resources.ApplyResources(this.loadToolStripMenuItem, "loadToolStripMenuItem");
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.simplifiedChineseToolStripMenuItem,
            this.traditionalChineseToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            resources.ApplyResources(this.languageToolStripMenuItem, "languageToolStripMenuItem");
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // simplifiedChineseToolStripMenuItem
            // 
            this.simplifiedChineseToolStripMenuItem.Name = "simplifiedChineseToolStripMenuItem";
            resources.ApplyResources(this.simplifiedChineseToolStripMenuItem, "simplifiedChineseToolStripMenuItem");
            this.simplifiedChineseToolStripMenuItem.Click += new System.EventHandler(this.simplifiedChineseToolStripMenuItem_Click);
            // 
            // traditionalChineseToolStripMenuItem
            // 
            this.traditionalChineseToolStripMenuItem.Name = "traditionalChineseToolStripMenuItem";
            resources.ApplyResources(this.traditionalChineseToolStripMenuItem, "traditionalChineseToolStripMenuItem");
            this.traditionalChineseToolStripMenuItem.Click += new System.EventHandler(this.traditionalChineseToolStripMenuItem_Click);
            // 
            // imageListScenario
            // 
            this.imageListScenario.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageListScenario.ImageSize = global::ADACommunicator.Properties.Settings.Default.ListImageSize;
            this.imageListScenario.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageListText
            // 
            this.imageListText.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageListText.ImageSize = global::ADACommunicator.Properties.Settings.Default.ListImageSize;
            this.imageListText.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // statusStripInfo
            // 
            this.statusStripInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusInfo});
            resources.ApplyResources(this.statusStripInfo, "statusStripInfo");
            this.statusStripInfo.Name = "statusStripInfo";
            // 
            // toolStripStatusInfo
            // 
            this.toolStripStatusInfo.Name = "toolStripStatusInfo";
            resources.ApplyResources(this.toolStripStatusInfo, "toolStripStatusInfo");
            // 
            // groupBoxSymbol
            // 
            this.groupBoxSymbol.Controls.Add(this.tableLayoutPanel3);
            resources.ApplyResources(this.groupBoxSymbol, "groupBoxSymbol");
            this.groupBoxSymbol.Name = "groupBoxSymbol";
            this.groupBoxSymbol.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.listViewText, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // listViewText
            // 
            resources.ApplyResources(this.listViewText, "listViewText");
            this.listViewText.HideSelection = false;
            this.listViewText.LargeImageList = this.imageListText;
            this.listViewText.MultiSelect = false;
            this.listViewText.Name = "listViewText";
            this.listViewText.UseCompatibleStateImageBehavior = false;
            this.listViewText.ItemActivate += new System.EventHandler(this.listViewText_ItemActivate);
            this.listViewText.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listViewText.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewText_ItemSelectionChanged);
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.buttonDeleteText, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.buttonEditText, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.buttonAddText, 1, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // buttonDeleteText
            // 
            resources.ApplyResources(this.buttonDeleteText, "buttonDeleteText");
            this.buttonDeleteText.Name = "buttonDeleteText";
            this.buttonDeleteText.UseVisualStyleBackColor = true;
            this.buttonDeleteText.Click += new System.EventHandler(this.buttonDeleteText_Click);
            // 
            // buttonEditText
            // 
            resources.ApplyResources(this.buttonEditText, "buttonEditText");
            this.buttonEditText.Name = "buttonEditText";
            this.buttonEditText.UseVisualStyleBackColor = true;
            this.buttonEditText.Click += new System.EventHandler(this.buttonEditText_Click);
            // 
            // buttonAddText
            // 
            resources.ApplyResources(this.buttonAddText, "buttonAddText");
            this.buttonAddText.Name = "buttonAddText";
            this.buttonAddText.UseVisualStyleBackColor = true;
            this.buttonAddText.Click += new System.EventHandler(this.buttonAddText_Click);
            // 
            // groupBoxCategory
            // 
            this.groupBoxCategory.Controls.Add(this.tableLayoutPanel2);
            resources.ApplyResources(this.groupBoxCategory, "groupBoxCategory");
            this.groupBoxCategory.Name = "groupBoxCategory";
            this.groupBoxCategory.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.listViewScenario, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // listViewScenario
            // 
            resources.ApplyResources(this.listViewScenario, "listViewScenario");
            this.listViewScenario.HideSelection = false;
            this.listViewScenario.LargeImageList = this.imageListScenario;
            this.listViewScenario.MultiSelect = false;
            this.listViewScenario.Name = "listViewScenario";
            this.listViewScenario.UseCompatibleStateImageBehavior = false;
            this.listViewScenario.ItemActivate += new System.EventHandler(this.listViewScenario_ItemActivate);
            this.listViewScenario.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listViewScenario.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewScenario_ItemSelectionChanged);
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.buttonAddSenario, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonEditSenario, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonDeleteSenario, 5, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // buttonAddSenario
            // 
            resources.ApplyResources(this.buttonAddSenario, "buttonAddSenario");
            this.buttonAddSenario.Name = "buttonAddSenario";
            this.buttonAddSenario.UseVisualStyleBackColor = true;
            this.buttonAddSenario.Click += new System.EventHandler(this.buttonAddScenario_Click);
            // 
            // buttonEditSenario
            // 
            resources.ApplyResources(this.buttonEditSenario, "buttonEditSenario");
            this.buttonEditSenario.Name = "buttonEditSenario";
            this.buttonEditSenario.UseVisualStyleBackColor = true;
            this.buttonEditSenario.Click += new System.EventHandler(this.buttonEditScenario_Click);
            // 
            // buttonDeleteSenario
            // 
            resources.ApplyResources(this.buttonDeleteSenario, "buttonDeleteSenario");
            this.buttonDeleteSenario.Name = "buttonDeleteSenario";
            this.buttonDeleteSenario.UseVisualStyleBackColor = true;
            this.buttonDeleteSenario.Click += new System.EventHandler(this.buttonDeleteScenario_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBoxSymbol, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxCategory, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // communicatorDataSet
            // 
            this.communicatorDataSet.DataSetName = "communicatorDataSet";
            this.communicatorDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStripInfo);
            this.Controls.Add(this.menuStripMain);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::ADACommunicator.Properties.Settings.Default, "Location", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::ADACommunicator.Properties.Settings.Default.Location;
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.statusStripInfo.ResumeLayout(false);
            this.statusStripInfo.PerformLayout();
            this.groupBoxSymbol.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.groupBoxCategory.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.communicatorDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ADACommunicatorDataSet communicatorDataSet;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simplifiedChineseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem traditionalChineseToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStripInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusInfo;
        private System.Windows.Forms.ImageList imageListText;
        private System.Windows.Forms.ImageList imageListScenario;
        private System.Windows.Forms.GroupBox groupBoxSymbol;
        private System.Windows.Forms.ListView listViewText;
        private System.Windows.Forms.Button buttonDeleteText;
        private System.Windows.Forms.Button buttonEditText;
        private System.Windows.Forms.Button buttonAddText;
        private System.Windows.Forms.GroupBox groupBoxCategory;
        private System.Windows.Forms.ListView listViewScenario;
        private System.Windows.Forms.Button buttonDeleteSenario;
        private System.Windows.Forms.Button buttonEditSenario;
        private System.Windows.Forms.Button buttonAddSenario;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    }
}

