<%@ Page Title="Reporte de Artículos sin Pedido" Language="C#" MasterPageFile="~/PedidoSugeridoLamosa.Master"
AutoEventWireup="true" CodeBehind="ArtSinPedido.aspx.cs" Inherits="PedidoSugeridoLamosa.Reporte.ArtSinPedido" %>

<%@ Register assembly="Infragistics35.Web.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics35.Web.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebGrid" tagprefix="igtbl" %>

<%@ Register assembly="Infragistics35.Web.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics35.Web.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.DataSourceControls" tagprefix="ig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Principal" runat="server">

    <!--Calendario-->
	<script src="../calendar/calendar.js" type="text/javascript"></script>
	<script src="../calendar/calendar-sp.js" type="text/javascript"></script>
	<script src="../calendar/date.js" type="text/javascript"></script>
	<script src="../calendar/calendar-setup.js" type="text/javascript"></script>	
	<link title="calendar-blue" media="all" href="../calendar/calendar-blue.css" type="text/css" rel="stylesheet" />
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
                                <asp:Label ID="lblTitulo" runat="server" Text="Reporte de Artículos sin Pedido"></asp:Label>
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
                    <table border="0px" cellpadding="0px" cellspacing="0px" class="textos_Login" style="width:100%; margin:auto;">
                        <tr style="">
                            <td style="width:200px;">
                                Clientes
                            </td>
                            
                            <td style="width:250px;">
                                <asp:TextBox ID="txtIdcliente" runat="server" CssClass="hidden"></asp:TextBox>
                                <asp:TextBox ID="txtCompDesc" runat="server"  CssClass="hidden"></asp:TextBox>
                                <asp:TextBox ID="txtClienteDesc" runat="server"  Width="200px"></asp:TextBox>
                                <img src="../Imagenes/tbfilter.gif" alt="seleccionar" style="cursor:pointer;" onclick="return openClientes();" />
                            </td>
                            <td>
                                Sucursales
                            </td>
                            <td>
                                <asp:TextBox ID="ddlEntrega" runat="server" CssClass="hidden" />
                                <asp:TextBox ID="txtEntrega" runat="server" Width="200px" ></asp:TextBox>
                                <img src="../Imagenes/tbfilter.gif" alt="seleccionar" style="cursor:pointer;" onclick="return openSucursales();" />
                            </td>
                        </tr>
                        <tr style="">
                            <td style="">
                                Fecha Inicio:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaInicio" runat="server" Width="200px"></asp:TextBox>
                                <img alt="" id="datepickInicio" height="20" src="../Imagenes/calendar.gif" style="cursor:pointer;" />
    							<script type="text/javascript">
    							    Calendar.setup
															({
															    inputField: "<%=txtFechaInicio.ClientID%>",        // id of the input field
															    ifFormat: "%d/%m/%Y",             // format of the input field
															    showsTime: false,                  // will display a time selector
															    button: "datepickInicio",             // trigger for the calendar (button ID)
															    singleClick: true,                   // double-click mode
															    step: 1                    // show all years in drop-down boxes (instead of every other year as default)
															});
            					</script>
                            </td>
                            <td>
                                Fecha Fin:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaFin" runat="server" Width="200px" ></asp:TextBox>
                                <img alt="" id="datepickFin" height="20" src="../Imagenes/calendar.gif" style="cursor:pointer;" />
    							<script type="text/javascript">
    							    Calendar.setup
															({
															    inputField: "<%=txtFechaFin.ClientID%>",        // id of the input field
															    ifFormat: "%d/%m/%Y",             // format of the input field
															    showsTime: false,                  // will display a time selector
															    button: "datepickFin",             // trigger for the calendar (button ID)
															    singleClick: true,                   // double-click mode
															    step: 1                    // show all years in drop-down boxes (instead of every other year as default)
															});
            					</script>
                            </td>
                        </tr>
                        <tr style="">
                            <td style="">
                                
                            </td>
                            <td>
                                <asp:Button ID="btnReporte" runat="server" Text="Ver Reporte" CssClass="Boton_01" 
                                  Width="130px" onclick="btnReporte_Click"  OnClientClick="return btnReporte_onclick();" />
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                <asp:Button ID="btnExcel" runat="server" Text="Exportar a Excel" CssClass="Boton_01" 
                                  Width="130px" onclick="btnExcel_Click" />
                            </td>
                        </tr>
                    </table>
                    <br />

                    <ig:WebHierarchicalDataGrid ID="whdg1" runat="server" 
                        AutoGenerateColumns="False" AutoGenerateBands="False"
                        DataKeyFields="id_cia,id_cliente"
                        Width="100%" oninit="whdg1_Init1"  >
                        <ExpandCollapseAnimation SlideOpenDirection="Auto" SlideOpenDuration="300" SlideCloseDirection="Auto" SlideCloseDuration="300" />
                        <Columns>
                            <ig:BoundDataField DataFieldName="id_cia" Key="id_cia" Header-Text="Id Compañia" Width="10%" />
                            <ig:BoundDataField DataFieldName="nom_cia" Key="nom_cia" Header-Text="Compañía" Width="30%" >
                                <Header Text="Compa&#241;&#237;a"></Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="id_cliente" Key="id_cliente" Header-Text="Id Cliente" Width="10%" />
                            <ig:BoundDataField DataFieldName="nom_cliente" Key="nom_cliente" Header-Text="Cliente" Width="40%">
                                <Header Text="Cliente"></Header>
                            </ig:BoundDataField>
                            
                            <ig:BoundDataField DataFieldName="num_articulos" Key="num_articulos" Header-Text="Artículos Sin Inventario" Width="10%" >
                                <Header Text="Art&#237;culos Sin Inventario"></Header>
                            </ig:BoundDataField>
                        </Columns>
                        <Bands>
                            <ig:Band AutoGenerateColumns="false"  Width="100%" >
                                <Columns>
                                    <ig:BoundDataField DataFieldName="id_entrega" Key="id_entrega" Header-Text="Id Entrega" Width="10%" />
                                    <ig:BoundDataField DataFieldName="nom_entrega" Key="nom_entrega" Header-Text="Sucursal Entrega" Width="80%" />
                                    <ig:BoundDataField DataFieldName="num_articulos" Key="num_articulos" Header-Text="Artículos Sin Inventario" Width="10%"/>
                                </Columns>
                                <Bands>
                                    <ig:Band AutoGenerateColumns="false" Width="100%" >
                                        <Columns>
                                            <ig:BoundDataField DataFieldName="Num_Periodo" Key="Num_Periodo" Header-Text="Fecha Reporte" />
                                            <ig:BoundDataField DataFieldName="id_articulo_revestimientos" Key="id_articulo_revestimientos" Header-Text="Código Art Lamosa" />
                                            <ig:BoundDataField DataFieldName="id_articulo_cliente" Key="id_articulo_cliente" Header-Text="Código Art Cliente" />
                                            <ig:BoundDataField DataFieldName="desc_articulo_cliente" Key="desc_articulo_cliente" Header-Text="Descripción" />
                                            <ig:BoundDataField DataFieldName="inv_maximo" Key="inv_maximo" Header-Text="Inventario&lt;br/&gt;Máximo" />
                                            <ig:BoundDataField DataFieldName="inv_minimo" Key="inv_minimo" Header-Text="Inventario&lt;br/&gt;Mínimo" />
                                            <ig:BoundDataField DataFieldName="inv_existente" Key="inv_existente" Header-Text="Inventario&lt;br/&gt;Existente" />
                                            <ig:BoundDataField DataFieldName="fecha_alta" Key="fecha_alta" Header-Text="Fec última&lt;br/&gt;captura Inv&lt;br/&gt;existente" />
                                        </Columns>
                                    </ig:Band>
                                </Bands>
                            </ig:Band>
                        </Bands>
                    </ig:WebHierarchicalDataGrid>
                         
                   
                    <br />
                                   </td>
            </tr>
        </table>
    </div>
    <asp:Literal ID="id_messages" runat="server"></asp:Literal>
    <div style="visibility:hidden; display:none;">
        <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtNomUsuario" runat="server"  Width="200px"></asp:TextBox>-->
                                <img src="../Imagenes/tbfilter.gif" alt="seleccionar" style="cursor:pointer;" onclick="return openUsuarios();" />
                                <asp:HiddenField ID="HddUser" runat="server" />
                                <asp:HiddenField ID="HddNombreUsuario" runat="server" />
    </div>
    <script language="javascript" src="../FuncionesJS/wz_tooltip.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">

        window.document.getElementById("<%=txtFechaInicio.ClientID%>").readOnly = "readonly";
        window.document.getElementById("<%=txtFechaFin.ClientID%>").readOnly = "readonly";
        window.document.getElementById("<%=txtEntrega.ClientID%>").readOnly = "readonly";
        window.document.getElementById("<%=txtClienteDesc.ClientID%>").readOnly = "readonly";
        


        function openUsuarios() {

            window.showModalDialog("webDialog.aspx?src=Usuario.aspx&titulo=Usuarios", window, "dialogWidth:500px;dialogHeight:550px;");
            return false;
        }

        function btnReporte_onclick() {
            if (window.document.getElementById("<%=txtIdcliente.ClientID%>").value == "") {
                alert("Favor de Seleccionar uno o mas clientes");
                return false;
            }
            if (window.document.getElementById("<%=ddlEntrega.ClientID%>").value == "") {
                alert("Favor de Seleccionar una o mas Sucursales");
                return false;
            }

        }

        function openClientes() {
            var URL = "vSeleccionCliente.aspx";
            window.showModalDialog("webDialog.aspx?src=" + URL, window, "dialogWidth:700px;dialogHeight:500px;");

        }

        function openSucursales() {

            var id_clienteS = window.document.getElementById("ctl00_Principal_txtIdcliente").value;
            var id_entregaS = window.document.getElementById("ctl00_Principal_ddlEntrega").value;
            if (id_clienteS == "") {
                alert("Favor de Seleccionar el Cliente");
                return;
            }
            var URL = "vSeleccionEntrega.aspx";
            URL += "&ids_cliente=" + id_clienteS;
            URL += "&ids_entrega=" + id_entregaS;

            window.showModalDialog("webDialog.aspx?src=" + URL, window, "dialogWidth:700px;dialogHeight:500px;");

        }

    </script>
    
   
    
    <ig:WebExcelExporter ID="WebExcelExporter1" runat="server" 
        DataExportMode="AllDataInDataSource" DownloadName="ArtSinPedido">
    </ig:WebExcelExporter>
    
   
    
</asp:Content>