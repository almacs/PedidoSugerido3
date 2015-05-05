using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PedidoSugeridoLamosa.Simulacion
{
    public partial class webDialog : System.Web.UI.Page
    {

        public String source = "";
        public String titulo = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            titulo = Request.QueryString["titulo"];
            source = Request.QueryString["src"] + "?" + Request.QueryString.ToString();
        }
    }
}
