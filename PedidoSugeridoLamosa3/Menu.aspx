<%@ Page Language="C#" MasterPageFile="~/PedidoSugeridoLamosa.Master" AutoEventWireup="true" 
        CodeBehind="Menu.aspx.cs" Inherits="PedidoSugeridoLamosa.Menu" Title="Herramienta de Abasto" %>
        
<asp:Content ID="Content1" ContentPlaceHolderID="Principal" runat="server">

<script src="FuncionesJS/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="FuncionesJS/htmlTabControl.js" type="text/javascript"></script>
    
<script language="javascript" type="text/javascript">
    
    function Cerrar() {
            window.close();
        }
    
    //window.onload = art;
</script>


   <asp:ScriptManager runat="server" ID="sm">
    <Scripts>
    </Scripts>
   </asp:ScriptManager>
    &nbsp;
    <table> 
            <tr>
                  <td style= "background: url(Imagenes/Inicio.png) no-repeat;height: 450px; width: 1355px; background-position:center" >
                    
                </td>
            </tr>
    </table>
 <script language="javascript" src="FuncionesJS/wz_tooltip.js" type="text/javascript"></script>
    
</asp:Content>
