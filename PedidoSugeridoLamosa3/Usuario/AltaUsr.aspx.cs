using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using Business;
using System.Text;

namespace PedidoSugeridoLamosa.Usuario
{
    public partial class AltaUsr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("../Login.aspx", true);
                return;
            }

            if (!Page.IsPostBack)
            {
                BUsuarios u = new BUsuarios(new ConnectionLP().getConnection());
                fill();
            }
        }

        private void fill()
        {
            DataSet du = new DataSet();
            BUsuarios dx = new BUsuarios(new ConnectionLP().getConnection());
            du = dx.Obtiene_usuarios();
            if (du.Tables.Count > 0)
            {
                agregaSucursal(du);
                UltraWebGrid1.DataSource = du.Tables[0];
                UltraWebGrid1.DataBind();
                UltraWebGrid1.Columns[0].Header.Caption = "Clave usuario";
                UltraWebGrid1.Columns[0].Width = 100;
                UltraWebGrid1.Columns[1].Width = 100;
                UltraWebGrid1.Columns[2].Hidden = true;
                UltraWebGrid1.Columns[3].Hidden = true;
                UltraWebGrid1.Columns[4].Hidden = true;
                UltraWebGrid1.Columns[5].Hidden = true;
                UltraWebGrid1.Columns[6].Hidden = true;
                UltraWebGrid1.Columns[7].Hidden = true;
                UltraWebGrid1.Columns[8].Hidden = true;
                UltraWebGrid1.Columns[9].Hidden = true;
                UltraWebGrid1.Columns[10].Width = 300;
                UltraWebGrid1.Columns[10].CellStyle.Wrap = true;
                UltraWebGrid1.Columns[11].Hidden = true;
                UltraWebGrid1.Columns[12].Width = 272;
                UltraWebGrid1.Columns[12].CellStyle.Wrap = true;
                UltraWebGrid1.Columns[13].Width = 102;
                UltraWebGrid1.Columns[14].Hidden = true;
                UltraWebGrid1.Columns[15].Width = 80;
                UltraWebGrid1.Columns[16].Hidden = true;
                UltraWebGrid1.Columns[17].Hidden = true;

            }
        }

        protected void UltraWebGrid1_InitializeLayout(object sender, EventArgs e)
        {
            fill();
        }

        protected void cambio_pagina(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            fill();
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
                ods.Tables[0].Rows[int_reg]["Cliente"] = strDescs.ToString();
                ods.Tables[0].Rows[int_reg]["id_cia"] = strDescComp.ToString();
            }
        }
    }
}
