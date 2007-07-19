namespace AdaSchedulePpc
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItemCurrent = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemSymbolView = new System.Windows.Forms.MenuItem();
            this.menuItemDetailView = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.adaScheduleDataSet1 = new AdaSchedulePpc.ADAMobileDataSet();
            this.symbolListView1 = new UtilitiesPpc.SymbolListView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemCurrent);
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItemCurrent
            // 
            this.menuItemCurrent.Text = "Current";
            this.menuItemCurrent.Click += new System.EventHandler(this.menuItemCurrent_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemSymbolView);
            this.menuItem1.MenuItems.Add(this.menuItemDetailView);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.menuItemExit);
            this.menuItem1.Text = "Menu";
            // 
            // menuItemSymbolView
            // 
            this.menuItemSymbolView.Text = "Symbol View";
            this.menuItemSymbolView.Click += new System.EventHandler(this.menuItemSymbolView_Click);
            // 
            // menuItemDetailView
            // 
            this.menuItemDetailView.Text = "Detail View";
            this.menuItemDetailView.Click += new System.EventHandler(this.menuItemDetailView_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "-";
            // 
            // adaScheduleDataSet1
            // 
            this.adaScheduleDataSet1.DataSetName = "ADAMobileDataSet";
            this.adaScheduleDataSet1.Prefix = "";
            this.adaScheduleDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // symbolListView1
            // 
            this.symbolListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.symbolListView1.Location = new System.Drawing.Point(0, 0);
            this.symbolListView1.Name = "symbolListView1";
            this.symbolListView1.Size = new System.Drawing.Size(240, 268);
            this.symbolListView1.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.Add(this.columnHeader3);
            this.listView1.Columns.Add(this.columnHeader1);
            this.listView1.Columns.Add(this.columnHeader2);
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(240, 268);
            this.listView1.TabIndex = 1;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listView1_ItemCheck);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Activity";
            this.columnHeader3.Width = 117;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "From";
            this.columnHeader1.Width = 60;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "To";
            this.columnHeader2.Width = 60;
            // 
            // menuItemExit
            // 
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.symbolListView1);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "ADA Schedule";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AdaSchedulePpc.ADAMobileDataSet adaScheduleDataSet1;
        private UtilitiesPpc.SymbolListView symbolListView1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.MenuItem menuItemCurrent;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemSymbolView;
        private System.Windows.Forms.MenuItem menuItemDetailView;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItemExit;
    }
}

