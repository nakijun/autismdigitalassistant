using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ADASchedule
{
    public partial class SelectDateForm : Form
    {
        private SelectionRange selectionRange;
        public SelectionRange SelectionRange
        {
            get { return selectionRange; }
        }

        public SelectDateForm()
        {
            InitializeComponent();
            selectionRange = new SelectionRange(DateTime.Now.Date, DateTime.Now.Date);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            selectionRange = monthCalendar1.SelectionRange;
            DialogResult = DialogResult.OK;
        }

        private void SelectDateForm_Load(object sender, EventArgs e)
        {
            monthCalendar1.SelectionRange = selectionRange;
        }
    }
}