<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sucursales.aspx.cs" Inherits="PedidoSugeridoLamosa.Sucursales" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Lamosa - Sucursal</title>
    <link href="~/Estiloscss/pro-line-down-fly/menu3.css" media="screen" rel="stylesheet" type="text/css" />
    <link href="~/Estiloscss/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="Estiloscss/menu.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="mm" runat="server">
    </asp:ScriptManager>
    <table align="center" width="1024px"  border="0" cellpadding="0" cellspacing="0" style="background-color:white;">
            <tr>
                <td  rowspan = "2" class= "Header1">&nbsp;     
                </td>
                <td  class= "Header2">
                    <label class="Header2Text" id="lblfecha" runat="server"></label> | <label class="Header2Text" id="lblHora" runat="server"></label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class= "Header3">&nbsp;     
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 20px">
                    <div id= "pro_linedrop"></div>
                </td>
            </tr>
            <tr>
                <td style="height:10px" colspan= "2"></td>
            </tr>
        </table>
        <table align="center" width="1024px"  border="0" cellpadding="0" cellspacing="0" style="background-color:white; height:400px;" >
            <tr style=" height:30px;">
                <td style=" width:10px; background-color:#eee;"></td>
                <td colspan= "2" style=" width:1014px;"  class ="lblTitulo1">
                   
                </td> 
            </tr>
            <tr>
                <td colspan = "3">
                     <table align="center" width="300px"  border="0" cellpadding="0" cellspacing="0" >
                       <tr>
                            <td class="textos_Login" style="height:30px;"> Sucursal:</td>
                            <td style="height:30px;">
                                <asp:DropDownList ID="cmbSucursal" CssClass="CmbLogin" runat="server" Height="22px" Width="122px">
                               <asp:ListItem Value= "1" Text="Monterrey"> </asp:ListItem>
                                  <asp:ListItem Value= "2" Text="Tecnologico"> </asp:ListItem>
                                  <asp:ListItem Value= "3" Text="San Nicolas"> </asp:ListItem>
                                  <asp:ListItem Value= "4" Text="Enrroque"> </asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="okbutton"  CssClass="Boton_01" CommandName="ok" Text="OK" runat="server" OnClick="OKButton_Click" />
                            </td>
                        </tr>
                    </table>      
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
