using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utilities;
using ADADataAccess.SymbolDataSetTableAdapters;
using System.IO;
using System.Diagnostics;
using ADADataAccess;

namespace ADASymbolPicker
{
    public partial class SymbolPickerForm : Form
    {
        static SymbolDataSet bufferedDataSet;

        private const string ENGLISH_CULTURE = "en";
        private const string SIMPLIFIED_CHINESE_CULTURE = "zh-CHS";
        private const string TRADITIONAL_CHINESE_CULTURE = "zh-CHT";

        private const string LOCALIZED_CATEGORY = "LocalizedCategory";
        private const string LOCALIZED_SYMBOL = "LocalizedCategory.LocalizedCategory_LocalizedSymbol";

        private string cultureName = ENGLISH_CULTURE;
        private SymbolListRefresher symbolListRefresher;

        private delegate void UpdateSymbolListItemDelegate(ListViewItem item, Image image);
        private UpdateSymbolListItemDelegate updateSymbolListItemDelegate;

        private delegate void AddSymbolListRangeDelegate(ListViewItem[] items);
        private AddSymbolListRangeDelegate addSymbolListRangeDelegate;

        private SymbolDataSet.LocalizedSymbolRow pickedSymbol;

        private int currentSymbolId;

        public SymbolDataSet.LocalizedSymbolRow PickedSymbol
        {
            get { return pickedSymbol; }
        }

        public SymbolPickerForm(int currentSymbolId)
        {
            InitializeComponent();

            this.updateSymbolListItemDelegate = new UpdateSymbolListItemDelegate(this.UpdateSymbolListItemMethod);
            this.addSymbolListRangeDelegate = new AddSymbolListRangeDelegate(this.AddSymbolListRangeMethod);

            this.symbolListRefresher = new SymbolListRefresher(this);
            this.symbolListRefresher.RefreshFinished += new RefreshFinished(symbolListRefresher_RefreshFinished);

            this.currentSymbolId = currentSymbolId;
        }

        private void SymbolPickerForm_Load(object sender, EventArgs e)
        {
            LoadDataSet(false);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedSymbols = this.listViewSymbol.SelectedItems;
            pickedSymbol = selectedSymbols[0].Tag as SymbolDataSet.LocalizedSymbolRow;
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void AddSymbolListRangeMethod(ListViewItem[] items)
        {
            this.listViewSymbol.Clear();
            this.imageListSymbol.Images.Clear();

            this.listViewSymbol.Items.AddRange(items);

            int selectedSymbol = 0;

            if (items.Length > 0)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].Tag == pickedSymbol)
                    {
                        selectedSymbol = i;
                        items[i].Focused = true;
                        items[i].EnsureVisible();
                        break;
                    }
                }

                this.listViewSymbol.SelectedIndices.Clear();
                this.listViewSymbol.SelectedIndices.Add(selectedSymbol);
                this.listViewSymbol.Focus();
            }
        }

        private void UpdateSymbolListItemMethod(ListViewItem item, Image image)
        {
            if (image != null)
            {
                item.ImageIndex = this.imageListSymbol.Images.Count;
                this.imageListSymbol.Images.Add(image);
            }
        }

        private void LoadDataSet(bool forceLoad)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (forceLoad || bufferedDataSet == null)
                {
                    LocalizedCategoryTableAdapter categoryAdapter = new LocalizedCategoryTableAdapter();
                    categoryAdapter.Fill(this.symbolDataSet.LocalizedCategory, cultureName);

                    LocalizedSymbolTableAdapter symbolAdapter = new LocalizedSymbolTableAdapter();
                    symbolAdapter.Fill(this.symbolDataSet.LocalizedSymbol, cultureName);

                    bufferedDataSet = symbolDataSet.Copy() as SymbolDataSet;
                }
                else
                {
                    symbolDataSet.Merge(bufferedDataSet);
                }
            }
            catch (Exception ex)
            {
                ReportError(ex.Message.ToString());
            }

            pickedSymbol = symbolDataSet.LocalizedSymbol.FindBySymbolId(this.currentSymbolId);

            RefreshCategoryListView();

            this.EnableButtons();

            Cursor.Current = Cursors.Default;
        }

        private void EnableButtons()
        {
            this.buttonOK.Enabled = (this.listViewSymbol.SelectedIndices.Count > 0);
        }

        private void RefreshCategoryListView()
        {
            int selectedCategory = 0;

            this.listViewCategory.Clear();
            this.imageListCategory.Images.Clear();

            int count = 0;
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

                if (pickedSymbol != null && pickedSymbol.LocalizedCategoryRow == row)
                {
                    selectedCategory = count;
                }

                count++;
            }

            this.listViewCategory.SelectedIndices.Clear();
            this.listViewCategory.SelectedIndices.Add(selectedCategory);
        }

        private void RefreshSymbolListView(DataRow currentCategoryRow)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (currentCategoryRow != null)
            {
                this.symbolListRefresher.DoRefresh((int)currentCategoryRow["CategoryId"]);
            }

            Cursor.Current = Cursors.Default;
        }

        void symbolListRefresher_RefreshFinished()
        {
        }

        private void ReportError(string statusMessage)
        {
            // If the caller passed in a message...
            if ((statusMessage != null) && (statusMessage != String.Empty))
            {
                // ...post the caller's message to the status bar.
                MessageBox.Show(statusMessage, "Symbol Picker",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                RefreshSymbolListView(e.Item.Tag as DataRow);
            }
        }

        public delegate void RefreshFinished();

        private class SymbolListRefresher : IDisposable
        {
            private ThreadExecuteTask threadExecute;

            public event RefreshFinished RefreshFinished;

            private SymbolPickerForm mainForm;

            private int currentCategoryId;

            public SymbolListRefresher(SymbolPickerForm mainForm)
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

        private void buttonReload_Click(object sender, EventArgs e)
        {
            LoadDataSet(true);
        }

        private void SymbolPickerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.symbolListRefresher.Dispose();
        }
    }
}