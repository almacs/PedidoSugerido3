using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace PedidoSugeridoLamosa
{
    public class ConnectionLP
    {
         public SqlConnection getConnection(){
            SqlConnection sqlconnection = new SqlConnection();
                    sqlconnection.ConnectionString = ConfigurationManager.ConnectionStrings["lamosaConnectionString"].ToString();
            return sqlconnection;
         }

         public SqlConnection getConnectionVoucher()
         {
             SqlConnection sqlconnection = new SqlConnection();
             sqlconnection.ConnectionString = ConfigurationManager.ConnectionStrings["lamosaVoucherConnectionString"].ToString();
             return sqlconnection;
         }
    }
        
}
