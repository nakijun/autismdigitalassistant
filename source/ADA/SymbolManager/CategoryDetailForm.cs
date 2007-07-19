using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SymbolManager.CategoryDetailDataSetTableAdapters;
using System.Drawing.Imaging;
using System.Media;
using System.IO;
using System.Data.SqlClient;
using SymbolManager.Properties;
using Utilities;

namespace SymbolManager
{
    public partial class CategoryDetailForm : Form
    {
        private const string CATEGORY = "Category";
        private const string LOCALIZED_CATEGORY = "LocalizedCategory";
        private const string LOCALIZED_CATEGORY_NAME = "LocalizedCategoryName";

        private int categoryId;
        private SoundPlayer player;
        private string cultureName;

        public CategoryDetailForm(SymbolDataSet.LocalizedCategoryRow currentLocalizedCategoryRow)
        {
            InitializeComponent();

            // Set up the SoundPlayer object.
            player = new SoundPlayer();

            if (currentLocalizedCategoryRow.RowState == DataRowState.Added)
            {
                categoryId = 0;

                this.BindingContext[this.categoryDetailDataSet, CATEGORY].AddNew();
                DataRowView rowView = this.BindingContext[this.categoryDetailDataSet, CATEGORY].Current as DataRowView;
                CategoryDetailDataSet.CategoryRow currentCategoryRow = rowView.Row as CategoryDetailDataSet.CategoryRow;

                rowView.BeginEdit();
                currentCategoryRow.Name = Guid.NewGuid();
                currentCategoryRow.IsActive = true;
                rowView.EndEdit();
            }
            else
            {
                categoryId = currentLocalizedCategoryRow.CategoryId;
            }

            this.categoryDetailDataSet.Merge(currentLocalizedCategoryRow.Table.DataSet.Tables[LOCALIZED_CATEGORY]);

            this.cultureName = Properties.Settings.Default.CultureName;
        }

        private void CategoryDetailForm_Load(object sender, EventArgs e)
        {
            try
            {
                LocalizedCategoryNameTableAdapter localizedCategoryNameAdapter = new LocalizedCategoryNameTableAdapter();
                localizedCategoryNameAdapter.Fill(this.categoryDetailDataSet.LocalizedCategoryName, categoryId, cultureName);

                if (categoryId > 0)
                {
                    CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();
                    categoryAdapter.Fill(this.categoryDetailDataSet.Category, categoryId);
                }

                byte[] image = this.categoryDetailDataSet.Category.Rows[0]["Image"] as byte[];
                if (image != null)
                {
                    this.pictureBoxImage.Image = ImageEngine.Resize(ImageEngine.FromArray(image), this.pictureBoxImage.Size);
                }

                EnableButtons();
            }
            catch (Exception ex)
            {
                ReportError(ex.Message.ToString());
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.BindingContext[this.categoryDetailDataSet, CATEGORY].EndCurrentEdit();
                this.BindingContext[this.categoryDetailDataSet, LOCALIZED_CATEGORY_NAME].EndCurrentEdit();

                DataRowView view = this.BindingContext[categoryDetailDataSet, CATEGORY].Current as DataRowView;
                CategoryDetailDataSet.CategoryRow row = view.Row as CategoryDetailDataSet.CategoryRow;

                if (row.IsImageNull())
                {
                    this.DialogResult = DialogResult.None;

                    // Initializes the variables to pass to the MessageBox.Show method.

                    string message = Resources.ImageNotSetMessage;
                    string caption = Resources.ErrorCaption;

                    // Displays the MessageBox.

                    MessageBox.Show(this, message, caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                foreach (CategoryDetailDataSet.LocalizedCategoryNameRow localizedNameRow in this.categoryDetailDataSet.LocalizedCategoryName.Rows)
                {
                    if (localizedNameRow.CultureName == this.cultureName)
                    {
                        if (localizedNameRow.IsNameNull() || localizedNameRow.Name.Trim().Length == 0)
                        {
                            this.DialogResult = DialogResult.None;

                            // Initializes the variables to pass to the MessageBox.Show method.

                            string message = string.Format(Resources.CannotBeEmptyMessage, localizedNameRow.Language);
                            string caption = Resources.ErrorCaption;

                            // Displays the MessageBox.

                            MessageBox.Show(this, message, caption, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                            return;
                        }

                        break;
                    }
                }

                CategoryDetailDataSet changes = (CategoryDetailDataSet)categoryDetailDataSet.GetChanges();
                if (changes != null)
                {
                    CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();
                    categoryAdapter.Connection.Open();
                    categoryAdapter.Update(changes);

                    SqlCommand sqlUpdateCommand = new SqlCommand();
                    sqlUpdateCommand.Connection = categoryAdapter.Connection;
                    sqlUpdateCommand.CommandText = @"UPDATE Resource SET [Text] = @Text "
                        + "WHERE (ResourceId = @ResourceId AND CultureId = @CultureId) ";

                    sqlUpdateCommand.Parameters.Add(new SqlParameter("@Text", System.Data.SqlDbType.NText, 0, "Text"));
                    sqlUpdateCommand.Parameters.Add(new SqlParameter("@ResourceId", System.Data.SqlDbType.UniqueIdentifier, 0, "ResourceId"));
                    sqlUpdateCommand.Parameters.Add(new SqlParameter("@CultureId", System.Data.SqlDbType.Int, 0, "CultureId"));

                    SqlCommand sqlInsertCommand = new SqlCommand();
                    sqlInsertCommand.Connection = categoryAdapter.Connection;
                    sqlInsertCommand.CommandText = @"INSERT INTO Resource([Text], ResourceId, CultureId) VALUES(@Text, @ResourceId, @CultureId) ";

                    sqlInsertCommand.Parameters.Add(new SqlParameter("@Text", System.Data.SqlDbType.NText, 0, "Text"));
                    sqlInsertCommand.Parameters.Add(new SqlParameter("@ResourceId", System.Data.SqlDbType.UniqueIdentifier, 0, "ResourceId"));
                    sqlInsertCommand.Parameters.Add(new SqlParameter("@CultureId", System.Data.SqlDbType.Int, 0, "CultureId"));

                    Guid resourceId = row.Name;

                    DataRow[] rows = this.categoryDetailDataSet.LocalizedCategoryName.Select("", "", DataViewRowState.ModifiedCurrent);

                    foreach (DataRow modifiedRow in rows)
                    {
                        if (modifiedRow.IsNull("ResourceId"))
                        {
                            sqlInsertCommand.Parameters["@Text"].Value = modifiedRow["Name"];
                            sqlInsertCommand.Parameters["@ResourceId"].Value = resourceId;
                            sqlInsertCommand.Parameters["@CultureId"].Value = modifiedRow["CultureId"];
                            sqlInsertCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            sqlUpdateCommand.Parameters["@Text"].Value = modifiedRow["Name"];
                            sqlUpdateCommand.Parameters["@ResourceId"].Value = resourceId;
                            sqlUpdateCommand.Parameters["@CultureId"].Value = modifiedRow["CultureId"];
                            sqlUpdateCommand.ExecuteNonQuery();
                        }
                    }

                    categoryAdapter.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                ReportError(ex.Message.ToString());
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void EnableButtons()
        {
            this.buttonCopyImage.Enabled = (this.pictureBoxImage.Image != null);
            this.buttonExportImage.Enabled = (this.pictureBoxImage.Image != null);

            DataRowView view = this.BindingContext[categoryDetailDataSet, CATEGORY].Current as DataRowView;
            byte[] sound = view.Row["Sound"] as byte[];
            this.buttonPlaySound.Enabled = (sound != null);
            this.buttonExportSound.Enabled = (sound != null);
        }

        private Image CreatePicture(Image image)
        {
            Image orginalImage = ImageEngine.Resize(image, image.Size);

            DataRowView view = this.BindingContext[categoryDetailDataSet, CATEGORY].Current as DataRowView;
            view.BeginEdit();
            view.Row["Image"] = ImageEngine.ToArray(orginalImage);
            view.EndEdit();

            return ImageEngine.Resize(image, this.pictureBoxImage.Size);
        }
        
        private void buttonImportImage_Click(object sender, EventArgs e)
        {
            this.BindingContext[this.categoryDetailDataSet, CATEGORY].EndCurrentEdit();

            OpenFileDialog dlg = new OpenFileDialog();

            // Make sure the dialog checks for existence of the 
            // selected file.
            dlg.CheckFileExists = true;

            dlg.Filter = Resources.ImageFileDialogFilter;
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Bitmap image = new Bitmap(dlg.FileName);
                    this.pictureBoxImage.Image = CreatePicture(image);

                    this.EnableButtons();
                }
                catch (Exception ex)
                {
                    ReportError(ex.Message.ToString());
                }
            }
        }

        private void buttonExportImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Filter = Resources.ImageFileDialogFilter;
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (FileStream fs = new FileStream(dlg.FileName, FileMode.Create))
                    {
                        DataRowView view = this.BindingContext[categoryDetailDataSet, CATEGORY].Current as DataRowView;
                        byte[] image = view.Row["Image"] as byte[];
                        fs.Write(image, 0, image.Length);
                    }
                }
                catch (Exception ex)
                {
                    ReportError(ex.Message.ToString());
                }
            }
        }

        private void buttonCopyImage_Click(object sender, EventArgs e)
        {
            if (this.pictureBoxImage.Image != null)
            {
                DataRowView view = this.BindingContext[categoryDetailDataSet, CATEGORY].Current as DataRowView;
                byte[] image = view.Row["Image"] as byte[];
                Clipboard.SetImage(ImageEngine.FromArray(image));
            }
        }

        private void buttonPasteImage_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                this.BindingContext[this.categoryDetailDataSet, CATEGORY].EndCurrentEdit();

                Image image = Clipboard.GetImage();
                this.pictureBoxImage.Image = CreatePicture(image);

                this.EnableButtons();
            }
        }

        private void buttonPlaySound_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView view = this.BindingContext[categoryDetailDataSet, CATEGORY].Current as DataRowView;

                using (MemoryStream ms = new MemoryStream(view.Row["Sound"] as byte[]))
                {
                    player.Stream = ms;
                    player.Play();
                }
            }
            catch (Exception ex)
            {
                ReportError(ex.Message);
            }
        }

        private void buttonImportSound_Click(object sender, EventArgs e)
        {
            this.BindingContext[this.categoryDetailDataSet, CATEGORY].EndCurrentEdit();

            // Create a new OpenFileDialog.
            OpenFileDialog dlg = new OpenFileDialog();

            // Make sure the dialog checks for existence of the 
            // selected file.
            dlg.CheckFileExists = true;

            // Allow selection of .wav files only.
            dlg.Filter = "WAV files (*.wav)|*.wav";
            dlg.DefaultExt = ".wav";

            // Activate the file selection dialog.
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file's path from the dialog.
                try
                {
                    // Assign the selected file's path to 
                    // the SoundPlayer object.  
                    player.SoundLocation = dlg.FileName;

                    // DoRefresh the .wav file.
                    player.Load();
                    player.Play();

                    DataRowView view = this.BindingContext[categoryDetailDataSet, CATEGORY].Current as DataRowView;
                    view.BeginEdit();
                    view.Row["Sound"] = File.ReadAllBytes(dlg.FileName);
                    view.EndEdit();

                    this.EnableButtons();
                }
                catch (Exception ex)
                {
                    ReportError(ex.Message);
                }
            }
        }

        private void buttonExportSound_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            // Allow selection of .wav files only.
            dlg.Filter = "WAV files (*.wav)|*.wav";
            dlg.DefaultExt = ".wav";

            // Activate the file selection dialog.
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file's path from the dialog.
                try
                {
                    DataRowView view = this.BindingContext[categoryDetailDataSet, CATEGORY].Current as DataRowView;
                    File.WriteAllBytes(dlg.FileName, view.Row["Sound"] as byte[]);
                }
                catch (Exception ex)
                {
                    ReportError(ex.Message);
                }
            }
        }

        private void ReportError(string statusMessage)
        {
            // If the caller passed in a message...
            if ((statusMessage != null) && (statusMessage != String.Empty))
            {
                // ...post the caller's message to the status bar.
                 MessageBox.Show(statusMessage, "Category Library Manager",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDeleteSound_Click(object sender, EventArgs e)
        {
            this.BindingContext[this.categoryDetailDataSet, CATEGORY].EndCurrentEdit();

            DataRowView view = this.BindingContext[categoryDetailDataSet, CATEGORY].Current as DataRowView;
            view.BeginEdit();
            view.Row["Sound"] = null;
            view.EndEdit();

            this.EnableButtons();
        }
    }
}