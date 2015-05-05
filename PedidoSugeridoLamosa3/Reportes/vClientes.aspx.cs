using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using System.Data;

namespace PedidoSugeridoLamosa.Reportes
{
    public partial class vClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtIdClienteD.Attributes.Add("onkeypress", "ValNumero_onkeypress();");
            txtIdClienteA.Attributes.Add("onkeypress", "ValNumero_onkeypress();");
        }

        
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BUsuarios dx = new BUsuarios(new ConnectionLP().getConnection());
            DataSet ods = dx.getClientes(txtDescCliente.Text, txtIdClienteD.Text, txtIdClienteA.Text);

            grid_Clientes.DataSource = ods;
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
