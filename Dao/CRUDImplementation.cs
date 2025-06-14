using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Dao
{
    public class CRUDImplementation : IServiceProvider
    {
        private static string connectionString = DBPropertyUtil.GetConnectionString();
        SqlConnection con=DBConnUtil.GetDbConnection(connectionString);
    }
}
