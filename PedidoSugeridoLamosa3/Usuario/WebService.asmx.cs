using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Business;
using System.Text;

namespace PedidoSugeridoLamosa
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    //[System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        #region InsertaUsuarios
        [WebMethod]
        public string InsertaUsuarios(  String id_usuario, String pass, String nombre, String paterno, String materno, String mail, 
                                        int tipo, String cliente, String id_entrega, int status, String cia) 
        {
            try
            {
                BUsuarios da = new BUsuarios(new ConnectionLP().getConnection());
                DataSet df = new DataSet();

                String ids_clientes = cliente;
                String ids_entrega = id_entrega;
                StringBuilder xmlClientes = new StringBuilder();
                StringBuilder xmlEntrega = new StringBuilder();

                xmlClientes.Append("<clientes>");
                for (int int_reg = 0; int_reg < ids_clientes.Split('|').Length - 1; int_reg++)
                {
                    xmlClientes.Append("<cliente>");
                    xmlClientes.Append("<id_cliente>");
                    xmlClientes.Append(ids_clientes.Split('|')[int_reg].Split('-')[1]);
                    xmlClientes.Append("</id_cliente>");
                    xmlClientes.Append("<id_cia>");
                    xmlClientes.Append(ids_clientes.Split('|')[int_reg].Split('-')[0]);
                    xmlClientes.Append("</id_cia>");
                    xmlClientes.Append("</cliente>");
                }
                xmlClientes.Append("</clientes>");

                xmlEntrega.Append("<entregas>");
                for (int int_reg = 0; int_reg < ids_entrega.Split('|').Length - 1; int_reg++)
                {
                    xmlEntrega.Append("<entrega>");
                    xmlEntrega.Append("<id_cliente>");
                    xmlEntrega.Append(ids_entrega.Split('|')[int_reg].Split('-')[1]);
                    xmlEntrega.Append("</id_cliente>");
                    xmlEntrega.Append("<id_cia>");
                    xmlEntrega.Append(ids_entrega.Split('|')[int_reg].Split('-')[0]);
                    xmlEntrega.Append("</id_cia>");
                    xmlEntrega.Append("<id_entrega>");
                    xmlEntrega.Append(ids_entrega.Split('|')[int_reg].Split('-')[2]);
                    xmlEntrega.Append("</id_entrega>");
                    xmlEntrega.Append("</entrega>");
                }
                xmlEntrega.Append("</entregas>");

                df = da.GuardaUsuario(id_usuario, pass, nombre, paterno, materno, mail, tipo, xmlClientes.ToString(), xmlEntrega.ToString(), status, cia);

                return df.Tables[0].Rows[0][0].ToString();
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion InsertaUsuarios
        #region GetEntrega
        [WebMethod]
        public string GetEntrega(String ComboValue, String select_value) 
        {
            try 
            {
                BUsuarios de = new BUsuarios(new ConnectionLP().getConnection());
                //userInfo.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)
                string[] ClienteComp = ComboValue.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries); 
                DataSet ds = de.ObtenDirEntrega(ClienteComp[0], ClienteComp[1]);
                StringBuilder options = new StringBuilder();
                if (ds.Tables.Count > 0) 
                { 
                    for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        options.Append(ds.Tables[0].Rows[i]["id_entrega"].ToString() + ",");
                        //options += "<option " + (select_value.Equals(ds.Tables[0].Rows[i]["id_entrega"]) ? "selected":"") + " value=\"" + ds.Tables[0].Rows[i]["id_entrega"] + "\">" + ds.Tables[0].Rows[i]["Sucursal"] + "</option>";
                    }
                    options.Append("|");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        options.Append(ds.Tables[0].Rows[i]["Sucursal"].ToString() + ",");
                        //options += "<option " + (select_value.Equals(ds.Tables[0].Rows[i]["id_entrega"]) ? "selected" : "") + " value=\"" + ds.Tables[0].Rows[i]["id_entrega"] + "\">" + ds.Tables[0].Rows[i]["Sucursal"] + "</option>";
                    }
                }
                return options.ToString();
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }
        #endregion GetEntrega
        #region obtieneUSR
        [WebMethod]
        public string obtieneUSR(string id_cliente)
        {
            try
            {
                Blogin de = new Blogin(new ConnectionLP().getConnection());
                DataSet ds = de.ObtieneClientes(id_cliente);
                System.Collections.Hashtable hash = new System.Collections.Hashtable();
                String options = "";
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "-1")
                    {
                        return "-1";
                    }
                    options += "tipo_usuario=" + ds.Tables[0].Rows[0]["id_tipo"];
                    options += "|";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        options += "<option value=\"" + ds.Tables[0].Rows[i]["Id_entrega"] + "\">" + ds.Tables[0].Rows[i]["Sucursal"] + "</option>";
                    }
                }
                return options;
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }
        #endregion obtieneUSR
        #region UpdateUsuario
        [WebMethod]
        public string UpdateUsuario(String id_usuario, String pass, String nombre, String paterno, String materno, String mail,
                                        int tipo, String cliente, String id_entrega, int status, String cia)
        {
            try
            {
                BUsuarios bu = new BUsuarios(new ConnectionLP().getConnection());


                String ids_clientes = cliente;
                String ids_entrega = id_entrega;
                StringBuilder xmlClientes = new StringBuilder();
                StringBuilder xmlEntrega = new StringBuilder();

                xmlClientes.Append("<clientes>");
                for (int int_reg = 0; int_reg < ids_clientes.Split('|').Length - 1; int_reg++)
                {
                    xmlClientes.Append("<cliente>");
                    xmlClientes.Append("<id_cliente>");
                    xmlClientes.Append(ids_clientes.Split('|')[int_reg].Split('-')[1]);
                    xmlClientes.Append("</id_cliente>");
                    xmlClientes.Append("<id_cia>");
                    xmlClientes.Append(ids_clientes.Split('|')[int_reg].Split('-')[0]);
                    xmlClientes.Append("</id_cia>");
                    xmlClientes.Append("</cliente>");
                }
                xmlClientes.Append("</clientes>");

                xmlEntrega.Append("<entregas>");
                for (int int_reg = 0; int_reg < ids_entrega.Split('|').Length - 1; int_reg++)
                {
                    xmlEntrega.Append("<entrega>");
                    xmlEntrega.Append("<id_cliente>");
                    xmlEntrega.Append(ids_entrega.Split('|')[int_reg].Split('-')[1]);
                    xmlEntrega.Append("</id_cliente>");
                    xmlEntrega.Append("<id_cia>");
                    xmlEntrega.Append(ids_entrega.Split('|')[int_reg].Split('-')[0]);
                    xmlEntrega.Append("</id_cia>");
                    xmlEntrega.Append("<id_entrega>");
                    xmlEntrega.Append(ids_entrega.Split('|')[int_reg].Split('-')[2]);
                    xmlEntrega.Append("</id_entrega>");
                    xmlEntrega.Append("</entrega>");
                }
                xmlEntrega.Append("</entregas>");


                string da = bu.UpdateUSR(id_usuario, pass, nombre, paterno, materno, mail, tipo, xmlClientes.ToString(), xmlEntrega.ToString(), status, cia);
                if (da != null)
                {
                    return "El usuario se modificó con exito";
                }
                else
                {
                    return "Hubo un error al intentar modificar el usuario intente nuevamente";
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion UpdateUsuario
   
    }
}
