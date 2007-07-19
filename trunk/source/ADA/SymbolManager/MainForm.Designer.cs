namespace SymbolManager
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
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simplifiedChineseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traditionalChineseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListCategory = new System.Windows.Forms.ImageList(this.components);
            this.imageListSymbol = new System.Windows.Forms.ImageList(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStripInfo = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxSymbol = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.listViewSymbol = new System.Windows.Forms.ListView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonDeleteSymbol = new System.Windows.Forms.Button();
            this.buttonEditSymbol = new System.Windows.Forms.Button();
            this.buttonAddSymbol = new System.Windows.Forms.Button();
            this.groupBoxCategory = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.listViewCategory = new System.Windows.Forms.ListView();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAddCategory = new System.Windows.Forms.Button();
            this.buttonEditCategory = new System.Windows.Forms.Button();
            this.buttonDeleteCategory = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.symbolDataSet = new SymbolManager.SymbolDataSet();
            this.menuStripMain.SuspendLayout();
            this.statusStripInfo.SuspendLayout();
            this.groupBoxSymbol.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBoxCategory.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.symbolDataSet)).BeginInit();
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
            this.toolStripMenuItem2,
            this.importToolStripMenuItem,
            this.exportToFilesToolStripMenuItem,
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
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            resources.ApplyResources(this.importToolStripMenuItem, "importToolStripMenuItem");
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToFilesToolStripMenuItem
            // 
            this.exportToFilesToolStripMenuItem.Name = "exportToFilesToolStripMenuItem";
            resources.ApplyResources(this.exportToFilesToolStripMenuItem, "exportToFilesToolStripMenuItem");
            this.exportToFilesToolStripMenuItem.Click += new System.EventHandler(this.exportToFilesToolStripMenuItem_Click);
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
            // imageListCategory
            // 
            this.imageListCategory.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageListCategory.ImageSize = global::SymbolManager.Properties.Settings.Default.ListImageSize;
            this.imageListCategory.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageListSymbol
            // 
            this.imageListSymbol.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageListSymbol.ImageSize = global::SymbolManager.Properties.Settings.Default.ListImageSize;
            this.imageListSymbol.TransparentColor = System.Drawing.Color.Transparent;
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
            this.tableLayoutPanel3.Controls.Add(this.listViewSymbol, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // listViewSymbol
            // 
            resources.ApplyResources(this.listViewSymbol, "listViewSymbol");
            this.listViewSymbol.HideSelection = false;
            this.listViewSymbol.LargeImageList = this.imageListSymbol;
            this.listViewSymbol.MultiSelect = false;
            this.listViewSymbol.Name = "listViewSymbol";
            this.listViewSymbol.UseCompatibleStateImageBehavior = false;
            this.listViewSymbol.ItemActivate += new System.EventHandler(this.listViewSymbol_ItemActivate);
            this.listViewSymbol.SelectedIndexChanged += new System.EventHandler(this.listViewSymbol_SelectedIndexChanged);
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.buttonDeleteSymbol, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.buttonEditSymbol, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.buttonAddSymbol, 1, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // buttonDeleteSymbol
            // 
            resources.ApplyResources(this.buttonDeleteSymbol, "buttonDeleteSymbol");
            this.buttonDeleteSymbol.Name = "buttonDeleteSymbol";
            this.buttonDeleteSymbol.UseVisualStyleBackColor = true;
            this.buttonDeleteSymbol.Click += new System.EventHandler(this.buttonDeleteSymbol_Click);
            // 
            // buttonEditSymbol
            // 
            resources.ApplyResources(this.buttonEditSymbol, "buttonEditSymbol");
            this.buttonEditSymbol.Name = "buttonEditSymbol";
            this.buttonEditSymbol.UseVisualStyleBackColor = true;
            this.buttonEditSymbol.Click += new System.EventHandler(this.buttonEditSymbol_Click);
            // 
            // buttonAddSymbol
            // 
            resources.ApplyResources(this.buttonAddSymbol, "buttonAddSymbol");
            this.buttonAddSymbol.Name = "buttonAddSymbol";
            this.buttonAddSymbol.UseVisualStyleBackColor = true;
            this.buttonAddSymbol.Click += new System.EventHandler(this.buttonAddSymbol_Click);
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
            this.tableLayoutPanel2.Controls.Add(this.listViewCategory, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // listViewCategory
            // 
            resources.ApplyResources(this.listViewCategory, "listViewCategory");
            this.listViewCategory.HideSelection = false;
            this.listViewCategory.LargeImageList = this.imageListCategory;
            this.listViewCategory.MultiSelect = false;
            this.listViewCategory.Name = "listViewCategory";
            this.listViewCategory.UseCompatibleStateImageBehavior = false;
            this.listViewCategory.ItemActivate += new System.EventHandler(this.listViewCategory_ItemActivate);
            this.listViewCategory.SelectedIndexChanged += new System.EventHandler(this.listViewCategory_SelectedIndexChanged);
            this.listViewCategory.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewCategory_ItemSelectionChanged);
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.buttonAddCategory, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonEditCategory, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonDeleteCategory, 5, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // buttonAddCategory
            // 
            resources.ApplyResources(this.buttonAddCategory, "buttonAddCategory");
            this.buttonAddCategory.Name = "buttonAddCategory";
            this.buttonAddCategory.UseVisualStyleBackColor = true;
            this.buttonAddCategory.Click += new System.EventHandler(this.buttonAddCategory_Click);
            // 
            // buttonEditCategory
            // 
            resources.ApplyResources(this.buttonEditCategory, "buttonEditCategory");
            this.buttonEditCategory.Name = "buttonEditCategory";
            this.buttonEditCategory.UseVisualStyleBackColor = true;
            this.buttonEditCategory.Click += new System.EventHandler(this.buttonEditCategory_Click);
            // 
            // buttonDeleteCategory
            // 
            resources.ApplyResources(this.buttonDeleteCategory, "buttonDeleteCategory");
            this.buttonDeleteCategory.Name = "buttonDeleteCategory";
            this.buttonDeleteCategory.UseVisualStyleBackColor = true;
            this.buttonDeleteCategory.Click += new System.EventHandler(this.buttonDeleteCategory_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBoxSymbol, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxCategory, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // symbolDataSet
            // 
            this.symbolDataSet.DataSetName = "SymbolDataSet";
            this.symbolDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStripInfo);
            this.Controls.Add(this.menuStripMain);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::SymbolManager.Properties.Settings.Default, "Location", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::SymbolManager.Properties.Settings.Default.Location;
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
            ((System.ComponentModel.ISupportInitialize)(this.symbolDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SymbolDataSet symbolDataSet;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simplifiedChineseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem traditionalChineseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.StatusStrip statusStripInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusInfo;
        private System.Windows.Forms.ImageList imageListSymbol;
        private System.Windows.Forms.ImageList imageListCategory;
        private System.Windows.Forms.GroupBox groupBoxSymbol;
        private System.Windows.Forms.ListView listViewSymbol;
        private System.Windows.Forms.Button buttonDeleteSymbol;
        private System.Windows.Forms.Button buttonEditSymbol;
        private System.Windows.Forms.Button buttonAddSymbol;
        private System.Windows.Forms.GroupBox groupBoxCategory;
        private System.Windows.Forms.ListView listViewCategory;
        private System.Windows.Forms.Button buttonDeleteCategory;
        private System.Windows.Forms.Button buttonEditCategory;
        private System.Windows.Forms.Button buttonAddCategory;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.ToolStripMenuItem exportToFilesToolStripMenuItem;
    }
}

