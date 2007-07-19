using System.Data;
using System.Data.SqlServerCe;
using UtilitiesPpc;
namespace AdaWorkSystemPpc
{
    partial class ADAMobileDataSet
    {
    }

    namespace ADAMobileDataSetTableAdapters
    {
        public partial class ScheduleTableAdapter : System.ComponentModel.Component
        {
            public void Init()
            {
                DataAdapterEngine.InitToUpdateIdentityColumn(Adapter);
            }
        }

        public partial class ActivityTableAdapter : System.ComponentModel.Component
        {
            public void Init()
            {
                DataAdapterEngine.InitToUpdateIdentityColumn(Adapter);
            }
        }
    }
}
