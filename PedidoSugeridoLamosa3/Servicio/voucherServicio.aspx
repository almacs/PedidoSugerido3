<%@ Page Title="Voucher de Servicio" Language="C#" MasterPageFile="~/PedidoSugeridoLamosa.Master"
AutoEventWireup="true" CodeBehind="voucherServicio.aspx.cs" Inherits="PedidoSugeridoLamosa.Servicio.voucherServicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Principal" runat="server">
    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
    <div id="divMain" style="width: 100%;">
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
            <tr style="height: 30px;">
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="lblTitulo1" onmouseover="this.T_DESCPANTALLA=true; this.T_BGIMG='../Imagenes/help2.gif'; this.T_HEIGHT=200; this.T_WIDTH=400; this.T_ABOVE=false; this.T_CLICKCLOSE=true; this.T_STICKY=true; return escape('<b>Voucher de Servicio</b><p></p>Pantalla de Voucher de Servicio');">
                                <asp:Label ID="lblTitulo" runat="server" Text="Voucher de Servicio"></asp:Label>&nbsp;&nbsp;<img alt="" src="../Imagenes/edtCalendarPrevMonth.gif" style="cursor:pointer" onclick="return imgPrev_onclick();" /> <asp:Label ID="lblFecha" runat="server"></asp:Label> <img alt="" src="../Imagenes/edtCalendarNextMonth.gif" style="cursor:pointer" onclick="return imgNext_onclick();"  /> 
                            </td>
                            <td align="left" style="width: 270px" class="lblTitulo1">
                            </td>
                            <td class= "ms-descriptiontextToolTip" align="right">
                                <img id="imgHelp" onmouseover="this.T_PADDING=2; this.T_INFORMATION=true; this.T_BGIMG=''; this.T_BGCOLOR='#FFFFDD'; this.T_HEIGHT=0; this.T_ABOVE=true; this.T_LEFT=true;  return escape('Muestra la descripcion de la pantalla');"
                                                        style="cursor: hand" onclick="return help_onclick();" alt="" src="../Imagenes/Information2_.jpg" name="imgHelp" /> 
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align:center">
                    <br />
                    <table border="0px" cellpadding="0px" cellspacing="0px" class="textos_Login" style="width:500px; margin:auto;">
                        <tr style="height:50px">
                            <td style="width:200px;">
                                &nbsp;
                            </td>
                            
                            <td style="width:250px;">
                                &nbsp;&nbsp;&nbsp;Gran Total
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right; height: 50px;">
                                Total General:
                            </td>
                            
                            <td style="background-image:url('../../Imagenes/bgvoucher.gif');background-repeat:no-repeat; height: 50px;">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblTotalGeneral" runat="server" Text="3,123,851" Width="80px" ></asp:Label> MT2
                            </td>
                        </tr>
                        <tr style="height:50px">
                            <td style="text-align:right; height: 50px;">
                                Enviado a Tiempo:
                            </td>
                            <td style="background-image:url('../../Imagenes/bgvoucher.gif');background-repeat:no-repeat;">
                                &nbsp;&nbsp; <asp:Label ID="lblEnviado" runat="server" Text="2,585,152" Width="80px" ></asp:Label> MT2&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lblEnviadoPorc" runat="server" Text="~83%" ></asp:Label>
                            </td>
                        </tr>
                        <tr style="height:50px">
                            <td style="text-align:right; height: 50px;">
                                Dias Promedio Todo Ciclo:
                            </td>
                            <td style="background-image:url('../../Imagenes/bgvoucher.gif');background-repeat:no-repeat;">
                                &nbsp;&nbsp;<asp:Label ID="lblPromedio" runat="server" Text="8.7" ></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:Literal ID="id_messages" runat="server"></asp:Literal>
    <div style="visibility:hidden; display:none;">
        <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
        <asp:Button ID="btnPrev" runat="server" Text="Prev" onclick="btnPrev_Click"/>
        <asp:Button ID="btnNext" runat="server" Text="Next" onclick="btnNext_Click"/>
        
    </div>
    <script language="javascript" src="../FuncionesJS/wz_tooltip.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
    
        function imgNext_onclick()
        {
            window.document.getElementById("<%=btnNext.ClientID%>").click();
        }

        function imgPrev_onclick() 
        {
            window.document.getElementById("<%=btnPrev.ClientID%>").click();
        }    
    </script>
    
</asp:Content>