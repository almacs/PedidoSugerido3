using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using Infragistics.WebUI.Shared;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Infragistics.Web.UI.ListControls;
using System.Collections.Generic;
using Infragistics.WebUI.UltraWebGrid;
using System.ComponentModel;
using CrystalDecisions.Shared;
using Infragistics.Shared;
using Infragistics.Documents.Excel;
using Business;
using System.Data.OleDb;
using System.Text;
using System.IO;

namespace PedidoSugeridoLamosa.Simulacion
{
    public partial class VentasPedidos : System.Web.UI.Page
    {
        #region fields

        private UsuarioInf UsrInf = null;
        private Dictionary<String, ArrayList> mapZonas = new Dictionary<string, ArrayList>();
        private DataTable dtArtCliente = null;
        private String sUnidadMedida = String.Empty;
        private bool bit_mess = false;
        private bool bit_load = true;
        private bool bit_cambio = false;
        public String perfilConsulta = "";

        #endregion fields

        #region loadBody
        private void loadBody(String strXML)
        {
            DataSet odsResult = new DataSet();
            DataTable dtDetalle = new DataTable();
            String sUnidadMedida = String.Empty;
            DateTime dtFechaSimulacion = DateTime.MinValue;

            dtDetalle.Columns.Add("Cve_Articulo_Cliente", typeof(String));
            dtDetalle.Columns.Add("Cve_Articulo", typeof(String));
            dtDetalle.Columns.Add("Desc_Articulo", typeof(String));
            dtDetalle.Columns.Add("Val_Min_Cliente", typeof(int));
            dtDetalle.Columns.Add("Val_Max_Cliente", typeof(int));
            dtDetalle.Columns.Add("Val_Inv_Cliente", typeof(int));

            //BackOrder
            dtDetalle.Columns.Add("Val_Ped_Pendiente", typeof(float));
            dtDetalle.Columns.Add("Val_Ped_SCred", typeof(float));
            dtDetalle.Columns.Add("Val_Ped_PendAssignCInv", typeof(float));
            dtDetalle.Columns.Add("Val_Ped_PendAssignSInv", typeof(float));
            dtDetalle.Columns.Add("Val_Ped_Assign", typeof(float));
            dtDetalle.Columns.Add("Val_Ped_InTransit", typeof(float));

            dtDetalle.Columns.Add("Val_PedMin_Cliente", typeof(float));
            dtDetalle.Columns.Add("Val_PedMax_Faltante", typeof(float));
            dtDetalle.Columns.Add("Val_PedCrit_Faltante", typeof(float));
            dtDetalle.Columns.Add("Val_Ajuste_Cliente", typeof(float));
            dtDetalle.Columns.Add("Val_Final", typeof(float));
            dtDetalle.Columns.Add("Cve_Zona", typeof(String));
            dtDetalle.Columns.Add("Val_LeadTime", typeof(String));
            dtDetalle.Columns.Add("Val_M2_Tarima", typeof(float));
            dtDetalle.Columns.Add("Val_Inv", typeof(float));
            dtDetalle.Columns.Add("Bit_Descontinuado", typeof(bool));

            BSimulacion osimulacion = new BSimulacion(new ConnectionLP().getConnection());
            odsResult = osimulacion.getSimulation(this.UsrInf.id_compania, this.UsrInf.id_cliente, this.UsrInf.id_sucursal, strXML, this.bit_load, this.bit_cambio, this.UsrInf.Id_usuario, this.UsrInf.id_tipo_usuario);
            DataView dvItems = new DataView(odsResult.Tables[0]);
            com.lamosa.sap.services.simulationAct.DT_Simulation_response ws_response = this.getSimulation(dvItems, odsResult.Tables[1].Rows[0]["Ids_Entrega"].ToString().Split(','));
            dtDetalle = odsResult.Tables[0];
            dtDetalle.Columns.Add("Val_Ped_InTransit_Update", typeof(bool));
            dtDetalle.Columns.Add("Act_PU_Facturacion", typeof(float));
            dtDetalle.Columns.Add("Act_Monto_Sugerido", typeof(float));
            dtDetalle.Columns.Add("Act_Historico_M1", typeof(float));
            dtDetalle.Columns.Add("Act_Historico_M2", typeof(float));
            dtDetalle.Columns.Add("Act_Historico_M3", typeof(float));

            
            //Llenar Campos de DataTable
            for (int int_reg = 0; int_reg < dtDetalle.Rows.Count; int_reg++)
            {
                String strCve_Articulo = dtDetalle.Rows[int_reg]["Cve_Articulo"].ToString();
                int int_itemws = -1;
                for (int int_item = 0; int_item < ws_response.Items.Length; int_item++)
                {
                    if (strCve_Articulo.Equals(ws_response.Items[int_item].ItemId))
                    {
                        int_itemws = int_item;
                        break;
                    }
                }
                //El WS no regresó el artículo
                if (-1 == int_itemws)
                {
                    continue;
                }

                dtDetalle.Rows[int_reg]["Val_Ped_Pendiente"] = ws_response.Items[int_itemws].BackOrder.Pending;
                dtDetalle.Rows[int_reg]["Val_Ped_SCred"] = ws_response.Items[int_itemws].BackOrder.NoCredit;
                dtDetalle.Rows[int_reg]["Val_Ped_PendAssignCInv"] = ws_response.Items[int_itemws].BackOrder.PendingToAssign;
                dtDetalle.Rows[int_reg]["Val_Ped_PendAssignSInv"] = ws_response.Items[int_itemws].BackOrder.PendingToAssingNoInventory;
                dtDetalle.Rows[int_reg]["Val_Ped_Assign"] = ws_response.Items[int_itemws].BackOrder.Assigned;
                //if (-1 == Convert.ToInt32(dtDetalle.Rows[int_reg]["Val_Ped_InTransit"]))
                //{
                    dtDetalle.Rows[int_reg]["Val_Ped_InTransit"] = ws_response.Items[int_itemws].BackOrder.InTransit;
                //}
                //dtDetalle.Rows[int_reg]["Val_Ped_InTransitOrig"] = ws_response.Items[int_itemws].BackOrder.InTransit;

                dtDetalle.Rows[int_reg]["Cve_Zona"] = ws_response.Items[int_itemws].ItemZone;
                dtDetalle.Rows[int_reg]["Val_LeadTime"] = ws_response.Items[int_itemws].ItemLeadTime;
                dtDetalle.Rows[int_reg]["Val_M2_Tarima"] = ws_response.Items[int_itemws].PalleteMT2;
                dtDetalle.Rows[int_reg]["Val_Inv"] = ws_response.Items[int_itemws].InventoryPending;
                dtDetalle.Rows[int_reg]["Act_PU_Facturacion"] = ws_response.Items[int_itemws].BillPrice;
                dtDetalle.Rows[int_reg]["Act_Historico_M1"] = ws_response.Items[int_itemws].History.Month_H1;
                dtDetalle.Rows[int_reg]["Act_Historico_M2"] = ws_response.Items[int_itemws].History.Month_H2;
                dtDetalle.Rows[int_reg]["Act_Historico_M3"] = ws_response.Items[int_itemws].History.Month_H3;

                dtDetalle.Rows[int_reg]["Bit_Descontinuado"] = Convert.ToBoolean(ws_response.Items[int_itemws].IsDiscontinued.ToString().ToLower());

                dtDetalle.Rows[int_reg]["Nombre_Cliente"] = ws_response.CustomerName;
                dtDetalle.Rows[int_reg]["Direccion_Cliente"] = ws_response.CustomerAddress;
                dtDetalle.Rows[int_reg]["Nombre_Compania"] = ws_response.CompanyName;
                dtDetalle.Rows[int_reg]["Direccion_Compania"] = ws_response.CompanyAddress;
                dtDetalle.Rows[int_reg]["Nombre_Cliente"] = ws_response.CustomerName;
            }

            
            sUnidadMedida = ws_response.CustomerUnitOfMeasure;
            dtFechaSimulacion = Convert.ToDateTime(odsResult.Tables[1].Rows[0]["fecha_simulacion"]);

            //cálculos
            for (int int_reg = 0; int_reg < dtDetalle.Rows.Count; int_reg++)
            {
                dtDetalle.Rows[int_reg]["IdNum"] = int_reg + 1;
                //variables
                float Val_Ped_SCred = float.Parse(dtDetalle.Rows[int_reg]["Val_Ped_SCred"].ToString());
                float Val_Ped_PendAssignCInv = float.Parse(dtDetalle.Rows[int_reg]["Val_Ped_PendAssignCInv"].ToString());
                float Val_Ped_PendAssignSInv = float.Parse(dtDetalle.Rows[int_reg]["Val_Ped_PendAssignSInv"].ToString());
                float Val_Ped_Assign = float.Parse(dtDetalle.Rows[int_reg]["Val_Ped_Assign"].ToString());
                float Val_Ped_InTransit = float.Parse(dtDetalle.Rows[int_reg]["Val_Ped_InTransit"].ToString());
                float Val_Min_Cliente = float.Parse(dtDetalle.Rows[int_reg]["Val_Min_Cliente"].ToString());
                float Val_PU_Fac = float.Parse(dtDetalle.Rows[int_reg]["Act_PU_Facturacion"].ToString());
                float Val_Max_Cliente = -1;
                if (Convert.IsDBNull(dtDetalle.Rows[int_reg]["Val_Max_Cliente"]))
                {
                    Val_Max_Cliente = 0;
                }
                else
                {
                    Val_Max_Cliente = Convert.ToInt32(dtDetalle.Rows[int_reg]["Val_Max_Cliente"]);
                }
                float Val_Inv_Cliente = -1;
                if (Convert.IsDBNull(dtDetalle.Rows[int_reg]["Val_Inv_Cliente"]))
                {
                    Val_Inv_Cliente = 0;
                }
                else
                {
                    Val_Inv_Cliente = Convert.ToInt32(dtDetalle.Rows[int_reg]["Val_Inv_Cliente"]);
                }
                float Val_Ped_Pendiente = float.Parse(dtDetalle.Rows[int_reg]["Val_Ped_Pendiente"].ToString());
                float Val_Ajuste_Cliente = float.Parse(dtDetalle.Rows[int_reg]["Val_Ajuste_Cliente"].ToString());
                float Val_M2_Tarima = float.Parse(dtDetalle.Rows[int_reg]["Val_M2_Tarima"].ToString());
                String Val_LeadTime = Convert.ToString(dtDetalle.Rows[int_reg]["Val_LeadTime"]);

                String Cve_Zona = dtDetalle.Rows[int_reg]["Cve_Zona"].ToString();

                //Pendientes
                dtDetalle.Rows[int_reg]["Val_Ped_Pendiente"] = Val_Ped_SCred +
                                                                Val_Ped_PendAssignCInv +
                                                                Val_Ped_PendAssignSInv +
                                                                Val_Ped_Assign +
                                                                Val_Ped_InTransit;
                Val_Ped_Pendiente = Val_Ped_SCred +
                                                                Val_Ped_PendAssignCInv +
                                                                Val_Ped_PendAssignSInv +
                                                                Val_Ped_Assign +
                                                                Val_Ped_InTransit;
                //Pedido Máximo Faltante
                //int Val_PedMax_Faltante = Ceiling(Val_Max_Cliente - (Val_Inv_Cliente + Val_Ped_Pendiente), Val_M2_Tarima);
                //Response.Write("<!--Val_Inv_Cliente:" + Val_Inv_Cliente.ToString() + "-->");
                //Response.Write("<!--Val_Max_Cliente:" + Val_Max_Cliente.ToString() + "-->");
                //Response.Write("<!--Val_Ped_Pendiente:" + Val_Ped_Pendiente.ToString() + "-->");
                if (Val_Inv_Cliente <= 0)
                {
                    Val_Inv_Cliente = 0;
                }
                if (Val_Max_Cliente <= 0)
                {
                    Val_Max_Cliente = 0;
                }
                float Val_PedMax_Faltante = Max(0, (Val_Max_Cliente - (Val_Inv_Cliente + Val_Ped_Pendiente)));
                //ACS-01-03-2015: se quito la validación del 90% del faltante
                    //Ceiling(Max(0, (Val_Max_Cliente - (Val_Inv_Cliente + Val_Ped_Pendiente))), Val_M2_Tarima);
                //OMJ-08-09-2011:Sin Redondeo a Tarima, solo si es el mayor al 90% del faltante.
                float Val_FaltanteReal = Max(0, Val_Max_Cliente - (Val_Inv_Cliente + Val_Ped_Pendiente));
                if (Val_FaltanteReal < ((0.9) * Val_PedMax_Faltante))
                {
                    Val_PedMax_Faltante = Val_FaltanteReal;
                }
                dtDetalle.Rows[int_reg]["Val_PedMax_Faltante"] = Val_PedMax_Faltante;
                //Valor Crítico
                //double valCrit = (0.9) * (Val_Min_Cliente - Val_Max_Cliente); //El valor que arroga es un negativo por eso el valor de critico siempre seria el de faltante. Se corrigio formula.
                double valCrit = (0.9) * (Val_Max_Cliente - Val_Min_Cliente);
                float Val_PedCrit_Faltante = 0;
                if (Val_PedMax_Faltante >= valCrit)
                {
                    Val_PedCrit_Faltante = Ceiling(Val_PedMax_Faltante, Val_M2_Tarima);

                }
                else
                {
                    Val_PedCrit_Faltante = Ceiling(0, Val_M2_Tarima);
                }
                dtDetalle.Rows[int_reg]["Val_PedCrit_Faltante"] = Val_PedCrit_Faltante;
                //Final
                float Val_Final = 0;
                if (Val_Ajuste_Cliente < 0)
                {
                    Val_Final = Val_PedCrit_Faltante + Ceiling(Val_Ajuste_Cliente, Val_M2_Tarima * -1);
                }
                else
                {
                    Val_Final = Val_PedCrit_Faltante + Ceiling(Val_Ajuste_Cliente, Val_M2_Tarima);
                }
                dtDetalle.Rows[int_reg]["Val_Final"] = Val_Final;
                dtDetalle.Rows[int_reg]["Act_Monto_Sugerido"] = Val_Final * Val_PU_Fac;
                //if (Val_Final == 0)
                //{
                //    dtDetalle.Rows[int_reg]["Val_LeadTime"] = DBNull.Value;
                //}

                dtDetalle.Rows[int_reg]["Val_Ped_InTransit_Update"] = false;

                //Resumen--Solo si tiene Zona definida
                if (Cve_Zona != "")
                {
                    if (!this.mapZonas.ContainsKey(Cve_Zona))
                    {
                        ArrayList datosZonaInit = new ArrayList(5);
                        datosZonaInit.Add(0.0);
                        datosZonaInit.Add(0.0);
                        datosZonaInit.Add(0.0);
                        datosZonaInit.Add(0.0);
                        datosZonaInit.Add(0.0);

                        mapZonas.Add(Cve_Zona, datosZonaInit);
                    }
                    ArrayList datosZona = mapZonas[Cve_Zona];
                    //Codigo para que tome el factor de conversion
                    Int32 Intfactor = 0;

                    if (sUnidadMedida == "M2")
                    {
                        Intfactor = 1800;
                    }
                    else if (sUnidadMedida == "CJA")
                    {
                        Intfactor = 1200;
                    }
                    else //"PLT"
                    {
                        Intfactor = 18;
                    }
                    datosZona[0] = Convert.ToDouble(datosZona[0]) + Val_PedCrit_Faltante;//Criticos
                    datosZona[1] = (Convert.ToDouble(datosZona[0]) / Intfactor);//TL
                    datosZona[2] = Convert.ToDouble(datosZona[2]) + Val_Final;//Con Ajustes
                    datosZona[3] = (Convert.ToDouble(datosZona[2]) / Intfactor);//TL
                    if (Val_LeadTime != "N/A")
                    {
                        if (Convert.ToDouble(datosZona[4]) < Convert.ToDouble(Val_LeadTime) && Convert.ToDouble(datosZona[2]) > 0.0 && Val_Final > 0)
                        {
                            datosZona[4] = Convert.ToDouble(Val_LeadTime);//Días Promesa
                        }
                    }
                }
            }

            this.grid_Detalle.DisplayLayout.Pager.CurrentPageIndex = 1;
            this.grid_Detalle.DataSource = dtDetalle;
            this.grid_Detalle.DataBind();

            this.grid_Detalle.Columns[3].Format = "#,###.##";
            this.grid_Detalle.Columns[4].Format = "#,###.##";
            this.grid_Detalle.Columns[5].Format = "#,###.##";
            this.grid_Detalle.Columns[6].Format = "#,###.##";
            this.grid_Detalle.Columns[7].Format = "#,###.##";
            this.grid_Detalle.Columns[8].Format = "#,###.##";
            this.grid_Detalle.Columns[9].Format = "#,###.##";
            this.grid_Detalle.Columns[10].Format = "#,###.##";
            this.grid_Detalle.Columns[11].Format = "#,###.##";
            this.grid_Detalle.Columns[12].Format = "#,###.##";
            this.grid_Detalle.Columns[13].Format = "#,###.##";
            this.grid_Detalle.Columns[14].Format = "#,###.##";
            this.grid_Detalle.Columns[15].Format = "#,###.##";
            this.grid_Detalle.Columns[16].Format = "#,###.##";
            this.grid_Detalle.Columns[18].Format = "#,###.##";
            this.grid_Detalle.Columns[19].Format = "#,###.##";
            this.grid_Detalle.Columns[20].Format = "#,###.##";
            this.grid_Detalle.Columns[21].Format = "#,###.##";

            //Establecer BackColor de la celda en rojo si el inventario es 0.
            for (int int_reg = 0; int_reg < dtDetalle.Rows.Count; int_reg++)
            {
                if (dtDetalle.Rows[int_reg]["Val_Inv_Cliente"].ToString() == "0")
                {
                    grid_Detalle.Rows[int_reg].Cells[5].Style.BackColor = System.Drawing.Color.Red;
                }
                //TODO:Validar el Query
                if (Convert.ToBoolean(dtDetalle.Rows[int_reg]["Bit_Descontinuado"]) == true)
                {
                    for (int int_col = 0; int_col < grid_Detalle.Columns.Count; int_col++)
                    {
                        grid_Detalle.Rows[int_reg].Cells[int_col].Style.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }

            this.fecha_simulacion.Text = "Última simulación:" + dtFechaSimulacion.ToString() + ". Cantidades expresadas en: " + sUnidadMedida;

            this.dtArtCliente = dtDetalle;
            Session.Add("dtDetalle", this.dtArtCliente);
            Session.Add("sUnidadMedida", sUnidadMedida);
            Session.Add("sDesFechaSimulacion", this.fecha_simulacion.Text);

            if (this.bit_load && odsResult.Tables[1].Rows[0]["Mensaje"].ToString() != "")
            {
                this.id_messages.Text = "<script>alert('" + odsResult.Tables[1].Rows[0]["Mensaje"].ToString() + "');</script>";
            }
            //dirección de entrega
            Session.Add("DirEntrega", "");
            if (ws_response.Stores.Length > 0)
            {
                String DirEntrega = ws_response.Stores[0].StoreAddress;
                Session["DirEntrega"] = DirEntrega;
            }

        }
        #endregion loadBody
        #region loadHeader
        private void loadHeader()
        {
            DataTable dtResumen = new DataTable();
            dtResumen.Columns.Add("Planta", typeof(String));
            dtResumen.Columns.Add("Sugerido", typeof(Double));
            dtResumen.Columns.Add("T/L", typeof(Double));
            dtResumen.Columns.Add("Con Ajustes", typeof(Double));
            dtResumen.Columns.Add("T/L_", typeof(Double));
            dtResumen.Columns.Add("Dias Promesa", typeof(Int32));

            foreach (KeyValuePair<String, ArrayList> zona in this.mapZonas)
            {
                ArrayList datosZona = zona.Value;
                dtResumen.Rows.Add(new object[] { zona.Key, datosZona[0], datosZona[1], datosZona[2], datosZona[3], datosZona[4] });
            }

            this.grid_Resumen.DataSource = dtResumen;
            this.grid_Resumen.DataBind();

            this.grid_Resumen.Columns[1].Format = "#,###.##";
            this.grid_Resumen.Columns[2].Format = "#,###.##";
            this.grid_Resumen.Columns[3].Format = "#,###.##";
            this.grid_Resumen.Columns[4].Format = "#,###.##";
        }
        #endregion loadHeader
        #region GenSimulacion
        private void GenSimulacion()
        {
            try
            {
                loadBody("");
                loadHeader();
            }
            catch (Exception ex)
            {
                id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
        #endregion GenSimulacion
        #region revisaNulos
        private string revisaNulos(String cadena)
        {
            if (cadena == null || cadena.Equals("null") || cadena.Equals(""))
            {
                return "0";
            }
            return cadena;
        }
        #endregion revisaNulos

        #region CheckData
        private bool CheckData()
        {
            for (int int_reg = 0; int_reg < grid_Detalle.Rows.Count; int_reg++)
            {
                // Inv.minimo, Inv. maximo, Inv. existente, Pedido minimo y Ajustes.
                if (this.grid_Detalle.Rows[int_reg].Cells[3].DataChanged == true ||
                    this.grid_Detalle.Rows[int_reg].Cells[4].DataChanged == true ||
                    this.grid_Detalle.Rows[int_reg].Cells[5].DataChanged == true ||
                    this.grid_Detalle.Rows[int_reg].Cells[13].DataChanged == true ||
                    this.grid_Detalle.Rows[int_reg].Cells[16].DataChanged == true)
                {
                    //Si el Inv. máximo es 0 o el Inv. mínimo es mayor al máximo no es válido.
                    if (Convert.ToInt32(this.grid_Detalle.Rows[int_reg].Cells[3].Value) > Convert.ToInt32(this.grid_Detalle.Rows[int_reg].Cells[4].Value))
                    //|| Convert.ToInt32(grid_Detalle.Rows[int_reg].Cells[4].Value) == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion CheckData
        #region GuardarDatosEnDataTable
        private DataTable GuardarDatosEnDataTable(DataTable dtObj, int PnumPagina)
        {
            int numPagina = 0;
            if (PnumPagina == -1)
                numPagina = this.grid_Detalle.DisplayLayout.Pager.PageSize * (this.grid_Detalle.DisplayLayout.Pager.CurrentPageIndex - 1);
            else
                numPagina = PnumPagina;

            for (int int_reg = 0; int_reg < this.grid_Detalle.Rows.Count; int_reg++)
            {
                if (this.grid_Detalle.Rows[int_reg].Cells[3].DataChanged == true ||
                    this.grid_Detalle.Rows[int_reg].Cells[4].DataChanged == true ||
                    this.grid_Detalle.Rows[int_reg].Cells[5].DataChanged == true ||
                    this.grid_Detalle.Rows[int_reg].Cells[13].DataChanged == true ||
                    this.grid_Detalle.Rows[int_reg].Cells[16].DataChanged == true)
                {
                    if (this.grid_Detalle.Rows[int_reg].Cells[5].DataChanged == true)
                    {
                        this.bit_cambio = true;
                    }
                    if (this.grid_Detalle.Rows[int_reg].Cells[3].Value == null)
                    {
                        this.grid_Detalle.Rows[int_reg].Cells[3].Value = 0;
                    }
                    if (this.grid_Detalle.Rows[int_reg].Cells[4].Value == null)
                    {
                        this.grid_Detalle.Rows[int_reg].Cells[4].Value = 0;
                    }
                    if (this.grid_Detalle.Rows[int_reg].Cells[5].Value == null)
                    {
                        this.grid_Detalle.Rows[int_reg].Cells[5].Value = 0;
                    }
                    if (this.grid_Detalle.Rows[int_reg].Cells[13].Value == null)
                    {
                        this.grid_Detalle.Rows[int_reg].Cells[13].Value = 0;
                    }
                    if (this.grid_Detalle.Rows[int_reg].Cells[16].Value == null)
                    {
                        this.grid_Detalle.Rows[int_reg].Cells[16].Value = 0;
                    }
                    dtObj.Rows[int_reg + numPagina]["Val_Min_Cliente"] = this.grid_Detalle.Rows[int_reg].Cells[3].Value;
                    dtObj.Rows[int_reg + numPagina]["Val_Max_Cliente"] = this.grid_Detalle.Rows[int_reg].Cells[4].Value;
                    dtObj.Rows[int_reg + numPagina]["Val_Inv_Cliente"] = this.grid_Detalle.Rows[int_reg].Cells[5].Value;
                    dtObj.Rows[int_reg + numPagina]["Val_PedMin_Cliente"] = this.grid_Detalle.Rows[int_reg].Cells[13].Value;
                    dtObj.Rows[int_reg + numPagina]["Val_Ajuste_Cliente"] = this.grid_Detalle.Rows[int_reg].Cells[16].Value;

                    this.grid_Detalle.Rows[int_reg].Cells[3].DataChanged = false;
                    this.grid_Detalle.Rows[int_reg].Cells[4].DataChanged = false;
                    this.grid_Detalle.Rows[int_reg].Cells[5].DataChanged = false;
                    this.grid_Detalle.Rows[int_reg].Cells[13].DataChanged = false;
                    this.grid_Detalle.Rows[int_reg].Cells[16].DataChanged = false;
                }

                if (this.grid_Detalle.Rows[int_reg].Cells[11].DataChanged == true)
                {

                    if (this.grid_Detalle.Rows[int_reg].Cells[11].Value == null)
                    {
                        this.grid_Detalle.Rows[int_reg].Cells[11].Value = 0;
                    }
                    dtObj.Rows[int_reg + numPagina]["Val_Ped_InTransit"] = this.grid_Detalle.Rows[int_reg].Cells[11].Value;
                    dtObj.Rows[int_reg + numPagina]["Val_Ped_InTransit_Update"] = true;
                    this.grid_Detalle.Rows[int_reg].Cells[11].DataChanged = false;
                }
            }
            return dtObj;
        }
        #endregion GuardarDatosEnDataTable
        #region GenerateXML
        private String GenerateXML(DataTable otbl)
        {
            StringBuilder strXML = new StringBuilder();
            strXML.Append("<articulos>");
            for (int int_reg = 0; int_reg < otbl.Rows.Count; int_reg++)
            {
                strXML.Append("<articulo>");
                strXML.Append("<cvearticulo>");
                strXML.Append(otbl.Rows[int_reg]["Cve_Articulo"].ToString());
                strXML.Append("</cvearticulo>");
                strXML.Append("<invmin>");
                strXML.Append(otbl.Rows[int_reg]["Val_Min_Cliente"].ToString());
                strXML.Append("</invmin>");
                strXML.Append("<invmax>");
                strXML.Append(otbl.Rows[int_reg]["Val_Max_Cliente"].ToString());
                strXML.Append("</invmax>");
                strXML.Append("<invexistente>");
                strXML.Append(otbl.Rows[int_reg]["Val_Inv_Cliente"].ToString());
                strXML.Append("</invexistente>");
                strXML.Append("<pedmin>");
                strXML.Append(otbl.Rows[int_reg]["Val_PedMin_Cliente"].ToString());
                strXML.Append("</pedmin>");
                strXML.Append("<ajuste>");
                strXML.Append(otbl.Rows[int_reg]["Val_Ajuste_Cliente"].ToString());
                strXML.Append("</ajuste>");
                strXML.Append("<intransit>");
                if ((bool)otbl.Rows[int_reg]["Val_Ped_InTransit_Update"] == true)
                {
                    strXML.Append(otbl.Rows[int_reg]["Val_Ped_InTransit"].ToString());
                }
                strXML.Append("</intransit>");
                strXML.Append("</articulo>");
            }
            strXML.Append("</articulos>");

            return strXML.ToString();
        }
        #endregion GenerateXML
        #region Ceiling
        private float Ceiling(float f_input, float f_ceilTo)
        {
            //Funcion de Excel

            if (f_ceilTo == 0)
                return 0;

            int input = Convert.ToInt32(f_input);
            int ceilTo = Convert.ToInt32(f_ceilTo);

            if ((input % ceilTo) != 0)
            {
                return ((float)(input / ceilTo) * ceilTo) + ceilTo;
            }
            else
            {
                return input;
            }
        }
        #endregion Ceiling
        #region Ceiling
        private float Max(float A, float B)
        {
            if (A > B)
                return A;
            return B;
        }
        #endregion Ceiling

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                this.Response.Redirect("../Login.aspx", true);
                return;
            }

            this.UsrInf = (UsuarioInf)Session["usuario"];
            perfilConsulta = UsrInf.Nombre_rol.ToString();

            if (this.bit_mess == false)
                this.id_messages.Text = "";

            // Agregar el tooltip a los asp:button
            this.btnCleanAjus.Attributes.Add("onmouseover", "return escape('Cambia a Cero los valores de la columna “Ajustes”')");
            this.btnProcesar.Attributes.Add("onmouseover", "return escape('Recalcula los datos para generar el Pedido Sugerido')");
            this.btnArtDesc.Attributes.Add("onmouseover", "return escape('Permite quitar los Artículos Descontinuados (Artículos Marcados en Rojo)')");
            this.btnCleanInv.Attributes.Add("onmouseover", "return escape('Cambia a Cero los valores de la columna “Inventario Existente”.')");
            this.btnExportar.Attributes.Add("onmouseover", "return escape('Permite exportar a Excel la tabla completa de simulación.')");

            //primera vez que entra a la página
            if (!this.Page.IsPostBack)
            {
                Session.Remove("dtDetalle");
                this.bit_load = true;
                this.GenSimulacion();
            }
            else
            {
                this.dtArtCliente = (DataTable)Session["dtDetalle"];
                this.sUnidadMedida = (String)Session["sUnidadMedida"];
            }

            if (perfilConsulta.Equals("Consulta"))
            {
                btnProcesar.Visible = false;
                btnArtDesc.Visible = false;
                btnCleanInv.Visible = false;
                btnCleanAjus.Visible = false;
                btnExportarOC.Visible = false;

                grid_Detalle.Columns[3].AllowUpdate = AllowUpdate.No;
                grid_Detalle.Columns[4].AllowUpdate = AllowUpdate.No;
                grid_Detalle.Columns[5].AllowUpdate = AllowUpdate.No;
                grid_Detalle.Columns[11].AllowUpdate = AllowUpdate.No;
                grid_Detalle.Columns[12].AllowUpdate = AllowUpdate.No;
                grid_Detalle.Columns[13].AllowUpdate = AllowUpdate.No;
                grid_Detalle.Columns[16].AllowUpdate = AllowUpdate.No;

            }
            if (perfilConsulta.Equals("Asesor"))
            {
                btnProcesar.Visible = false;
                btnArtDesc.Visible = false;
                btnCleanInv.Visible = false;
                btnCleanAjus.Visible = false;
                btnExportarOC.Visible = false;

                grid_Detalle.Columns[3].AllowUpdate = AllowUpdate.No;
                grid_Detalle.Columns[4].AllowUpdate = AllowUpdate.No;
                grid_Detalle.Columns[5].AllowUpdate = AllowUpdate.No;
                grid_Detalle.Columns[11].AllowUpdate = AllowUpdate.No;
                grid_Detalle.Columns[12].AllowUpdate = AllowUpdate.No;
                grid_Detalle.Columns[13].AllowUpdate = AllowUpdate.No;
                grid_Detalle.Columns[16].AllowUpdate = AllowUpdate.No;
            }
            grid_Detalle.Columns[13].Hidden = true;
            grid_Detalle.Columns[16].Hidden = true;
        }
        #endregion Page_Load
        #region btnProcesar_Click
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable dtDetalle = (DataTable)Session["dtDetalle"];
                //int numPagina = this.grid_Detalle.DisplayLayout.Pager.PageSize * (this.grid_Detalle.DisplayLayout.Pager.CurrentPageIndex - 1);

                if (!CheckData())
                {
                    this.id_messages.Text = "<script>alert('Los campos de valor del inventario no son correctos, favor de validarlos.');</script>";
                    this.bit_mess = true;
                    return;
                }

                this.GuardarDatosEnDataTable(this.dtArtCliente, -1);
                String sXML = this.GenerateXML(this.dtArtCliente);
                this.bit_load = false;
                this.loadBody(sXML);
                this.loadHeader();
                this.txtCambio.Text = "0";
            }
            catch (Exception ex)
            {
                this.id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
        #endregion btnProcesar_Click
        #region btnGuardar_Click
        /*protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable dtDetalle = (DataTable)Session["dtDetalle"];
                //int numPagina = this.grid_Detalle.DisplayLayout.Pager.PageSize * (this.grid_Detalle.DisplayLayout.Pager.CurrentPageIndex - 1);

                if (!this.CheckData())
                {
                    this.id_messages.Text = "<script>alert('Los campos de valor del inventario no son correctos, favor de validarlos.');</script>";
                    this.bit_mess = true;
                    return;
                }

                this.GuardarDatosEnDataTable(this.dtArtCliente, -1);
                String sXML = this.GenerateXML(this.dtArtCliente);
                this.bit_load = false;
                this.loadBody(sXML);
                this.loadHeader();
                this.id_messages.Text = "<script>alert('Los datos se han guardado satisfatoriamente.');</script>";
                this.txtCambio.Text = "0";
            }
            catch (Exception ex)
            {
                this.id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }*/
        #endregion btnGuardar_Click
        #region btnExportar_Click
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            String strCommand = "";
            String step = "0";
            //Request.ApplicationPath
            String strFileRes = Request.PhysicalApplicationPath + @"\Simulacion\archivos\PedidoSugeridoLayout.xls";
            String strFileResDes = strFileRes.Replace("Layout.xls", "") + this.UsrInf.Nombre_sucursal + ".xls";
            System.IO.File.Copy(strFileRes, strFileResDes, true);
            //Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\MyExcel.xls;Extended Properties="Excel 8.0;HDR=Yes;IMEX=1";
            //string strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + strFileResDes + ";Excel 12.0;HDR=YES;";
            string strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + strFileResDes + ";Excel 8.0;HDR=Yes";
            OleDbConnection connExcel = new OleDbConnection(strExcelConn);
            OleDbCommand cmdExcel = new OleDbCommand();
            try
            {
                cmdExcel.Connection = connExcel;
                //Check if the Sheet Exists
                connExcel.Open();
                step = "1";
                String strColumns = System.Configuration.ConfigurationManager.AppSettings["ExcelLayoutDetalle"];


                DataTable otbl = (DataTable)Session["dtDetalle"];

                for (int int_reg = 0; int_reg < otbl.Rows.Count; int_reg++)
                {
                    step = "2";
                    strCommand = "'" + revisaNulos(otbl.Rows[int_reg]["Cve_Articulo_Cliente"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Cve_Articulo"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Desc_Articulo"].ToString().Replace("'", "´")) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Val_Min_Cliente"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Val_Max_Cliente"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Val_Inv_Cliente"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Val_Ped_Pendiente"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Val_Ped_SCred"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Val_Ped_PendAssignCInv"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Val_Ped_PendAssignSInv"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Val_Ped_Assign"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Val_Ped_InTransit"].ToString()) + "','" +
                        //revisaNulos(otbl.Rows[int_reg]["Val_PedMin_Cliente"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Val_PedMax_Faltante"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Val_PedCrit_Faltante"].ToString()) + "','" +
                        //revisaNulos(otbl.Rows[int_reg]["Val_Ajuste_Cliente"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Val_Final"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Cve_Zona"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Val_LeadTime"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Val_M2_Tarima"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Val_Inv"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Act_PU_Facturacion"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Act_Monto_Sugerido"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Act_Historico_M1"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Act_Historico_M2"].ToString()) + "','" +
                                              revisaNulos(otbl.Rows[int_reg]["Act_Historico_M3"].ToString()) + "'";


                    strCommand = "INSERT INTO [Pedidos$] (" + strColumns + ") VALUES(" + strCommand + ") ";
                    System.Console.WriteLine(strCommand);
                    step = "3";
                    cmdExcel.CommandText = strCommand;
                    step = "4";
                    cmdExcel.ExecuteNonQuery();
                    step = "5";
                }

                step = "6";

                strColumns = System.Configuration.ConfigurationManager.AppSettings["ExcelLayoutResumen"];

                for (int int_reg = 0; int_reg < grid_Resumen.Rows.Count; int_reg++)
                {
                    step = "7";
                    strCommand = "'" + revisaNulos(grid_Resumen.Rows[int_reg].Cells[0].Value.ToString()) + "','" +
                                            revisaNulos(grid_Resumen.Rows[int_reg].Cells[1].Value.ToString()) + "','" +
                                            revisaNulos(grid_Resumen.Rows[int_reg].Cells[2].Value.ToString()) + "','" +
                                            revisaNulos(grid_Resumen.Rows[int_reg].Cells[3].Value.ToString()) + "','" +
                                            revisaNulos(grid_Resumen.Rows[int_reg].Cells[4].Value.ToString()) + "','" +
                                            revisaNulos(grid_Resumen.Rows[int_reg].Cells[5].Value.ToString()) + "'";

                    strCommand = "INSERT INTO [Resumen$] (" + strColumns + ") VALUES(" + strCommand + ") ";

                    System.Console.WriteLine(strCommand);
                    step = "8";
                    cmdExcel.CommandText = strCommand;
                    step = "9";
                    cmdExcel.ExecuteNonQuery();
                    step = "10";
                }



                connExcel.Close();
                step = "11";
            }
            catch (Exception ex)
            {
                id_messages.Text = "<script>alert('" + ex.Message + "');</script>";
                Response.Write(ex.StackTrace);
                Response.Write(strCommand);
                Response.Write(".step:" + step);
                return;
            }
            finally
            {
                if (connExcel != null && connExcel.State != ConnectionState.Closed)
                    connExcel.Close();
                cmdExcel.Dispose();
                connExcel.Dispose();

            }
            System.IO.FileInfo ofile = new FileInfo(Request.PhysicalApplicationPath + "Simulacion\\archivos\\PedidoSugerido" + this.UsrInf.Nombre_sucursal + ".xls");
            Response.AddHeader("content-disposition", "attachment;filename=" + "PedidoSugerido" + this.UsrInf.Nombre_sucursal + ".xls");
            Response.AddHeader("Content-Length", ofile.Length.ToString());
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Charset = "";
            Response.TransmitFile(ofile.FullName);
        }
        #endregion btnExportar_Click
        #region btnCleanInv_Click
        protected void btnCleanInv_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDetalle = (DataTable)Session["dtDetalle"];
                int numPagina = grid_Detalle.DisplayLayout.Pager.PageSize * (grid_Detalle.DisplayLayout.Pager.CurrentPageIndex - 1);
                bool bit_valido = CheckData();
                if (bit_valido == false)
                {
                    id_messages.Text = "<script>alert('Los campos de valor del inventario no son correctos, favor de validarlos.');</script>";
                    bit_mess = true;
                    return;
                }

                this.GuardarDatosEnDataTable(dtDetalle, -1);

                for (int int_reg = 0; int_reg < dtDetalle.Rows.Count; int_reg++)
                {
                    dtDetalle.Rows[int_reg]["Val_Inv_Cliente"] = 0;
                }
                String strXML = GenerateXML(dtDetalle);
                bit_load = false;
                loadBody(strXML);
                loadHeader();
                id_messages.Text = "<script>alert('El inventario actual se ha inicializado en 0 en todos los articulos.');</script>";
                txtCambio.Text = "0";
            }
            catch (Exception ex)
            {
                id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
        #endregion btnCleanInv_Click
        #region btnCleanAjus_Click
        protected void btnCleanAjus_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDetalle = (DataTable)Session["dtDetalle"];
                int numPagina = grid_Detalle.DisplayLayout.Pager.PageSize * (grid_Detalle.DisplayLayout.Pager.CurrentPageIndex - 1);
                bool bit_valido = CheckData();
                if (bit_valido == false)
                {
                    id_messages.Text = "<script>alert('Los campos de valor del inventario no son correctos, favor de validarlos.');</script>";
                    bit_mess = true;
                    return;
                }
                this.GuardarDatosEnDataTable(dtDetalle, -1);
                for (int int_reg = 0; int_reg < dtDetalle.Rows.Count; int_reg++)
                {
                    dtDetalle.Rows[int_reg]["Val_Ajuste_Cliente"] = 0;
                }
                String strXML = GenerateXML(dtDetalle);
                bit_load = false;
                loadBody(strXML);
                loadHeader();
                id_messages.Text = "<script>alert('El ajuste se ha inicializado en 0 en todos los articulos.');</script>";
                txtCambio.Text = "0";
            }
            catch (Exception ex)
            {
                id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
        #endregion btnCleanAjus_Click
        #region grid_Detalle_PageIndexChanged
        protected void grid_Detalle_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            try
            {
                int numPagina = grid_Detalle.DisplayLayout.Pager.PageSize * (e.OldPageIndex - 1);
                DataTable dtDetalle = (DataTable)Session["dtDetalle"];
                bool bit_valido = CheckData();
                if (bit_valido == false)
                {
                    id_messages.Text = "<script>alert('Los campos de valor del inventario no son correctos, favor de validarlos.');</script>";
                    bit_mess = true;
                    return;
                }
                this.GuardarDatosEnDataTable(dtDetalle, numPagina);
                Session.Add("dtDetalle", dtDetalle);
                grid_Detalle.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
                grid_Detalle.DataSource = dtDetalle;
                grid_Detalle.DataBind();
            }
            catch (Exception ex)
            {
                id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
        #endregion grid_Detalle_PageIndexChanged
        #region btComentariosSi_Click
        protected void btComentariosSi_Click(object sender, EventArgs e)
        {
            try
            {
                this.GuardarDatosEnDataTable(this.dtArtCliente, -1);

                DataView dvObj = new DataView(this.dtArtCliente);
                dvObj.RowFilter = "Val_Final > 0";
                if (dvObj.Count == 0)
                {
                    this.id_messages.Text = "<script>alert('No se puede exportar a PDF ya que no existen artículos con Pedido Sugerido.');</script>";
                    return;
                }
                else
                {
                    Session.Add("dtArtClienteConValFinal", dvObj.ToTable());
                    this.Response.Redirect("PedidosSugeridosComentarios.aspx", true);
                }
            }
            catch (Exception ex)
            {
                id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
        #endregion btComentariosSi_Click
        #region btComentariosNo_Click
        protected void btComentariosNo_Click(object sender, EventArgs e)
        {
            try
            {
                this.GuardarDatosEnDataTable(this.dtArtCliente, -1);

                DataView dvObj = new DataView(this.dtArtCliente);
                dvObj.RowFilter = "Val_Final > 0";
                if (dvObj.Count == 0)
                {
                    this.id_messages.Text = "<script>alert('No se puede exportar a PDF ya que no existen artículos con Pedido Sugerido.');</script>";
                    return;
                }
                else
                {
                    PedidoSugerido3 rptReporte = new PedidoSugerido3();
                    //rptReporte.Load(Server.MapPath("PedidoSugerido3.rpt"));

                    rptReporte.SetDataSource(dvObj);
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
            }
            catch (Exception ex)
            {
                this.id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
        #endregion btComentariosNo_Click
        #region btnArtDesc_Click
        protected void btnArtDesc_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                this.id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
        #endregion btnArtDesc_Click
        
        #region getSimulation
        private com.lamosa.sap.services.simulationAct.DT_Simulation_response getSimulation(DataView dvItems, String[] Ids_Entrega)
        {
            try
            {
                com.lamosa.sap.services.simulationAct.SI_Simulation_OutboundClient ws_client = new com.lamosa.sap.services.simulationAct.SI_Simulation_OutboundClient("HTTP_Port4");

                com.lamosa.sap.services.simulationAct.DT_Simulation_request ws_request = new com.lamosa.sap.services.simulationAct.DT_Simulation_request();
                com.lamosa.sap.services.simulationAct.DT_Simulation_response ws_response = new com.lamosa.sap.services.simulationAct.DT_Simulation_response();

                com.lamosa.sap.services.simulationAct.DT_Simulation_requestStore[] reqStores = new com.lamosa.sap.services.simulationAct.DT_Simulation_requestStore[Ids_Entrega.Length];
                com.lamosa.sap.services.simulationAct.DT_Simulation_requestItem[] reqItems = new com.lamosa.sap.services.simulationAct.DT_Simulation_requestItem[dvItems.Count];


                ws_client.ClientCredentials.UserName.UserName = System.Configuration.ConfigurationManager.AppSettings["userWSSAP"];
                ws_client.ClientCredentials.UserName.Password = System.Configuration.ConfigurationManager.AppSettings["pwdWSSAP"];

                for (int int_reg = 0; int_reg < Ids_Entrega.Length; int_reg++)
                {
                    com.lamosa.sap.services.simulationAct.DT_Simulation_requestStore reqStore = new com.lamosa.sap.services.simulationAct.DT_Simulation_requestStore();
                    reqStore.StoreId = Ids_Entrega[int_reg];
                    reqStores[int_reg] = reqStore;
                }



                for (int int_reg = 0; int_reg < dvItems.Count; int_reg++)
                {
                    com.lamosa.sap.services.simulationAct.DT_Simulation_requestItem reqItem = new com.lamosa.sap.services.simulationAct.DT_Simulation_requestItem();
                    reqItem.ItemId = dvItems[int_reg]["Cve_Articulo"].ToString();
                    reqItems[int_reg] = reqItem;
                }

                ws_request.CompayId = this.UsrInf.id_compania;
                ws_request.CustomerId = this.UsrInf.id_cliente;
                ws_request.Items = reqItems;
                ws_request.Stores = reqStores;

                ws_response = ws_client.SI_Simulation_Outbound(ws_request);

                return ws_response;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al Invocar el Servicio de SAP. " + ex.Message, ex);
            }


        }

        #endregion getSimulation
    }
}
