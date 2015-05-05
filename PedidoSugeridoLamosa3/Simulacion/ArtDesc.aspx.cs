using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Business;

namespace PedidoSugeridoLamosa.Simulacion
{
    public partial class ArtDesc : System.Web.UI.Page
    {
        private UsuarioInf UsrInf;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack && Session["dtDetalle"] != null)
            {
                DataTable otbl = (DataTable)Session["dtDetalle"];
                DataView oview = new DataView(otbl);
                oview.RowFilter = "Bit_Descontinuado = 1";
                ViewState.Add("btnProcesar", Request.QueryString["btnProcesar"]);
                grdArticulosDesc.DataSource = oview;
                grdArticulosDesc.DataBind();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //grdArticulosDesc.Rows[0].Cells[0].Text
                StringBuilder strXML = new StringBuilder();
                strXML.Append("<articulos>");
                bool bit_almenosuno = false;
                for (int int_reg = 0; int_reg < grdArticulosDesc.Rows.Count; int_reg++)
                {
                    if (Convert.ToBoolean(grdArticulosDesc.Rows[int_reg].Cells[0].Text) == false)
                    {
                        bit_almenosuno = true;
                        strXML.Append("<articulo>");
                        strXML.Append("<cvearticulo>");
                        strXML.Append(grdArticulosDesc.Rows[int_reg].Cells[3].Text);
                        strXML.Append("</cvearticulo>");
                        strXML.Append("</articulo>");
                    }
                }
                strXML.Append("</articulos>");
                this.UsrInf = (UsuarioInf)Session["usuario"];
                if (bit_almenosuno == true)
                {
                    BSimulacion osimulacion = new BSimulacion(new ConnectionLP().getConnection());
                    osimulacion.QuitaArticulos(this.UsrInf.id_compania, this.UsrInf.id_cliente, this.UsrInf.id_sucursal, strXML.ToString());

                    this.id_messages.Text = "<script>alert('Artículos Removidos Satisfactoriamente'); window.parent.dialogArguments.document.getElementById('" + ViewState["btnProcesar"] + "').click(); window.parent.parent.close();</script>";
                }
                else
                {
                    this.id_messages.Text = "<script>alert('Favor de Quitar al Menos 1 Artículo');</script>";
                }


            }
            catch (Exception ex)
            {
                this.id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
    }
}
