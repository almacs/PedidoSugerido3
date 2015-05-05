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
    public class BArticulos
    {

        #region fields

        SqlConnection conection;

        #endregion fields

        #region Constructors and Destructor
        public BArticulos(SqlConnection con)
        {
            conection = con;
        }
        #endregion Constructors and Destructor

        #region ObtenArticulosCliente
        public DataSet ObtenArticulosCliente(String id_cliente, string comp)
        {
            SqlConnection sqlC = conection;
            try
            {
                DataSet ds = new DataSet();
                SqlCommand sql = new SqlCommand("Seleccion_articulos", sqlC);
                sql.CommandType = CommandType.StoredProcedure;
                SqlParameter opar = new SqlParameter("@pCliente", id_cliente);
                sql.Parameters.Add(opar);
                SqlParameter opar2 = new SqlParameter("@compani", comp);
                sql.Parameters.Add(opar2);
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
        #endregion ObtenArticulosCliente
        #region GuardaCarga
        public DataSet GuardaCarga(string id_cliente, string id_comp, string id_entrega, string cargaXml, string archivo, string id_usuario, string strarchivousuario)
        {
            SqlConnection sqlC = conection; 
            try
            {
                DataSet ds = new DataSet();
                SqlCommand sql = new SqlCommand("actualiza_articulos", sqlC);
                sql.CommandType = CommandType.StoredProcedure;
                SqlParameter opar = new SqlParameter("@strXml", cargaXml);
                sql.Parameters.Add(opar);
                SqlParameter opar2 = new SqlParameter("@id_cliente", id_cliente);
                sql.Parameters.Add(opar2);
                SqlParameter opar3 = new SqlParameter("@id_cia", id_comp);
                sql.Parameters.Add(opar3);
                SqlParameter opar7 = new SqlParameter("@pid_entrega", id_entrega);
                sql.Parameters.Add(opar7);
                SqlParameter opar4 = new SqlParameter("@id_usuario", id_usuario);
                sql.Parameters.Add(opar4);
                SqlParameter opar5 = new SqlParameter("@archivo", archivo);
                sql.Parameters.Add(opar5);
                SqlParameter opar6 = new SqlParameter("@archivo_cliente", strarchivousuario);
                sql.Parameters.Add(opar6);
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
        #endregion GuardaCarga
        #region GuardaCargaInventario
        public DataSet GuardaCargaInventario(string id_cliente, string id_comp, string id_entrega, string cargaXml, string archivo, string id_usuario, string strarchivousuario) 
        {
            SqlConnection sqlC = conection;
            try
            {
                DataSet ds = new DataSet();
                SqlCommand sql = new SqlCommand("Carga_inventario", sqlC);
                sql.CommandType = CommandType.StoredProcedure;
                SqlParameter opar = new SqlParameter("@strXml", cargaXml);
                sql.Parameters.Add(opar);
                SqlParameter opar2 = new SqlParameter("@id_cliente", id_cliente);
                sql.Parameters.Add(opar2);
                SqlParameter opar3 = new SqlParameter("@id_cia", id_comp);
                sql.Parameters.Add(opar3);
                SqlParameter opar7 = new SqlParameter("@pid_entrega", id_entrega);
                sql.Parameters.Add(opar7);
                SqlParameter opar4 = new SqlParameter("@id_usuario", id_usuario);
                sql.Parameters.Add(opar4);
                SqlParameter opar5 = new SqlParameter("@archivo", archivo);
                sql.Parameters.Add(opar5);
                SqlParameter opar6 = new SqlParameter("@archivo_cliente", strarchivousuario);
                sql.Parameters.Add(opar6);
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
        #endregion GuardaCargaInventario
        #region getDatosPlantillaAtributos
        public DataSet getDatosPlantillaAtributos(string id_cliente, string id_comp, string id_entrega)
        {
            SqlConnection sqlC = conection;

            try
            {
                DataSet ds = new DataSet();
                SqlCommand sql = new SqlCommand("spDatosPlantillaAtributos_s", sqlC);
                sql.CommandType = CommandType.StoredProcedure;
                SqlParameter opar3 = new SqlParameter("@pid_cia", id_comp);
                sql.Parameters.Add(opar3);
                SqlParameter opar2 = new SqlParameter("@pid_cliente", id_cliente);
                sql.Parameters.Add(opar2);
                SqlParameter opar4 = new SqlParameter("@pid_entrega", id_entrega);
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
        #endregion getDatosPlantillaAtributos

    }
}
