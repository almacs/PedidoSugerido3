<%@ Page Title="Carga de Inventario" Language="C#" MasterPageFile="~/PedidoSugeridoLamosa.Master" AutoEventWireup="true" CodeBehind="CargaDatos.aspx.cs" Inherits="PedidoSugeridoLamosa.CargaArchivos.CargaDatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Principal" runat="server">

    <script src="../FuncionesJS/jquery-1.4.2.js" type="text/javascript"></script>
    <script src= "../FuncionesJS/htmlTabControl.js" type="text/javascript"></script>
    
    <asp:ScriptManager ID="sm" runat="server">
        <Scripts>
            <asp:ScriptReference Path="CallWebServiceMethods.js" />
        </Scripts>
        <Services>
            <asp:ServiceReference Path="WebService.asmx" />
        </Services>
    </asp:ScriptManager>
        
    <script language="javascript" type="text/javascript">
        function getPlantilla() 
        {
            window.document.getElementById("<%=btnPlantilla.ClientID%>").click();
        }
    </script>
          
    <table align="center" width="100%"  border="0" cellpadding="0" cellspacing="0" style="background-color:white; height:450px;" >
        <tr>
            <td style="height:10px" colspan = "3" align="right">    
            </td>
        </tr>
        <tr style=" height:30px;">
            <td style=" width:10px; background-color:#eee;">
            </td>
                <td colspan= "2" style=" width:1014px;" class ="lblTitulo1" onmouseover="this.T_DESCPANTALLA=true; this.T_BGIMG='../Imagenes/help2.gif'; this.T_HEIGHT=200; this.T_WIDTH=400; this.T_ABOVE=false; this.T_CLICKCLOSE=true; this.T_STICKY=true; return escape('<b>Carga de Datos</b> <p></p>La sección Carga Datos le permite actualizar de manera rápida mediante la carga de un archivo de Excel, el inventario real existente de dichos artículos de manera periódica.');">
                    <asp:Label ID="lblTitulo" runat="server" Text="Carga de Inventario Existente" >
                    </asp:Label>
                    <br/>
                </td> 
                <td class= "ms-descriptiontextToolTip" align="right">
                    <img id="imgHelp" onmouseover="this.T_PADDING=2; this.T_INFORMATION=true; this.T_BGIMG=''; this.T_BGCOLOR='#FFFFDD'; this.T_HEIGHT=0; this.T_ABOVE=true; this.T_LEFT=true;  return escape('Muestra la descripcion de la pantalla');"
                            style="cursor: hand" onclick="return help_onclick();" alt="" src="../Imagenes/Information2_.jpg" name="imgHelp" /> 
                </td>
            </tr>
            <tr>
                <td style="height:10px" colspan = "3">
                </td>
            </tr>
            <tr>
                <td class="sub" style="height: 18px" colspan="3" onmouseover="this.T_DESCPANTALLA=true; this.T_BGIMG='../Imagenes/help2.gif'; this.T_HEIGHT=200; this.T_WIDTH=400; this.T_ABOVE=false; this.T_CLICKCLOSE=true; this.T_STICKY=true; return escape('<b>Descargar Plantilla Inventario Existente/Atributos de Artículos</b> <p></p>Le permite descargar un archivo con el formato requerido para la carga de datos.');">
                    <a href="javascript:getPlantilla();" class="Boton_01" >Descargar Plantilla de Inventario Existente</a>
                </td>
            </tr>
            <tr>
                <td colspan = "3">
                     <table align= "center"  border="0" cellpadding="0" cellspacing="0" 
                         style="width: 400px" >
                        <tr>
                            <td class="textos_Login" style="height:30px; "> Seleccionar archivo:</td>
                            <td style="height:30px; width: 159px;">
                                  <input  id="File1" type= "hidden" class="Boton_01" title="Examinar"  
                                      value="Examinar" style="width: 103px" />
                                      <asp:FileUpload ID="Uploadfile" CssClass="Boton_01" runat="server" title="Examinar" Width="250px"/> 
                            </td>
                          
                        </tr>
                        <tr>
                            <td class="textos_Login" style="height:30px; "> </td>
                            <td style="height:30px; width: 159px;" align= "right">
                                <asp:Button ID="CargaButton" CssClass="Boton_01" Text="Cargar"
                                        runat="server" OnClick="CargaButton_Click" Width="83px"/>
                            </td>
                        </tr>
                       
                        <tr>
                            <td align="center" colspan="2">
                            
                            </td>
                        </tr>
                    </table>      
                    <div style="visibility:hidden; display:none;">
                        <asp:Button ID="btn_Log" runat="server" onclick="btn_Log_Click" Text="Log" />
                        <asp:Button ID="btnPlantilla" runat="server" Text="Plantilla" onclick="btnPlantilla_Click" />
                    </div>
                </td>
            </tr>
        <asp:Literal ID="id_Inv" runat="server"></asp:Literal>
    </table>
    
    <script language="javascript" src="../FuncionesJS/wz_tooltip.js" type="text/javascript">
    </script>
    
</asp:Content>
