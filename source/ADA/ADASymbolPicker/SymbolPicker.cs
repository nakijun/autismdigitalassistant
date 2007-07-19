using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ADADataAccess;

namespace ADASymbolPicker
{
    public class SymbolPicker
    {
        public SymbolDataSet.LocalizedSymbolRow PickSymbol(Form parentForm, int currentSymbolId)
        {
            SymbolPickerForm f = new SymbolPickerForm(currentSymbolId);

            if (DialogResult.OK == f.ShowDialog(parentForm))
            {
                return f.PickedSymbol;
            }

            return null;
        }
    }
}
