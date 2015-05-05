using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Web;
using System.Web.UI.WebControls;

namespace Business
{
    public class BRelacionCP
    {
        private SqlConnection conection;

        public BRelacionCP(SqlConnection con)
        {
            this.conection = con;
        }



        public DataSet setCustomerItems(string id_cliente, string id_comp, string Xml, string id_usuario, string id_entrega, string bandera, string des, string archivo, string strarchivousuario)
        {
            SqlConnection sqlC = conection;

            try
            {
                DataSet ds = new DataSet();
                SqlCommand sql = new SqlCommand("SpGuardaArtRelacion", sqlC);
                sql.CommandTimeout = 0;
                sql.CommandType = CommandType.StoredProcedure;
                SqlParameter opar = new SqlParameter("@pxml", Xml);
                sql.Parameters.Add(opar);
                SqlParameter opar2 = new SqlParameter("@pid_usuario", id_usuario);
                sql.Parameters.Add(opar2);
                SqlParameter opar3 = new SqlParameter("@pid_cliente", id_cliente);
                sql.Parameters.Add(opar3);
                SqlParameter opar4 = new SqlParameter("@pid_comp", id_comp);
                sql.Parameters.Add(opar4);
                SqlParameter opar5 = new SqlParameter("@pid_entrega", id_entrega);
                sql.Parameters.Add(opar5);
                SqlParameter opar6 = new SqlParameter("@ban", bandera);
                sql.Parameters.Add(opar6);
                SqlParameter opar7 = new SqlParameter("@delItems", des);
                sql.Parameters.Add(opar7);
                SqlParameter opar8 = new SqlParameter("@archivo", archivo);
                sql.Parameters.Add(opar8);
                SqlParameter opar9 = new SqlParameter("@archivo_cliente", strarchivousuario);
                sql.Parameters.Add(opar9);

                SqlDataAdapter data = new SqlDataAdapter(sql);
                data.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                // return null;
                throw ex;
            }
            finally
            {
                sqlC.Close();
            }
        }
        

        public DataSet getCustomerItems(String id_cliente, String id_cia, String id_entrega, String Xml_Items)
        {
            SqlConnection sqlC = conection;
            try
            {


                DataSet ds = new DataSet();
                SqlCommand sql = new SqlCommand("sapArtCliente_s", sqlC);
                sql.CommandTimeout = 0;
                sql.CommandType = CommandType.StoredProcedure;
                SqlParameter opar = new SqlParameter("@pid_cliente", id_cliente);
                sql.Parameters.Add(opar);
                SqlParameter opar2 = new SqlParameter("@pid_cia", id_cia);
                sql.Parameters.Add(opar2);
                SqlParameter opar3 = new SqlParameter("@pid_entrega", id_entrega);
                sql.Parameters.Add(opar3);
                SqlParameter opar4 = new SqlParameter("@pXml_Items", Xml_Items);
                sql.Parameters.Add(opar4);
                SqlDataAdapter data = new SqlDataAdapter(sql);
                //data.SelectCommand = sql;
                data.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                // return null;
                throw ex;
            }
            finally
            {
                sqlC.Close();
            }
        }
        
      

     
    }


    
}
