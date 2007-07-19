using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.Windows.Forms;
using AdaSyncPpc.ADAMobileDataSetTableAdapters;
using System.Data.SqlServerCe;
using System.IO;
using System.Drawing.Imaging;
using System.Collections;
using UtilitiesPpc;
using System.Diagnostics;

namespace AdaSyncPpc
{
    public partial class SymbolListForm : AdaBaseForm
    {
        public delegate void SymbolSelectedHandler(object sender, SymbolInfo selectedSymbol);

        private const int COLUMNS = 3;

        private const int ROWS = 3;

        private const int CENTER = COLUMNS * ROWS / 2;

        private Button2[] buttons;

        private SqlCeResultSet resultSet;

        System.Drawing.Color defaultBorderColoer;

        public SqlCeResultSet ResultSet
        {
            get { return resultSet; }
            set { resultSet = value; }
        }

        public event SymbolSelectedHandler SymbolSelected;

        private SymbolInfo selectedSymbol;

        public SymbolInfo SelectedSymbol
        {
            get { return selectedSymbol; }
            set { selectedSymbol = value; }
        }

        private int currentPage;

        private int currentSymbolInPage;

        private SymbolListLoaderManager symbolListLoaderManager;

        private int totalSymbols;

        public int TotalSymbols
        {
            get { return totalSymbols; }
            set { totalSymbols = value; }
        }
        private bool showTextOnButtons;

        public bool ShowTextOnButtons
        {
            get { return showTextOnButtons; }
            set { showTextOnButtons = value; }
        }

        public int TotalPages
        {
            get
            {
                int totalPages = this.totalSymbols / this.buttons.Length;

                if (this.totalSymbols % this.buttons.Length > 0)
                {
                    totalPages++;
                }

                return totalPages;
            }
        }

        public SymbolListForm()
        {
            InitializeComponent();

            this.defaultBorderColoer = button1.BorderColor;

            buttons = new Button2[9];
            buttons[0] = button1;
            buttons[1] = button2;
            buttons[2] = button3;
            buttons[3] = button4;
            buttons[4] = button5;
            buttons[5] = button6;
            buttons[6] = button7;
            buttons[7] = button8;
            buttons[8] = button9;
        }

        private void SymbolExplorerForm_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.symbolListLoaderManager = new SymbolListLoaderManager(button1.ClientSize, buttons.Length, this.resultSet, this.TotalPages);
            Cursor.Current = Cursors.Default;

            this.RefreshDisplay(CENTER);
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button2 btn = sender as Button2;

            this.selectedSymbol = btn.Tag as SymbolInfo;
            if (SymbolSelected != null)
            {
                SymbolSelected(sender, this.selectedSymbol);
            }
        }

        private void RefreshDisplay(int currentSymbolInPage)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.currentSymbolInPage = 0;

            for (int i = 0; i < buttons.Length; i++)
            {
                if (i < this.symbolListLoaderManager.CurrentSymbolList.Count)
                {
                    SymbolInfo info = this.symbolListLoaderManager.CurrentSymbolList[i] as SymbolInfo;
                    buttons[i].Visible = true;

                    if (this.showTextOnButtons)
                    {
                        buttons[i].Text = info.Text;
                    }
                    else
                    {
                        buttons[i].Text = null;
                    }

                    buttons[i].Image = info.Image;
                    buttons[i].Tag = info;

                    if (i == currentSymbolInPage)
                    {
                        this.currentSymbolInPage = currentSymbolInPage;
                    }
                }
                else
                {
                    buttons[i].Visible = false;
                }

                buttons[i].Update();
            }

            SymbolInfo info2 = this.buttons[this.currentSymbolInPage].Tag as SymbolInfo;
            buttons[this.currentSymbolInPage].Focus();
            this.textBoxName.Text = info2.Text;

            this.menuItemPrev.Enabled = (this.currentPage > 0);
            int totalPages = this.TotalPages;
            this.menuItemNext.Enabled = (this.currentPage < totalPages - 1);

            UpdateSelectedSymbolIndex();

            this.Update();

            Cursor.Current = Cursors.Default;
        }

        private void UpdateSelectedSymbolIndex()
        {
            this.labelSelectedSymbolIndex.Text = string.Format("{0}/{1}",
                this.currentSymbolInPage + 1 + this.currentPage * this.buttons.Length,
                this.totalSymbols);
            this.labelSelectedSymbolIndex.Update();
        }

        private void checkBoxName_CheckStateChanged(object sender, EventArgs e)
        {
            showTextOnButtons = this.checkBoxName.Checked;
            this.RefreshDisplay(this.currentSymbolInPage);
        }

        private void button_GotFocus(object sender, EventArgs e)
        {
            Button2 btn = sender as Button2;
            SymbolInfo info = btn.Tag as SymbolInfo;

            btn.BorderColor = System.Drawing.Color.Red;

            if (info != null)
            {
                this.textBoxName.Text = info.Text;
            }
            for (int i = 0; i < this.buttons.Length; i++)
            {
                if (btn == this.buttons[i])
                {
                    this.currentSymbolInPage = i;
                    UpdateSelectedSymbolIndex();
                }
                this.buttons[i].Update();
            }
        }

        private void button_LostFocus(object sender, EventArgs e)
        {
            Button2 btn = sender as Button2;
            btn.BorderColor = this.defaultBorderColoer;
        }

        private void menuItemPrev_Click(object sender, EventArgs e)
        {
            this.MoveToPreviousPage();
        }

        private void menuItemNext_Click(object sender, EventArgs e)
        {
            this.MoveToNextPage();
        }

        private void SymbolExplorerForm_Closing(object sender, CancelEventArgs e)
        {
            this.symbolListLoaderManager.Dispose();
        }

        private void button_KeyDown(object sender, KeyEventArgs e)
        {
            int totalSymbolInPage = this.symbolListLoaderManager.CurrentSymbolList.Count;

            if ((this.currentSymbolInPage == 0) && ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Left)))
            {
                this.MoveToPreviousPage();
            }
            else if ((this.currentSymbolInPage == totalSymbolInPage - 1) && ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.Right)))
            {
                this.MoveToNextPage();
            }
            else
            {
                int nextSymbol = -1;

                if (e.KeyCode == Keys.Up)
                {
                    nextSymbol = this.currentSymbolInPage - COLUMNS;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    nextSymbol = this.currentSymbolInPage + COLUMNS;
                }
                else if (e.KeyCode == Keys.Left)
                {
                    nextSymbol = this.currentSymbolInPage - 1;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    nextSymbol = this.currentSymbolInPage + 1;
                }

                if (nextSymbol >= 0 && nextSymbol < totalSymbolInPage)
                {
                    this.buttons[nextSymbol].Focus();
                }
            }
        }

        private void MoveToPreviousPage()
        {
            if (this.currentPage > 0)
            {
                this.currentPage--;
                this.symbolListLoaderManager.MoveToPreviousPage();
                this.RefreshDisplay(CENTER);
            }
        }

        private void MoveToNextPage()
        {
            if (this.currentPage < this.TotalPages - 1)
            {
                this.currentPage++;
                this.symbolListLoaderManager.MoveToNextPage();
                this.RefreshDisplay(CENTER);
            }
        }
    }

    public delegate void LoadDataFinished();

    public class SymbolInfo
    {
        public int Id;
        public string Text;
        public Image Image;
        public byte[] Sound;
        public SymbolInfo Category;
    }

    public class SymbolListLoader : IDisposable
    {
        private Size imageSize;

        private int countPerPage;

        private ThreadExecuteTask threadExecute;

        private SqlCeResultSet resultSet;

        public event LoadDataFinished DataLoaded;

        private ArrayList symbolList;

        private int lastLoadedPage;

        private int firstRecordindex;

        public ArrayList SymbolList
        {
            get { return symbolList; }
        }

        public SymbolListLoader(Size imageSize, int countPerPage, SqlCeResultSet resultSet)
        {
            this.countPerPage = countPerPage;
            this.imageSize = imageSize;
            this.resultSet = resultSet;
            this.symbolList = new ArrayList();
            this.lastLoadedPage = -1;
        }

        public bool IsRunning
        {
            get
            {
                return this.threadExecute != null && (this.threadExecute.State == ThreadExecuteTask.ProcessingState.running || this.threadExecute.State == ThreadExecuteTask.ProcessingState.requestAbort);
            }
        }

        public void WaitForFinished()
        {
            int count = 0;
            while (this.IsRunning && count < 50)//Wait for 5 seconds only
            {
                System.Threading.Thread.Sleep(100);
                count++;
            }
        }

        public void Load(bool sync, int pageNumber)
        {
            if (this.lastLoadedPage != pageNumber)
            {
                firstRecordindex = pageNumber * this.countPerPage;

                if (sync)
                {
                    LoadData(null);
                }
                else
                {
                    //Package the class' method entry point up in a delegate
                    ThreadExecuteTask.ExecuteMeOnAnotherThread delegateCallCode;
                    delegateCallCode = new ThreadExecuteTask.ExecuteMeOnAnotherThread(this.LoadData);

                    //Tell the thread to get going!
                    threadExecute = new ThreadExecuteTask(delegateCallCode);
                }

                this.lastLoadedPage = pageNumber;
            }
        }

        private void LoadData(ThreadExecuteTask threadExecute)
        {
            try
            {
                this.symbolList.Clear();

                for (int i = 0; i < countPerPage; i++)
                {
                    //If an abort has been requested, we should quit
                    if (threadExecute != null && threadExecute.State ==
                      ThreadExecuteTask.ProcessingState.requestAbort)
                    {
                        threadExecute.setProcessingState(ThreadExecuteTask.ProcessingState.aborted);
                        System.Windows.Forms.MessageBox.Show("aborted");
                        return;
                    }

                    bool ok;
                    if (i == 0)
                    {
                        ok = this.resultSet.ReadAbsolute(this.firstRecordindex);
                        Debug.Assert(ok, "Failed to seek to position: " + this.firstRecordindex);
                    }
                    else
                    {
                        ok = resultSet.Read();
                    }

                    if (ok)
                    {
                        SymbolInfo info = new SymbolInfo();

                        info.Id = resultSet.GetInt32(0);

                        object image = resultSet.GetValue(1);
                        using (MemoryStream ms = new MemoryStream(image as byte[]))
                        {
                            info.Image = ResizeImage(new Bitmap(ms), this.imageSize);
                        }

                        info.Sound = resultSet.GetValue(2) as byte[];

                        info.Text = resultSet.GetString(3);

                        symbolList.Add(info);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            if (this.DataLoaded != null)
            {
                this.DataLoaded();
            }
        }

        private Image ResizeImage(Image image, Size size)
        {
            //Create a new Bitmap object
            Image canvas = new Bitmap(size.Width, size.Height, PixelFormat.Format16bppRgb555);

            //create an object that will do the drawing operations
            Graphics artist = Graphics.FromImage(canvas);

            // Create rectangle for displaying image.
            Rectangle destRect = new Rectangle(0, 0, size.Width, size.Height);

            // Create rectangle for source image.
            Rectangle srcRect = new Rectangle(0, 0, image.Width, image.Height);

            // Draw image to Graphics.
            artist.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);

            //now the drawing is done, we can discard the artist object
            artist.Dispose();

            //return the picture
            return canvas;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (this.threadExecute != null && this.threadExecute.State == UtilitiesPpc.ThreadExecuteTask.ProcessingState.running)
            {
                this.threadExecute.setProcessingState(UtilitiesPpc.ThreadExecuteTask.ProcessingState.requestAbort);
                this.WaitForFinished();
            }

            this.symbolList.Clear();
        }

        #endregion
    }

    public class SymbolListLoaderManager : IDisposable
    {
        private int currentPage;

        private SymbolListLoader[] symbolLoaderList;

        private SqlCeResultSet resultSet;

        private int totalPages;

        public ArrayList CurrentSymbolList
        {
            get { return this.symbolLoaderList[this.currentPage % this.symbolLoaderList.Length].SymbolList; }
        }

        public SymbolListLoaderManager(Size imageSize, int countPerPage, SqlCeResultSet resultSet, int totalPages)
        {
            this.symbolLoaderList = new SymbolListLoader[3];
            for (int i = 0; i < this.symbolLoaderList.Length; i++)
            {
                this.symbolLoaderList[i] = new SymbolListLoader(imageSize, countPerPage, resultSet);
            }

            this.resultSet = resultSet;

            this.totalPages = totalPages;

            if (totalPages > 0)
            {
                this.symbolLoaderList[0].Load(true, 0);
            }

            if (totalPages > 1)
            {
                this.symbolLoaderList[1].Load(false, 1);
            }
        }

        public void MoveToNextPage()
        {
            this.WaitForFinished();

            this.currentPage++;

            int nextPage = this.currentPage + 1;
            if (nextPage < this.totalPages)
            {
                this.symbolLoaderList[nextPage % this.symbolLoaderList.Length].Load(false, nextPage);
            }
        }

        public void MoveToPreviousPage()
        {
            this.WaitForFinished();

            this.currentPage--;

            int previousPage = this.currentPage - 1;
            if (previousPage >= 0)
            {
                this.symbolLoaderList[previousPage % this.symbolLoaderList.Length].Load(false, previousPage);
            }

        }

        private void WaitForFinished()
        {
            Cursor.Current = Cursors.WaitCursor;

            for (int i = 0; i < this.symbolLoaderList.Length; i++)
            {
                this.symbolLoaderList[i].WaitForFinished();
            }

            Cursor.Current = Cursors.Default;
        }

        #region IDisposable Members

        public void Dispose()
        {
            for (int i = 0; i < this.symbolLoaderList.Length; i++)
            {
                this.symbolLoaderList[i].Dispose();
            }

            GC.Collect();
        }

        #endregion
    }
}