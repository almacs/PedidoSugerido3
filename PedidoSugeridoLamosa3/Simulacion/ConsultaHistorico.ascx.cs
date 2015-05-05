using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;

namespace PedidoSugeridoLamosa.Simulacion
{
    public partial class ConsultaHistorico : System.Web.UI.UserControl
    {
        private UsuarioInf UsrInf = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.UsrInf = (UsuarioInf)Session["usuario"];
            if (!Page.IsPostBack)
            {
                com.lamosa.sap.services.history.DT_History_respose ws_response = this.getHistory();
                DataTable dtDetalle = new DataTable();

                dtDetalle.Columns.Add("itemID", typeof(String));
                dtDetalle.Columns.Add("ItemCustomerID", typeof(String));
                dtDetalle.Columns.Add("ItemDesc", typeof(String));
                dtDetalle.Columns.Add("UnitOfMeasure", typeof(String));
                dtDetalle.Columns.Add("Month_H1", typeof(String));
                dtDetalle.Columns.Add("Month_H2", typeof(String));
                dtDetalle.Columns.Add("Month_H3", typeof(String));
                dtDetalle.Columns.Add("Month_H4", typeof(String));
                dtDetalle.Columns.Add("Month_H5", typeof(String));
                dtDetalle.Columns.Add("Month_H6", typeof(String));

                for (int int_reg = 0; int_reg < ws_response.Items.Count(); int_reg++)
                {
                    DataRow dr = dtDetalle.NewRow();
                    dr["itemID"] = ws_response.Items[int_reg].ItemID;
                    dr["ItemCustomerID"] = ws_response.Items[int_reg].ItemCustomerID;
                    dr["ItemDesc"] = ws_response.Items[int_reg].ItemDesc;
                    dr["UnitOfMeasure"] = ws_response.Items[int_reg].UnitOfMeasure;
                    dr["Month_H1"] = ws_response.Items[int_reg].Month_H1;
                    dr["Month_H2"] = ws_response.Items[int_reg].Month_H2;
                    dr["Month_H3"] = ws_response.Items[int_reg].Month_H3;
                    dr["Month_H4"] = ws_response.Items[int_reg].Month_H4;
                    dr["Month_H5"] = ws_response.Items[int_reg].Month_H5;
                    dr["Month_H6"] = ws_response.Items[int_reg].Month_H6;

                    dtDetalle.Rows.Add(dr);
                }
                SetViewState(dtDetalle);
                gvGrid.DataSource = dtDetalle;
                gvGrid.DataBind();

                if (ws_response.Items.Count() > 0) btnExportar.Visible = true;
            }
        }

        private DataTable GetViewState()
        {
            return (DataTable)ViewState["dsCarga"];
        }

        private void SetViewState(DataTable ds)
        {
            ViewState["dsCarga"] = ds;
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportToExcel(gvGrid);
        }

        private void ExportToExcel(GridView catalogGrid)
        {
            if (catalogGrid.Rows.Count > 0 && catalogGrid.Visible == true)
            {
                string sFileName = string.Format("Historico_{0}_{1}_{2}.xls", this.UsrInf.id_compania, this.UsrInf.id_cliente, DateTime.Now.ToString("yyyyMMdd_hhmmss"));
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "UTF-8";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + sFileName);
                Response.ContentEncoding = System.Text.Encoding.Default;
                using (StringWriter oStringWriter = new StringWriter())
                {
                    using (HtmlTextWriter oHtmlTextWriter = new HtmlTextWriter(oStringWriter))
                    {
                        Page page = new Page();
                        HtmlForm tempForm = new HtmlForm();
                        catalogGrid.EnableViewState = false;
                        catalogGrid.AllowPaging = false;
                        catalogGrid.AllowSorting = false;
                        catalogGrid.DataSource = GetViewState();
                        catalogGrid.DataBind();
                        page.EnableEventValidation = false;
                        page.DesignerInitialize();
                        PrepareGridViewForExport(catalogGrid);
                        tempForm.Controls.Add(catalogGrid);
                        page.Controls.Add(tempForm);
                        page.RenderControl(oHtmlTextWriter);
                        Response.Write(oStringWriter.ToString());
                        Response.End();
                    }
                }
            }
        }

        private void PrepareGridViewForExport(Control gv)
        {

            LinkButton lb = new LinkButton();
            Literal l = new Literal();
            string name = String.Empty;
            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton) || gv.Controls[i].GetType().Name == "DataControlLinkButton")
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }
            }
        }

        private com.lamosa.sap.services.history.DT_History_respose getHistory()
        {
            try
            {
                com.lamosa.sap.services.history.SI_History_OutboundClient ws_client = new com.lamosa.sap.services.history.SI_History_OutboundClient("HTTP_Port5");

                com.lamosa.sap.services.history.DT_History_request ws_request = new com.lamosa.sap.services.history.DT_History_request();
                com.lamosa.sap.services.history.DT_History_respose ws_response = new com.lamosa.sap.services.history.DT_History_respose();

                ws_client.ClientCredentials.UserName.UserName = System.Configuration.ConfigurationManager.AppSettings["userWSSAP"];
                ws_client.ClientCredentials.UserName.Password = System.Configuration.ConfigurationManager.AppSettings["pwdWSSAP"];

                ws_request.CompayId = this.UsrInf.id_compania;
                ws_request.CustomerId = this.UsrInf.id_cliente;

                ws_response = ws_client.SI_History_Outbound(ws_request);

                return ws_response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Invocar el Servicio de SAP. " + ex.Message, ex);
            }
        }
    }
}