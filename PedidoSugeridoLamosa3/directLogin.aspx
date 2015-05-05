<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="directLogin.aspx.cs" Inherits="PedidoSugeridoLamosa.directLogin" %>

<%@ Register assembly="Infragistics35.Web.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"> 
    <title>Herramienta de Abasto - Login</title>
    <link href="~/Estiloscss/pro-line-down-fly/menu3.css" media="screen" rel="stylesheet"
        type="text/css" />
    <link href="~/Estiloscss/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="Estiloscss/menu.css" rel="Stylesheet" type="text/css" />

    

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
                             <!--   Contraseña:-->
                            </td>
                            <td style="height: 30px;">
                                <asp:TextBox ID="txtContrasena" CssClass="TxtLogin" runat="server" 
                                     Height="16px" Width="250px"></asp:TextBox>
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
                            <td align="center" colspan="2">
                                <asp:Button ID="LoginButton" CssClass="Boton_01" Text="Iniciar sesión"
                                    runat="server" OnClick="LoginButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
     
 
        <asp:Literal ID="id_literal" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
