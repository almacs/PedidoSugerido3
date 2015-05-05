using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

namespace Business
{
    public class BServicio
    {
        private SqlConnection conection;

        public BServicio(SqlConnection con)
        {
            conection = con;
        }

        public DataSet getConsultaVoucher(String id_compania, String id_cliente, String id_entrega, String fecha)
        {
            SqlConnection sqlC = conection;
            try
            {
                DataSet ds = new DataSet();

                SqlCommand sql = new SqlCommand("spConsultaVoucher_s", sqlC);
                sql.CommandTimeout = 0;
                sql.CommandType = CommandType.StoredProcedure;
                SqlParameter opar = new SqlParameter("@pid_cia", id_compania);
                sql.Parameters.Add(opar);
                SqlParameter opar2 = new SqlParameter("@pid_cliente", id_cliente);
                sql.Parameters.Add(opar2);
                SqlParameter opar3 = new SqlParameter("@pid_entrega", id_entrega);
                sql.Parameters.Add(opar3);
                SqlParameter opar4 = new SqlParameter("@pFecha", fecha);
                sql.Parameters.Add(opar4);
                SqlDataAdapter data = new SqlDataAdapter(sql);
                data.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlC.Close();
            }
        }
    }
}
