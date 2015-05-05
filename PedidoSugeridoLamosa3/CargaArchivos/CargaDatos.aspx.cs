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
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.ComponentModel;
using CrystalDecisions.Shared;
using Infragistics.Shared;
using Infragistics.Documents.Excel;
using Microsoft.Office.Interop.Excel;
using System.Text;
using Business;
using System.Text.RegularExpressions;

namespace PedidoSugeridoLamosa.CargaArchivos
{
	public partial class CargaDatos : System.Web.UI.Page
    {

        #region fields

        private UsuarioInf usrInf;

        #endregion fields

        #region OnlyNumbers
        public static bool OnlyNumbers(string num)
        {
            Regex patron = new Regex("[^\\d,\\d]");
            if (!patron.IsMatch(num))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion OnlyNumbers
        #region GenerateLog
        private void GenerateLog(String strNomArchivo, String strNomArchivoCliente, bool bitDetalle, String DescEstatus, System.Data.DataTable dtDetalle)
        {
            String strNomArchivoLog = strNomArchivo + ".log";

            String strDirLog = System.Configuration.ConfigurationManager.AppSettings["DirLogArchivos"];
            StringBuilder strTextLog = new StringBuilder();
            strTextLog.Append("Usuario:" + usrInf.Nombre_usuario + " " + usrInf.Apellido_paterno + " " + usrInf.Apellido_materno + "\r\n");
            strTextLog.Append("Nombre Archivo:" + strNomArchivoCliente + "\r\n");
            strTextLog.Append("Tipo Archivo: Atributos de Artículos" + "\r\n");
            strTextLog.Append("Fecha Carga:" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss" + "\r\n"));
            strTextLog.Append("Estatus Carga:" + DescEstatus + "\r\n");
            if (bitDetalle == true)//Detalle
            {
                strTextLog.Append("Detalle de Errores:\r\n");
                for (int int_reg = 0; int_reg < dtDetalle.Rows.Count; int_reg++)
                {
                    strTextLog.Append("     articulo:" + dtDetalle.Rows[int_reg]["id_articulo"].ToString());
                    strTextLog.Append(";Estatus:" + dtDetalle.Rows[int_reg]["desc_rechazo"].ToString());
                    strTextLog.Append("\r\n");
                }
                //strTextLog.Append("Motivo de Rechazo:" + DescRazonError + "\r\n");        
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
           id_Inv.Text = "";
           usrInf = (UsuarioInf)Session["usuario"];
        }
        #endregion Page_Load
        #region btnPlantilla_Click
        protected void btnPlantilla_Click(object sender, EventArgs e)
        {
            // NEITEK - Erwin Gomez Rivero.
            // Se agrego la columna 'Descripcion Articulo Cliente' para el reporte en Excel.
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            String strFileRes = Request.PhysicalApplicationPath + System.Configuration.ConfigurationManager.AppSettings["DirInventario"];
            String strFileResDes = strFileRes.Replace(".xls", "") + usrInf.Nombre_sucursal + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            System.IO.File.Copy(strFileRes, strFileResDes, true);

            string strExcelConn = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + strFileResDes + ";Extended Properties=\"Excel 8.0;HDR=YES;\"";
            OleDbConnection connExcel = new OleDbConnection(strExcelConn);
            OleDbCommand cmdExcel = new OleDbCommand();

            try
            {
                cmdExcel.Connection = connExcel;
                connExcel.Open();

                BArticulos dx = new BArticulos(new ConnectionLP().getConnection());
                DataSet dc = dx.getDatosPlantillaAtributos(usrInf.id_cliente, usrInf.id_compania, usrInf.id_sucursal);
                System.Data.DataTable otbl = dc.Tables[0];

                String strColumns = System.Configuration.ConfigurationManager.AppSettings["DirInventarioColumns"];
                String sCveArticuloCliente = String.Empty;
                String sDesArticuloCliente = String.Empty;
                int Val_Inv = -1;
                String sCommand = String.Empty;

                for (int int_reg = 0; int_reg < otbl.Rows.Count; int_reg++)
                {
                    sCveArticuloCliente = "'" + otbl.Rows[int_reg]["Cve_Articulo_Cliente"].ToString() + "'";
                    sDesArticuloCliente = "'" + otbl.Rows[int_reg]["desc_articulo_cliente"].ToString() + "'";
                    Val_Inv = Convert.ToInt32(otbl.Rows[int_reg]["Val_Inv_Cliente"].ToString());

                    sCommand = "INSERT INTO [Inventario$] (" + strColumns + ") VALUES(" + sCveArticuloCliente + ", " + sDesArticuloCliente + ", " + Val_Inv + ") ";
                    System.Console.WriteLine(sCommand);
                    cmdExcel.CommandText = sCommand;
                    cmdExcel.ExecuteNonQuery();
                }
                connExcel.Close();
            }
            catch (Exception ex)
            {
                id_Inv.Text = "<script>alert('" + ex.Message + "');</script>";
            }
            finally
            {
                if (connExcel != null && connExcel.State != ConnectionState.Closed)
                    connExcel.Close();
                cmdExcel.Dispose();
                connExcel.Dispose();

            }

            // Trasmitir el archivo Excel generado.
            System.IO.FileInfo ofile = new FileInfo(strFileResDes);
            Response.AddHeader("content-disposition", "attachment;filename=" + "Inventario" + usrInf.Nombre_sucursal + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
            Response.AddHeader("Content-Length", ofile.Length.ToString());
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Charset = "";
            Response.TransmitFile(ofile.FullName);
            Response.End();
        }
        #endregion btnPlantilla_Click
        #region CargaButton_Click
        protected void CargaButton_Click(object sender, EventArgs e)
        {
            if (Uploadfile.HasFile)
            {
                usrInf = (UsuarioInf)Session["usuario"];

                String sNomArchivoCliente = Uploadfile.FileName;
                String sNomArchivoClienteSinExt = Uploadfile.FileName.Substring(0, Uploadfile.FileName.LastIndexOf('.'));
                String sExtNomArchivoCliente = sNomArchivoCliente.Substring(sNomArchivoCliente.LastIndexOf('.') + 1, sNomArchivoCliente.Length - (sNomArchivoCliente.LastIndexOf('.') + 1));
                String sNomArchivoLog = usrInf.Id_usuario + "-" + sNomArchivoClienteSinExt + "-" + DateTime.Now.ToString("ddMMyyyy - HHmmss") + ".log";
                String sNomArchivoLogSinExt = sNomArchivoLog.Substring(0, sNomArchivoLog.LastIndexOf('.'));
                String strRazon = String.Empty;

                if ( (sExtNomArchivoCliente.ToUpper() != "XLS") && (sExtNomArchivoCliente.ToUpper() != "XLSX") )
                {
                    id_Inv.Text = "<script>if(confirm('No se pudo cargar el archivo. ¿ Desea ver el registro de carga ?')){ window.document.getElementById('ctl00_Principal_btn_Log').click(); }</script>";
                    
                    strRazon = "El archivo no es válido. No es un documento de Excel.";
                    GenerateLog(sNomArchivoLogSinExt, sNomArchivoCliente, false, strRazon, null);

                    ViewState.Add("NomLog", sNomArchivoLog);
                    ViewState.Add("NomCliente", sNomArchivoCliente);
                    return;
                }
                Uploadfile.SaveAs(System.Configuration.ConfigurationManager.AppSettings["DirArchivos"] + sNomArchivoLogSinExt);
                DataSet dt = new DataSet();
                string pathdir = System.Configuration.ConfigurationManager.AppSettings["DirArchivos"] + sNomArchivoLogSinExt;
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

                    // Valida exista el WorkSheet 'Atributos'.
                    System.Data.DataTable dtTables = conEx.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    for (int int_reg = 0; int_reg < dtTables.Rows.Count; int_reg++)
                    {
                        if (dtTables.Rows[int_reg]["TABLE_NAME"].ToString().Equals("Inventario$"))
                        {
                            validaArchivo = true;
                            break;
                        }
                    }

                    if (!validaArchivo)
                    {
                        strRazon = "El archivo no cumple con la plantilla original. No tiene la pestaña ´Inventario´";
                    }
                    conEx.Close();

                    // Validacion de las columnas.
                    bool bitclave = false;
                    bool bDesArticuloCliente = false;
                    bool bitinv = false;
                    conEx.Open();
                    System.Data.DataTable dtColumnNames = conEx.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, "Inventario$", null });
                    for (int int_reg = 0; int_reg < dtColumnNames.Rows.Count; int_reg++)
                    {
                        string nom_col = dtColumnNames.Rows[int_reg]["COLUMN_NAME"].ToString();
                        if (nom_col == "Clave del Artículo Cliente")
                        {
                            bitclave = true;
                        }
                        else if (nom_col == "Descripción del Artículo Cliente")
                        {
                            bDesArticuloCliente = true;
                        }
                        else if (nom_col == "Inventario existente del cliente")
                        {
                            bitinv = true;
                        }
                    }

                    if (validaArchivo == true &&
                            (bitclave == false ||
                            bDesArticuloCliente == false ||
                            bitinv == false))
                    {
                        validaArchivo = false;
                        strRazon = "El archivo no cumple con la plantilla original. No tiene las columnas especificadas.";
                    }
                    conEx.Close();

                    if (validaArchivo)
                    {
                        OleDbCommand cmdselect = new OleDbCommand("SELECT * FROM [Inventario$]", conEx);
                        OleDbDataAdapter data = new OleDbDataAdapter();
                        data.SelectCommand = cmdselect;
                        data.Fill(dt);

                        StringBuilder strXml = new StringBuilder();
                        strXml.Append("<articulos>");
                        bool valida = true;
                        dt.Tables[0].Columns.Add("id_articulo"); 
                        dt.Tables[0].Columns.Add("desc_rechazo");

                        for (int i = 1; i < dt.Tables[0].Rows.Count; i++)
                        {
                            dt.Tables[0].Rows[i]["id_articulo"] = dt.Tables[0].Rows[i][0].ToString();
                            
                            dt.Tables[0].Rows[i]["desc_rechazo"] = "";
                            strXml.Append("<articulo>");
                            strXml.Append("<clave>");
                            strXml.Append(dt.Tables[0].Rows[i][0].ToString());
                            strXml.Append("</clave>");
                            strXml.Append("<inventario>");
                            strXml.Append(dt.Tables[0].Rows[i][2].ToString());
                            strXml.Append("</inventario>");
                            strXml.Append("</articulo>");
                        }
                        strXml.Append("</articulos>");

                        if (!valida)
                        {
                            id_Inv.Text = "<script>if(confirm('No se pudo cargar el archivo. ¿ Desea ver el registro de carga ?')){ window.document.getElementById('ctl00_Principal_btn_Log').click(); }</script>";

                            strRazon = "Uno o mas registros tienen un dato inválido, (Solo acepta numeros el inventario)";
                            GenerateLog(sNomArchivoLogSinExt, sNomArchivoCliente, true, strRazon, dt.Tables[0]);

                            ViewState.Add("NomLog", sNomArchivoLog);
                            ViewState.Add("NomCliente", sNomArchivoCliente);
                            return;
                        }

                        BArticulos dx = new BArticulos(new ConnectionLP().getConnection());
                        DataSet dc = dx.GuardaCargaInventario(usrInf.id_cliente, usrInf.id_compania, usrInf.id_sucursal, strXml.ToString(), sNomArchivoLogSinExt, usrInf.Id_usuario, sNomArchivoCliente);

                        string Estatus = dc.Tables[1].Rows[0]["Estatus"].ToString();
                        string Desc_Estatus = dc.Tables[1].Rows[0]["Desc_Estatus"].ToString();
                        bool warn = ((bool)dc.Tables[1].Rows[0]["Warn"]);
                        
                        //se produjo un error controlado
                        if (Estatus == "1")
                        {
                            GenerateLog(sNomArchivoLogSinExt, sNomArchivoCliente, true, Desc_Estatus, dc.Tables[0]);
                            //id_Inv.Text = "<script>if(confirm('No se cumplen las reglas para realizar la carga. Para ver el detalle de la falla presione ´OK´.')){ window.document.getElementById('ctl00_Principal_btn_Log').click(); }</script>";
                            this.id_Inv.Text = "<script>if(confirm('No se cumplen las reglas para realizar la carga. Para ver el detalle de la falla presione ´OK´.')){ xpos=(screen.width/2)-(300/2); ypos=(screen.height/2)-(200/2);window.open('webDialog.aspx?titulo=Log&src=getLogDatos.aspx&NomLog=" + sNomArchivoLog + "&NomCliente=" + sNomArchivoCliente + "', '_blank', 'left=' + xpos + ',top=' + ypos + ',height=200,width=300,location=no,menubar=no,resizable=yes,scrollbars=no,status=yes,toolbar=no'); }</script>";
                            ViewState.Add("NomLog", sNomArchivoLog);
                            ViewState.Add("NomCliente", sNomArchivoCliente);
                        }
                        else if (warn)
                        {
                            GenerateLog(sNomArchivoLogSinExt, sNomArchivoCliente, true, Desc_Estatus, dc.Tables[0]);
                            //id_Inv.Text = "<script>if(confirm('" + Desc_Estatus + ". Sin embargo hubo articulos que no se cargaron. Para ver el detalle de la falla presione ´OK´.')){ window.document.getElementById('ctl00_Principal_btn_Log').click(); }</script>";
                            this.id_Inv.Text = "<script>if(confirm('" + Desc_Estatus + ". Sin embargo hubo articulos que no se cargaron. Para ver el detalle de la falla presione ´OK´.')){ xpos=(screen.width/2)-(300/2); ypos=(screen.height/2)-(200/2);window.open('webDialog.aspx?titulo=Log&src=getLogDatos.aspx&NomLog=" + sNomArchivoLog + "&NomCliente=" + sNomArchivoCliente + "', '_blank', 'left=' + xpos + ',top=' + ypos + ',height=200,width=300,location=no,menubar=no,resizable=yes,scrollbars=no,status=yes,toolbar=no'); }</script>";
                            ViewState.Add("NomLog", sNomArchivoLog);
                            ViewState.Add("NomCliente", sNomArchivoCliente);
                        }
                        else
                        {
                            id_Inv.Text = "<script>alert('" + Desc_Estatus + "'); </script>";
                        }
                    }
                    else
                    {
                        //this.id_Inv.Text = "<script>if(confirm('No se pudo cargar el archivo. Para ver el detalle de la falla presione ´OK´.')){ window.document.getElementById('ctl00_Principal_btn_Log').click(); }</script>";
                        this.id_Inv.Text = "<script>if(confirm('No se pudo cargar el archivo.  Para ver el detalle de la falla presione ´OK´.')){ xpos=(screen.width/2)-(300/2); ypos=(screen.height/2)-(200/2);window.open('webDialog.aspx?titulo=Log&src=getLogDatos.aspx&NomLog=" + sNomArchivoLog + "&NomCliente=" + sNomArchivoCliente + "', '_blank', 'left=' + xpos + ',top=' + ypos + ',height=200,width=300,location=no,menubar=no,resizable=yes,scrollbars=no,status=yes,toolbar=no'); }</script>";
                        GenerateLog(sNomArchivoLogSinExt, sNomArchivoCliente, false, strRazon, null);

                        ViewState.Add("NomLog", sNomArchivoLog);
                        ViewState.Add("NomCliente", sNomArchivoCliente);
                    }
                }
                catch(Exception ex)
                {
                    this.id_Inv.Text = "<script>alert('" + ex.Message + "'); </script>";
                }
                finally
                {
                    conEx.Close();
                }
             }
        }
        #endregion CargaButton_Click
        #region btn_Log_Click
        protected void btn_Log_Click(object sender, EventArgs e)
        {
            System.IO.FileInfo ofile = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["DirLogArchivos"] + ViewState["NomLog"]);
            Response.AddHeader("content-disposition", "attachment;filename=" + ViewState["NomCliente"] + ".log");
            Response.AddHeader("Content-Length", ofile.Length.ToString());
            Response.ContentType = "application/octet-stream";// "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Charset = "";
            Response.TransmitFile(ofile.FullName);
            Response.End();
        }
        #endregion btn_Log_Click

    }
}
