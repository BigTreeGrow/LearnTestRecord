using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using SqlSugar;

namespace thinger.AutomaticStoreMotionDAL
{
    public class SqlSugarHelper
    {
        public static string ConnectionString = string.Empty;


        public static SqlSugarClient SqlSugarClient
        {
            get
            {
                return new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = ConnectionString,
                    DbType = DbType.Sqlite,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.SystemTable
                });

            }
        }

    }
}

