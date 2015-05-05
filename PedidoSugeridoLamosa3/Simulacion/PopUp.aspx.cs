using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PedidoSugeridoLamosa.Simulacion
{
    public partial class PopUp : System.Web.UI.Page
    {
        public PlaceHolder Body = new PlaceHolder();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Body.Controls.Add(LoadControl(Request["urlStr"]));
        }
    }
}
