using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    public static class DBHelper
    {
        private static readonly string _connString =
            ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connString);
        }
    }
}
