using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Business
{
    public class BReportes
    {
        #region fields

        private SqlConnection conection = null;

        #endregion fields

        #region Constructors and destructor
        public BReportes(SqlConnection con)
        {
            this.conection = con;
        }
        #endregion Constructors and destructor

        public DataSet getCompanias()
        {
            SqlConnection sqlC = conection;
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("Listar_Companias", sqlC);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter data = new SqlDataAdapter(cmd);
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


        public DataSet getSucursales_Usuario(String id_cia, String id_usuario)
        {
            SqlConnection sqlC = conection;
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("Muestra_Sucursales_Usuario", sqlC);
                cmd.CommandType = CommandType.StoredProcedure;

                //Parametros
                SqlParameter param = new SqlParameter("@id_cia", id_cia);
                cmd.Parameters.Add(param);

                SqlParameter param2 = new SqlParameter("@id_usuario", id_usuario);
                cmd.Parameters.Add(param2);

                SqlDataAdapter data = new SqlDataAdapter(cmd);
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

        public DataSet getMonitoreoSimulaciones(String xmlEntrega , String fecini, String fecfin)
        {

            SqlConnection sqlC = conection;
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("Muestra_Monitoreo_Simulaciones", sqlC);
                cmd.CommandType = CommandType.StoredProcedure;

                //Parametros
                SqlParameter param = new SqlParameter("@pEntregasXML", xmlEntrega);
                cmd.Parameters.Add(param);

                SqlParameter param4 = new SqlParameter("@fecini", fecini);
                cmd.Parameters.Add(param4);

                SqlParameter param5 = new SqlParameter("@fecfin", fecfin);
                cmd.Parameters.Add(param5);

                SqlDataAdapter data = new SqlDataAdapter(cmd);
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
