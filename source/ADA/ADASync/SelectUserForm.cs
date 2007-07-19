using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ADASync
{
    public partial class SelectUserForm : Form
    {
        public ADAUserDataSet UserDataSet
        {
            get { return adaUserDataSet1; }
        }

        public SelectUserForm()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            BindingContext[adaUserDataSet1, "Device"].EndCurrentEdit();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}