namespace AdaCommunicatorPpc
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
            this.menuItemSelect = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.adaScenarioDataSet1 = new AdaCommunicatorPpc.ADAMobileDataSet();
            this.symbolListView1 = new UtilitiesPpc.SymbolListView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemSelect);
            this.mainMenu1.MenuItems.Add(this.menuItemExit);
            // 
            // menuItemSelect
            // 
            this.menuItemSelect.Enabled = false;
            this.menuItemSelect.Text = "Select";
            this.menuItemSelect.Click += new System.EventHandler(this.menuItemSelect_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // adaScenarioDataSet1
            // 
            this.adaScenarioDataSet1.DataSetName = "ADAMobileDataSet";
            this.adaScenarioDataSet1.Prefix = "";
            this.adaScenarioDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // symbolListView1
            // 
            this.symbolListView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.symbolListView1.Location = new System.Drawing.Point(0, 22);
            this.symbolListView1.Name = "symbolListView1";
            this.symbolListView1.Size = new System.Drawing.Size(240, 246);
            this.symbolListView1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(240, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.symbolListView1);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "ADA Communicator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private AdaCommunicatorPpc.ADAMobileDataSet adaScenarioDataSet1;
        private UtilitiesPpc.SymbolListView symbolListView1;
        private System.Windows.Forms.MenuItem menuItemSelect;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.TextBox textBox1;
    }
}

