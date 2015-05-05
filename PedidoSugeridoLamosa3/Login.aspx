<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PedidoSugeridoLamosa.Login" %>

<%@ Register assembly="Infragistics35.Web.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"> 
    <title>Herramienta de Abasto - Login</title>
    <link href="~/Estiloscss/pro-line-down-fly/menu3.css" media="screen" rel="stylesheet"
        type="text/css" />
    <link href="~/Estiloscss/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="Estiloscss/menu.css" rel="Stylesheet" type="text/css" />

    <script src="FuncionesJS/jquery-1.4.2.js" type="text/javascript"></script>
    <script type="text/javascript">

        var tipo_usuario = 0;
   
        $(function() {
            $("#<%=txtUsuario.ClientID %>").blur(function() {
                    $("#txtContrasena.ClienteID").focus();
                    obtieneUSR($(this).val());
            });
        })

        function hide() {

            $find('<%=WebDialogWindow2.ClientID%>').set_windowState($IG.DialogWindowState.Hidden);
            
            if (document.getElementById("WebDialogWindow2_tmpl_Id_usr").value != '' && document.getElementById("WebDialogWindow2_tmpl_passw").value != '' && document.getElementById("WebDialogWindow2_tmpl_newpass").value != '') {
                var pass = document.getElementById("WebDialogWindow2_tmpl_newpass").value;
                var er_cp = /^(?=(?:.*?\d){1})(?=(?:.*?[A-Za-z]){2})\w{8,}$/     //3 numeros o cadena vacia /^(?=.*\d{2})(?=.*[a-zA-Z]{2}).{8,}$/
                if (!er_cp.test(pass)) {
                    alert('Se requieren como minimo 8 caracteres , dos letras y al menos un número para la contraseña.')
                    return false
                }
            }
            
        }

        function ocultar() {
            $find('<%=WebDialogWindow1.ClientID%>').set_windowState($IG.DialogWindowState.Hidden);
        }

        function ActionButton(event) {
            if (event.which || event.keyCode) {
                if ((event.which == 13) || (event.keyCode == 13)) {
                    document.getElementById('<% = btnEnviar.ClientID %>').click();
                    return false;
                }
            }
            else {
                return true
            };
        }

        function ActionButtonCC(event) {
            if (event.which || event.keyCode) {
                if ((event.which == 13) || (event.keyCode == 13)) {
                    document.getElementById('<% = Button1.ClientID %>').click();
                    return false;
                }
            }
            else {
                return true
            };
        }
    </script>

    <style type="text/css">
        .style8
        {
            font-family: Arial;
            font-size: 14px;
            font-style: normal;
            font-weight: bold;
            color: #000000;
            text-align: left;
            height: 30px;
            width: 143px;
        }
    </style>

</head>
<body class="fondoLogin">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="sm" runat="server">
            <Scripts>
                <asp:ScriptReference Path= "Usuario/CallWebServiceMethods.js" />
            </Scripts>
            <Services>
                <asp:ServiceReference Path="Usuario/WebService.asmx" />
            </Services>
        </asp:ScriptManager>
        <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0" style="background-color: white;">
            <tr>
                <td rowspan="2" class="Header1_login">
                    &nbsp;
                </td>
                <td class="Header2">
                    <label class="Header2Text" id="lblfecha" runat="server">
                    </label>
                    |
                    <label class="Header2Text" id="lblHora" runat="server">
                    </label>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="Header3_login">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 20px">
                    <div id="pro_linedrop_log">
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="2">
                </td>
            </tr>
        </table>
        <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0" style="background-color: white;
            height: 520px;">
            <tr style="height: 30px;">
                <td style="width: 10px; background-color: #eee;">
                </td>
                <td colspan="2" style="width: 1014px;" class="lblTitulo1">
                    <asp:Label ID="lblTitulo" runat="server" Text="Inicio de sesion"></asp:Label><br />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table align="center" border="0" cellpadding="0" cellspacing="0" 
                        style="width: 471px">
                        <tr>
                            <td class="style8">
                                Clave usuario:
                            </td>
                            <td style="height: 30px;">
                                <asp:TextBox ID="txtUsuario" CssClass="TxtLogin" runat="server" Height="17px" 
                                    Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8">
                                Contraseña:
                            </td>
                            <td style="height: 30px;">
                                <asp:TextBox ID="txtContrasena" CssClass="TxtLogin" runat="server" 
                                    TextMode="Password" Height="16px" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trSucursal">
                            <td class="style8">
                                Sucursal:
                            </td>
                            <td style="height: 30px;">
                                <asp:DropDownList ID="cmbSucursal" CssClass="CmbLogin" runat="server" Height="18px"
                                    Width="250px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="height: 30px; font-size: 10px; font-family: Arial;">
                                <a href="#" onclick=" $find('<%=WebDialogWindow1.ClientID%>').set_windowState($IG.DialogWindowState.Normal);">
                                    ¿Ha olvidado la contraseña?</a>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="height: 30px; font-size: 10px; font-family: Arial;">
                                <a href="#" onclick=" $find('<%=WebDialogWindow2.ClientID%>').set_windowState($IG.DialogWindowState.Normal);">
                                    Cambiar contraseña</a>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="LoginButton" CssClass="Boton_01" Text="Iniciar sesión"
                                    runat="server" OnClick="LoginButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <ig:WebDialogWindow ID="WebDialogWindow1" runat="server" Height="190px" Width="580px"
            Modal="true" InitialLocation="Centered" Font-Size="10px" 
            WindowState="Hidden">
            <ContentPane BackColor="#FAFAFA">
                <Template>
                    <div style="padding: 5px;">
                        <table>
                            <tr>
                                <td class="textos_01">
                                    ¿Olvidaste tu contraseña?
                                </td>
                            </tr>
                            <tr>
                                <td class="textos_01">
                                    Te agradecemos que escribas tu clave de usuario, y nosotros te enviaremos tu clave
                                    secreta
                                    <br />
                                    a la dirección electrónica que registraste.
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="textos_01">
                                    Clave de usuario:
                                    <asp:TextBox ID="Id_usuario" runat="server" onkeypress="return ActionButton(event);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button CssClass="Boton_01" ID="btnEnviar" runat="server" OnClick="btnEnviar_Click" OnClientClick="ocultar()"
                                        Text="Enviar" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </Template>
            </ContentPane>
        </ig:WebDialogWindow>
        <ig:WebDialogWindow ID="WebDialogWindow2" runat="server" Height="245px" Width="550px"
            Modal="true" InitialLocation="Centered" Font-Size="10px" 
            WindowState="Hidden">
            <ContentPane BackColor="#FAFAFA">
                <Template>
                    <div style="padding: 5px;">
                        <table>
                            <tr>
                                <td class="lblTitulo1">
                                   Cambiar contraseña
                                </td>
                            </tr>
                            <tr>
                                <td class="textos_01">
                                   Clave de usuario:
                                </td>
                                <td>
                                    <asp:TextBox ID="Id_usr" runat="server" onkeypress="return ActionButtonCC(event);"></asp:TextBox>
                                </td>
                                <td>
                                    * Campo Requerido
                                </td>
                            </tr>
                            <tr>
                                <td class="textos_01">
                                     Contraseña actual:
                                </td>
                                <td>
                                    <asp:TextBox MaxLength="16" TextMode="Password" ID="passw" runat="server" onkeypress="return ActionButtonCC(event);"></asp:TextBox>
                                </td>
                                <td>
                                    * Campo Requerido
                                </td>
                            </tr>
                            <tr>
                                <td class="textos_01">
                                   Contraseña nueva:
                                </td>
                                <td>
                                    <asp:TextBox MaxLength="16" TextMode="Password" ID="newpass" runat="server" onkeypress="return ActionButtonCC(event);"></asp:TextBox>
                                </td>
                                <td>
                                    * Campo Requerido
                                </td>
                            </tr>
                             <tr>
                                <td class="textos_01">
                                   Confirmar contraseña nueva:
                                </td>
                                <td>
                                    <asp:TextBox MaxLength="16" TextMode="Password" ID="copynewpass" runat="server" onkeypress="return ActionButtonCC(event);"></asp:TextBox>
                                </td>
                                <td>
                                    * Campo Requerido
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                       <asp:Button CssClass="Boton_01" ID="Button1" runat="server" 
                                           OnClick="btnAct_Click" OnClientClick="return hide()"
                                        Text="Cambiar contraseña" Width="153px"  />
                                </td>
                                <td align="right">
                                        <asp:Button CssClass="Boton_01" ID="Button2" runat="server" OnClientClick="hide()"
                                        Text="Cancelar" />
                                </td>
                                <td>
                                    
                                </td>
                            </tr>
                        </table>
                    </div>
                </Template>
            </ContentPane>
        </ig:WebDialogWindow>
        <asp:Literal ID="id_literal" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
