using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using System.Data;

namespace PedidoSugeridoLamosa.Servicio
{
    public partial class voucherServicio : System.Web.UI.Page
    {

        UsuarioInf usrinf;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("../Login.aspx", true);
                return;
            }
            usrinf = (UsuarioInf)Session["usuario"];

            id_messages.Text = "";

            if (!Page.IsPostBack)//primera vez que entra a la página
            {
                lblFecha.Text = DateTime.Now.ToString("MMM - yyyy").ToUpper();
                txtFecha.Text = DateTime.Now.ToString("yyyyMM");
                getConsultaVoucher();
            }
        }


        private void getConsultaVoucher()
        {
            try
            {
                BServicio oservicio = new BServicio(new ConnectionLP().getConnectionVoucher());
                DataSet ods = oservicio.getConsultaVoucher(usrinf.id_compania, usrinf.id_cliente, usrinf.id_sucursal, txtFecha.Text);
                if ((ods.Tables.Count == 0) || (ods.Tables[0].Rows.Count == 0))
                {
                    id_messages.Text = "<script>alert('No hay datos disponibles');</script>";    
                    return;
                }
                

                lblTotalGeneral.Text = String.Format("{0:#,###.00}",ods.Tables[0].Rows[0]["TotalGeneral"]);
                lblEnviado.Text = String.Format("{0:#,###.00}",ods.Tables[0].Rows[0]["TotalTiempo"]);
                if ((double)ods.Tables[0].Rows[0]["TotalGeneral"] != 0 && (double)ods.Tables[0].Rows[0]["TotalTiempo"] != 0)
                    lblEnviadoPorc.Text = String.Format("{0:#.00}", (100.00 / (double)ods.Tables[0].Rows[0]["TotalGeneral"] * (double)ods.Tables[0].Rows[0]["TotalTiempo"])) + "%";
                else
                    lblEnviadoPorc.Text = "0.00%";
                lblPromedio.Text = String.Format("{0:#,###.00}", ods.Tables[0].Rows[0]["TotalDias"]);

                

                
            }
            catch (Exception ex)
            {
                id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }


        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                String anio = txtFecha.Text.Substring(0, 4);
                String mes = txtFecha.Text.Substring(4, 2);

                if (mes.Equals("01"))
                {
                    anio = String.Format("{0:0000}", Convert.ToInt32(anio) - 1);
                    mes = "12";
                }
                else
                {
                    mes = String.Format("{0:00}", Convert.ToInt32(mes) - 1);
                }
                txtFecha.Text = anio + mes;
                lblFecha.Text = new DateTime(Convert.ToInt32(anio), Convert.ToInt32(mes), 1).ToString("MMM - yyyy").ToUpper();
                getConsultaVoucher();
            }
            catch (Exception ex)
            {
                id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                String anio = txtFecha.Text.Substring(0, 4);
                String mes = txtFecha.Text.Substring(4, 2);

                if (mes.Equals("12"))
                {
                    anio = String.Format("{0:0000}", Convert.ToInt32(anio) + 1);
                    mes = "01";
                }
                else
                {
                    mes = String.Format("{0:00}", Convert.ToInt32(mes) + 1);
                }
                txtFecha.Text = anio + mes;
                lblFecha.Text = new DateTime(Convert.ToInt32(anio), Convert.ToInt32(mes), 1).ToString("MMM - yyyy").ToUpper();
                getConsultaVoucher();
            }
            catch (Exception ex)
            {
                id_messages.Text = "<script>alert('" + ex.Message.Replace("'", "´") + "');</script>";
            }
        }
    }
}
