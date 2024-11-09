using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinger.AutomaticStoreMotionDAL
{

    public class SqlSugarService
    {
        public static void SetConnectionString(string ConnectionString)
        {
            SqlSugarHelper.ConnectionString = ConnectionString;
        }

    }
}
