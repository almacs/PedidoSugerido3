using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using System.Data;
using System.Xml.Linq;
using System.Text;
using System.Data.OleDb;
using System.IO;

namespace PedidoSugeridoLamosa.Catalogos
{
    public partial class RelacionCP : System.Web.UI.Page
    {

        #region fields

        private UsuarioInf usrinf = null;

        #endregion fields

        #region SaveData
        private DataView SaveData(DataView otbl, int PnumPagina, bool bit_copiarIds)
        {
            int numPagina = 0;
            if (PnumPagina == -1)
                numPagina = UltraWebGrid1.DisplayLayout.Pager.PageSize * (UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex - 1);
            else
                numPagina = PnumPagina;

            //si el check de todos es verdadero, se modifican todos los checks
            if (chekCopyall.Checked == true)
            {
                for (int int_reg = 0; int_reg < otbl.Count; int_reg++)
                {
                    otbl[int_reg]["Bit_Activo"] = 1;
                }
            }
            else
            {
                //si se desactivó el checado de todos, se limpiarán todos.
                if (txtCheckAllVerify.Text == "1")
                {
                    for (int int_reg = 0; int_reg < otbl.Count; int_reg++)
                    {
                        otbl[int_reg]["Bit_Activo"] = 0;
                    }
                }
                if (txtCheckAllVerify.Text == "2")
                {
                    for (int int_reg = 0; int_reg < otbl.Count; int_reg++)
                    {
                        otbl[int_reg]["Bit_Activo"] = 1;
                    }
                }
                txtCheckAllVerify.Text = "";

                //se verifican los cambios
                for (int int_reg = 0; int_reg < UltraWebGrid1.Rows.Count; int_reg++)
                {
                    if (UltraWebGrid1.Rows[int_reg].Cells[0].DataChanged == true ||
                        UltraWebGrid1.Rows[int_reg].Cells[1].DataChanged == true)
                    {

                        if (Convert.ToBoolean(UltraWebGrid1.Rows[int_reg].Cells[0].Value) == true)
                            otbl[int_reg + numPagina]["Bit_Activo"] = 1;
                        else
                            otbl[int_reg + numPagina]["Bit_Activo"] = 0;
                        otbl[int_reg + numPagina]["Cve_ArticuloCliente"] = UltraWebGrid1.Rows[int_reg].Cells[1].Value;

                        UltraWebGrid1.Rows[int_reg].Cells[0].DataChanged = false;
                        UltraWebGrid1.Rows[int_reg].Cells[1].DataChanged = false;
                    }
                }
            }
            if (bit_copiarIds == true)
            {
                for (int int_reg = 0; int_reg < otbl.Count; int_reg++)
                {
                    if (Convert.ToBoolean(otbl[int_reg]["Bit_Activo"]) == true)
                    {
                        if (otbl[int_reg]["Cve_ArticuloCliente"].ToString() == "")
                        {
                            otbl[int_reg]["Cve_ArticuloCliente"] = otbl[int_reg]["Cve_Articulo"].ToString();
                        }
                    }
                }
            }

            return otbl;
        }
        #endregion SaveData
        #region fill
        private void fill(bool bit_consulta)
        {
            DataSet dt = new DataSet();
            DataView odv;
            if (bit_consulta == true)
            {


                dt = this.getCustomerItems();
                odv = new DataView(dt.Tables[0]);
                Session.Add("dtDetalleCP", odv);
            }
            else
            {
                odv = (DataView)Session["dtDetalleCP"];
            }

            UltraWebGrid1.DataSource = odv;
            UltraWebGrid1.DataBind();
            UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
            UltraWebGrid1.Columns[0].Type = Infragistics.WebUI.UltraWebGrid.ColumnType.CheckBox;
            UltraWebGrid1.Columns[0].Width = Unit.Percentage(10); //Unit.Pixel(10);
            UltraWebGrid1.Columns[0].Header.Caption = "Pedido Sugerido";
            UltraWebGrid1.Columns[0].DataType = "System.Int";
            UltraWebGrid1.Columns[0].EditorControlID = new CheckBox().UniqueID;

            UltraWebGrid1.Columns[1].Header.Caption = "Artículo cliente";
            UltraWebGrid1.Columns[1].Width = Unit.Percentage(15);
            UltraWebGrid1.Columns[1].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
            UltraWebGrid1.Columns[1].FieldLen = 16;
            UltraWebGrid1.Columns[1].DataType = "System.String";

            UltraWebGrid1.Columns[2].Header.Caption = "Articulo genérico";
            UltraWebGrid1.Columns[2].Width = Unit.Percentage(15);

            UltraWebGrid1.Columns[3].Header.Caption = "Descripción";
            UltraWebGrid1.Columns[3].Width = Unit.Percentage(60);

            UltraWebGrid1.Columns[4].Hidden = true;
            //UltraWebGrid1.Columns[5].Hidden = true;
            //UltraWebGrid1.Columns[6].Hidden = true;

            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {
                hddClavesAllCliente.Value = hddClavesAllCliente.Value + dt.Tables[0].Rows[i]["Cve_Articulo"] + ",";
                if (Convert.ToBoolean(dt.Tables[0].Rows[i]["Bit_Activo"]) == true)
                {
                    hddClaveArtCliente.Value = hddClaveArtCliente.Value + dt.Tables[0].Rows[i]["Cve_Articulo"] + ",";
                }
            }
        }
        #endregion fill
        #region GenerateLog
        private void GenerateLog(String strNomArchivo, String strNomArchivoCliente, bool bitDetalle, String DescEstatus, System.Data.DataTable dtDetalle)
        {
            String strNomArchivoLog = strNomArchivo + ".log";
            String strDirLog = System.Configuration.ConfigurationManager.AppSettings["DirLogSeleccion"];
            StringBuilder strTextLog = new StringBuilder();

            strTextLog.Append("Usuario:        " + usrinf.Nombre_usuario + " " + usrinf.Apellido_paterno + " " + usrinf.Apellido_materno + "\r\n");
            strTextLog.Append("Nombre Archivo: " + strNomArchivoCliente + "\r\n");
            strTextLog.Append("Tipo Archivo:   Carga Masiva" + "\r\n");
            strTextLog.Append("Fecha Carga:    " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss" + "\r\n"));
            strTextLog.Append("Estatus Carga:  " + DescEstatus + "\r\n");

            //Detalle
            if (bitDetalle == true)
            {
                strTextLog.Append("\r\nDetalle de Errores:\r\n");
                for (int int_reg = 0; int_reg < dtDetalle.Rows.Count; int_reg++)
                {
                    strTextLog.Append("   articulo: " + dtDetalle.Rows[int_reg]["Cve_Articulo"].ToString());

                    if (Convert.ToBoolean(dtDetalle.Rows[int_reg]["bit_activo"]) == true)
                    {
                        strTextLog.Append("; Estatus: OK");
                    }
                    else
                    {
                        strTextLog.Append("; Estatus: " + dtDetalle.Rows[int_reg]["desc_rechazo"].ToString());
                    }

                    strTextLog.Append("\r\n");
                }
            }

            // Write the string to a file.
            System.IO.StreamWriter ofile = new System.IO.StreamWriter(strDirLog + strNomArchivoLog);
            ofile.WriteLine(strTextLog.ToString());
            ofile.Close();
        }
        #endregion GenerateLog

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("../Login.aspx", true);
                return;
            }
            id_mesaje.Text = "";
            this.usrinf = (UsuarioInf)Session["usuario"];
            if (!Page.IsPostBack)
            {
                hddClienteInSession.Value = usrinf.id_cliente.ToString();
                Session.Remove("dtDetalleCP");
                txtDesc.Attributes.Add("onkeypress", "return txtDesc_onkeypress();");
                hddBanderaBusq.Value = "0";
                fill(true);
                Session.Add("orden", "ASC");
            }
        }
        #endregion Page_Load
        #region btnBuscar_Click
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            hddBanderaBusq.Value = "1";
            // UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex
            UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
            fill(true);
            hddDes.Value = "";
        }
        #endregion btnBuscar_Click
        #region btnAceptar_Click
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                this.usrinf = (UsuarioInf)Session["usuario"];

                // Guarda los datos del Grid en el DataView 'dtDetalleCP'.
                DataView dtDetalle = (DataView)Session["dtDetalleCP"];
                int numPagina = UltraWebGrid1.DisplayLayout.Pager.PageSize * (UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex - 1);
                SaveData(dtDetalle, numPagina, false);
                Session.Add("dtDetalleCP", dtDetalle);


                
                // Obtener los datos del DataView, filtrando los articulos:
                // Que no estan seleccionados para Pedido Sugerido.
                // Que no tienen ClaveArticuloCliente.
                StringBuilder sXml = new StringBuilder();
                sXml.Append("<articulos>");
                for (int int_reg = 0; int_reg < dtDetalle.Count; int_reg++)
                {
                    if (Convert.ToBoolean(dtDetalle[int_reg]["Bit_Activo"]) == true 
                        //&& dtDetalle[int_reg]["Cve_ArticuloCliente"].ToString() != ""
                        )
                    {
                        sXml.Append("<articulo ");
                        sXml.Append(" cliente=\"");
                        sXml.Append(dtDetalle[int_reg]["Cve_ArticuloCliente"].ToString());
                        sXml.Append("\" generico=\"");
                        sXml.Append(dtDetalle[int_reg]["Cve_Articulo"].ToString());
                        sXml.Append("\" descripcion=\"");
                        sXml.Append(dtDetalle[int_reg]["Desc_Articulo"].ToString());
                        sXml.Append("\" calidad=\"");
                        sXml.Append(dtDetalle[int_reg]["id_calidad"].ToString());
                        sXml.Append("\" />");
                    }
                }
                sXml.Append("</articulos>");

                // Guardar los articulos en la base de datos.
                BRelacionCP drp = new BRelacionCP(new ConnectionLP().getConnection());
                DataSet dt = drp.setCustomerItems(this.usrinf.id_cliente, this.usrinf.id_compania, sXml.ToString(), this.usrinf.Id_usuario, this.usrinf.id_sucursal, this.hddBanderaBusq.Value, this.hddDes.Value, null, null);
                DataView dvSeleccion = new DataView(dt.Tables[0]);
                this.setCustomerItems(dvSeleccion);
                string sDesEstatus = dt.Tables[1].Rows[0]["mensaje"].ToString();
                this.id_mesaje.Text = "<script>alert('" + sDesEstatus + "');</script>";

                this.fill(true);
            }
            catch (Exception ex)
            {
                id_mesaje.Text = "<script>alert('" + ex.Message.Replace("'", "´").Replace(Convert.ToChar(13), ' ').Replace(Convert.ToChar(10), ' ') + "');</script>";
            }
        }
        #endregion btnAceptar_Click
        #region btnCargar_Click
        protected void btnCargar_Click(object sender, EventArgs e)
        {
            if (this.File_Cargar.HasFile)
            {
                this.usrinf = (UsuarioInf)Session["usuario"];

                String sNomArchivoCliente = this.File_Cargar.FileName;
                String sNomArchivoClienteSinExt = this.File_Cargar.FileName.Substring(0, this.File_Cargar.FileName.LastIndexOf('.'));
                String sExtNomArchivoCliente = sNomArchivoCliente.Substring(sNomArchivoCliente.LastIndexOf('.') + 1, sNomArchivoCliente.Length - (sNomArchivoCliente.LastIndexOf('.') + 1));
                String sNomArchivoLog = this.usrinf.Id_usuario + "-" + sNomArchivoClienteSinExt + "-" + DateTime.Now.ToString("ddMMyyyy - HHmmss") + ".log";
                String sNomArchivoLogSinExt = sNomArchivoLog.Substring(0, sNomArchivoLog.LastIndexOf('.'));
                String strRazon = String.Empty;

                if ((sExtNomArchivoCliente.ToUpper() != "XLS") && (sExtNomArchivoCliente.ToUpper() != "XLSX"))
                {
                    id_mesaje.Text = "<script>if(confirm('No se pudo cargar el archivo. ¿ Desea ver el registro de carga ?')){ window.document.getElementById('" + this.btn_Log.ClientID + "').click(); }</script>";

                    strRazon = "El archivo no es válido. No es un documento de Excel.";
                    GenerateLog(sNomArchivoLogSinExt, sNomArchivoCliente, false, strRazon, null);

                    ViewState.Add("NomLog", sNomArchivoLog);
                    ViewState.Add("NomCliente", sNomArchivoCliente);
                    return;
                }

                this.File_Cargar.SaveAs(System.Configuration.ConfigurationManager.AppSettings["DirArticulosSeleccion"] + sNomArchivoLogSinExt);

                DataSet dt = new DataSet();
                string pathdir = System.Configuration.ConfigurationManager.AppSettings["DirArticulosSeleccion"] + sNomArchivoLogSinExt;
                string strExcelConn = String.Empty;

                if (sExtNomArchivoCliente.ToUpper() == "XLS")
                {
                    strExcelConn = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + pathdir + ";Extended Properties=\"Excel 8.0;HDR=YES;\"";
                }
                else if (sExtNomArchivoCliente.ToUpper() == "XLSX")
                {
                    strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathdir + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
                }

                OleDbConnection conEx = new OleDbConnection(strExcelConn);
                OleDbCommand cmdComand = new OleDbCommand();
                bool validaArchivo = false;

                try
                {
                    cmdComand.Connection = conEx;
                    conEx.Open();

                    // Valida exista el WorkSheet 'relacionAP'.
                    System.Data.DataTable dtTables = conEx.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    for (int int_reg = 0; int_reg < dtTables.Rows.Count; int_reg++)
                    {
                        if (dtTables.Rows[int_reg]["TABLE_NAME"].ToString().Equals("relacionAP$"))
                        {
                            validaArchivo = true;
                            break;
                        }
                    }

                    if (!validaArchivo)
                    {
                        strRazon = "El archivo no cumple con la plantilla original. No tiene la pestaña ´relacionAP´";
                    }
                    conEx.Close();

                    // Validacion de las columnas.
                    bool bSeleccionado = false;
                    bool bArticuloCliente = false;
                    bool bArticuloGenerico = false;
                    bool bCalidad = false;
                    bool bDescripcion = false;

                    conEx.Open();
                    System.Data.DataTable dtColumnNames = conEx.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, "relacionAP$", null });
                    for (int int_reg = 0; int_reg < dtColumnNames.Rows.Count; int_reg++)
                    {
                        string nom_col = dtColumnNames.Rows[int_reg]["COLUMN_NAME"].ToString();
                        if (nom_col == "Seleccionado")
                        {
                            bSeleccionado = true;
                        }
                        else if (nom_col == "ArticuloCliente")
                        {
                            bArticuloCliente = true;
                        }
                        else if (nom_col == "ArticuloGenerico")
                        {
                            bArticuloGenerico = true;
                        }
                        else if (nom_col == "Calidad")
                        {
                            bCalidad = true;
                        }
                        else if (nom_col == "Descripcion")
                        {
                            bDescripcion = true;
                        }
                    }

                    if (validaArchivo == true &&
                        (bSeleccionado == false ||
                        bArticuloCliente == false ||
                        bArticuloGenerico == false ||
                        bCalidad == false ||
                        bDescripcion == false))
                    {
                        validaArchivo = false;
                        strRazon = "El archivo no cumple con la plantilla original. No tiene las columnas especificadas.";
                    }
                    conEx.Close();

                    if (validaArchivo == true)
                    {
                        conEx.Open();
                        OleDbCommand cmdselect = new OleDbCommand("SELECT * FROM [relacionAP$] ", conEx);
                        OleDbDataAdapter data = new OleDbDataAdapter();
                        data.SelectCommand = cmdselect;
                        data.Fill(dt);

                        StringBuilder sXml = new StringBuilder();
                        sXml.Append("<articulos>");

                        for (int i = 1; i < dt.Tables[0].Rows.Count; i++)
                        {
                            if (!"".Equals(dt.Tables[0].Rows[i][0].ToString()))
                            {
                                sXml.Append("<articulo");
                                sXml.Append(" seleccion=\"");
                                sXml.Append(dt.Tables[0].Rows[i][0].ToString());
                                sXml.Append("\" cliente=\"");
                                sXml.Append(dt.Tables[0].Rows[i][1].ToString());
                                sXml.Append("\" generico=\"");
                                sXml.Append(dt.Tables[0].Rows[i][2].ToString());
                                sXml.Append("\" descripcion=\"");
                                sXml.Append(dt.Tables[0].Rows[i][4].ToString());
                                sXml.Append("\" calidad=\"");
                                sXml.Append(dt.Tables[0].Rows[i][3].ToString());
                                sXml.Append("\" />");
                            }
                        }
                        sXml.Append("</articulos>");

                        BRelacionCP drp = new BRelacionCP(new ConnectionLP().getConnection());
                        //DataSet dc = drp.GuardaSeleccionArticulos(this.usrinf.id_cliente, this.usrinf.id_compania, sXml.ToString(), this.usrinf.Id_usuario, this.usrinf.id_sucursal, this.hddBanderaBusq.Value, this.hddDes.Value, sNomArchivoLogSinExt, sNomArchivoCliente);
                        DataSet dc = drp.setCustomerItems(this.usrinf.id_cliente, this.usrinf.id_compania, sXml.ToString(), this.usrinf.Id_usuario, this.usrinf.id_sucursal, "0", "", sNomArchivoLogSinExt, sNomArchivoCliente);
                        DataView dvSeleccion = new DataView(dc.Tables[0]);
                        this.setCustomerItems(dvSeleccion);
                        string sDesEstatus = dc.Tables[1].Rows[0]["mensaje"].ToString();
                        bool warn = Convert.ToBoolean(dc.Tables[3].Rows[0]["Warn"]);

                        //se produjo un error controlado
                        if (warn)
                        {
                            GenerateLog(sNomArchivoLogSinExt, sNomArchivoCliente, true, sDesEstatus, dc.Tables[2]);
                            //id_mesaje.Text = "<script>if(confirm('" + sDesEstatus + ". Sin embargo hubo articulos que no se cargaron. Para ver el detalle de la falla presione ´OK´.')){ window.document.getElementById('" + this.btn_Log.ClientID + "').click(); }</script>";
                            //id_mesaje.Text = "<script>if(confirm('" + sDesEstatus + ". Sin embargo hubo articulos que no se cargaron. Para ver el detalle de la falla presione ´OK´.')){ window.document.getElementById('DownloadFileIFrame').src = 'getLog.aspx?NomLog=" + sNomArchivoLog + "&NomCliente=" + sNomArchivoCliente + "'; }</script>";
                            id_mesaje.Text = "<script>if(confirm('" + sDesEstatus + ". Sin embargo hubo articulos que no se cargaron. Para ver el detalle de la falla presione ´OK´.')){ xpos=(screen.width/2)-(300/2); ypos=(screen.height/2)-(200/2);window.open('webDialog.aspx?titulo=Log&src=getLog.aspx&NomLog=" + sNomArchivoLog + "&NomCliente=" + sNomArchivoCliente + "', '_blank', 'left=' + xpos + ',top=' + ypos + ',height=200,width=300,location=no,menubar=no,resizable=yes,scrollbars=no,status=yes,toolbar=no'); }</script>";
                            ViewState.Add("NomLog", sNomArchivoLog);
                            ViewState.Add("NomCliente", sNomArchivoCliente);
                        }
                        else
                        {
                            id_mesaje.Text = "<script>alert('" + sDesEstatus + "'); </script>";
                        }

                        this.fill(true);
                    }
                    else
                    {
                        GenerateLog(sNomArchivoLogSinExt, sNomArchivoCliente, false, strRazon, null);
                        this.id_mesaje.Text = "<script>if(confirm('No se pudo cargar el archivo.  Para ver el detalle de la falla presione ´OK´.')){ window.document.getElementById('" + this.btn_Log.ClientID + "').click(); }</script>";
                        //this.id_mesaje.Text = "<script>if(confirm('No se pudo cargar el archivo.  Para ver el detalle de la falla presione ´OK´.')){ window.document.getElementById('DownloadFileIFrame').src = 'getLog.aspx?NomLog=" + sNomArchivoLog + "&NomCliente=" + sNomArchivoCliente + "'; }</script>";

                        ViewState.Add("NomLog", sNomArchivoLog);
                        ViewState.Add("NomCliente", sNomArchivoCliente);
                    }
                }
                catch (Exception ex)
                {
                    this.id_mesaje.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "'); </script>";
                }
                finally
                {
                    conEx.Close();
                }
            }
        }
        #endregion btnCargar_Click
        #region btnGetPlantilla_Click
        protected void btnGetPlantilla_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            String strFileRes = Request.PhysicalApplicationPath + System.Configuration.ConfigurationManager.AppSettings["DirRelacionAP"];
            String strFileResDes = strFileRes.Replace(".xls", "") + usrinf.Nombre_sucursal + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            System.IO.File.Copy(strFileRes, strFileResDes, true);
            string strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + strFileResDes + ";Excel 8.0;HDR=YES;";
            OleDbConnection connExcel = new OleDbConnection(strExcelConn);
            OleDbCommand cmdExcel = new OleDbCommand();
            try
            {
                cmdExcel.Connection = connExcel;
                connExcel.Open();

                String strColumns = System.Configuration.ConfigurationManager.AppSettings["DirRelacionAPColumns"];

                System.Data.DataTable otbl;
                DataSet dc = this.getCustomerItems();
                otbl = dc.Tables[0];

                for (int int_reg = 0; int_reg < otbl.Rows.Count; int_reg++)
                {
                    string select = "";
                    if ("1".Equals(otbl.Rows[int_reg]["Bit_Activo"].ToString()))
                        select = "X";
                    String strCommand = "'" + select + "',";
                    strCommand += "'" + otbl.Rows[int_reg]["Cve_ArticuloCliente"].ToString() + "',";
                    strCommand += "'" + otbl.Rows[int_reg]["Cve_Articulo"].ToString() + "',";
                    strCommand += "'" + otbl.Rows[int_reg]["id_calidad"].ToString() + "',";
                    strCommand += "'" + otbl.Rows[int_reg]["Desc_Articulo"].ToString().Replace("'", "´") + "'";

                    strCommand = "INSERT INTO [relacionAP$] (" + strColumns + ") VALUES(" + strCommand + ") ";
                    System.Console.WriteLine(strCommand);
                    cmdExcel.CommandText = strCommand;
                    cmdExcel.ExecuteNonQuery();
                }

                connExcel.Close();
            }
            catch (Exception ex)
            {
                id_mesaje.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
                return;
            }
            finally
            {
                if (connExcel != null && connExcel.State != ConnectionState.Closed)
                    connExcel.Close();
                cmdExcel.Dispose();
                connExcel.Dispose();

            }
            System.IO.FileInfo ofile = new FileInfo(strFileResDes);
            Response.AddHeader("content-disposition", "attachment;filename=" + "RelacionAP" + usrinf.Nombre_sucursal + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
            Response.AddHeader("Content-Length", ofile.Length.ToString());
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Charset = "";
            Response.TransmitFile(ofile.FullName);
            Response.End();
        }
        #endregion btnGetPlantilla_Click
        #region btnChangeID_Click
        protected void btnChangeID_Click(object sender, EventArgs e)
        {

        }
        #endregion btnChangeID_Click
        #region BtnRefresh_Click
        protected void BtnRefresh_Click(object sender, EventArgs e)
        {
            fill(true);
        }
        #endregion BtnRefresh_Click
        #region UltraWebGrid1_PageIndexChanged
        protected void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            try
            {
                int numPagina = UltraWebGrid1.DisplayLayout.Pager.PageSize * (e.OldPageIndex - 1);
                DataView dtDetalle = (DataView)Session["dtDetalleCP"];
                SaveData(dtDetalle, numPagina, false);
                Session.Add("dtDetalleCP", dtDetalle);
                UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
                UltraWebGrid1.DataSource = dtDetalle;
                UltraWebGrid1.DataBind();
            }
            catch (Exception ex)
            {
                id_mesaje.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
        #endregion UltraWebGrid1_PageIndexChanged
        #region btnCopiarIds_Click
        protected void btnCopiarIds_Click(object sender, EventArgs e)
        {
            try
            {
                int numPagina = UltraWebGrid1.DisplayLayout.Pager.PageSize * (UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex - 1);
                DataView dtDetalle = (DataView)Session["dtDetalleCP"];
                SaveData(dtDetalle, numPagina, true);
                Session.Add("dtDetalleCP", dtDetalle);
                UltraWebGrid1.DataSource = dtDetalle;
                UltraWebGrid1.DataBind();
            }
            catch (Exception ex)
            {
                id_mesaje.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
        #endregion btnCopiarIds_Click
        #region UltraWebGrid1_SortColumn
        protected void UltraWebGrid1_SortColumn(object sender, Infragistics.WebUI.UltraWebGrid.SortColumnEventArgs e)
        {
            //e.ColumnNo

            try
            {
                //int numPagina = 0;
                //DataView odv = (DataView)Session["dtDetalleCP"];
                //String orden = (String)Session["orden"];
                //if (orden == null)
                //{
                //    orden = "ASC";
                //    Session.Add("orden", "ASC");
                //}
                //odv.Sort = "" + odv.Table.Columns[e.ColumnNo].ColumnName + "  " + Session["orden"];
                //SaveData(odv, numPagina, false);
                //Session.Add("dtDetalleCP", odv);
                //UltraWebGrid1.DataSource = odv;
                //UltraWebGrid1.DataBind();
                //if (Session["orden"].ToString() == "ASC")
                //{
                //    Session["orden"] = "DESC";
                //}
            }
            catch (Exception ex)
            {
                id_mesaje.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }

        }
        #endregion UltraWebGrid1_SortColumn
        #region btnSort_Click
        protected void btnSort_Click(object sender, EventArgs e)
        {
            try
            {
                int numPagina = 0;
                DataView odv = (DataView)Session["dtDetalleCP"];
                String orden = (String)Session["orden"];
                if (orden == null)
                {
                    orden = "ASC";
                    Session.Add("orden", "ASC");
                }

                SaveData(odv, numPagina, false);
                odv.Sort = "" + odv.Table.Columns[Convert.ToInt32(hddIDSortColumn.Value)].ColumnName + "  " + Session["orden"];
                Session.Add("dtDetalleCP", odv);
                UltraWebGrid1.DataSource = odv;
                UltraWebGrid1.DataBind();
                if (Session["orden"].ToString() == "ASC")
                {
                    Session["orden"] = "DESC";
                }
                else
                {
                    Session["orden"] = "ASC";
                }
            }
            catch (Exception ex)
            {
                id_mesaje.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
        #endregion btnSort_Click
        #region btn_Log_Click
        protected void btn_Log_Click(object sender, EventArgs e)
        {
            System.IO.FileInfo ofile = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["DirLogSeleccion"] + ViewState["NomLog"]);
            Response.AddHeader("content-disposition", "attachment;filename=" + ViewState["NomCliente"] + ".log");
            Response.AddHeader("Content-Length", ofile.Length.ToString());
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";// "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Charset = "";
            Response.TransmitFile(ofile.FullName);
            Response.End();
        }
        #endregion btn_Log_Click


        #region getCustomerItems
        private DataSet getCustomerItems()
        {
            com.lamosa.sap.services.itemmaster.SI_ItemMaster_OutboundClient ws_client = new com.lamosa.sap.services.itemmaster.SI_ItemMaster_OutboundClient("HTTP_Port");
            com.lamosa.sap.services.itemmaster.DT_ItemMaster_request ws_request = new com.lamosa.sap.services.itemmaster.DT_ItemMaster_request();
            com.lamosa.sap.services.itemmaster.DT_ItemMaster_response ws_response = new com.lamosa.sap.services.itemmaster.DT_ItemMaster_response();
            com.lamosa.sap.services.itemmaster.DT_ItemMaster_requestItem[] reqItems = new com.lamosa.sap.services.itemmaster.DT_ItemMaster_requestItem[1];
            com.lamosa.sap.services.itemmaster.DT_ItemMaster_requestItem reqItem = new com.lamosa.sap.services.itemmaster.DT_ItemMaster_requestItem();

            ws_client.ClientCredentials.UserName.UserName = System.Configuration.ConfigurationManager.AppSettings["userWSSAP"];
            ws_client.ClientCredentials.UserName.Password = System.Configuration.ConfigurationManager.AppSettings["pwdWSSAP"];

            reqItem.ItemDesc = txtDesc.Text.ToUpper().Trim();
            reqItems[0] = reqItem;

            ws_request.CompayId = usrinf.id_compania.ToString();
            ws_request.CustomerId = usrinf.id_cliente.ToString();
            ws_request.Items = reqItems;

            ws_response = ws_client.SI_ItemMaster_Outbound(ws_request);

            StringBuilder strXML = new StringBuilder();

            strXML.Append("<Items>");

            if (ws_response.Items != null)
            {
                for (int int_reg = 0; int_reg < ws_response.Items.Length; int_reg++)
                {

                    strXML.Append("<Item ");

                    strXML.Append(" ItemCustomerId=\"");
                    strXML.Append(ws_response.Items[int_reg].ItemCustomerId);
                    strXML.Append("\" ItemId=\"");
                    strXML.Append(ws_response.Items[int_reg].ItemId);
                    strXML.Append("\" ItemDesc=\"");
                    strXML.Append(ws_response.Items[int_reg].ItemDesc.Replace("\"", ""));
                    strXML.Append("\" QualityId=\"");
                    strXML.Append(ws_response.Items[int_reg].QualityId);

                    strXML.Append("\" />");
                }
            }

            strXML.Append("</Items>");


            BRelacionCP dx = new BRelacionCP(new ConnectionLP().getConnection());
            return dx.getCustomerItems(usrinf.id_cliente.ToString(), usrinf.id_compania.ToString(), usrinf.id_sucursal, strXML.ToString());

        }

        #endregion getCustomerItems
        #region setCustomerItems
        private void setCustomerItems(DataView dtDetalle)
        {

            com.lamosa.sap.services.itemmasterupdate.SI_ItemMasterUpdate_OutboundClient ws_client = new com.lamosa.sap.services.itemmasterupdate.SI_ItemMasterUpdate_OutboundClient("HTTP_Port1");
            com.lamosa.sap.services.itemmasterupdate.DT_ItemMasterUpdate_request ws_request = new com.lamosa.sap.services.itemmasterupdate.DT_ItemMasterUpdate_request();
            com.lamosa.sap.services.itemmasterupdate.DT_ItemMasterUpdate_response ws_response = new com.lamosa.sap.services.itemmasterupdate.DT_ItemMasterUpdate_response();
            com.lamosa.sap.services.itemmasterupdate.DT_ItemMasterUpdate_requestItem[] reqItems = new com.lamosa.sap.services.itemmasterupdate.DT_ItemMasterUpdate_requestItem[dtDetalle.Count];
            

            ws_client.ClientCredentials.UserName.UserName = System.Configuration.ConfigurationManager.AppSettings["userWSSAP"];
            ws_client.ClientCredentials.UserName.Password = System.Configuration.ConfigurationManager.AppSettings["pwdWSSAP"];


            for (int int_reg = 0; int_reg < dtDetalle.Count; int_reg++)
            {
                if (Convert.ToBoolean(dtDetalle[int_reg]["Bit_Activo"]) == true 
                    //&& dtDetalle[int_reg]["Cve_ArticuloCliente"].ToString() != ""
                    )
                {
                    com.lamosa.sap.services.itemmasterupdate.DT_ItemMasterUpdate_requestItem reqItem = new com.lamosa.sap.services.itemmasterupdate.DT_ItemMasterUpdate_requestItem();
                    reqItem.ItemId = dtDetalle[int_reg]["Cve_Articulo"].ToString();
                    reqItem.ItemCustomerId = dtDetalle[int_reg]["Cve_ArticuloCliente"].ToString();
                    reqItems[int_reg] = reqItem;
                }
            }


            ws_request.CompayId = usrinf.id_compania.ToString();
            ws_request.CustomerId = usrinf.id_cliente.ToString();
            ws_request.Items = reqItems;

            if (dtDetalle.Count > 0)
            {
                ws_response = ws_client.SI_ItemMasterUpdate_Outbound(ws_request);
                String processed = ws_response.Processed;
            }


        }

        #endregion setCustomerItems
    }

}
