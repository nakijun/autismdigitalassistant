using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace UtilitiesPpc
{
    public partial class SymbolListView : UserControl
    {
        const int NUMBER_OF_ROWS = 3;
        const int NUMBER_OF_COLUMNS = 3;
        const int MAX_SYMBOLS_IN_PAGE = NUMBER_OF_COLUMNS * NUMBER_OF_ROWS;

        private int _currentPage;
        private int _firstSymbolInPage;
        private int _lastSymbolInPage;

        public class SymbolListItem
        {
            private bool _checked;

            public bool Checked
            {
                get { return _checked; }
                set { _checked = value; }
            }

            private string _text;

            public string Text
            {
                get { return _text; }
                set { _text = value; }
            }

            private Image _image;

            public Image Image
            {
                get { return _image; }
                set { _image = value; }
            }

            private byte[] _sound;

            public byte[] Sound
            {
                get { return _sound; }
                set { _sound = value; }
            }

            internal void Dispose()
            {
                _image.Dispose();
            }
        }

        public class SymbolListItemCollection
        {
            private List<SymbolListItem> _itemList = new List<SymbolListItem>();

            private event EventHandler _itemCleard;

            public event EventHandler ItemCleard
            {
                add { _itemCleard += value; }
                remove { _itemCleard -= value; }
            }

            public int Count
            {
                get { return _itemList.Count; }
            }

            public SymbolListItem this[int index]
            {
                get { return _itemList[index]; }
                set { _itemList[index] = value; }
            }

            public void Clear()
            {
                foreach (SymbolListItem item in _itemList)
                {
                    item.Dispose();
                }

                _itemList.Clear();

                if (_itemCleard != null)
                {
                    _itemCleard(this, new EventArgs());
                }
            }


            public void Add(SymbolListItem item)
            {
                _itemList.Add(item);
            }
        }

        private SymbolListItemCollection items;

        public SymbolListItemCollection Items
        {
            get { return items; }
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                int oldSelectedIndex = _selectedIndex;

                _selectedIndex = value;

                if (oldSelectedIndex != _selectedIndex)
                {
                    if (_selectedIndex > _lastSymbolInPage || _selectedIndex < _firstSymbolInPage)
                    {
                        RecalcCurrentPage();
                    }

                    Debug.WriteLine(string.Format("Select item: {0}", _selectedIndex));
                    if (_selectedIndexChanged != null)
                    {
                        _selectedIndexChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        private void RecalcCurrentPage()
        {
            _currentPage = _selectedIndex / MAX_SYMBOLS_IN_PAGE;

            _firstSymbolInPage = _currentPage * MAX_SYMBOLS_IN_PAGE;
            _lastSymbolInPage = _firstSymbolInPage + MAX_SYMBOLS_IN_PAGE - 1;

            if (_lastSymbolInPage >= items.Count)
            {
                _lastSymbolInPage = items.Count - 1;
            }
        }

        private event EventHandler _itemActivated;

        public event EventHandler ItemActivated
        {
            add { _itemActivated += value; }
            remove { _itemActivated -= value; }
        }

        private event EventHandler _selectedIndexChanged;

        public event EventHandler SelectedIndexChanged
        {
            add { _selectedIndexChanged += value; }
            remove { _selectedIndexChanged -= value; }
        }

        public SymbolListView()
        {
            InitializeComponent();

            items = new SymbolListItemCollection();
            items.ItemCleard += new EventHandler(items_ItemCleard);
            _selectedIndex = -1;
        }

        void items_ItemCleard(object sender, EventArgs e)
        {
            SelectedIndex = -1;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Calculate required dimensions
            Size faceSize = this.ClientRectangle.Size;
            int width = (int)(faceSize.Width / NUMBER_OF_COLUMNS);
            int height = (int)(faceSize.Height / NUMBER_OF_ROWS);
            int x, y;

            // Paint clock face
            using (Pen normalPen = new Pen(Color.Black, 1))
            {
                using (Pen selectedPen = new Pen(Color.Red, 2))
                {
                    using (Brush brush = new SolidBrush(Color.Green))
                    {
                        using (Font font = new Font("Times New Roman", 8, FontStyle.Regular))
                        {
                            int count = _currentPage * MAX_SYMBOLS_IN_PAGE;
                            _firstSymbolInPage = count;
                            _lastSymbolInPage = count - 1;

                            for (int i = 0; i < NUMBER_OF_ROWS; i++)
                            {
                                for (int j = 0; j < NUMBER_OF_COLUMNS; j++)
                                {
                                    x = j * width;
                                    y = i * height;

                                    Pen pen = (count == SelectedIndex ? selectedPen : normalPen);
                                    Rectangle borderRect = new Rectangle(x, y, width - (int)pen.Width, height - (int)pen.Width);
                                    g.DrawRectangle(pen, borderRect);

                                    borderRect = new Rectangle(x + (int)pen.Width, y + (int)pen.Width, borderRect.Width - (int)pen.Width, borderRect.Height - (int)pen.Width);
                                    //g.FillRectangle(count == SelectedIndex ? selectedBrush : normalBrush, borderRect);

                                    if (count < items.Count)
                                    {
                                        Image image = items[count].Image;

                                        if (image != null)
                                        {
                                            Rectangle srcRect = new Rectangle(0, 0, image.Size.Width, image.Size.Height);
                                            Rectangle destRect = GetDestinationRect(srcRect, borderRect);
                                            g.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);
                                        }

                                        string text = items[count].Text;

                                        if (text != null)
                                        {
                                            g.DrawString(text, font, brush, borderRect.X, borderRect.Y);
                                        }

                                        if (items[count].Checked)
                                        {
                                            Point[] points = new Point[6];
                                            const int TICK_WIDTH = 10;

                                            points[0].X = borderRect.X;
                                            points[0].Y = borderRect.Y + borderRect.Height * 2 / 3;

                                            points[1].X = borderRect.X + borderRect.Width / 2;
                                            points[1].Y = borderRect.Y + borderRect.Height;

                                            points[2].X = borderRect.X + borderRect.Width;
                                            points[2].Y = borderRect.Y + borderRect.Height / 3;

                                            points[3].X = points[2].X;
                                            points[3].Y = points[2].Y - TICK_WIDTH;

                                            points[4].X = points[1].X;
                                            points[4].Y = points[1].Y - TICK_WIDTH;

                                            points[5].X = points[0].X;
                                            points[5].Y = points[0].Y - TICK_WIDTH;

                                            g.FillPolygon(brush, points);
                                        }

                                        _lastSymbolInPage++;
                                    }

                                    count++;
                                }
                            }
                        }
                    }
                }
            }

            // Let the base class fire the Paint event
            base.OnPaint(e);
        }

        private Rectangle GetDestinationRect(Rectangle srcRect, Rectangle borderRect)
        {
            int width, height;
            double widthRatio = (double)srcRect.Width / borderRect.Width;
            double heightRatio = (double)srcRect.Height / borderRect.Height;

            if (widthRatio > heightRatio)
            {
                width = borderRect.Width;
                height = (int)(srcRect.Height / widthRatio);
            }
            else
            {
                width = (int)(srcRect.Width / heightRatio);
                height = borderRect.Height;
            }

            int dx = (borderRect.Width - width) / 2;
            int dy = (borderRect.Height - height) / 2;

            return new Rectangle(borderRect.X + dx, borderRect.Y + dy, width, height);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (_lastSymbolInPage > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.ActivateSelectedItem();
                }
                else
                {
                    int nextSymbol = -1;
                    bool pageChanged = true;

                    if ((this.SelectedIndex == _firstSymbolInPage) && ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Left)))
                    {
                        nextSymbol = this.MoveToPreviousPage();
                    }
                    else if ((this.SelectedIndex == _lastSymbolInPage) && ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.Right)))
                    {
                        nextSymbol = this.MoveToNextPage();
                    }
                    else
                    {
                        int temp = -1;
                        pageChanged = false;

                        if (e.KeyCode == Keys.Up)
                        {
                            temp = this.SelectedIndex - NUMBER_OF_COLUMNS;
                        }
                        else if (e.KeyCode == Keys.Down)
                        {
                            temp = this.SelectedIndex + NUMBER_OF_COLUMNS;
                        }
                        else if (e.KeyCode == Keys.Left)
                        {
                            temp = this.SelectedIndex - 1;
                        }
                        else if (e.KeyCode == Keys.Right)
                        {
                            temp = this.SelectedIndex + 1;
                        }

                        if (temp >= _firstSymbolInPage && temp <= _lastSymbolInPage)
                        {
                            nextSymbol = temp;
                        }
                    }

                    if (nextSymbol >= 0)
                    {
                        if (pageChanged)
                        {
                            this.Invalidate();
                        }
                        else
                        {
                            this.Invalidate(GetBorderRect(SelectedIndex));
                            this.Invalidate(GetBorderRect(nextSymbol));
                        }

                        this.SelectedIndex = nextSymbol;
                    }
                }
            }
        }

        public void SelectCenterSymbol()
        {
            Invalidate(GetBorderRect(SelectedIndex));
            SelectedIndex = GetCenterSymbolIndex();
            Invalidate(GetBorderRect(SelectedIndex));
        }

        private void ActivateSelectedItem()
        {
            Debug.WriteLine(string.Format("Activate item: {0}", SelectedIndex));

            if (_itemActivated != null)
            {
                _itemActivated(this, EventArgs.Empty);
            }
        }

        private int MoveToNextPage()
        {
            if ((_currentPage + 1) * MAX_SYMBOLS_IN_PAGE < items.Count)
            {
                _currentPage++;
                return GetCenterSymbolIndex();
            }

            return -1;
        }

        private int GetCenterSymbolIndex()
        {
            int start = _currentPage * MAX_SYMBOLS_IN_PAGE;
            int center = start + MAX_SYMBOLS_IN_PAGE / 2;

            if (center < items.Count)
            {
                return center;
            }

            return start;
        }

        private int MoveToPreviousPage()
        {
            if (_currentPage > 0)
            {
                _currentPage--;
                return GetCenterSymbolIndex();
            }

            return -1;
        }

        private void SymbolListView_MouseDown(object sender, MouseEventArgs e)
        {
            Size faceSize = this.ClientRectangle.Size;
            int width = (int)(faceSize.Width / NUMBER_OF_COLUMNS);
            int height = (int)(faceSize.Height / NUMBER_OF_ROWS);
            int nextSymbol = _firstSymbolInPage + e.X / width + e.Y / height * NUMBER_OF_COLUMNS;

            if (nextSymbol <= _lastSymbolInPage)
            {
                Invalidate(GetBorderRect(SelectedIndex));
                this.SelectedIndex = nextSymbol;
                Invalidate(GetBorderRect(SelectedIndex));
                this.ActivateSelectedItem();
            }
        }

        private Rectangle GetBorderRect(int index)
        {
            Size faceSize = this.ClientRectangle.Size;
            int width = (int)(faceSize.Width / NUMBER_OF_COLUMNS);
            int height = (int)(faceSize.Height / NUMBER_OF_ROWS);
            int x, y;

            index = index - _currentPage * MAX_SYMBOLS_IN_PAGE;

            x = (index % NUMBER_OF_COLUMNS) * width;
            y = (index / NUMBER_OF_COLUMNS) * height;

            return new Rectangle(x, y, width, height);
        }
    }
}
