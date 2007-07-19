using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;

namespace UtilitiesPpc
{
    public class DataAdapterEngine
    {
        public static void InitToUpdateIdentityColumn(SqlCeDataAdapter dataAdapter)
        {
            dataAdapter.RowUpdated += new System.Data.SqlServerCe.SqlCeRowUpdatedEventHandler(
                delegate(object sender, System.Data.SqlServerCe.SqlCeRowUpdatedEventArgs e)
                {
                    if (e.StatementType == StatementType.Insert && e.Status == UpdateStatus.Continue)
                    {
                        string cmdText = "SELECT @@IDENTITY FROM [" + e.TableMapping.DataSetTable + "]";
                        SqlCeCommand selectCommand = new SqlCeCommand(cmdText, e.Command.Connection);
                        SqlCeDataReader reader = selectCommand.ExecuteReader();

                        if (reader.Read())
                        {
                            e.Row[e.Row.Table.PrimaryKey[0].ColumnName] = reader[0];
                            e.Row.AcceptChanges();
                        }
                    }
                });
        }

    }
}
