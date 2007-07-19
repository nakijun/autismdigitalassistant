using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UtilitiesPpc;

namespace AdaTimerPpc
{
    public partial class SettingForm : AdaBaseForm
    {
        private bool isPreAlarmEnabled;

        public bool IsPreAlarmEnabled
        {
            get { return isPreAlarmEnabled; }
            set { isPreAlarmEnabled = value; }
        }

        private int alarmPeriod;

        public int AlarmPeriod
        {
            get { return alarmPeriod; }
            set { alarmPeriod = value; }
        }

        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            this.checkBoxAlarmEnabled.Checked = this.isPreAlarmEnabled;
            this.numericUpDownAlarmPeriod.Value = this.alarmPeriod;

            this.RefreshControls();
        }

        private void menuItemCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void checkBoxAlarmEnabled_CheckStateChanged(object sender, EventArgs e)
        {
            this.isPreAlarmEnabled = this.checkBoxAlarmEnabled.Checked;
            this.RefreshControls();
        }

        private void RefreshControls()
        {
            this.numericUpDownAlarmPeriod.Enabled = this.isPreAlarmEnabled;
        }

        private void SettingForm_Closing(object sender, CancelEventArgs e)
        {
            this.alarmPeriod = Decimal.ToInt32(this.numericUpDownAlarmPeriod.Value);
        }
    }
}