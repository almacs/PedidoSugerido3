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
    public class BSimulacion
    {

        #region fields

        private SqlConnection conection = null;

        #endregion fields

        #region Constructors and destructor
        public BSimulacion(SqlConnection con)
        {
            this.conection = con;
        }
        #endregion Constructors and destructor

        #region getSimulation
        public DataSet getSimulation(String id_compania, String id_cliente, String id_entrega, String MovXML, bool bit_load, bool bit_cambio, String id_usuario, String id_tipo_usuario)
        {
            SqlConnection sqlC = this.conection;

            try
            {
                DataSet ds = new DataSet();

                SqlCommand sql = new SqlCommand("sapSimulacion_s", sqlC);
                sql.CommandTimeout = 0;
                sql.CommandType = CommandType.StoredProcedure;
                SqlParameter opar = new SqlParameter("@pid_cia", id_compania);
                sql.Parameters.Add(opar);
                SqlParameter opar2 = new SqlParameter("@pid_cliente", id_cliente);
                sql.Parameters.Add(opar2);
                SqlParameter opar3 = new SqlParameter("@pid_entrega", id_entrega);
                sql.Parameters.Add(opar3);
                SqlParameter opar4 = new SqlParameter("@pMovXML", MovXML);
                sql.Parameters.Add(opar4);
                SqlParameter opar5 = new SqlParameter("@pbitInicio", bit_load);
                sql.Parameters.Add(opar5);
                SqlParameter opar6 = new SqlParameter("@pbitCambio", bit_cambio);
                sql.Parameters.Add(opar6);
                SqlParameter opar7 = new SqlParameter("@pIdTipoUsuario", id_tipo_usuario);
                sql.Parameters.Add(opar7);
                SqlParameter opar8 = new SqlParameter("@pIdUsuario", id_usuario);
                sql.Parameters.Add(opar8);
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
        #endregion getSimulation

        #region ActualizarComentariosArticulos
        public void ActualizarComentariosArticulos(String IdCompania, String IdCliente, String IdEntrega, String sXML)
        {
            SqlConnection sqlCon = this.conection;

            try
            {
                SqlCommand sqlCmd = new SqlCommand("spActualizarComentariosArticulos", sqlCon);
                sqlCmd.CommandTimeout = 0;

                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlParameter p1 = new SqlParameter("@pid_cia", IdCompania);
                sqlCmd.Parameters.Add(p1);
                SqlParameter p2 = new SqlParameter("@pid_cliente", IdCliente);
                sqlCmd.Parameters.Add(p2);
                SqlParameter p3 = new SqlParameter("@pid_entrega", IdEntrega);
                sqlCmd.Parameters.Add(p3);
                SqlParameter p4 = new SqlParameter("@pMovXML", sXML);
                sqlCmd.Parameters.Add(p4);
                SqlParameter p5 = new SqlParameter("@Res", SqlDbType.Int);
                p5.Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add(p5);

                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCon.Close();
            }
        }
        #endregion ActualizarComentariosArticulos



        public DataSet QuitaArticulos(String id_compania, String id_cliente, String id_entrega, String MovXML)
        {
            SqlConnection sqlC = this.conection;

            try
            {
                DataSet ds = new DataSet();

                SqlCommand sql = new SqlCommand("spArtClienteDesc_Up", sqlC);
                sql.CommandTimeout = 0;
                sql.CommandType = CommandType.StoredProcedure;
                SqlParameter opar = new SqlParameter("@pid_cia", id_compania);
                sql.Parameters.Add(opar);
                SqlParameter opar2 = new SqlParameter("@pid_cliente", id_cliente);
                sql.Parameters.Add(opar2);
                SqlParameter opar3 = new SqlParameter("@pid_entrega", id_entrega);
                sql.Parameters.Add(opar3);
                SqlParameter opar4 = new SqlParameter("@pArtXML", MovXML);
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
