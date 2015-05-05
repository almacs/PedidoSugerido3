using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PedidoSugeridoLamosa.Reportes
{
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Se llena el grid de los usuarios  
            SqlConnection slq = new ConnectionLP().getConnection();
            DataSet ds = new DataSet();

            try
            {
                SqlCommand cmd = new SqlCommand("Muestra_usuarios", slq);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter data = new SqlDataAdapter(cmd);
                data.Fill(ds);
                agregaSucursal(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                slq.Close();
            }
            
            grid_Sucursales.DataSource = ds;
            grid_Sucursales.DataBind();
        }

        protected void grid_Sucursales_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            this.grid_Sucursales.DisplayLayout.AllowDeleteDefault = Infragistics.WebUI.UltraWebGrid.AllowDelete.No;
            this.grid_Sucursales.DisplayLayout.AllowSortingDefault = Infragistics.WebUI.UltraWebGrid.AllowSorting.OnClient;

        }


        private void agregaSucursal(DataSet ods)
        {
            DataView odv = new DataView(ods.Tables[1]);
            for (int int_reg = 0; int_reg < ods.Tables[0].Rows.Count; int_reg++)
            {
                odv.RowFilter = "Clave_usuario='" + ods.Tables[0].Rows[int_reg]["Clave usuario"].ToString() + "'";
                StringBuilder strIds = new StringBuilder();
                StringBuilder strDescs = new StringBuilder();
                for (int int_col = 0; int_col < odv.Count; int_col++)
                {
                    strIds.Append(odv[int_col]["id_cia"] + "-" + odv[int_col]["id_cliente"] + "-" + odv[int_col]["id_entrega"] + "|");
                    strDescs.Append(odv[int_col]["Sucursal"] + "|");
                }
                ods.Tables[0].Rows[int_reg]["id_entrega"] = strIds.ToString();
                ods.Tables[0].Rows[int_reg]["Sucursal"] = strDescs.ToString();
            }

            odv = new DataView(ods.Tables[2]);
            for (int int_reg = 0; int_reg < ods.Tables[0].Rows.Count; int_reg++)
            {
                odv.RowFilter = "Clave_usuario='" + ods.Tables[0].Rows[int_reg]["Clave usuario"].ToString() + "'";
                StringBuilder strIds = new StringBuilder();
                StringBuilder strDescs = new StringBuilder();
                StringBuilder strDescComp = new StringBuilder();
                for (int int_col = 0; int_col < odv.Count; int_col++)
                {
                    strIds.Append(odv[int_col]["id_cia"] + "-" + odv[int_col]["id_cliente"] + "|");
                    strDescs.Append(odv[int_col]["Cliente"] + "|");
                    strDescComp.Append(odv[int_col]["Compania"] + "|");
                }
                ods.Tables[0].Rows[int_reg]["id_cliente"] = strIds.ToString();
                ods.Tables[0].Rows[int_reg]["nom_cliente"] = strDescs.ToString();
                ods.Tables[0].Rows[int_reg]["id_cia"] = strDescComp.ToString();
            }
        }
    }
}
