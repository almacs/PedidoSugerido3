using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using System.Data;
using System.Text;

namespace PedidoSugeridoLamosa.Usuario
{
    public partial class UsuarioEntrega : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            BUsuarios dx = new BUsuarios(new ConnectionLP().getConnection());
            String ids_clientes = Request.QueryString["id_cliente"];
            String ids_entrega = Request.QueryString["ids_entrega"];
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


            DataSet ods = dx.getClienteEntrega(xmlClientes.ToString(), xmlEntrega.ToString());

            grid_Sucursales.DataSource = ods;
            grid_Sucursales.DataBind();


            grid_Sucursales.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
            grid_Sucursales.Columns[0].Type = Infragistics.WebUI.UltraWebGrid.ColumnType.CheckBox;
            grid_Sucursales.Width = Unit.Percentage(100);
            grid_Sucursales.Columns[0].Width = Unit.Percentage(5);
            grid_Sucursales.Columns[1].Width = Unit.Percentage(10);
            grid_Sucursales.Columns[2].Width = Unit.Percentage(10);
            grid_Sucursales.Columns[3].Width = Unit.Percentage(10);
            grid_Sucursales.Columns[4].Width = Unit.Percentage(10);
            grid_Sucursales.Columns[5].Width = Unit.Percentage(25);
            grid_Sucursales.Columns[6].Width = Unit.Percentage(30);
            grid_Sucursales.Columns[7].Hidden = true;
        }
    }
}
