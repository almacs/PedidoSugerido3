using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Business;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PedidoSugeridoLamosa.Reportes
{
    public partial class MonitorSimulacion : System.Web.UI.Page
    {

        #region fields

        private UsuarioInf UsrInf = null;

        #endregion fields


        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["usuario"] == null)
                {
                    this.Response.Redirect("../Login.aspx", true);
                    return;
                }

                this.UsrInf = (UsuarioInf)Session["usuario"];

                //primera vez que entra a la página
                if (!this.Page.IsPostBack)
                {
                    Session.Remove("Muestra_MonSimula");

                    txtFechaInicio.Text = "01/" + DateTime.Today.ToString("MM/yyyy") + " 00:00";
                    txtFechaFin.Text = DateTime.Now.AddMinutes(30).ToString("dd/MM/yyyy HH:mm");
                }
            }
            catch (Exception ex)
            {
                this.id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
        #endregion Page_Load


        protected void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                Infragistics.Web.UI.DataSourceControls.WebHierarchicalDataSource odatSource = new Infragistics.Web.UI.DataSourceControls.WebHierarchicalDataSource();
                Infragistics.Web.UI.DataSourceControls.DataView odatviewClient = new Infragistics.Web.UI.DataSourceControls.DataView();
                Infragistics.Web.UI.DataSourceControls.DataView odatviewEntrega = new Infragistics.Web.UI.DataSourceControls.DataView();
                Infragistics.Web.UI.DataSourceControls.DataView odatviewDetail = new Infragistics.Web.UI.DataSourceControls.DataView();
                Infragistics.Web.UI.DataSourceControls.DataRelation odatRealClient = new Infragistics.Web.UI.DataSourceControls.DataRelation();
                Infragistics.Web.UI.DataSourceControls.DataRelation odatRealEntrega = new Infragistics.Web.UI.DataSourceControls.DataRelation();
                odatSource.ID = "odatSource";
                odatviewClient.ID = "odatviewClient";
                odatviewEntrega.ID = "odatviewEntrega";
                odatviewDetail.ID = "odatviewDetail";

                StringBuilder xmlEntrega = new StringBuilder();
                String ids_entrega = ddlEntrega.Text;

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



                BReportes oreportes = new BReportes(new ConnectionLP().getConnection());
                DataSet ds = oreportes.getMonitoreoSimulaciones(xmlEntrega.ToString(), FormatoFecha(txtFechaInicio.Text), FormatoFecha(txtFechaFin.Text));

                odatviewClient.DataSource = ds.Tables[0];
                odatviewEntrega.DataSource = ds.Tables[1];
                odatviewDetail.DataSource = ds.Tables[2];
                odatSource.DataViews.Add(odatviewClient);
                odatSource.DataViews.Add(odatviewEntrega);
                odatSource.DataViews.Add(odatviewDetail);

                odatRealClient.ParentColumns = new String[] { "id_cia", "id_cliente" };
                odatRealClient.ChildColumns = new String[] { "id_cia", "id_cliente" };
                odatRealClient.ParentDataViewID = odatviewClient.ID;
                odatRealClient.ChildDataViewID = odatviewEntrega.ID;

                odatRealEntrega.ParentColumns = new String[] { "id_cia", "id_cliente", "id_entrega" };
                odatRealEntrega.ChildColumns = new String[] { "id_cia", "id_cliente", "id_entrega" };
                odatRealEntrega.ParentDataViewID = odatviewEntrega.ID;
                odatRealEntrega.ChildDataViewID = odatviewDetail.ID;

                odatSource.DataRelations.Add(odatRealClient);
                odatSource.DataRelations.Add(odatRealEntrega);

                whdg1.Bands[0].DataMember = "odatviewEntrega";
                whdg1.Bands[0].DataKeyFields = "id_cia,id_cliente,id_entrega";

                whdg1.Bands[0].Bands[0].DataMember = "odatviewDetail";
                whdg1.Bands[0].Bands[0].DataKeyFields = "id_cia,id_cliente,id_entrega";
                Session["Muestra_MonSimula"] = odatSource;
                whdg1.DataSource = odatSource;
                whdg1.DataBind();
                

            }
            catch (Exception ex)
            {
                this.id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
        
        
        private String FormatoFecha(String fecha)
        {
            String result;
            result = fecha.Substring(6, 4) + fecha.Substring(3, 2) + fecha.Substring(0, 2) + fecha.Substring(10, 6);
            return result;
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            
            
            try
            {
                if (Session["Muestra_MonSimula"] != null)
                {

                    WebExcelExporter1.Export(this.whdg1);


                    /*
                    Infragistics.Web.UI.DataSourceControls.WebHierarchicalDataSource odatSource = (Infragistics.Web.UI.DataSourceControls.WebHierarchicalDataSource)Session["Muestra_ArtSinPedido"];
                
                    this.whdg1.DataSource = odatSource;

                    

                    Infragistics.Documents.Excel.Workbook oExcel = new Infragistics.Documents.Excel.Workbook();
                    oExcel.Worksheets.Add("MonSimulacion");
                    Infragistics.Web.UI.GridControls.WebExcelExporter oExportar = new Infragistics.Web.UI.GridControls.WebExcelExporter();

                    //Se exporta el grid
                    oExportar.ExportMode = Infragistics.Web.UI.GridControls.ExportMode.Download;
                    oExportar.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.AllDataInDataSource;
                    
                    oExportar.Export(this.whdg1, oExcel.Worksheets[0],  10, 0);

                    //Se agrega el título
                    oExcel.Worksheets[0].Rows[0].Cells[2].Value = "Sistema de Pedido Sugerido: Monitoreo de Simulaciones ";

                    //Se agregan los filtros
                    
                    oExcel.Worksheets[0].Rows[4].Cells[0].Value = "Fecha Inicio";
                    oExcel.Worksheets[0].Rows[4].Cells[1].Value = this.txtFechaInicio.Text;

                    oExcel.Worksheets[0].Rows[5].Cells[0].Value = "Fecha Fin";
                    oExcel.Worksheets[0].Rows[5].Cells[1].Value = this.txtFechaFin.Text;

                    oExcel.Worksheets[0].Rows[6].Cells[0].Value = "Fecha Reporte";
                    oExcel.Worksheets[0].Rows[6].Cells[1].Value = DateTime.Now.ToString();

                    oExcel.Save(Request.PhysicalApplicationPath + "Simulacion\\archivos\\Reporte_MonitoreoSimulacion.xls");

                    System.IO.FileInfo ofile = new System.IO.FileInfo(Request.PhysicalApplicationPath + "Simulacion\\archivos\\Reporte_MonitoreoSimulacion.xls");
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Reporte_MonitoreoSimulacion.xls");
                    Response.AddHeader("Content-Length", ofile.Length.ToString());
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.Charset = "";
                    Response.TransmitFile(ofile.FullName);
            * */
                }

            }
            
            catch (Exception ex)
            {
                this.id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
             
        }


        private string revisaNulos(String cadena)
        {
            if (cadena == null || cadena.Equals("null") || cadena.Equals(""))
            {
                return "0";
            }
            return cadena;
        }

        protected void whdg1_Init(object sender, EventArgs e)
        {
            if (Session["Muestra_MonSimula"] != null && Page.IsPostBack == true)
            {
                Infragistics.Web.UI.DataSourceControls.WebHierarchicalDataSource odatSource = (Infragistics.Web.UI.DataSourceControls.WebHierarchicalDataSource)Session["Muestra_MonSimula"];
                this.whdg1.DataSource = odatSource;
            }
        }


    }
}
