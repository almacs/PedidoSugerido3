using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace PedidoSugeridoLamosa.Usuario
{
    public partial class vSeleccionCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            DataTable otable = new DataTable();
            otable.Columns.Add("Seleccion");
            otable.Columns.Add("Id Compañia");
            otable.Columns.Add("Id Cliente");
            otable.Columns.Add("Compañia");
            otable.Columns.Add("Cliente");
            otable.Columns.Add("Clave");


            

            grid_Clientes.DataSource = otable;
            grid_Clientes.DataBind();


            grid_Clientes.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
            grid_Clientes.Columns[0].Type = Infragistics.WebUI.UltraWebGrid.ColumnType.CheckBox;
            grid_Clientes.Width = Unit.Percentage(100);
            grid_Clientes.Columns[0].Width = Unit.Percentage(10);
            grid_Clientes.Columns[1].Width = Unit.Percentage(15);
            grid_Clientes.Columns[2].Width = Unit.Percentage(15);
            grid_Clientes.Columns[3].Width = Unit.Percentage(20);
            grid_Clientes.Columns[4].Width = Unit.Percentage(40);
            grid_Clientes.Columns[5].Hidden = true;
        }
    }
}
