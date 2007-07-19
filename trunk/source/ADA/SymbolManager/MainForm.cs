using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SymbolManager.SymbolDataSetTableAdapters;
using System.Threading;
using System.Globalization;
using SymbolManager.Properties;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;
using Utilities;

namespace SymbolManager
{
    public partial class MainForm : Form
    {

        private const string ENGLISH_CULTURE = "en";
        private const string SIMPLIFIED_CHINESE_CULTURE = "zh-CHS";
        private const string TRADITIONAL_CHINESE_CULTURE = "zh-CHT";

        private const string LOCALIZED_CATEGORY = "LocalizedCategory";
        private const string LOCALIZED_SYMBOL = "LocalizedCategory.LocalizedCategory_LocalizedSymbol";

        private string cultureName;
        private SymbolListRefresher symbolListRefresher;

        private delegate void UpdateSymbolListItemDelegate(ListViewItem item, Image image);
        private UpdateSymbolListItemDelegate updateSymbolListItemDelegate;

        private delegate void AddSymbolListRangeDelegate(ListViewItem[] items);
        private AddSymbolListRangeDelegate addSymbolListRangeDelegate;

        public MainForm()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.CultureName);

            InitializeComponent();

            this.updateSymbolListItemDelegate = new UpdateSymbolListItemDelegate(this.UpdateSymbolListItemMethod);
            this.addSymbolListRangeDelegate = new AddSymbolListRangeDelegate(this.AddSymbolListRangeMethod);

            this.symbolListRefresher = new SymbolListRefresher(this);
            this.symbolListRefresher.RefreshFinished += new RefreshFinished(symbolListRefresher_RefreshFinished);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ChangeCulture(Properties.Settings.Default.CultureName);
        }

        private void AddSymbolListRangeMethod(ListViewItem[] items)
        {
            this.listViewSymbol.Clear();
            this.imageListSymbol.Images.Clear();

            this.listViewSymbol.Items.AddRange(items);
            this.listViewSymbol.SelectedIndices.Add(0);
        }

        private void UpdateSymbolListItemMethod(ListViewItem item, Image image)
        {
            if (image != null)
            {
                item.ImageIndex = this.imageListSymbol.Images.Count;
                this.imageListSymbol.Images.Add(image);
            }
        }

        private void LoadDataSet()
        {
            Cursor.Current = Cursors.WaitCursor;

            int selectedCategory = 0;
            if (this.listViewCategory.SelectedIndices.Count > 0)
            {
                selectedCategory = this.listViewCategory.SelectedIndices[0];
            }

            int selectedSymbol = 0;
            if (this.listViewSymbol.SelectedIndices.Count > 0)
            {
                selectedSymbol = this.listViewSymbol.SelectedIndices[0];
            }

            try
            {
                LocalizedCategoryTableAdapter cultureAdapter = new LocalizedCategoryTableAdapter();
                cultureAdapter.Fill(this.symbolDataSet.LocalizedCategory, cultureName);

                LocalizedSymbolTableAdapter symbolAdapter = new LocalizedSymbolTableAdapter();
                symbolAdapter.Fill(this.symbolDataSet.LocalizedSymbol, cultureName);
            }
            catch (Exception ex)
            {
                ReportError(ex.Message.ToString());
            }

            RefreshCategoryListView();

            this.listViewSymbol.Clear();
            this.imageListSymbol.Images.Clear();

            if (this.listViewCategory.Items.Count > 0)
            {
                if (selectedCategory >= this.listViewCategory.Items.Count)
                {
                    selectedCategory = this.listViewCategory.Items.Count - 1;
                }

                this.listViewCategory.SelectedIndices.Clear();
                this.listViewCategory.SelectedIndices.Add(selectedCategory);
            }

            if (this.listViewSymbol.Items.Count > 0)
            {
                if (selectedSymbol >= this.listViewSymbol.Items.Count)
                {
                    selectedSymbol = this.listViewSymbol.Items.Count - 1;
                }

                this.listViewSymbol.SelectedIndices.Clear();
                this.listViewSymbol.SelectedIndices.Add(selectedSymbol);
            }

            this.EnableButtons();

            Cursor.Current = Cursors.Default;
        }

        private void EnableButtons()
        {
            this.buttonEditSymbol.Enabled = (this.listViewSymbol.SelectedIndices.Count > 0);
            this.buttonDeleteSymbol.Enabled = (this.listViewSymbol.SelectedIndices.Count > 0);

            this.buttonEditCategory.Enabled = (this.listViewCategory.SelectedIndices.Count > 0);
            this.buttonDeleteCategory.Enabled = (this.listViewCategory.SelectedIndices.Count > 0);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadDataSet();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RefreshCategoryListView()
        {
            this.toolStripStatusInfo.Text = Resources.RefreshCategoryListView;
            this.statusStripInfo.Refresh();

            this.listViewCategory.Clear();
            this.imageListCategory.Images.Clear();

            foreach (DataRow row in this.symbolDataSet.LocalizedCategory.Select("", "[Name] ASC"))
            {
                string name = row["Name"] as string;
                byte[] image = row["Image"] as byte[];

                ListViewItem item;

                if (image != null)
                {
                    using (MemoryStream ms = new MemoryStream(image))
                    {
                        item = this.listViewCategory.Items.Add(name, this.imageListCategory.Images.Count);
                        this.imageListCategory.Images.Add(new Bitmap(ms));
                    }
                }
                else
                {
                    item = this.listViewCategory.Items.Add(name);
                }

                item.Tag = row;
            }

            this.toolStripStatusInfo.Text = Resources.Ready;
        }

        private void RefreshSymbolListView(DataRow currentCategoryRow)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.toolStripStatusInfo.Text = Resources.RefreshSymbolListView;
            this.statusStripInfo.Refresh();

            if (currentCategoryRow != null)
            {
                this.symbolListRefresher.DoRefresh((int)currentCategoryRow["CategoryId"]);
            }

            Cursor.Current = Cursors.Default;
        }

        void symbolListRefresher_RefreshFinished()
        {
            this.toolStripStatusInfo.Text = Resources.Ready;
        }

        private void ChangeCulture(string cultureName)
        {
            if (cultureName != this.cultureName)
            {
                this.cultureName = cultureName;
                Properties.Settings.Default.CultureName = cultureName;

                if (cultureName == ENGLISH_CULTURE)
                {
                    this.englishToolStripMenuItem.Checked = true;
                    this.simplifiedChineseToolStripMenuItem.Checked = false;
                    this.traditionalChineseToolStripMenuItem.Checked = false;
                }
                else if (cultureName == SIMPLIFIED_CHINESE_CULTURE)
                {
                    this.englishToolStripMenuItem.Checked = false;
                    this.simplifiedChineseToolStripMenuItem.Checked = true;
                    this.traditionalChineseToolStripMenuItem.Checked = false;
                }
                else if (cultureName == TRADITIONAL_CHINESE_CULTURE)
                {
                    this.englishToolStripMenuItem.Checked = false;
                    this.simplifiedChineseToolStripMenuItem.Checked = false;
                    this.traditionalChineseToolStripMenuItem.Checked = true;
                }

                if (Thread.CurrentThread.CurrentUICulture.Name != cultureName)
                {
                    CultureInfo newCulture = new CultureInfo(cultureName);
                    FormLanguageSwitchSingleton.Instance.ChangeCurrentThreadUICulture(newCulture);
                    FormLanguageSwitchSingleton.Instance.ChangeLanguage(this);
                    //FormLanguageSwitchSingleton.Instance.ChangeLanguage(this, newCulture);
                }

                this.LoadDataSet();
            }
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCulture(ENGLISH_CULTURE);
        }

        private void simplifiedChineseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCulture(SIMPLIFIED_CHINESE_CULTURE);
        }

        private void traditionalChineseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeCulture(TRADITIONAL_CHINESE_CULTURE);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
            this.symbolListRefresher.Dispose();
        }

        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            SymbolDataSet.LocalizedCategoryRow currentCategoryRow = this.symbolDataSet.LocalizedCategory.NewLocalizedCategoryRow();
            this.symbolDataSet.LocalizedCategory.AddLocalizedCategoryRow(currentCategoryRow);

            CategoryDetailForm detailForm = new CategoryDetailForm(currentCategoryRow);

            if (DialogResult.OK == detailForm.ShowDialog(this))
            {
                this.LoadDataSet();
            }
            else
            {
                this.symbolDataSet.LocalizedCategory.RejectChanges();
            }
        }

        private void buttonEditCategory_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItems = this.listViewCategory.SelectedItems;
            SymbolDataSet.LocalizedCategoryRow currentCategoryRow = selectedItems[0].Tag as SymbolDataSet.LocalizedCategoryRow;
            CategoryDetailForm detailForm = new CategoryDetailForm(currentCategoryRow);

            if (DialogResult.OK == detailForm.ShowDialog(this))
            {
                this.LoadDataSet();
            }
        }

        private void buttonDeleteCategory_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItems = this.listViewCategory.SelectedItems;
            SymbolDataSet.LocalizedCategoryRow currentCategoryRow = selectedItems[0].Tag as SymbolDataSet.LocalizedCategoryRow;

            // Initializes the variables to pass to the MessageBox.Show method.

            string message = string.Format(Resources.ConfirmDeletionMessage, currentCategoryRow.Name);
            string caption = Resources.ConfirmDeletionCaption;
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(this, message, caption, buttons,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    LocalizedCategoryTableAdapter categoryAdapter = new LocalizedCategoryTableAdapter();
                    categoryAdapter.Connection.Open();

                    SqlCommand sqlUpdateCommand = new SqlCommand();
                    sqlUpdateCommand.Connection = categoryAdapter.Connection;
                    sqlUpdateCommand.CommandText = @"UPDATE Symbol SET IsActive = 'false' WHERE (CategoryId = @CategoryId) ";

                    sqlUpdateCommand.Parameters.Add(new SqlParameter("@CategoryId", System.Data.SqlDbType.Int, 0, "CategoryId"));
                    sqlUpdateCommand.Parameters["@CategoryId"].Value = currentCategoryRow.CategoryId;
                    sqlUpdateCommand.ExecuteNonQuery();

                    sqlUpdateCommand.CommandText = @"UPDATE Category SET IsActive = 'false' WHERE (CategoryId = @CategoryId) ";
                    sqlUpdateCommand.ExecuteNonQuery();

                    categoryAdapter.Connection.Close();

                    this.LoadDataSet();
                }
                catch (Exception ex)
                {
                    ReportError(ex.Message.ToString());
                }
            }
        }

        private void buttonAddSymbol_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItems = this.listViewCategory.SelectedItems;
            SymbolDataSet.LocalizedCategoryRow currentCategoryRow = selectedItems[0].Tag as SymbolDataSet.LocalizedCategoryRow;

            SymbolDataSet.LocalizedSymbolRow currentSymbolRow = this.symbolDataSet.LocalizedSymbol.NewLocalizedSymbolRow();
            currentSymbolRow.CategoryId = currentCategoryRow.CategoryId;
            this.symbolDataSet.LocalizedSymbol.AddLocalizedSymbolRow(currentSymbolRow);

            SymbolDetailForm detailForm = new SymbolDetailForm(currentSymbolRow);

            if (DialogResult.OK == detailForm.ShowDialog(this))
            {
                this.LoadDataSet();
            }
            else
            {
                this.symbolDataSet.LocalizedSymbol.RejectChanges();
            }
        }

        private void buttonEditSymbol_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedSymbols = this.listViewSymbol.SelectedItems;
            SymbolDataSet.LocalizedSymbolRow currentSymbolRow = selectedSymbols[0].Tag as SymbolDataSet.LocalizedSymbolRow;
            SymbolDetailForm detailForm = new SymbolDetailForm(currentSymbolRow);

            if (DialogResult.OK == detailForm.ShowDialog(this))
            {
                this.LoadDataSet();
            }
        }

        private void buttonDeleteSymbol_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedSymbols = this.listViewSymbol.SelectedItems;
            SymbolDataSet.LocalizedSymbolRow currentSymbolRow = selectedSymbols[0].Tag as SymbolDataSet.LocalizedSymbolRow;

            // Initializes the variables to pass to the MessageBox.Show method.

            string message = string.Format(Resources.ConfirmDeletionMessage, currentSymbolRow.Name);
            string caption = Resources.ConfirmDeletionCaption;
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(this, message, caption, buttons,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    LocalizedSymbolTableAdapter symbolAdapter = new LocalizedSymbolTableAdapter();
                    symbolAdapter.Connection.Open();

                    SqlCommand sqlUpdateCommand = new SqlCommand();
                    sqlUpdateCommand.Connection = symbolAdapter.Connection;
                    sqlUpdateCommand.CommandText = @"UPDATE Symbol SET IsActive = 'false' WHERE (SymbolId = @SymbolId) ";

                    sqlUpdateCommand.Parameters.Add(new SqlParameter("@SymbolId", System.Data.SqlDbType.Int, 0, "SymbolId"));
                    sqlUpdateCommand.Parameters["@SymbolId"].Value = currentSymbolRow.SymbolId;
                    sqlUpdateCommand.ExecuteNonQuery();

                    symbolAdapter.Connection.Close();

                    this.LoadDataSet();
                }
                catch (Exception ex)
                {
                    ReportError(ex.Message.ToString());
                }
            }
        }

        private void ReportError(string statusMessage)
        {
            // If the caller passed in a message...
            if ((statusMessage != null) && (statusMessage != String.Empty))
            {
                // ...post the caller's message to the status bar.
                MessageBox.Show(statusMessage, "Symbol Library Manager",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog.ShowNewFolderButton = false;

            DialogResult result = this.folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;

                try
                {
                    this.ImportSymbols(this.folderBrowserDialog.SelectedPath);
                    this.LoadDataSet();
                }
                catch (Exception ex)
                {
                    ReportError(ex.Message.ToString());
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private void ImportSymbols(string folderPath)
        {
            int categoryCount = 0;
            int symbolCount = 0;

            List<string> allDirs = new List<string>();
            allDirs.Add(folderPath);

            string[] dirs = Directory.GetDirectories(folderPath, "*.*", SearchOption.AllDirectories);
            allDirs.AddRange(dirs);

            ADADataSetTableAdapters.CultureTableAdapter cultureAdapter = new SymbolManager.ADADataSetTableAdapters.CultureTableAdapter();
            ADADataSetTableAdapters.CategoryTableAdapter categoryAdapter = new SymbolManager.ADADataSetTableAdapters.CategoryTableAdapter();
            ADADataSetTableAdapters.ResourceTableAdapter resourceAdapter = new SymbolManager.ADADataSetTableAdapters.ResourceTableAdapter();
            ADADataSetTableAdapters.SymbolTableAdapter symbolAdapter = new SymbolManager.ADADataSetTableAdapters.SymbolTableAdapter();

            ADADataSet dataSetCulture = new ADADataSet();
            cultureAdapter.Fill(dataSetCulture.Culture);
            DataRow[] cultureRows = dataSetCulture.Culture.Select("Name='" + this.cultureName + "'");
            ADADataSet.CultureRow cultureRow = cultureRows[0] as ADADataSet.CultureRow;

            foreach (string dir in allDirs)
            {
                List<string> allFiles = new List<string>();
                string[] searchPatterns = { "*.wmf", "*.jpg", "*.bmp" };

                foreach (string searchPattern in searchPatterns)
                {
                    string[] files = Directory.GetFiles(dir, searchPattern, SearchOption.TopDirectoryOnly);
                    allFiles.AddRange(files);
                }

                if (allFiles.Count > 0)
                {
                    allFiles.Sort();

                    using (ADADataSet dataSet = new ADADataSet())
                    {
                        //insert category
                        ADADataSet.CategoryRow categoryRow = dataSet.Category.NewCategoryRow();
                        categoryRow.Name = Guid.NewGuid();
                        categoryRow.IsActive = true;
                        categoryRow.Image = LoadImage(allFiles[0]);
                        dataSet.Category.AddCategoryRow(categoryRow);

                        ADADataSet.ResourceRow resourceRow = dataSet.Resource.NewResourceRow();
                        resourceRow.ResourceId = categoryRow.Name;
                        resourceRow.CultureId = cultureRow.CultureId;
                        string dirName = Path.GetDirectoryName(dir);
                        //resourceRow.Text = Path.GetFileName(dirName) + ": " + Path.GetFileName(dir);
                        resourceRow.Text = Path.GetFileName(dir);
                        dataSet.Resource.AddResourceRow(resourceRow);

                        categoryAdapter.Update(dataSet);
                        resourceAdapter.Update(dataSet);
                        dataSet.Resource.Clear();
                        dataSet.AcceptChanges();

                        //insert symbols
                        foreach (string file in allFiles)
                        {
                            ADADataSet.SymbolRow symbolRow = dataSet.Symbol.NewSymbolRow();
                            symbolRow.Image = LoadImage(file);
                            symbolRow.CategoryId = categoryRow.CategoryId;
                            symbolRow.Name = Guid.NewGuid();
                            symbolRow.IsActive = true;
                            dataSet.Symbol.AddSymbolRow(symbolRow);

                            resourceRow = dataSet.Resource.NewResourceRow();
                            resourceRow.ResourceId = symbolRow.Name;
                            resourceRow.CultureId = cultureRow.CultureId;
                            resourceRow.Text = Path.GetFileNameWithoutExtension(file);
                            dataSet.Resource.AddResourceRow(resourceRow);

                            symbolAdapter.Update(dataSet);
                            resourceAdapter.Update(dataSet);

                            dataSet.Symbol.Clear();
                            dataSet.Resource.Clear();
                            dataSet.AcceptChanges();

                            symbolCount++;

                            this.toolStripStatusInfo.Text = string.Format(Resources.ImportingSymbolsStatus, categoryCount, symbolCount);
                            this.statusStripInfo.Refresh();

                            Application.DoEvents();
                            GC.Collect();
                        }
                    }

                    categoryCount++;

                    this.toolStripStatusInfo.Text = string.Format(Resources.ImportingSymbolsStatus, categoryCount, symbolCount);
                    this.statusStripInfo.Refresh();
                }
            }
        }

        private byte[] LoadImage(string fileName)
        {
            Bitmap image = new Bitmap(fileName);

            //Size size = global::SymbolManager.Properties.Settings.Default.ImageSize;
            Size size = image.Size;

            //Create a new Bitmap object
            Image canvas = new Bitmap(size.Width, size.Height, PixelFormat.Format16bppRgb555);

            //create an object that will do the drawing operations
            Graphics artist = Graphics.FromImage(canvas);

            // Create rectangle for displaying image.
            Rectangle destRect = new Rectangle(0, 0, size.Width, size.Height);
            artist.FillRectangle(Brushes.White, destRect);

            // Create rectangle for source image.
            Rectangle srcRect = new Rectangle(0, 0, image.Width, image.Height);

            // Draw image to Graphics.
            artist.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);

            //now the drawing is done, we can discard the artist object
            artist.Dispose();

            using (MemoryStream ms = new MemoryStream())
            {
                canvas.Save(ms, ImageFormat.Png);

                return ms.ToArray();
            }
        }

        private void listViewCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        private void listViewSymbol_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        private void listViewCategory_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                this.listViewCategory.Update();
                //RefreshSymbolListView(e.Item.Tag as DataRow);
                RefreshSymbolListView(e.Item.Tag as DataRow);
            }
        }

        private void listViewCategory_ItemActivate(object sender, EventArgs e)
        {
            this.buttonEditCategory_Click(sender, e);
        }

        private void listViewSymbol_ItemActivate(object sender, EventArgs e)
        {
            this.buttonEditSymbol_Click(sender, e);
        }

        public delegate void RefreshFinished();

        private class SymbolListRefresher : IDisposable
        {
            private ThreadExecuteTask threadExecute;

            public event RefreshFinished RefreshFinished;

            private MainForm mainForm;

            private int currentCategoryId;

            public SymbolListRefresher(MainForm mainForm)
            {
                this.mainForm = mainForm;
            }

            public void DoRefresh(int currentCategoryId)
            {
                if (this.threadExecute != null)
                {
                    this.threadExecute.Dispose();
                }

                this.currentCategoryId = currentCategoryId;

                //Package the class' method entry point up in a delegate
                ThreadExecuteTask.ExecuteMeOnAnotherThread delegateCallCode;
                delegateCallCode = new ThreadExecuteTask.ExecuteMeOnAnotherThread(this.Refresh);

                //Tell the thread to get going!
                this.threadExecute = new ThreadExecuteTask(delegateCallCode);
            }

            private void Refresh(ThreadExecuteTask threadExecute)
            {
                try
                {
                    DataRow[] childRows = this.mainForm.symbolDataSet.LocalizedSymbol.Select("CategoryId=" + this.currentCategoryId, "[Name] ASC");

                    ListViewItem[] items = new ListViewItem[childRows.Length];

                    for (int i = 0; i < items.Length; i++)
                    {
                        items[i] = new ListViewItem();
                        items[i].Text = childRows[i]["Name"] as string;
                        items[i].Tag = childRows[i];
                    }

                    this.mainForm.Invoke(this.mainForm.addSymbolListRangeDelegate, new object[] { items });

                    for (int i = 0; i < items.Length; i++)
                    {
                        //If an abort has been requested, we should quit
                        if (this.threadExecute.State == ThreadExecuteTask.ProcessingState.requestAbort)
                        {
                            threadExecute.setProcessingState(ThreadExecuteTask.ProcessingState.aborted);
                            Debug.WriteLine("aborted");
                            return;
                        }

                        DataRow row = childRows[i];
                        byte[] image = row["Image"] as byte[];
                        Image img = null;

                        if (image != null)
                        {
                            using (MemoryStream ms = new MemoryStream(image))
                            {
                                img = new Bitmap(ms);
                            }
                        }

                        this.mainForm.Invoke(this.mainForm.updateSymbolListItemDelegate, new object[] { items[i], img });
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }

                if (this.RefreshFinished != null)
                {
                    this.RefreshFinished();
                }
            }

            #region IDisposable Members

            public void Dispose()
            {
                if (this.threadExecute != null)
                {
                    this.threadExecute.Dispose();
                }
            }

            #endregion
        }

        private void exportToFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog.ShowNewFolderButton = true;

            DialogResult result = this.folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;

                try
                {
                    this.ExportSymbols(this.folderBrowserDialog.SelectedPath);
                }
                catch (Exception ex)
                {
                    ReportError(ex.Message.ToString());
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private void ExportSymbols(string folderPath)
        {
            int categoryCount = 0;
            int symbolCount = 0;

            foreach (DataRow row in this.symbolDataSet.LocalizedCategory.Select("", "[Name] ASC"))
            {
                string name = row["Name"] as string;

                string subFolder = folderPath + "\\" + name;
                Directory.CreateDirectory(subFolder);

                DataRow[] childRows = this.symbolDataSet.LocalizedSymbol.Select("CategoryId=" + row["CategoryId"], "[Name] ASC");

                foreach (DataRow rowSymbol in childRows)
                {
                    name = rowSymbol["Name"] as string;
                    string filename = subFolder + "\\" + name + ".bmp";
                    using (FileStream fs = new FileStream(filename, FileMode.Create))
                    {
                        byte[] image = rowSymbol["Image"] as byte[];
                        fs.Write(image, 0, image.Length);
                    }

                    symbolCount++;

                    this.toolStripStatusInfo.Text = string.Format(Resources.ExportingSymbolsStatus, categoryCount, symbolCount);
                    this.statusStripInfo.Refresh();

                    Application.DoEvents();
                    GC.Collect();
                }

                categoryCount++;

                this.toolStripStatusInfo.Text = string.Format(Resources.ExportingSymbolsStatus, categoryCount, symbolCount);
                this.statusStripInfo.Refresh();
            }
        }
    }
}