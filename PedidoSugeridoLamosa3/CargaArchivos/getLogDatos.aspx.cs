using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace PedidoSugeridoLamosa.CargaArchivos
{
    public partial class getLogDatos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.IO.FileInfo ofile = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["DirLogArchivos"] + Request.QueryString["NomLog"]);
            Response.AddHeader("content-disposition", "attachment;filename=" + Request.QueryString["NomCliente"] + ".log");
            Response.AddHeader("Content-Length", ofile.Length.ToString());
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";// "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Charset = "";
            Response.TransmitFile(ofile.FullName);
            Response.End();
        }
    }
}
