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
    public class BUsuarios
    {
        private SqlConnection conection;

        public BUsuarios(SqlConnection con)
        {
            this.conection = con;
        }
        public DataSet GuardaUsuario(String id_usuario, String contrasenia, String nombre, String ap_paterno, String ap_materno,
                                    String mail, Int32 tipo, String id_cliente, String id_entrega, Int32 id_estatus, String cia)
        {
            SqlConnection sqlC = conection;
            try
            {
                DataSet ds = new DataSet();
                SqlCommand sql = new SqlCommand("Insert_Usuarios", sqlC);
                sql.CommandType = CommandType.StoredProcedure;
                SqlParameter opar = new SqlParameter("@id_usuario", id_usuario);
                sql.Parameters.Add(opar);
                SqlParameter opar2 = new SqlParameter("@Nombre", nombre);
                sql.Parameters.Add(opar2);
                SqlParameter opar3 = new SqlParameter("@ap_paterno", ap_paterno);
                sql.Parameters.Add(opar3);
                SqlParameter opar4 = new SqlParameter("@ap_materno", ap_materno);
                sql.Parameters.Add(opar4);
                SqlParameter opar5 = new SqlParameter("@contrass", contrasenia);
                sql.Parameters.Add(opar5);
                SqlParameter opar6 = new SqlParameter("@mail", mail);
                sql.Parameters.Add(opar6);
                SqlParameter opar7 = new SqlParameter("@pXCliente", id_cliente);
                sql.Parameters.Add(opar7);
                SqlParameter opar8 = new SqlParameter("@id_tipo", tipo);
                sql.Parameters.Add(opar8);
                SqlParameter opar9 = new SqlParameter("@pXEntrega", id_entrega);
                sql.Parameters.Add(opar9);
                SqlParameter opar10 = new SqlParameter("@id_estatus", id_estatus);
                sql.Parameters.Add(opar10);
                //SqlParameter opar11 = new SqlParameter("@id_cia", cia);
                //sql.Parameters.Add(opar11);
                SqlDataAdapter data = new SqlDataAdapter(sql);
                data.Fill(ds);
                return ds;

            }
            catch (Exception ex)
            {
               // return null;
                throw ex;

            }
            finally {
                sqlC.Close();
              //  dr.Close();
            }
        }


        public DataSet Obtiene_usuarios()
        {
            SqlConnection sqlC = conection;
            
           
            try
            {
                DataSet dt = new DataSet();
                SqlCommand sqlcmd = new SqlCommand("Muestra_usuarios",sqlC);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter data = new SqlDataAdapter();
                data.SelectCommand = sqlcmd;
                data.Fill(dt);
                return dt;
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

        public DataSet ObtenDirEntrega(String id_cliente, String id_cia) 
        {
            SqlConnection sqlC = conection;
            try
            {
                DataSet ds = new DataSet();
                SqlCommand sql = new SqlCommand("Llena_sucursales", sqlC);
                sql.CommandType = CommandType.StoredProcedure;
                SqlParameter opar = new SqlParameter("@id_cliente", id_cliente);
                sql.Parameters.Add(opar);
                SqlParameter opar1 = new SqlParameter("@id_cia", id_cia);
                sql.Parameters.Add(opar1);
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

        public string UpdateUSR(String id_usuario, String contrasenia, String nombre, String ap_paterno, String ap_materno,
                                  String mail, Int32 tipo, String id_cliente, String id_entrega, Int32 id_estatus, String cia)
        {
            SqlConnection sqlC = conection;
            SqlDataReader dr = null;
            try
            { 
                sqlC = conection;
                SqlCommand command = new SqlCommand();
                command.Connection = sqlC;
                sqlC.Open();
                command.CommandText = "Update_Usuarios";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@id_usuario", id_usuario));
                command.Parameters.Add(new SqlParameter("@nom_usuario", nombre));
                command.Parameters.Add(new SqlParameter("@ap_paterno", ap_paterno));
                command.Parameters.Add(new SqlParameter("@ap_materno", ap_materno));
                command.Parameters.Add(new SqlParameter("@pass", contrasenia));
                command.Parameters.Add(new SqlParameter("@mail", mail));
                command.Parameters.Add(new SqlParameter("@pXCliente", id_cliente));
                command.Parameters.Add(new SqlParameter("@pXEntrega", id_entrega));
                command.Parameters.Add(new SqlParameter("@id_tipoUsr", tipo));
                command.Parameters.Add(new SqlParameter("@estatus", id_estatus));
                //command.Parameters.Add(new SqlParameter("@id_comp", cia));
                dr = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                //return null;
                throw ex;

            }
            finally
            {
                sqlC.Close();
                dr.Close();
            }
            return "Ok";
        }

        public DataSet getClienteEntrega(String ids_clientes, String ids_entrega)
        {
            SqlConnection sqlC = conection;
            try
            {
                DataSet ds = new DataSet();
                SqlCommand sql = new SqlCommand("spClienteEntrega_s", sqlC);
                sql.CommandType = CommandType.StoredProcedure;
                //sql.Parameters.Add(new SqlParameter("@pid_cia", id_cia));
                sql.Parameters.Add(new SqlParameter("@pid_cliente", ids_clientes));
                sql.Parameters.Add(new SqlParameter("@pids_entrega", ids_entrega));
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


        public DataSet getClientes(String DescCliente, String IDClienteD, String IDClienteA)
        {
            SqlConnection sqlC = conection;
            try
            {
                DataSet ds = new DataSet();
                SqlCommand sql = new SqlCommand("spCliente_s", sqlC);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.Add(new SqlParameter("@pNom_Cliente", DescCliente));
                if (!"".Equals(IDClienteD))
                {
                    sql.Parameters.Add(new SqlParameter("@pId_ClienteD", IDClienteD));
                }
                if (!"".Equals(IDClienteA))
                {
                    sql.Parameters.Add(new SqlParameter("@pId_ClienteA", IDClienteA));
                }
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

        public DataSet getClienteUsuario(String Id_Usuario)
        {
            SqlConnection sqlC = conection;
            try
            {
                DataSet ds = new DataSet();
                SqlCommand sql = new SqlCommand("spClientesUsuario_s", sqlC);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.Add(new SqlParameter("@pId_Usuario", Id_Usuario));
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
