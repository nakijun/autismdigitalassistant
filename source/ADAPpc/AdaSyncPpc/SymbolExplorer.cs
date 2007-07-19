using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.Diagnostics;
using AdaSyncPpc.Properties;

namespace AdaSyncPpc
{
    public partial class SymbolExplorer
    {
        private string cultureName;

        private SymbolQueryTableAdapter tableAdapter;

        private SymbolInfo  selectedCategory;

        private SymbolListForm categoryForm;

        private SymbolListForm symbolForm;
        
        public SymbolInfo  SelectedCategory
        {
            get { return selectedCategory; }
        }

        public SymbolExplorer(string cultureName)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.categoryForm = new SymbolListForm();
            this.categoryForm.Text = Resources.SelectCategory;

            this.categoryForm.SymbolSelected += new SymbolListForm.SymbolSelectedHandler(categoryForm_SymbolSelected);

            this.symbolForm = new SymbolListForm();
            this.symbolForm.SymbolSelected += new AdaSyncPpc.SymbolListForm.SymbolSelectedHandler(symbolForm_SymbolSelected);

            this.cultureName = cultureName;

            this.tableAdapter = new SymbolQueryTableAdapter();

            this.tableAdapter.CategoryCommand.Parameters[0].Value = cultureName;
            this.tableAdapter.SymbolCommand.Parameters[0].Value = cultureName;

            Cursor.Current = Cursors.Default;
        }

        void symbolForm_SymbolSelected(object sender, SymbolInfo selectedSymbol)
        {
            this.symbolForm.DialogResult = DialogResult.OK;
        }

        void categoryForm_SymbolSelected(object sender, SymbolInfo selectedSymbol)
        {
            this.selectedCategory = selectedSymbol;
            this.categoryForm.DialogResult = DialogResult.OK;
        }

        public SymbolInfo BrowseSymbol()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                this.tableAdapter.CategoryCommand.Connection.Open();

                SqlCeCommand cmd = this.tableAdapter.CategoryCommand.Connection.CreateCommand();
                cmd.CommandText = "SELECT count(*) from Category";
                this.categoryForm.TotalSymbols = (int)cmd.ExecuteScalar();
                cmd.Dispose();

                this.categoryForm.ResultSet = this.tableAdapter.CategoryCommand.ExecuteResultSet(ResultSetOptions.Insensitive | ResultSetOptions.Scrollable);

                Cursor.Current = Cursors.Default;

                if (this.categoryForm.ShowDialog() == DialogResult.OK && this.categoryForm.SelectedSymbol != null)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    if (this.categoryForm.ResultSet != null)
                    {
                        this.categoryForm.ResultSet.Close();
                        this.categoryForm.ResultSet = null;
                    }

                    this.tableAdapter.CategoryCommand.Connection.Close();

                    this.tableAdapter.SymbolCommand.Parameters[1].Value = this.categoryForm.SelectedSymbol.Id;
                    this.tableAdapter.SymbolCommand.Connection.Open();

                    SqlCeCommand cmd2 = this.tableAdapter.SymbolCommand.Connection.CreateCommand();
                    cmd2.CommandText = "SELECT count(*) from Symbol WHERE CategoryId=" + this.categoryForm.SelectedSymbol.Id;
                    this.symbolForm.TotalSymbols = (int)cmd2.ExecuteScalar();
                    cmd2.Dispose();

                    this.symbolForm.ResultSet = this.tableAdapter.SymbolCommand.ExecuteResultSet(ResultSetOptions.Insensitive | ResultSetOptions.Scrollable);
                    this.symbolForm.ShowTextOnButtons = this.categoryForm.ShowTextOnButtons;

                    Cursor.Current = Cursors.Default;

                    if (this.symbolForm.ShowDialog() == DialogResult.OK)
                    {
                        this.symbolForm.SelectedSymbol.Category = this.selectedCategory;
                        return this.symbolForm.SelectedSymbol;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                if (this.categoryForm.ResultSet != null)
                {
                    this.categoryForm.ResultSet.Close();
                    this.categoryForm.ResultSet = null;
                }

                this.tableAdapter.CategoryCommand.Connection.Close();

                if (this.symbolForm.ResultSet != null)
                {
                    this.symbolForm.ResultSet.Close();
                    this.symbolForm.ResultSet = null;
                }

                this.tableAdapter.SymbolCommand.Connection.Close();

                if (Cursor.Current != Cursors.Default)
                {
                    Cursor.Current = Cursors.Default;
                }
            }

            return null;
        }
    }
}
