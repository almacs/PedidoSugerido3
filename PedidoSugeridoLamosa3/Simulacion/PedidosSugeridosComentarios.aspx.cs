using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.Web;
using System.Text;
using Business;

namespace PedidoSugeridoLamosa.Simulacion
{
    public partial class PedidosSugeridosComentarios : System.Web.UI.Page
    {

        #region fields

        private UsuarioInf UsrInf = null;
        private DataTable dtArtClienteConValFinal = null;
        private String sUnidadMedida = String.Empty;

        #endregion fields

        #region GuardarComentariosEnDataTable
        private void GuardarComentariosEnDataTable(DataTable dtObj)
        {
            for (int iReg = 0; iReg < this.gvArtComentarios.Rows.Count; iReg++)
            {
                if (this.gvArtComentarios.Rows[iReg].Cells[2].DataChanged)
                {
                    dtObj.Rows[iReg]["Comentarios"] = this.gvArtComentarios.Rows[iReg].Cells[2].Value;
                    this.gvArtComentarios.Rows[iReg].Cells[2].DataChanged = false;
                }
            }
        }
        #endregion SaveData
        #region GenerarXML
        private String GenerarXML(DataTable dtObj)
        {
            StringBuilder strXML = new StringBuilder();
            strXML.Append("<articulos>");
            for (int int_reg = 0; int_reg < dtObj.Rows.Count; int_reg++)
            {
                strXML.Append("<articulo>");

                strXML.Append("<cvearticulo>");
                strXML.Append(dtObj.Rows[int_reg]["Cve_Articulo"].ToString());
                strXML.Append("</cvearticulo>");

                strXML.Append("<Comentarios>");
                strXML.Append(dtObj.Rows[int_reg]["Comentarios"].ToString());
                strXML.Append("</Comentarios>");

                strXML.Append("</articulo>");
            }
            strXML.Append("</articulos>");

            return strXML.ToString();
        }
        #endregion GenerarXML
        #region GuardarComentariosEnDataBase
        private void GuardarComentariosEnDataBase()
        {
            try
            {
                BSimulacion oSim = new BSimulacion(new ConnectionLP().getConnection());
                String sXML = this.GenerarXML(this.dtArtClienteConValFinal);
                oSim.ActualizarComentariosArticulos(this.UsrInf.id_compania, this.UsrInf.id_cliente, this.UsrInf.id_sucursal, sXML);
            }
            catch (Exception ex)
            {
                this.id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
        #endregion GuardarComentariosEnDataBase

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                this.Response.Redirect("../Login.aspx", true);
                return;
            }

            this.UsrInf = (UsuarioInf)Session["usuario"];
            this.dtArtClienteConValFinal = (DataTable)Session["dtArtClienteConValFinal"];
            this.sUnidadMedida = (String)Session["sUnidadMedida"];
            this.lbDesFechaSimulacion.Text = Convert.ToString(Session["sDesFechaSimulacion"]);

            // Agregar el tooltip a los asp:button
            this.btExportarFormatoOC.Attributes.Add("onmouseover", "return escape('Genera un archivo de Orden de compra en formato PDF basándose la cantidad “Final” de cada artículo.')");

            //Se obtienen los datos solo la primera vez que entra a la página.
            if (!this.Page.IsPostBack)
            {
                this.gvArtComentarios.DataSource = this.dtArtClienteConValFinal;
                this.gvArtComentarios.DataBind();
            }
        }
        #endregion Page_Load
        #region btExportarFormatoOC_Click
        protected void btExportarFormatoOC_Click(object sender, EventArgs e)
        {
            try
            {
                this.GuardarComentariosEnDataTable(this.dtArtClienteConValFinal);
                this.GuardarComentariosEnDataBase();

                PedidoSugerido3 rptReporte = new PedidoSugerido3();
                //rptReporte.Load(Server.MapPath("PedidoSugerido.rpt"));
                 
                rptReporte.SetDataSource(this.dtArtClienteConValFinal);
                rptReporte.SetParameterValue("id_cliente", this.UsrInf.id_cliente);
                if (!this.UsrInf.id_sucursal.Equals("CON"))
                {
                    rptReporte.SetParameterValue("desc_entrega", Session["DirEntrega"].ToString());
                    rptReporte.SetParameterValue("id_entrega", this.UsrInf.id_sucursal);
                }
                else
                {
                    rptReporte.SetParameterValue("desc_entrega", "");
                    rptReporte.SetParameterValue("id_entrega", "");
                }
                rptReporte.SetParameterValue("unidad_medida", this.sUnidadMedida);
                rptReporte.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, this.Response, true, "PedidoSugerido_OrdenCompra");
            }
            catch (Exception ex)
            {
                this.id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
        #endregion btExportarFormatoOC_Click

    }
}
