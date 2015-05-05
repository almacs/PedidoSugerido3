using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Business;
using System.Text;

namespace PedidoSugeridoLamosa
{
    public partial class PedidoSugeridoLamosa : System.Web.UI.MasterPage
    {

        #region fields

        private UsuarioInf usrinf = null;
        public String EmularIE7 = String.Empty;
        public String FuncionesJSScript = String.Empty;
        public String sDirectorioAplicacion = String.Empty;
        public String sConsolidado = "";

        #endregion fields

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.usrinf = (UsuarioInf)Session["usuario"];
                if (usrinf.Nombre_rol.ToString() == "Cliente") 
                {
                    Cliente.Value = "TRUE";
                }

                if (usrinf.Nombre_rol.ToString() == "Administrador")
                {
                    Cliente.Value = "FALSE";
                }

                if (usrinf.Nombre_rol.ToString() == "Consulta")
                {
                    Cliente.Value = "TRUE-C";
                }

                if (usrinf.Nombre_rol.ToString() == "Asesor")
                {
                    Cliente.Value = "TRUE-A";
                }
                if (usrinf.id_sucursal.ToString() == "CON")
                {
                    sConsolidado = "CON";
                }

                sDirectorioAplicacion = System.Configuration.ConfigurationManager.AppSettings["DirectorioAplicacion"];

                if ((this.usrinf.id_compania.ToString() == "792") || (this.usrinf.id_compania.ToString() == "092") || (this.usrinf.id_compania.ToString() == "R302") || (this.usrinf.id_compania.ToString() == "R303"))
                {
                    // LAMOSA
                    Menu.Value = "<link href=\"" + sDirectorioAplicacion + "/Estiloscss/menu_Lamosa.css\" rel=\"Stylesheet\" type=\"text/css\" />";
                    MenuProl.Value = "<link href=\"" + sDirectorioAplicacion + "/Estiloscss/pro-line-down-fly_lamosa/menu3.css\" media=\"screen\" rel=\"stylesheet\" type=\"text/css\" />";
                }
                else if ((this.usrinf.id_compania.ToString() == "791") || (this.usrinf.id_compania.ToString() == "091") || (this.usrinf.id_compania.ToString() == "R301"))
                {
                    // PORCELANITE
                    Menu.Value = "<link href=\"" + sDirectorioAplicacion + "/Estiloscss/menu.css\" rel=\"Stylesheet\" type=\"text/css\" />";
                    MenuProl.Value = "<link href=\"" + sDirectorioAplicacion + "/Estiloscss/pro-line-down-fly/menu3.css\" media=\"screen\" rel=\"stylesheet\" type=\"text/css\" />";
                }
                else if (this.usrinf.id_compania.ToString() == "0")
                {
                    // ADMINISTRADOR
                    Menu.Value = "<link href=\"" + sDirectorioAplicacion + "/Estiloscss/menu_Lamosa.css\" rel=\"Stylesheet\" type=\"text/css\" />";
                    MenuProl.Value = "<link href=\"" + sDirectorioAplicacion + "/Estiloscss/pro-line-down-fly_lamosa/menu3.css\" media=\"screen\" rel=\"stylesheet\" type=\"text/css\" />";
                }

                FuncionesJSScript = "<script src=\"" + sDirectorioAplicacion + "/FuncionesJS/jquery-1.4.2.js\" type=\"text/javascript\"></script><script src=\"" + sDirectorioAplicacion + "/FuncionesJS/menu.js\" type=\"text/javascript\"></script>";
                
                this.LblClavecli.Text = usrinf.id_cliente.ToString();
                this.LblNomcliente.Text = usrinf.Nombre_cliente.ToString();
                this.LblEntSuc.Text = usrinf.id_sucursal.ToString();
                this.LblSuc.Text = usrinf.Nombre_sucursal.ToString();

                lnkManualUsr.NavigateUrl = this.sDirectorioAplicacion + System.Configuration.ConfigurationManager.AppSettings["DirManual"];

                Response.Write(this.Request.QueryString.ToString());
                if (Request.Url.LocalPath.Contains("/Simulacion/VentasPedidos.aspx"))
                {
                    this.EmularIE7 = "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\" >";
                }
                else
                {
                    this.EmularIE7 = "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=8\" >";
                }

                if (!IsPostBack)
                {
                    setAmbienteAsesor();
                }
                
  
            }
            catch
            {
               // Response.Redirect("../Login.aspx", false);
            }
        }
        #endregion Page_Load

        protected void btnClienteEntrega_Click(object sender, EventArgs e)
        {
            try
            {
                this.usrinf = (UsuarioInf)Session["usuario"];

                this.usrinf.id_compania = txtClienteSel.Text.Split('-')[0];
                this.usrinf.id_cliente = txtClienteSel.Text.Split('-')[1];
                this.usrinf.id_sucursal = txtEntregaSel.Text;

                sDirectorioAplicacion = System.Configuration.ConfigurationManager.AppSettings["DirectorioAplicacion"];

                if ((this.usrinf.id_compania.ToString() == "792") || (this.usrinf.id_compania.ToString() == "092") || (this.usrinf.id_compania.ToString() == "R302") || (this.usrinf.id_compania.ToString() == "R303"))
                {
                    // LAMOSA
                    Menu.Value = "<link href=\"" + sDirectorioAplicacion + "/Estiloscss/menu_Lamosa.css\" rel=\"Stylesheet\" type=\"text/css\" />";
                    MenuProl.Value = "<link href=\"" + sDirectorioAplicacion + "/Estiloscss/pro-line-down-fly_lamosa/menu3.css\" media=\"screen\" rel=\"stylesheet\" type=\"text/css\" />";
                }
                else if ((this.usrinf.id_compania.ToString() == "791") || (this.usrinf.id_compania.ToString() == "091") || (this.usrinf.id_compania.ToString() == "R301"))
                {
                    // PORCELANITE
                    Menu.Value = "<link href=\"" + sDirectorioAplicacion + "/Estiloscss/menu.css\" rel=\"Stylesheet\" type=\"text/css\" />";
                    MenuProl.Value = "<link href=\"" + sDirectorioAplicacion + "/Estiloscss/pro-line-down-fly/menu3.css\" media=\"screen\" rel=\"stylesheet\" type=\"text/css\" />";
                }
                else if (this.usrinf.id_compania.ToString() == "0")
                {
                    // ADMINISTRADOR
                    Menu.Value = "<link href=\"" + sDirectorioAplicacion + "/Estiloscss/menu_Lamosa.css\" rel=\"Stylesheet\" type=\"text/css\" />";
                    MenuProl.Value = "<link href=\"" + sDirectorioAplicacion + "/Estiloscss/pro-line-down-fly_lamosa/menu3.css\" media=\"screen\" rel=\"stylesheet\" type=\"text/css\" />";
                }

                FuncionesJSScript = "<script src=\"" + sDirectorioAplicacion + "/FuncionesJS/jquery-1.4.2.js\" type=\"text/javascript\"></script><script src=\"" + sDirectorioAplicacion + "/FuncionesJS/menu.js\" type=\"text/javascript\"></script>";

                this.LblClavecli.Text = usrinf.id_cliente.ToString();
                this.LblEntSuc.Text = usrinf.id_sucursal.ToString();

                setAmbienteAsesor();


            }
            catch
            {
                Response.Redirect(sDirectorioAplicacion + "/Login.aspx", false);
            }
            

            Response.Redirect(Request.Url.AbsolutePath, true);
        }

        private void setAmbienteAsesor()
        {
            if (usrinf.Nombre_rol.ToString() == "Asesor")
            {
                this.LblNomcliente.Style.Add("display", "none");
                this.LblSuc.Style.Add("display", "none");

                System.Collections.Hashtable ohashCliente = new System.Collections.Hashtable();

                DataTable UsuData = usrinf.UsuData;

                StringBuilder strClientesAsesor = new StringBuilder();
                strClientesAsesor.Append("<script language=\"javascript\" type=\"text/javascript\">");

                for (int int_reg = 0; int_reg < UsuData.Rows.Count; int_reg++)
                {
                    String id_cliente = UsuData.Rows[int_reg]["id_cia"] + "-" + UsuData.Rows[int_reg]["id_cliente"];

                    if (!ohashCliente.Contains(id_cliente))
                    {
                        strClientesAsesor.Append("addCliente('" + id_cliente + "','" + UsuData.Rows[int_reg]["Cliente"] + "');\r\n");
                        ohashCliente.Add(id_cliente, true);
                    }

                    strClientesAsesor.Append("addEntrega('" + id_cliente + "','" + UsuData.Rows[int_reg]["id_entrega"] + "','" + UsuData.Rows[int_reg]["Sucursal"] + "');\r\n");
                }

                strClientesAsesor.Append("loadCombosEntrega('" + usrinf.id_compania + "-" + usrinf.id_cliente + "','" + usrinf.id_sucursal + "');\r\n</script>");
                litClientesAsesor.Text = strClientesAsesor.ToString();
            }
            else
            {
                this.cmbCliente.Style.Add("display", "none");
                this.cmbEntrega.Style.Add("display", "none");
                this.lnkClienteEntrega.Style.Add("display", "none");
            }
        }

    }
}
