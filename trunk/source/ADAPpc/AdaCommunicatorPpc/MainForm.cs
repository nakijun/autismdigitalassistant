using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UtilitiesPpc;
using AdaCommunicatorPpc.ADAMobileDataSetTableAdapters;
using Microsoft.Win32;

namespace AdaCommunicatorPpc
{
    public partial class MainForm : AdaBaseForm
    {
        private ADAMobileDataSet.ScenarioRow _scenarioRow;
        private FliteTTS _tts;

        public MainForm()
        {
            InitializeComponent();

            this.symbolListView1.SelectedIndexChanged += new EventHandler(symbolListView1_SelectedIndexChanged);
            this.symbolListView1.ItemActivated += new EventHandler(symbolListView1_ItemActivated);
            _tts = new FliteTTS();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadDataSet();
        }

        private void LoadDataSet()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                adaScenarioDataSet1.EnforceConstraints = false;

                SymbolTableAdapter symbolAdapter = new SymbolTableAdapter();
                symbolAdapter.Fill(adaScenarioDataSet1.Symbol);

                ScenarioTableAdapter scenarioAdapter = new ScenarioTableAdapter();
                scenarioAdapter.Fill(adaScenarioDataSet1.Scenario);

                TextTableAdapter textAdapter = new TextTableAdapter();
                textAdapter.Fill(adaScenarioDataSet1.Text);

                adaScenarioDataSet1.EnforceConstraints = true;
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }

            RefreshViews();

            Cursor.Current = Cursors.Default;
        }

        private void RefreshViews()
        {
            try
            {
                if (_scenarioRow != null)
                {
                    ShowTextButtons(_scenarioRow);
                }
                else
                {
                    ShowScenarios();
                }
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }
        }

        void symbolListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (symbolListView1.SelectedIndex < 0)
            {
                textBox1.Text = "";
            }
            else if (_scenarioRow != null)
            {
                DataRow[] dataRows = adaScenarioDataSet1.Text.Select("ScenarioId=" + _scenarioRow.ScenarioId, "Name ASC");
                ADAMobileDataSet.TextRow textRow = dataRows[symbolListView1.SelectedIndex] as ADAMobileDataSet.TextRow;
                textBox1.Text = textRow.Descripton;
            }
            else
            {
                DataRow[] dataRows = adaScenarioDataSet1.Scenario.Select("", "Name ASC");
                ADAMobileDataSet.ScenarioRow senarioRow = dataRows[symbolListView1.SelectedIndex] as ADAMobileDataSet.ScenarioRow;
                textBox1.Text = senarioRow.Name;
            }
        }

        private void symbolListView1_ItemActivated(object sender, EventArgs args)
        {
            if (_scenarioRow != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                _tts.SayIt(textBox1.Text);
                Cursor.Current = Cursors.Default;
            }
            else
            {
                DataRow[] dataRows = adaScenarioDataSet1.Scenario.Select("", "Name ASC");
                ADAMobileDataSet.ScenarioRow row = dataRows[symbolListView1.SelectedIndex] as ADAMobileDataSet.ScenarioRow;
                ShowTextButtons(row);
            }
        }

        private void ShowScenarios()
        {
            ADAMobileDataSet.ScenarioRow lastScenarioRow = null;

            if (_scenarioRow != null)
            {
                lastScenarioRow = _scenarioRow;
                _scenarioRow = null;
            }

            symbolListView1.Items.Clear();

            DataRow[] dataRows = adaScenarioDataSet1.Scenario.Select("", "Name ASC");
            int count = dataRows.Length;

            int selectedScenario = (count > 0 ? 0 : -1);

            for (int i = 0; i < count; i++)
            {
                ADAMobileDataSet.ScenarioRow row = dataRows[i] as ADAMobileDataSet.ScenarioRow;
                SymbolListView.SymbolListItem item = new SymbolListView.SymbolListItem();

                if (!row.IsSymbolIdNull())
                {
                    if (!row.SymbolRow.IsImageNull())
                    {
                        item.Image = ImageEngine.FromArray(row.SymbolRow.Image);
                    }

                    if (!row.SymbolRow.IsSoundNull())
                    {
                        item.Sound = row.SymbolRow.Sound;
                    }
                }

                item.Text = row.Name;

                symbolListView1.Items.Add(item);

                if (lastScenarioRow == row)
                {
                    selectedScenario = i;
                }
            }

            if (selectedScenario >= 0)
            {
                symbolListView1.SelectedIndex = selectedScenario;
            }

            menuItemSelect.Text = "Select";
            menuItemExit.Text = "Exit";

            this.Text = "Select Scenario";
            symbolListView1.Invalidate();
            symbolListView1.Focus();
        }

        private void ShowTextButtons(ADAMobileDataSet.ScenarioRow scenarioRow)
        {
            int scenarioId = scenarioRow.ScenarioId;

            _scenarioRow = scenarioRow;

            symbolListView1.Items.Clear();

            DataRow[] dataRows = adaScenarioDataSet1.Text.Select("ScenarioId=" + scenarioId, "Name ASC");
            int count = dataRows.Length;

            for (int i = 0; i < count; i++)
            {
                ADAMobileDataSet.TextRow textRow = dataRows[i] as ADAMobileDataSet.TextRow;
                SymbolListView.SymbolListItem item = new SymbolListView.SymbolListItem();

                if (!textRow.IsSymbolIdNull())
                {
                    if (!textRow.SymbolRow.IsImageNull())
                    {
                        item.Image = ImageEngine.FromArray(textRow.SymbolRow.Image);
                    }

                    if (!textRow.SymbolRow.IsSoundNull())
                    {
                        item.Sound = textRow.SymbolRow.Sound;
                    }
                }

                item.Text = textRow.Name;

                symbolListView1.Items.Add(item);
            }

            if (count > 0)
            {
                symbolListView1.SelectedIndex = 0;
            }

            symbolListView1.Invalidate();
            symbolListView1.Focus();

            menuItemSelect.Text = "Detail";
            menuItemExit.Text = "Back";

            this.Text = scenarioRow.Name;
        }

        private void ShowDetail(int index)
        {
            TextDetailForm detailForm = new TextDetailForm();
            DataRow[] dataRows = adaScenarioDataSet1.Text.Select("ScenarioId=" + _scenarioRow.ScenarioId, "Name ASC");
            detailForm.TextRow = dataRows[index] as ADAMobileDataSet.TextRow;

            detailForm.ShowDialog();
        }

        private void ReportError(Exception ex)
        {
            string statusMessage = ex.Message.ToString() + "\r\n" + ex.StackTrace;

            MessageBox.Show(statusMessage, this.Text);
        }

        private void menuItemSelect_Click(object sender, EventArgs e)
        {
            if (_scenarioRow == null)
            {
                DataRow[] dataRows = adaScenarioDataSet1.Scenario.Select("", "Name ASC");
                ADAMobileDataSet.ScenarioRow row = dataRows[symbolListView1.SelectedIndex] as ADAMobileDataSet.ScenarioRow;
                ShowTextButtons(row);
            }
            else
            {
                ShowDetail(symbolListView1.SelectedIndex);
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            if (_scenarioRow == null)
            {
                Close();
            }
            else
            {
                ShowScenarios();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            menuItemSelect.Enabled = (textBox1.Text.Length > 0);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                symbolListView1.Focus();
            }
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
            if (_scenarioRow != null)
            {
                e.Cancel = true;
                ShowScenarios();
            }
        }

    }
}