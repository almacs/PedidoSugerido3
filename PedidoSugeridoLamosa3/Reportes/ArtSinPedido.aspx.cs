using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infragistics.Web.UI.DataSourceControls;

namespace PedidoSugeridoLamosa.Reporte
{
    public partial class ArtSinPedido : System.Web.UI.Page
    {
        #region fields

        private UsuarioInf UsrInf = null;

        #endregion fields

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("../Login.aspx", true);
                return;
            }

            this.UsrInf = (UsuarioInf)Session["usuario"];

            if (!Page.IsPostBack)//primera vez que entra a la página
            {
                SqlConnection slq = new ConnectionLP().getConnection();

                try
                {
                    Session.Remove("Muestra_ArtSinPedido");

                    txtFechaInicio.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtFechaFin.Text = DateTime.Today.ToString("dd/MM/yyyy");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    slq.Close();
                }
            }

            //if (this.HddNombreUsuario.Value.Length > 0)
            //    this.txtNomUsuario.Text = this.HddNombreUsuario.Value.ToString();
        }



        protected void btnReporte_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            WebHierarchicalDataSource odatSource = new WebHierarchicalDataSource();
            Infragistics.Web.UI.DataSourceControls.DataView odatviewClient = new Infragistics.Web.UI.DataSourceControls.DataView();
            Infragistics.Web.UI.DataSourceControls.DataView odatviewEntrega = new Infragistics.Web.UI.DataSourceControls.DataView();
            Infragistics.Web.UI.DataSourceControls.DataView odatviewDetail = new Infragistics.Web.UI.DataSourceControls.DataView();
            Infragistics.Web.UI.DataSourceControls.DataRelation odatRealClient = new Infragistics.Web.UI.DataSourceControls.DataRelation();
            Infragistics.Web.UI.DataSourceControls.DataRelation odatRealEntrega = new Infragistics.Web.UI.DataSourceControls.DataRelation();
            odatSource.ID = "odatSource";
            odatviewClient.ID = "odatviewClient";
            odatviewEntrega.ID = "odatviewEntrega";
            odatviewDetail.ID = "odatviewDetail";


 

            


            // Se muestra el grid con los datos, aplicando los filtros previamente seleccionados
                    
                SqlConnection slq = new ConnectionLP().getConnection();

                try
                {
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

                    StringBuilder xmlEntregaHoy = new StringBuilder();
                    xmlEntregaHoy.Append("<entregas>");


                    if (ConvertirFecha(this.txtFechaFin.Text) >= DateTime.Today)
                    {
                        SqlCommand cmd2 = new SqlCommand("sapArtSinPedido", slq);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.CommandTimeout = 0;
                        //Parametros
                        SqlParameter paramE = new SqlParameter("@pEntregasXML", xmlEntrega.ToString());
                        cmd2.Parameters.Add(paramE);
                        SqlDataAdapter data2 = new SqlDataAdapter(cmd2);
                        data2.Fill(ds);
                        for (int int_reg = 0; int_reg < ds.Tables[0].Rows.Count; int_reg++)
                        {
                            System.Data.DataView oview = new System.Data.DataView(ds.Tables[1]);
                            String id_cia = ds.Tables[0].Rows[int_reg]["id_cia"].ToString();
                            String id_cliente = ds.Tables[0].Rows[int_reg]["id_cliente"].ToString();
                            String id_sucursal = ds.Tables[0].Rows[int_reg]["id_entrega"].ToString();
                            oview.RowFilter = "id_cia = '" + id_cia + "' " +
                                              "AND id_cliente = '" + id_cliente + "' " +
                                              "AND id_entrega = '" + id_sucursal + "' ";

                            com.lamosa.sap.services.backorder.DT_BackOrder_response ws_response  = this.getBackorder(oview, id_cia, id_cliente, id_sucursal);


                            for (int int_items = 0; int_items < ws_response.Items.Length; int_items++)
                            {
                                xmlEntregaHoy.Append("<entrega>");
                                xmlEntregaHoy.Append("<id_cliente>");
                                xmlEntregaHoy.Append(id_cliente);
                                xmlEntregaHoy.Append("</id_cliente>");
                                xmlEntregaHoy.Append("<id_cia>");
                                xmlEntregaHoy.Append(id_cia);
                                xmlEntregaHoy.Append("</id_cia>");
                                xmlEntregaHoy.Append("<id_entrega>");
                                xmlEntregaHoy.Append(id_sucursal);
                                xmlEntregaHoy.Append("</id_entrega>");
                                xmlEntregaHoy.Append("<id_articulo>");
                                xmlEntregaHoy.Append(ws_response.Items[int_items].ItemId);
                                xmlEntregaHoy.Append("</id_articulo>");
                                xmlEntregaHoy.Append("<id_articulo_cliente>");
                                xmlEntregaHoy.Append(ws_response.Items[int_items].ItemCustomerId);
                                xmlEntregaHoy.Append("</id_articulo_cliente>");
                                xmlEntregaHoy.Append("<id_articulo_desc>");
                                xmlEntregaHoy.Append(ws_response.Items[int_items].ItemDesc);
                                xmlEntregaHoy.Append("</id_articulo_desc>");
                                xmlEntregaHoy.Append("<pedidos_pendientes>");
                                xmlEntregaHoy.Append(ws_response.Items[int_items].InventoryPending);
                                xmlEntregaHoy.Append("</pedidos_pendientes>");
                                xmlEntregaHoy.Append("<val_conv>");
                                xmlEntregaHoy.Append(ws_response.Items[int_items].ConvertionValue);
                                xmlEntregaHoy.Append("</val_conv>");
                                xmlEntregaHoy.Append("</entrega>");
                            }

                            

                        }

                        xmlEntregaHoy.Append("</entregas>");
                    }


                    ds.Dispose();
                    ds = null;
                    ds = new DataSet();

                    SqlCommand cmd = new SqlCommand("Muestra_ArtSinPedido", slq);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    //Parametros
                    SqlParameter param = new SqlParameter("@pEntregasXML", xmlEntrega.ToString());
                    cmd.Parameters.Add(param);

                    SqlParameter param4 = new SqlParameter("@pFecIni", FormatoFecha(this.txtFechaInicio.Text));
                    cmd.Parameters.Add(param4);

                    SqlParameter param5 = new SqlParameter("@pFecFin", FormatoFecha(this.txtFechaFin.Text));
                    cmd.Parameters.Add(param5);
                    SqlParameter param6 = new SqlParameter("@pEntregasXMLHoy", xmlEntregaHoy.ToString());
                    cmd.Parameters.Add(param6);

                    SqlDataAdapter data = new SqlDataAdapter(cmd);
                    data.Fill(ds);


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
                    Session["Muestra_ArtSinPedido"] = odatSource;
                    whdg1.DataSource = odatSource;
                    whdg1.DataBind();
                    
    

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    slq.Close();
                }
            
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {


            if (Session["Muestra_ArtSinPedido"] != null)
            {


                WebExcelExporter1.Export(this.whdg1);

                /*WebHierarchicalDataSource odatSource = (WebHierarchicalDataSource)Session["Muestra_ArtSinPedido"];
                this.whdg1.InitialDataBindDepth = -1;
                this.whdg1.DataSource = odatSource;

                //Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter oExportar = new Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter();
                Infragistics.Documents.Excel.Workbook oExcel = new Infragistics.Documents.Excel.Workbook();
                oExcel.Worksheets.Add("ArtSinPedido");
                Infragistics.Web.UI.GridControls.WebExcelExporter oExportar = new Infragistics.Web.UI.GridControls.WebExcelExporter();

                //Se exporta el grid
                
                oExportar.Export(this.whdg1, oExcel.Worksheets[0], 6, 0);

                //Se agrega el título
                oExcel.Worksheets[0].Rows[0].Cells[2].Value = "Sistema de Pedido Sugerido: Reporte de Artículos sin Pedido";
                oExcel.Worksheets[0].Rows[0].Cells[2].CellFormat.Font.Bold = Infragistics.Documents.Excel.ExcelDefaultableBoolean.True;

                //Se agregan los filtros
                oExcel.Worksheets[0].Rows[1].Cells[0].Value = "Comercializadoras";
                oExcel.Worksheets[0].Rows[1].Cells[1].Value = txtCompDesc.Text;
                
                oExcel.Worksheets[0].Rows[2].Cells[0].Value = "Clientes";
                oExcel.Worksheets[0].Rows[2].Cells[1].Value = txtClienteDesc.Text;

                oExcel.Worksheets[0].Rows[3].Cells[0].Value = "Sucursales Entrega";
                oExcel.Worksheets[0].Rows[3].Cells[1].Value = txtEntrega.Text;

                oExcel.Worksheets[0].Rows[4].Cells[0].Value = "Fecha Reporte";
                oExcel.Worksheets[0].Rows[4].Cells[1].Value = DateTime.Now.ToString();

                oExcel.Save(Request.PhysicalApplicationPath + "Simulacion\\archivos\\Reporte_ArtsSinPedido.xls");

                System.IO.FileInfo ofile = new System.IO.FileInfo(Request.PhysicalApplicationPath + "Simulacion\\archivos\\Reporte_ArtsSinPedido.xls");
                Response.AddHeader("content-disposition", "attachment;filename=" + "Reporte_ArtsSinPedido.xls");
                Response.AddHeader("Content-Length", ofile.Length.ToString());
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Charset = "";
                Response.TransmitFile(ofile.FullName);

                this.whdg1.InitialDataBindDepth = 0;*/
            }
        }

        private String FormatoFecha(String fecha)
        {
            String result;
            result = fecha.Substring(6, 4) + fecha.Substring(3, 2) + fecha.Substring(0, 2);
            return result;
        }

        private DateTime ConvertirFecha(String fecha)
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("es-MX", true);
            DateTime dt2 = DateTime.Parse(fecha, culture, System.Globalization.DateTimeStyles.AssumeLocal);
            return dt2;
        }

        protected void whdg1_Init1(object sender, EventArgs e)
        {
            if (Session["Muestra_ArtSinPedido"] != null && Page.IsPostBack == true)
            {
                WebHierarchicalDataSource odatSource = (WebHierarchicalDataSource)Session["Muestra_ArtSinPedido"];
                this.whdg1.DataSource = odatSource;
            }
        }




        #region getBackorder
        private com.lamosa.sap.services.backorder.DT_BackOrder_response getBackorder(System.Data.DataView dvItems, String id_cia, String id_cliente, String id_sucursal)
        {
            try
            {
                com.lamosa.sap.services.backorder.SI_BackOrder_OutboundClient ws_client = new com.lamosa.sap.services.backorder.SI_BackOrder_OutboundClient("HTTP_Port3");

                com.lamosa.sap.services.backorder.DT_BackOrder_request ws_request = new com.lamosa.sap.services.backorder.DT_BackOrder_request();
                com.lamosa.sap.services.backorder.DT_BackOrder_response ws_response = new com.lamosa.sap.services.backorder.DT_BackOrder_response();



                com.lamosa.sap.services.backorder.DT_BackOrder_requestStore[] reqStores = new com.lamosa.sap.services.backorder.DT_BackOrder_requestStore[1];
                com.lamosa.sap.services.backorder.DT_BackOrder_requestItem[] reqItems = new com.lamosa.sap.services.backorder.DT_BackOrder_requestItem[dvItems.Count];


                ws_client.ClientCredentials.UserName.UserName = System.Configuration.ConfigurationManager.AppSettings["userWSSAP"];
                ws_client.ClientCredentials.UserName.Password = System.Configuration.ConfigurationManager.AppSettings["pwdWSSAP"];

                com.lamosa.sap.services.backorder.DT_BackOrder_requestStore reqStore = new com.lamosa.sap.services.backorder.DT_BackOrder_requestStore();
                reqStore.StoreId = id_sucursal;
                reqStores[0] = reqStore;




                for (int int_reg = 0; int_reg < dvItems.Count; int_reg++)
                {
                    com.lamosa.sap.services.backorder.DT_BackOrder_requestItem reqItem = new com.lamosa.sap.services.backorder.DT_BackOrder_requestItem();
                    reqItem.ItemId = dvItems[int_reg]["id_articulo_revestimientos"].ToString();
                    reqItems[int_reg] = reqItem;
                }

                ws_request.CompayId = id_cia;
                ws_request.CustomerId = id_cliente;
                ws_request.Items = reqItems;
                ws_request.Stores = reqStores;

                ws_response = ws_client.SI_BackOrder_Outbound(ws_request);

                return ws_response;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al Invocar el Servicio de SAP. " + ex.Message, ex);
            }


        }

        #endregion getBackorder
    }
}
