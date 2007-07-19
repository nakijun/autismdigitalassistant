using System;
using System.Collections.Generic;
using System.Text;
using AdaSyncPpc.ADAMobileDataSetTableAdapters;
using System.Data.SqlServerCe;

namespace AdaSyncPpc
{
    public class SymbolQueryTableAdapter : QueriesTableAdapter
    {
        private SqlCeCommand categoryCommand;

        private SqlCeCommand symbolCommand;

        public SymbolQueryTableAdapter()
        {
            using (CultureTableAdapter cta = new CultureTableAdapter())
            {
                categoryCommand = this.CommandCollection[0] as SqlCeCommand;
                categoryCommand.Connection.ConnectionString = cta.Connection.ConnectionString;

                symbolCommand = this.CommandCollection[1] as SqlCeCommand;
                symbolCommand.Connection.ConnectionString = cta.Connection.ConnectionString;
            }
        }

        public SqlCeCommand CategoryCommand
        {
            get
            {
                return categoryCommand;
            }
        }

        public SqlCeCommand SymbolCommand
        {
            get
            {
                return this.symbolCommand;
            }
        }
    }
}
