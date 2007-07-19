using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ADACommunicator.ADACommunicatorDataSetTableAdapters;
using System.Threading;
using System.Globalization;
using ADACommunicator.Properties;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;
using Utilities;

namespace ADACommunicator
{
    public partial class MainForm : Form
    {
        private const string ENGLISH_CULTURE = "en";
        private const string SIMPLIFIED_CHINESE_CULTURE = "zh-CHS";
        private const string TRADITIONAL_CHINESE_CULTURE = "zh-CHT";

        private string _cultureName;

        private ADACommunicatorDataSet.ScenarioRow _currentScenarioRow;
        private ADACommunicatorDataSet.TextRow _currentTextRow;

        public MainForm()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.CultureName);

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ChangeCulture(Properties.Settings.Default.CultureName);
        }

        private void LoadDataSet()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                communicatorDataSet.EnforceConstraints = false;

                SymbolTableAdapter symbolAdapter = new SymbolTableAdapter();
                symbolAdapter.Fill(communicatorDataSet.Symbol);

                ScenarioTableAdapter scenarioAdapter = new ScenarioTableAdapter();
                scenarioAdapter.Fill(communicatorDataSet.Scenario);

                TextTableAdapter textAdapter = new TextTableAdapter();
                textAdapter.Fill(communicatorDataSet.Text);

                communicatorDataSet.EnforceConstraints = true;
                communicatorDataSet.AcceptChanges();
            }
            catch (Exception ex)
            {
                ReportError(ex.Message.ToString());
            }

            RefreshScenarioListView();

            this.EnableButtons();

            Cursor.Current = Cursors.Default;
        }

        private void EnableButtons()
        {
            this.buttonEditText.Enabled = (this.listViewText.SelectedIndices.Count > 0);
            this.buttonDeleteText.Enabled = (this.listViewText.SelectedIndices.Count > 0);

            this.buttonEditSenario.Enabled = (this.listViewScenario.SelectedIndices.Count > 0);
            this.buttonDeleteSenario.Enabled = (this.listViewScenario.SelectedIndices.Count > 0);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadDataSet();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RefreshScenarioListView()
        {
            this.toolStripStatusInfo.Text = Resources.RefreshCategoryListView;
            this.statusStripInfo.Refresh();

            this.listViewScenario.Clear();
            this.imageListScenario.Images.Clear();
            DataRow[] rows = this.communicatorDataSet.Scenario.Select(@"IsActive = 'true'", "[Name] ASC");

            int selectedScenario = 0;

            foreach (ADACommunicatorDataSet.ScenarioRow row in rows)
            {
                string name = row.Name;
                byte[] image = row.IsSymbolIdNull() ? null : row.SymbolRow.Image;

                ListViewItem item;

                if (image != null)
                {
                    using (MemoryStream ms = new MemoryStream(image))
                    {
                        item = this.listViewScenario.Items.Add(name, this.imageListScenario.Images.Count);
                        this.imageListScenario.Images.Add(new Bitmap(ms));
                    }
                }
                else
                {
                    item = this.listViewScenario.Items.Add(name);
                }

                item.Tag = row;

                if (_currentScenarioRow == row)
                {
                    selectedScenario = this.listViewScenario.Items.Count - 1;
                }
            }

            if (this.listViewScenario.Items.Count > 0)
            {
                this.listViewScenario.SelectedIndices.Clear();
                this.listViewScenario.SelectedIndices.Add(selectedScenario);
            }
            else
            {
                _currentScenarioRow = null;
            }

            this.toolStripStatusInfo.Text = Resources.Ready;
        }

        private void RefreshTextListView()
        {
            int selectedText = 0;

            this.toolStripStatusInfo.Text = Resources.RefreshCategoryListView;
            this.statusStripInfo.Refresh();

            this.listViewText.Clear();
            this.imageListText.Images.Clear();
            DataRow[] rows = this.communicatorDataSet.Text.Select(
                "ScenarioId = " + _currentScenarioRow.ScenarioId, "[Name] ASC");

            foreach (ADACommunicatorDataSet.TextRow row in rows)
            {
                string name = row.Name;
                byte[] image = row.IsSymbolIdNull() ? null : row.SymbolRow.Image;

                ListViewItem item;

                if (image != null)
                {
                    using (MemoryStream ms = new MemoryStream(image))
                    {
                        item = this.listViewText.Items.Add(name, this.imageListText.Images.Count);
                        this.imageListText.Images.Add(new Bitmap(ms));
                    }
                }
                else
                {
                    item = this.listViewText.Items.Add(name);
                }

                item.Tag = row;

                if (_currentTextRow == row)
                {
                    selectedText = this.listViewText.Items.Count - 1;
                }
            }

            if (this.listViewText.Items.Count > 0)
            {
                this.listViewText.SelectedIndices.Clear();
                this.listViewText.SelectedIndices.Add(selectedText);
            }
            else
            {
                _currentTextRow = null;
            }

            this.toolStripStatusInfo.Text = Resources.Ready;
        }

        private void ChangeCulture(string cultureName)
        {
            if (cultureName != this._cultureName)
            {
                this._cultureName = cultureName;
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

            if (communicatorDataSet.HasChanges())
            {
                string statusMessage = "Data has been changed. Do you want to save it?";

                DialogResult result = MessageBox.Show(statusMessage, this.Text,
                   MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveDataSet();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void buttonAddScenario_Click(object sender, EventArgs e)
        {
            ScenarioDetailForm f = new ScenarioDetailForm();
            f.CommunicatorDataSet.Merge(communicatorDataSet);

            ADACommunicatorDataSet.ScenarioRow newScenarioRow = f.CommunicatorDataSet.Scenario.NewScenarioRow();
            newScenarioRow.Name = "";
            newScenarioRow.IsActive = true;
            f.CommunicatorDataSet.Scenario.AddScenarioRow(newScenarioRow);

            f.CommunicatorDataSet.DefaultViewManager.DataViewSettings["Scenario"].RowFilter = "ScenarioId=" + newScenarioRow.ScenarioId;

            if (f.ShowDialog() == DialogResult.OK)
            {
                communicatorDataSet.Merge(f.CommunicatorDataSet);
                _currentScenarioRow = communicatorDataSet.Scenario.FindByScenarioId(newScenarioRow.ScenarioId);
                RefreshScenarioListView();
            }
        }

        private void buttonEditScenario_Click(object sender, EventArgs e)
        {
            ScenarioDetailForm f = new ScenarioDetailForm();
            f.CommunicatorDataSet.Merge(communicatorDataSet);

            f.CommunicatorDataSet.DefaultViewManager.DataViewSettings["Scenario"].RowFilter =
                "ScenarioId=" + _currentScenarioRow.ScenarioId;

            if (f.ShowDialog() == DialogResult.OK)
            {
                communicatorDataSet.Merge(f.CommunicatorDataSet);
                RefreshScenarioListView();
            }
        }

        private void buttonDeleteScenario_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are sure to delete " + _currentScenarioRow.Name + "?",
                "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _currentScenarioRow.IsActive = false;
                RefreshScenarioListView();
            }
        }

        private void buttonAddText_Click(object sender, EventArgs e)
        {
            TextDetailForm f = new TextDetailForm();
            f.CommunicatorDataSet.Merge(communicatorDataSet);

            ADACommunicatorDataSet.TextRow newTextRow = f.CommunicatorDataSet.Text.NewTextRow();
            newTextRow.ScenarioId = _currentScenarioRow.ScenarioId;
            newTextRow.Name = "";
            newTextRow.Descripton = "";
            f.CommunicatorDataSet.Text.AddTextRow(newTextRow);

            f.CommunicatorDataSet.DefaultViewManager.DataViewSettings["Text"].RowFilter = "TextId=" + newTextRow.TextId;

            if (f.ShowDialog() == DialogResult.OK)
            {
                communicatorDataSet.Merge(f.CommunicatorDataSet);
                _currentTextRow = communicatorDataSet.Text.FindByTextId(newTextRow.TextId);
                RefreshTextListView();
            }
        }

        private void buttonEditText_Click(object sender, EventArgs e)
        {
            TextDetailForm f = new TextDetailForm();
            f.CommunicatorDataSet.Merge(communicatorDataSet);

            f.CommunicatorDataSet.DefaultViewManager.DataViewSettings["Text"].RowFilter = "TextId=" + _currentTextRow.TextId;

            if (f.ShowDialog() == DialogResult.OK)
            {
                communicatorDataSet.Merge(f.CommunicatorDataSet);
                RefreshTextListView();
            }
        }

        private void buttonDeleteText_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are sure to delete " + _currentTextRow.Name + "?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _currentTextRow.Delete();
                RefreshTextListView();
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

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        private void listViewScenario_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                this.listViewScenario.Update();
                _currentScenarioRow = e.Item.Tag as ADACommunicatorDataSet.ScenarioRow;
                RefreshTextListView();
            }
        }

        private void listViewText_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                _currentTextRow = e.Item.Tag as ADACommunicatorDataSet.TextRow;
            }
        }

        private void listViewScenario_ItemActivate(object sender, EventArgs e)
        {
            this.buttonEditScenario_Click(sender, e);
        }

        private void listViewText_ItemActivate(object sender, EventArgs e)
        {
            this.buttonEditText_Click(sender, e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDataSet();
        }

        private void SaveDataSet()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                communicatorDataSet.EnforceConstraints = false;

                ScenarioTableAdapter scenarioAdapter = new ScenarioTableAdapter();
                scenarioAdapter.Update(communicatorDataSet);

                TextTableAdapter textAdapter = new TextTableAdapter();
                textAdapter.Update(communicatorDataSet);

                communicatorDataSet.EnforceConstraints = true;
                communicatorDataSet.AcceptChanges();
            }
            catch (Exception ex)
            {
                ReportError(ex.Message.ToString());
            }

            Cursor.Current = Cursors.Default;
        }

    }
}