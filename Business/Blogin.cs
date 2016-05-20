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
  public class Blogin
    {
      SqlConnection connection;

      public Blogin(SqlConnection con)
      {
          connection = con;
      }

      public DataSet ObtieneClientes(String id_cliente)
      {
          SqlConnection slq = connection;
          try
          {
              DataSet ds = new DataSet();
              SqlCommand cmd = new SqlCommand("Muestra_usuarios_login", slq);
              cmd.CommandType = CommandType.StoredProcedure;
              SqlParameter param = new SqlParameter("@id_usuario", id_cliente);
              cmd.Parameters.Add(param);
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
              slq.Close();
          }
      }

      public DataSet Acceso(String id_usuario, String password, String entrega)
      {
          SqlConnection slq = connection;
          try
          {
              DataSet ds = new DataSet();
              SqlCommand cmd = new SqlCommand("Acceso_login", slq);
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.Add(new SqlParameter("@id_cliente", id_usuario));
              cmd.Parameters.Add(new SqlParameter("@pass", password));
              cmd.Parameters.Add(new SqlParameter("@id_entrega", entrega));
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
              slq.Close();
          }
      }

      public DataSet Acceso(String id_usuario, String password, String entrega, int prevalidado)
      {
          SqlConnection slq = connection;
          try
          {
              DataSet ds = new DataSet();
              SqlCommand cmd = new SqlCommand("Acceso_login", slq);
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.Add(new SqlParameter("@id_cliente", id_usuario));
              cmd.Parameters.Add(new SqlParameter("@pass", password));
              cmd.Parameters.Add(new SqlParameter("@id_entrega", entrega));
              cmd.Parameters.Add(new SqlParameter("@prevalidado", prevalidado));
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
              slq.Close();
          }
      }

      public DataSet CambiaContraseña(String id_usuario, string pass, string newpass)
      {
          SqlConnection slq = connection;
          try
          {
              DataSet ds = new DataSet();
              SqlCommand cmd = new SqlCommand("SpCambiaContrasenia", slq);
              cmd.CommandType = CommandType.StoredProcedure;
              SqlParameter param = new SqlParameter("@id_usuario", id_usuario);
              cmd.Parameters.Add(param);
              SqlParameter param2 = new SqlParameter("@pass", pass);
              cmd.Parameters.Add(param2);
              SqlParameter param3 = new SqlParameter("@newpass", newpass);
              cmd.Parameters.Add(param3);
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
              slq.Close();
          }
      }

    }
}
