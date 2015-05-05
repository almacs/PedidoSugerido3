using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq; 
using Business;

namespace PedidoSugeridoLamosa
{
    public partial class directLogin: System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["usuario"] != null)
            //{
            //    Response.Redirect(Request.ApplicationPath + "Menu.aspx", true);
            //    return;
            //}

            try
            {
                String pContrasena = Request.QueryString["p_contrasena"];
                String pUser = Request.QueryString["p_user"];

                Blogin de = new Blogin(new ConnectionLP().getConnection());
                DataSet ds = de.ObtieneClientes(pUser);


                cmbSucursal.DataSource = ds;
                cmbSucursal.DataTextField = "Sucursal";
                cmbSucursal.DataValueField = "Id_entrega";
                cmbSucursal.DataBind();

                txtUsuario.Text = pUser;
                txtUsuario.Enabled = false;
                txtContrasena.Text = pContrasena;
                txtContrasena.Visible = false;
            }
            catch (Exception exp) {
                id_literal.Text = "<script>alert('Problemas con la conexión intente más tarde.'); document.forms[0].reset();</script>";

            }

            //lblfecha.InnerText = String.Format("{0:dd} {0:MMMM} {0:yyyy}", DateTime.Now);
            //lblHora.InnerText = DateTime.Now.ToShortTimeString();
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if ((txtUsuario.Text == "" && txtContrasena.Text == "") || txtContrasena.Text == "" || Request.Params["cmbSucursal"] == null)
            {
                id_literal.Text = "<script>alert('Introduzca un usuario y una contraseña validos.'); document.forms[0].reset();</script>";
            }
            else
            {
                // Response.Redirect("Menu.aspx");
                Blogin de = new Blogin(new ConnectionLP().getConnection());
                DataSet ds = de.Acceso(txtUsuario.Text, txtContrasena.Text, Request.Params["cmbSucursal"]);



                if (ds.Tables[0].Rows.Count > 0)
                {
                    //Si el usuario esta inactivo
                    if (ds.Tables[0].Rows[0]["Estatus1"].ToString() == "2")
                    {
                        id_literal.Text = "<script>alert('Usuario Inactivo.'); document.forms[0].reset;</script>";
                        txtUsuario.Text = "";
                    }
                    else
                    {
                        DataTable dtUsuario = ds.Tables[0];
                        DataRow drUsuario = dtUsuario.Rows[0];
                        UsuarioInf usr = new UsuarioInf();
                        usr.Id_usuario = drUsuario["Clave usuario"].ToString().Trim();
                        usr.Nombre_usuario = drUsuario["Nombre"].ToString().Trim();
                        usr.Apellido_paterno = drUsuario["Ap_paterno"].ToString().Trim();
                        usr.Apellido_materno = drUsuario["Ap_materno"].ToString().Trim();
                        usr.Password = drUsuario["Contraseña"].ToString().Trim();
                        usr.Mail = drUsuario["Correo"].ToString().Trim();
                        usr.id_cliente = drUsuario["id_cliente"].ToString().Trim();
                        usr.id_compania = drUsuario["id_cia"].ToString().Trim();
                        usr.id_tipo_usuario = drUsuario["id_tipo"].ToString().Trim();
                        usr.id_estatus = Convert.ToInt32(drUsuario["Estatus1"]);
                        usr.Nombre_cliente = drUsuario["Cliente"].ToString().Trim();
                        usr.id_sucursal = drUsuario["id_entrega"].ToString().Trim();
                        usr.Nombre_sucursal = drUsuario["Sucursal"].ToString().Trim();
                        usr.Nombre_rol = drUsuario["Tipo usuario"].ToString().Trim();
                        usr.UsuData = dtUsuario;

                        Session["usuario"] = usr;

                        Response.Redirect("Menu.aspx");
                        //if ds.Tables[0].Rows[0]["Tipo usuario"].ToString() == Cliente
                        Session["cliente"] = "Cliente"; //ds.Tables[0].Rows[0]["Tipo usuario"].ToString()
                    }
                }
                else
                    this.id_literal.Text = "<script>alert('Contraseña no valida. Vuelva a intentarlo'); document.forms[0].reset;</script>";
                txtUsuario.Text = "";
                // Response.Write("<script>alert('Contraseña no valida. Vuelva a intentarlo');</script");
            }

        }

        protected void btnAct_Click(object sender, EventArgs e)
        {
            if (Id_usr.Text == "" || passw.Text == "" || newpass.Text == "" || copynewpass.Text == "")
            {
                id_literal.Text = "<script>alert('Falla al solicitar su cambio de contraseña. Verifique la información');</script";
                // Page.ClientScript.RegisterStartupScript(typeof(Page), "hide()", "hide()", true);
            }
            else
            {
                if (newpass.Text == copynewpass.Text)
                {
                    Blogin dx = new Blogin(new ConnectionLP().getConnection());
                    DataSet ds = dx.CambiaContraseña(Id_usr.Text, passw.Text, newpass.Text);

                    string msg = ds.Tables[0].Rows[0][0].ToString();
                    if (msg == "Usuario y/o contraseña no validos, vuelva a intentarlo.")
                    {
                        id_literal.Text = "<script>alert('" + msg + "');</script>";
                        return;
                    }
                    id_literal.Text = "<script>alert('" + msg + "');</script>";

                    //Para enviar mail de seguridad en el cambio de la contraseña....
                    MailMessage text = new MailMessage();
                    text.From = new MailAddress("WebMaster@Porcelanite-Lamosa.com");
                    DataSet df = dx.ObtieneClientes(Id_usr.Text);
                    if (df.Tables[0].Rows[0][0].ToString() == "-1")
                    {
                        id_literal.Text = "<script>alert('El usuario no existe. Vuelva a intentarlo');</script";
                        return;
                    }
                    else
                    {
                        text.To.Add(df.Tables[0].Rows[0]["Correo"].ToString());
                        text.Priority = MailPriority.High;
                        text.IsBodyHtml = true;
                        text.Subject = "Pedido Sugerido: Confirmación de Cambio de Contraseña";
                        text.Body = "<b>Asunto:</b> <p></p>Su contraseña se ha cambiado con éxito. Favor de confimar sus datos.<p></p>Usuario: " + df.Tables[0].Rows[0]["Clave usuario"].ToString() + "<br></br>Contraseña: " + df.Tables[0].Rows[0]["Contraseña"].ToString() + "<br></br>Si usted no ha realizado de manera intencional un cambio de contraseña, reportelo con el administrador del sistema. <br></br> Favor de no contestar este correo. ";

                        text.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                        text.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "192.168.12.5";
                        smtp.Send(text);
                        //try
                        //   {
                        //       smtp.Send(text);
                        //       id_literal.Text = "<script>alert('E-mail enviado');</script";
                        //       //Response.Write("E-mail enviado...!!");
                        //   }
                        //catch (Exception ex)
                        //   {
                        //       throw ex;

                        //   }
                        text.Dispose();

                        // Page.ClientScript.RegisterStartupScript(typeof(Page), "hide()", "hide()", true);
                    }

                }
                else
                    id_literal.Text = "<script>alert('La nueva contraseña que ha escrito no coincide. Vuelva a intentarlo.');</script";
            }


            //Page.ClientScript.RegisterStartupScript(Page.GetTy pe(), "nombreFuncion", "//aqui pones el codigo de la funcion o el nombre", True) 
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            //Crear el objeto para el e-mail.
            MailMessage text = new MailMessage();

            //definir el remitente
            text.From = new MailAddress("WebMaster@Porcelanite-Lamosa.com");

            Blogin du = new Blogin(new ConnectionLP().getConnection());
            DataSet ds = du.ObtieneClientes(Id_usuario.Text);
            //si el usuario no existe
            if (ds.Tables[0].Rows[0][0].ToString() == "-1")
            {
                id_literal.Text = "<script>alert('El usuario no existe. Vuelva a intentarlo');</script";
                return;
            }
            else
            {
                //agregar destinatarios
                text.To.Add(ds.Tables[0].Rows[0]["Correo"].ToString());
                //text.To.Add("ramgatica@gmail.com");


                //Enviar cópia para.
                //  text.To.Add("amorales@neitek.com");

                //Enviar con copia oculta
                //  text.Bcc.Add("jesus_chap@hotmail.com");

                //define prioridad de email
                text.Priority = MailPriority.High;

                //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
                text.IsBodyHtml = true;

                //define el titulo del email
                text.Subject = " Pedido Sugerido: Recordatorio de contraseña";

                //define el cuerpo del email
                text.Body = "<b>Asunto:</b> <p></p>Usted ha solicitado un recordatorio de contraseña en el portal de Pedido Sugerido, a continuación le proporcionamos sus datos de acceso.<p></p>Usuario: " + ds.Tables[0].Rows[0]["Clave usuario"].ToString() + "<br></br>Contraseña: " + ds.Tables[0].Rows[0]["Contraseña"].ToString() + " <br></br> Favor de no contestar este correo.";

                //Evitar problemas de caractares "extranhos"
                text.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                text.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

                //creamos el objeto del smtp
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "192.168.12.5";

                try
                {
                    smtp.Send(text);
                    id_literal.Text = "<script>alert('Correo enviado');</script";
                    //Response.Write("E-mail enviado...!!");
                }
                catch (Exception ex)
                {
                    Response.Write("Ocurrio un problema e-mail no enviado. Error = " + ex.Message);

                }
                //limpio memoria del objeto de mail
                text.Dispose();
            }

        }

    }
}
