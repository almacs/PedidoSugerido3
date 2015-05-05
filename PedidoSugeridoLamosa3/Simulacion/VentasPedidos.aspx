<%@ Page Title="Simulacion de ventas" Language="C#" MasterPageFile="~/PedidoSugeridoLamosa.Master"
    AutoEventWireup="true" CodeBehind="VentasPedidos.aspx.cs" Inherits="PedidoSugeridoLamosa.Simulacion.VentasPedidos" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>

<%@ Register Assembly="Infragistics35.Web.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>

<asp:Content ID="PedidoSugerido" ContentPlaceHolderID="Principal" runat="server">
<link rel="stylesheet" href="../FuncionesJS/jquery/css/base/jquery-ui.css" />
<script type="text/javascript" src="../FuncionesJS/jquery/ui/jquery-ui.js"></script>
<style type="text/css">
.ui-dialog { z-index: 1000 !important ;}
</style>
<script language="javascript" type="text/javascript">
    function openDialog(ptitle, source, width, height) {
        if ($('#dialog').length > 0) {
            $('#dialog').remove();
        }
        $("#dialog").html("");
        $('<div id="dialog"></div>').appendTo('body');

        var frame = "<iframe width='100%' height='100%' frameborder='0' scrolling='yes' allowtransparency='true' src='" + source + "'></iframe>";
        var dialog = 'dialog';
        $("#dialog").dialog({
            zIndex: 1000,
            autoOpen: false,
            height: height + 60,
            width: width + 50,
            modal: true,
            title: ptitle,
            resizable: false,
            position: 'center',
            close: function() {
                $(this).html("");
                $(this).remove();
            }
        });
        $("#dialog").html(frame);
        $("#dialog").dialog("open");
        return false;
    }
        var arr_cds = new Array();
        var bit_Pantalla = false;

//        window.onload = openWindowWait();
//        window.onload = window.document.getElementById("Cargando").style.visibility = "visible";

//        function Espera() {
//            window.document.getElementById("tblCargando").style.visibility = "visible";
//            window.document.getElementById("tblCargando").style.display = "block";
//            window.document.getElementById("divMain").style.display = "none";
//            bit_Pantalla = true;
//        }
//        function openWindowWait() {
//            window.open('../PaginaEspera.aspx', '', 'toolbar=no,location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=no,copyhistory=no,width=420,height=80,top=300,left=300');
//        }

        function btnExportar_onclick()
        {
            bit_Pantalla = true;
        }

        function btnProcesar_onclick()
        {
            //debugger;
            //grid_Detalle
            window.document.getElementById("tblCargando").style.visibility = "visible";
            window.document.getElementById("tblCargando").style.display = "block";
            window.document.getElementById("divMain").style.display = "none";
            bit_Pantalla = true;
        }
        
        function btnArtDesc_onclick()
        {
            //window.open('webDialog.aspx?src=ArtDesc.aspx&titulo=Quitar Articulos Descontinuados', '', 'width=800,height=600,top=300,left=300');
            //

            window.showModalDialog("webDialog.aspx?src=ArtDesc.aspx&titulo=Quitar Articulos Descontinuados&btnProcesar=<%=btnProcesar.ClientID%>", window, "dialogWidth:850px;dialogHeight:650px;");
            return false;
        }

        function btnCleanInv_onclick()
        {
            if (!confirm('¿ Está seguro de limpiar los inventarios?'))
            {
                return false;
            }
            window.document.getElementById("tblCargando").style.visibility = "visible";
            window.document.getElementById("tblCargando").style.display = "block";
            window.document.getElementById("divMain").style.display = "none";
            bit_Pantalla = true;
        }

        function btnCleanAjus_onclick()
        {
            if (!confirm('¿ Está seguro de limpiar los ajustes?'))
            {
                return false;
            }
            window.document.getElementById("tblCargando").style.visibility = "visible";
            window.document.getElementById("tblCargando").style.display = "block";
            window.document.getElementById("divMain").style.display = "none";
            bit_Pantalla = true;
        }
        
        function btnPDF_onclick()
        {
            bit_Pantalla = true;
        }

        function btnGuardar_onclick()
        {
            window.document.getElementById("tblCargando").style.visibility = "visible";
            window.document.getElementById("tblCargando").style.display = "block";
            window.document.getElementById("divMain").style.display = "none";
            bit_Pantalla = true;
        }
        
        // Erwin Gomez Rivero
        // Funciones para Mostrar/Ocultar Detalle Pedidos Pendientes.
        function cursor_wait() {
            document.body.style.cursor = 'wait';
            document.status = 'Espere por favor...';
        }

        function cursor_clear() {
            document.body.style.cursor = 'default';
            document.status = '';
        }
        
        function imgReturnDataInTransitBaan_onclick(obj)
        {
            //alert(obj.getAttribute("idnum"));
            //obj.parentNode.parentNode.cells(8).children(0).innerText = obj.transitobaan;
            EnterEditMode("ctl00_Principal_grid_Detalle", obj.getAttribute("idnum") - 1, 11, obj.getAttribute("transitobaan"));
            //igtbl_getGridById("ctl00_Principal_grid_Detalle").Rows.rows[0].cells[11].setValue();
            

        }
        
        
        function EnterEditMode(gridName, id, celltoedit,value)
 {
     //alert("gridName = "+gridName+" :id = "+id+" :celltoedit = "+celltoedit);
     id = igtbl_getGridById("ctl00_Principal_grid_Detalle").Rows.rows[id].Id;
     var grid = igtbl_getGridById(gridName);
 
 var row = igtbl_getRowById(id);
 igtbl_setActiveRow(gridName,row);

 var rowname = id.substring(0, id.lastIndexOf('_'));

 rowname = rowname+ '_10'; 

igtbl_setActiveCell(gridName,igtbl_getElementById(rowname));
 var cell = row.getCell(celltoedit); //obtain object of current cell to be edited


 
 cell.beginEdit(); //start current cell in edit mode
 cell.setValue(value);
 cell.endEdit(true);
 
 }

//method to call when a cell is clicked
 function CellClick(gn,cid)
 { 
var celltoedit = cid.substring(cid.lastIndexOf('_')+1);
 //alert("cid = "+cid+" :gn = "+gn+" :celltoedit = "+celltoedit);
 var row = igtbl_getRowById(cid);
 var band = row.Band 
if(band.Index ==0)
 {
 if(celltoedit != 0 && celltoedit != 8 && celltoedit != 12 && celltoedit != 3 && celltoedit != 7)
 {
 //alert("celltoedit ="+celltoedit);
 EnterEditMode(gn, cid, celltoedit);
 }
 }
 else
 {
 EnterEditMode(gn, cid, celltoedit);
 }
 }





        
        
        function btMostrarOcultarDetPedidosPendientes_onclick() {
            cursor_wait();
            
            var display = '';
            var boton = window.document.getElementById('btMostrarOcultarDetPedidosPendientes');
            
            if (boton.value == 'Ocultar Detalle Pedidos Pendientes') {
                display = 'none';
                boton.value = 'Mostrar Detalle Pedidos Pendientes';
            }
            else if (boton.value == 'Mostrar Detalle Pedidos Pendientes') {
                display = '';
                boton.value = 'Ocultar Detalle Pedidos Pendientes';
            }
            
            // Celdas de encabezado
            window.document.getElementById('ctl00xPrincipalxgridxDetalle_c_0_7').style.display = display;
            window.document.getElementById('ctl00xPrincipalxgridxDetalle_c_0_8').style.display = display;
            window.document.getElementById('ctl00xPrincipalxgridxDetalle_c_0_9').style.display = display;
            window.document.getElementById('ctl00xPrincipalxgridxDetalle_c_0_10').style.display = display;
            window.document.getElementById('ctl00xPrincipalxgridxDetalle_c_0_11').style.display = display;

            var displayT = display;
            if ('<%=perfilConsulta%>' == 'Consulta' || '<%=perfilConsulta%>' == 'Asesor') 
            {
                displayT = 'none';
            }
            window.document.getElementById('ctl00xPrincipalxgridxDetalle_c_0_12').style.display = displayT;
            var gridDetalleDD = window.document.getElementById("G_ctl00xPrincipalxgridxDetalle");
            for (var int_reg = 1; int_reg < gridDetalleDD.rows.length; int_reg++) {
                gridDetalleDD.rows(int_reg).cells(3).children(0).children(0).rows(0).cells(9).children(0).style.display = displayT;
            }

            
            
            
            
            //Celdas de cuerpo
            var tabla = window.document.getElementById("G_ctl00xPrincipalxgridxDetalle");
            var int_reg = 1;
            for (int_reg = 1; int_reg < tabla.rows.length; int_reg++) {
                tabla.rows[int_reg].cells[3].children[0].children[0].getElementsByTagName('tr')[0].getElementsByTagName('td')[4].style.display = display;
                tabla.rows[int_reg].cells[3].children[0].children[0].getElementsByTagName('tr')[0].getElementsByTagName('td')[5].style.display = display;
                tabla.rows[int_reg].cells[3].children[0].children[0].getElementsByTagName('tr')[0].getElementsByTagName('td')[6].style.display = display;
                tabla.rows[int_reg].cells[3].children[0].children[0].getElementsByTagName('tr')[0].getElementsByTagName('td')[7].style.display = display;
                tabla.rows[int_reg].cells[3].children[0].children[0].getElementsByTagName('tr')[0].getElementsByTagName('td')[8].style.display = display;
                tabla.rows[int_reg].cells[3].children[0].children[0].getElementsByTagName('tr')[0].getElementsByTagName('td')[9].style.display = displayT;

                // Otra manera
                //tabla.rows[int_reg].cells[3].children[0].children[0].rows[0].cells[4].style.display = display;
                //tabla.rows[int_reg].cells[3].children[0].children[0].rows[0].cells[5].style.display = display;
                //tabla.rows[int_reg].cells[3].children[0].children[0].rows[0].cells[6].style.display = display;
                //tabla.rows[int_reg].cells[3].children[0].children[0].rows[0].cells[7].style.display = display;
                //tabla.rows[int_reg].cells[3].children[0].children[0].rows[0].cells[8].style.display = display;
            }
            
            cursor_clear();
        }

        // Mostrar/Ocultar el Cuadro de Dialogo de Confirmacion.
        function ShowHiddenConfirmationDialog(bShow) {
            if (bShow) {
                $find('<%=wdwComentariosOC.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
            }
            else {
                $find('<%=wdwComentariosOC.ClientID%>').set_windowState($IG.DialogWindowState.Hidden);
            }
            return false;
        }
        
    </script>
    
    <div id="divMain" style="width: 100%;">
        <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
            <tbody>
                <tr>
                    <td style="height: 10px">
                    
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="lblTitulo1" onmouseover="this.T_DESCPANTALLA=true; this.T_BGIMG='../Imagenes/help2.gif'; this.T_HEIGHT=200; this.T_WIDTH=400; this.T_ABOVE=false; this.T_CLICKCLOSE=true; this.T_STICKY=true; return escape('<b>Simulacion de Pedido Sugerido</b><p></p>Es la pantalla principal del sistema y se utiliza para desplegar los artículos previamente seleccionados con sus respectivos valores de inventario, cantidad pendiente de entregar y su status, así como el Inventario actual por zona de su proveedor, e información adicional. En base a estos valores se le sugiere una cantidad a pedir en la columna “Faltantes Críticos” la  cual usted podrá modificar en la columna de “Ajustes” y ejecutar el botón “Procesar”. El dato resultante de la columna “Final” será la cantidad considerada como Pedido Sugerido');">
                                    <asp:Label ID="lblTitulo" runat="server" Text="Simulación de Herramienta de Abasto">
                                    </asp:Label>
                                    <br/>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="fecha_simulacion" CssClass="textos_Login" runat="server">
                                    </asp:Label>
                                    <br/>
                                </td>
                                <td align="left" style="width: 270px" class="lblTitulo1">
                                    
                                </td>
                                <td class= "ms-descriptiontextToolTip" align="right">
                                    <a class="Internal_button" id="AddUserBtn" href="#" onclick="return openDialog('Historico', 'PopUp.aspx?urlStr=ConsultaHistorico.ascx', 800, 400); return false;">...</a>
                                    <img id="imgHelp" onmouseover="this.T_PADDING=2; this.T_INFORMATION=true; this.T_BGIMG=''; this.T_BGCOLOR='#FFFFDD'; this.T_HEIGHT=0; this.T_ABOVE=true; this.T_LEFT=true;  return escape('Muestra la descripcion de la pantalla');"
                                        style="cursor: hand" onclick="return help_onclick();" alt="" src="../Imagenes/Information2_.jpg" name="imgHelp" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="50%" align="left" valign="middle">
                                    <table>
                                        <tr>   
                                            <td>
                                                <asp:Button ID="btnProcesar" runat="server" Text="Procesar" CssClass="Boton_01" 
                                                    Width="130px" onclick="btnProcesar_Click" OnClientClick="return btnProcesar_onclick();" />
                                                <asp:Button ID="btnArtDesc" runat="server" Text="Quitar Articulos Desc" CssClass="Boton_01"
                                                    Width="150px" OnClientClick="return btnArtDesc_onclick();" />
                                                <asp:Button ID="btnExportar" runat="server" Text="Exportar a Excel" CssClass="Boton_01"
                                                    Width="150px" OnClientClick="return btnExportar_onclick();" OnClick="btnExportar_Click" />
                                            </td>
                                        </tr>
                                        <tr>   
                                            <td>
                                                <asp:Button ID="btnCleanInv" runat="server" Text="Limpiar Inventario" CssClass="Boton_01" 
                                                    Width="130px" OnClientClick="return btnCleanInv_onclick();" onclick="btnCleanInv_Click" />
                                                <asp:Button ID="btnCleanAjus" runat="server" Text="Limpia Ajustes" CssClass="Boton_01" 
                                                    Width="150px" OnClientClick="return btnCleanAjus_onclick();" 
                                                    onclick="btnCleanAjus_Click" />
                                                <input type="button" value="Exportar Formato OC" style="width: 150px;" class="Boton_01" 
                                                    onclick="ShowHiddenConfirmationDialog(true);" 
                                                    runat = "server" id="btnExportarOC" name="btnExportarOC"
                                                    onmouseover="return escape('Genera un archivo de Orden de compra en formato PDF basándose la cantidad “Final” de cada artículo.')" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="50%" align="right" valign="middle"
                                    onmouseover="this.T_DESCPANTALLA=true; this.T_BGIMG='../Imagenes/help2.gif'; this.T_HEIGHT=200; this.T_WIDTH=400; this.T_ABOVE=false; this.T_CLICKCLOSE=true; this.T_STICKY=true; return escape('<b>Tabla Resumen</b><p></p>Muestra el número de camiones necesarios por Zona así como los días promesa de entrega tanto para la cantidad sugerida neta como para la cantidad sugerida incluyendo Ajustes.');" >
                                    <igtbl:UltraWebGrid ID="grid_Resumen" runat="server" EnableAppStyling="False" >
                                        <Bands>
                                            <igtbl:UltraGridBand>
                                                <Columns>
                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Planta" Width="55px">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                        <ValueList DisplayMember="Planta">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Planta">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Sugerido" Width="70px" AllowUpdate="No" 
                                                        Format="">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                        <ValueList DisplayMember="Sugerido">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Sugerido">
                                                            <RowLayoutColumnInfo OriginX="1" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="1" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="T/L" Width="65px">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                        <ValueList DisplayMember="T/L">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Cant. camiones">
                                                            <RowLayoutColumnInfo OriginX="2" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="2" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Con Ajustes" 
                                                        Width="70px">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                        <ValueList DisplayMember="Con Ajustes">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Con Ajustes">
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="T/L_" Width="65px">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                        <ValueList DisplayMember="T/L_">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Cant. camiones_">
                                                            <RowLayoutColumnInfo OriginX="4" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="4" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Dias Promesa" 
                                                        Width="55px">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                        <ValueList DisplayMember="Dias Promesa">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Dias Promesa">
                                                            <RowLayoutColumnInfo OriginX="5" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="5" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                </Columns>
                                                <AddNewRow Visible="NotSet" View="NotSet">
                                                
                                                </AddNewRow>
                                            </igtbl:UltraGridBand>
                                        </Bands>
                                        <DisplayLayout Name="UltraWebGrid2" AllowDeleteDefault="No"
                                            NoDataMessage="" RowHeightDefault="20px" SelectTypeRowDefault="Single" TableLayout="Fixed"
                                            Version="3.00" CellClickActionDefault="RowSelect" LoadOnDemand="Xml" CellPaddingDefault="1"
                                             RowSelectorsDefault="No" AutoGenerateColumns="False">
                                            <RowAlternateStyleDefault BackColor="#E5E5E5">
                                            </RowAlternateStyleDefault>
                                            <EditCellStyleDefault BackColor="silver">
                                            </EditCellStyleDefault>
                                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                            </FooterStyleDefault>
                                            <HeaderStyleDefault BackColor="#666666" BorderColor="Black" BorderStyle="Solid" Font-Bold="True"
                                                ForeColor="White">
                                            </HeaderStyleDefault>
                                            <RowSelectorStyleDefault BorderStyle="Solid">
                                            </RowSelectorStyleDefault>
                                            <RowStyleDefault BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Names="Verdana" Font-Size="8pt" ForeColor="Black">
                                            </RowStyleDefault>
                                            <SelectedRowStyleDefault BackColor="#FFFFB3" ForeColor="Black" Font-Bold="True">
                                            </SelectedRowStyleDefault>
                                            <AddNewBox Hidden="true">
                                            </AddNewBox>
                                            <ActivationObject BorderColor="Blue" BorderWidth="2px" BorderStyle="Solid">
                                            </ActivationObject>
                                            <FilterOptionsDefault>
                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                    Font-Size="11px" Width="200px">
                                                </FilterDropDownStyle>
                                                <FilterHighlightRowStyle BackColor="#999999" ForeColor="White">
                                                </FilterHighlightRowStyle>
                                            </FilterOptionsDefault>
                                        </DisplayLayout>
                                    </igtbl:UltraWebGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style="height: 33px;">
                            <td style="width:30%;">
                                <input id="btMostrarOcultarDetPedidosPendientes" type="button" value="Ocultar Detalle Pedidos Pendientes" 
                                    class="Boton_01" style="width:250px;" onclick="return btMostrarOcultarDetPedidosPendientes_onclick();" />
                            </td>
                            <td style="text-align:left; vertical-align:middle; width:70%;">
                                Buscar:
                                <input id="txtBuscarItem" name="txtBuscarItem" type="text" style="width:200px; vertical-align:middle;" onkeypress="return validar(event);" />
                                en:
                                <asp:DropDownList ID="cboOpcionBusqueda" runat="server" type="text" style="width:150px; vertical-align:middle;" >
                                    <asp:ListItem Value="-1">Todo</asp:ListItem>
                                    <asp:ListItem Value="0">Artículo</asp:ListItem>
                                    <asp:ListItem Value="1">Descripción</asp:ListItem>
                                    <asp:ListItem Value="R">Resto</asp:ListItem>
                                </asp:DropDownList>
                                <input id="btnBuscarItem" class="Boton_01" name="btnBuscarItem" type="button" onclick="return btnBuscarItem_onclick();" value="Buscar" style="width:100px;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
                <tr>
                    <td onmouseover="this.T_DESCPANTALLA=true; this.T_BGIMG='../Imagenes/help2.gif'; this.T_HEIGHT=200; this.T_WIDTH=400; this.T_ABOVE=false; this.T_CLICKCLOSE=true; this.T_STICKY=true; return escape('<b>Tabla Simulacion</b><p></p>Despliega la información necesaria en el cálculo del Pedido Sugerido. Las columnas en azul le permiten a usted modificar la información desplegada. Será necesario ejecutar el botón “Procesar” para recalcular la información.');">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left">
                                    <igtbl:UltraWebGrid ID="grid_Detalle" runat="server" Width="100%" Height="500px" EnableAppStyling="False" 
                                        onpageindexchanged="grid_Detalle_PageIndexChanged">
                                        <Bands>
                                            <igtbl:UltraGridBand>
                                                <Columns>
                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Cve_Articulo_Cliente" 
                                                        Width="75px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Height="40px" />
                                                        <Header Caption="Artículo Cliente" Fixed="True" />
                                                        <CellButtonStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                                                        </CellButtonStyle>
                                                        <ValueList DisplayMember="Cve_Articulo_Cliente" />
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Cve_Articulo" Hidden="True" 
                                                        AllowUpdate="No" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Height="40px" />
                                                        <Header Caption="Artículo Genérico" Fixed="True">
                                                            <RowLayoutColumnInfo OriginX="1" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="1" />
                                                        </Footer>
                                                        <ValueList DisplayMember="Cve_Articulo" />
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Desc_Articulo" 
                                                        Width="220px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Height="40px" />
                                                        <Header Caption="Descripción del Artículo" Fixed="True">
                                                            <RowLayoutColumnInfo OriginX="2" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="2" />
                                                        </Footer>
                                                        <CellButtonStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True">
                                                        </CellButtonStyle>
                                                        <ValueList DisplayMember="Desc_Articulo" />
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Val_Min_Cliente" 
                                                        DataType="System.Int32" FieldLen="5" Width="60px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" BackColor="#0033CC" HorizontalAlign="Center" 
                                                            VerticalAlign="Middle" Height="40px" />
                                                        <ValueList DisplayMember="Val_Min_Cliente" />
                                                        <CellStyle ForeColor="#0033CC" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                        <Header Caption="Inv. Mínimo">
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Val_Max_Cliente" 
                                                        DataType="System.Int32" FieldLen="5" Width="60px" AllowResize="Fixed">
                                                        <HeaderStyle BackColor="#0033CC" Wrap="True" HorizontalAlign="Center" 
                                                            VerticalAlign="Middle" Height="40px" />
                                                        <ValueList DisplayMember="Val_Max_Cliente">
                                                        </ValueList>
                                                        <CellStyle ForeColor="#0033CC" HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Inv. Máximo">
                                                            <RowLayoutColumnInfo OriginX="4" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="4" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Val_Inv_Cliente" 
                                                        Width="63px" DataType="System.Int32" FieldLen="5" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            BackColor="#0033CC" Height="40px" />
                                                        <ValueList DisplayMember="Val_Inv_Cliente">
                                                        </ValueList>
                                                        <CellStyle ForeColor="#0033CC" HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Inv. Existente">
                                                            <RowLayoutColumnInfo OriginX="5" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="5" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Val_Ped_Pendiente" AllowUpdate="No" 
                                                        Width="65px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Height="40px" />
                                                        <ValueList DisplayMember="Val_Ped_Pendiente">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Pedidos Pendientes">
                                                            <RowLayoutColumnInfo OriginX="6" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="6" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Val_Ped_SCred" AllowUpdate="No" 
                                                        Width="60px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            BackColor="#D7E1F3" ForeColor="Black" Height="40px" />
                                                        <ValueList DisplayMember="Val_Ped_SCred">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Sin Crédito">
                                                            <RowLayoutColumnInfo OriginX="7" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="7" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Val_Ped_PendAssignCInv" AllowUpdate="No" 
                                                        Width="60px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            BackColor="#D7E1F3" ForeColor="Black" Height="40px" />
                                                        <ValueList DisplayMember="Val_Ped_PendAssignCInv">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Por asignar con Inv.">
                                                            <RowLayoutColumnInfo OriginX="8" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="8" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Val_Ped_PendAssignSInv" AllowUpdate="No" 
                                                        Width="60px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            BackColor="#D7E1F3" ForeColor="Black" Height="40px" />
                                                        <ValueList DisplayMember="Val_Ped_PendAssignSInv">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Por asignar sin Inv.">
                                                            <RowLayoutColumnInfo OriginX="9" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="9" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Val_Ped_Assign" AllowUpdate="No" 
                                                        Width="65px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            BackColor="#D7E1F3" ForeColor="Black" Height="40px" />
                                                        <ValueList DisplayMember="Val_Ped_Assign">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Asignado por Embarcar">
                                                            <RowLayoutColumnInfo OriginX="10" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="10" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Val_Ped_InTransit" AllowUpdate="Yes" 
                                                        Width="60px" DataType="System.Int32" FieldLen="5" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" 
                                                            VerticalAlign="Middle" BackColor="#D7E1F3" ForeColor="Black" 
                                                            Height="40px" />
                                                        <ValueList DisplayMember="Val_Ped_InTransit">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="En Tránsito">
                                                            <RowLayoutColumnInfo OriginX="11" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="11" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:TemplatedColumn BaseColumnName="Val_Ped_InTransit" AllowUpdate="Yes" 
                                                        Width="40px" DataType="System.Int32" FieldLen="5" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" 
                                                            VerticalAlign="Middle" BackColor="#D7E1F3" ForeColor="Black" 
                                                            Height="40px" />
                                                        <ValueList DisplayMember="Val_Ped_InTransit">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="">
                                                            <RowLayoutColumnInfo OriginX="11" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="11" />
                                                        </Footer>
                                                        <CellTemplate>
                                                            <img alt="Establecer Cantidad En Tránsito de BAAN" src="../Imagenes/back.jpg" style="cursor:pointer; width:20px" title="Establecer Cantidad En Transito de BAAN" transitobaan="<%# DataBinder.Eval(Container, "DataItem.Val_Ped_InTransitOrig")%>"  idnum="<%# DataBinder.Eval(Container, "DataItem.IdNum")%>" onclick="return imgReturnDataInTransitBaan_onclick(this);" />
                                                            
                                                        </CellTemplate>
                                                    </igtbl:TemplatedColumn>
                                                    <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Val_PedMin_Cliente" 
                                                        Width="60px" DataType="System.Int32" FieldLen="5" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            BackColor="#0033CC" Height="40px" />
                                                        <ValueList DisplayMember="Val_PedMin_Cliente">
                                                        </ValueList>
                                                        <CellStyle ForeColor="#0033CC" HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Pedido Mínimo">
                                                            <RowLayoutColumnInfo OriginX="12" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="12" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Val_PedMax_Faltante" AllowUpdate="No" 
                                                        Width="60px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Height="40px" />
                                                        <ValueList DisplayMember="Val_PedMax_Faltante">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Faltante">
                                                            <RowLayoutColumnInfo OriginX="13" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="13" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Val_PedCrit_Faltante" AllowUpdate="No" 
                                                        Width="60px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" 
                                                            VerticalAlign="Middle" Height="40px" />
                                                        <ValueList DisplayMember="Val_PedCrit_Faltante">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Faltante Crítico">
                                                            <RowLayoutColumnInfo OriginX="14" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="14" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Val_Ajuste_Cliente" 
                                                        Width="60px" DataType="System.Int32" FieldLen="5" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            BackColor="#0033CC" Height="40px" />
                                                        <ValueList DisplayMember="Val_Ajuste_Cliente">
                                                        </ValueList>
                                                        <CellStyle ForeColor="#0033CC" HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Ajustes">
                                                            <RowLayoutColumnInfo OriginX="15" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="15" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Val_Final" AllowUpdate="No" Width="60px" 
                                                        AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Height="40px" />
                                                        <ValueList DisplayMember="Val_Final">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Sugerido Final">
                                                            <RowLayoutColumnInfo OriginX="16" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="16" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Cve_Zona" AllowUpdate="No" 
                                                        Width="80px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Height="40px" />
                                                        <ValueList DisplayMember="Cve_Zona">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Zona">
                                                            <RowLayoutColumnInfo OriginX="17" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="17" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Val_LeadTime" AllowUpdate="No" 
                                                        Width="50px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Height="40px" />
                                                        <ValueList DisplayMember="Val_LeadTime">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Dias promesa">
                                                            <RowLayoutColumnInfo OriginX="18" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="18" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Val_M2_Tarima" AllowUpdate="No" 
                                                        Width="50px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Height="40px" />
                                                        <ValueList DisplayMember="Val_M2_Tarima">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Conv. a Tarima">
                                                            <RowLayoutColumnInfo OriginX="19" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="19" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Val_Inv" AllowUpdate="No" 
                                                        Width="60px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" 
                                                            VerticalAlign="Middle" Height="40px" />
                                                        <ValueList DisplayMember="Val_Inv">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Inv.">
                                                            <RowLayoutColumnInfo OriginX="20" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="20" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Act_PU_Facturacion" AllowUpdate="No" 
                                                        Width="60px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" 
                                                            VerticalAlign="Middle" Height="40px" />
                                                        <ValueList DisplayMember="Act_PU_Facturacion">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="P. U.">
                                                            <RowLayoutColumnInfo OriginX="21" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="21" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Act_Monto_Sugerido" AllowUpdate="No" 
                                                        Width="60px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" 
                                                            VerticalAlign="Middle" Height="40px" />
                                                        <ValueList DisplayMember="Act_Monto_Sugerido">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Monto Sugerido">
                                                            <RowLayoutColumnInfo OriginX="22" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="22" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Act_Historico_M1" AllowUpdate="No" 
                                                        Width="60px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" 
                                                            VerticalAlign="Middle" Height="40px" />
                                                        <ValueList DisplayMember="Act_Historico_M1">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Historico Mes 1">
                                                            <RowLayoutColumnInfo OriginX="23" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="23" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Act_Historico_M2" AllowUpdate="No" 
                                                        Width="60px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" 
                                                            VerticalAlign="Middle" Height="40px" />
                                                        <ValueList DisplayMember="Act_Historico_M2">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Historico Mes 2">
                                                            <RowLayoutColumnInfo OriginX="24" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="24" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn BaseColumnName="Act_Historico_M3" AllowUpdate="No" 
                                                        Width="60px" AllowResize="Fixed">
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" 
                                                            VerticalAlign="Middle" Height="40px" />
                                                        <ValueList DisplayMember="Act_Historico_M3">
                                                        </ValueList>
                                                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle">
                                                        </CellStyle>
                                                        <Header Caption="Historico Mes 3">
                                                            <RowLayoutColumnInfo OriginX="25" />
                                                        </Header>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="25" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                </Columns>
                                                <RowTemplateStyle BackColor="White" BorderColor="White" BorderStyle="Ridge">
                                                    <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" 
                                                        WidthTop="3px" />
                                                </RowTemplateStyle>
                                                <AddNewRow Visible="NotSet" View="NotSet">
                                                </AddNewRow>
                                            </igtbl:UltraGridBand>
                                        </Bands>
                                        <DisplayLayout Name="UltraWebGrid2" AllowDeleteDefault="No" AllowUpdateDefault="RowTemplateOnly"
                                            NoDataMessage="" RowHeightDefault="20px" SelectTypeRowDefault="Single" TableLayout="Fixed"
                                            Version="3.00" CellClickActionDefault="RowSelect" LoadOnDemand="Xml" AllowAddNewDefault="Yes"
                                            AllowColSizingDefault="Free" AllowSortingDefault="OnClient" CellPaddingDefault="1"
                                             RowSelectorsDefault="No" RowSizingDefault="Free" 
                                            AutoGenerateColumns="False" BorderCollapseDefault="Separate" 
                                            StationaryMargins="Header" UseFixedHeaders="True">
                                            <FrameStyle Width="100%" Height="500px">
                                            </FrameStyle>
                                            <RowAlternateStyleDefault BackColor="#E5E5E5">
                                            </RowAlternateStyleDefault>
                                            <Pager PageSize="20">
                                                <PagerStyle Font-Size= "11px"  Font-Names="Arial" BackColor= "#666666" ForeColor="White" Height="20px" BorderStyle= "Solid" BorderColor="Black" BorderWidth= "1px"/>
                                            </Pager>
                                            <EditCellStyleDefault BackColor="Silver">
                                            </EditCellStyleDefault>
                                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                            </FooterStyleDefault>
                                            <HeaderStyleDefault BackColor="#666666" BorderColor="Black" BorderStyle="Solid" Font-Bold="True"
                                                ForeColor="White">
                                            </HeaderStyleDefault>
                                            <RowSelectorStyleDefault BorderStyle="Solid">
                                            </RowSelectorStyleDefault>
                                            <RowStyleDefault BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Names="Verdana" Font-Size="8pt" ForeColor="Black">
                                            </RowStyleDefault>
                                            <SelectedRowStyleDefault BackColor="#FFFFB3" Font-Bold="True" ForeColor="Black">
                                            </SelectedRowStyleDefault>
                                            <ActivationObject BorderColor="Black" BorderWidth="" BorderStyle="Dotted">
                                            </ActivationObject>
                                            <AddNewRowDefault View="Top">
                                            </AddNewRowDefault>
                                            <FilterOptionsDefault>
                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                    Font-Size="11px" Width="200px">
                                                </FilterDropDownStyle>
                                                <FilterHighlightRowStyle BackColor="#999999" ForeColor="White">
                                                </FilterHighlightRowStyle>
                                            </FilterOptionsDefault>
                                        </DisplayLayout>
                                    </igtbl:UltraWebGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <ig:WebDialogWindow ID="wdwComentariosOC" runat="server" Height="150px" 
            Width="250px" InitialLocation="Centered" Modal="True" 
            BackColor="#FAFAFA" WindowState="Hidden">
            <Header CaptionText="Pedido Sugerido" BackColor="#FAFAFA" >
                <CloseBox Visible="False" />
            </Header>
            <ContentPane BackColor="#FAFAFA">
                <Template>
                    <div style="text-align: center; font-family:Arial; font-size: 10pt; position: relative; height: 100%; width: 100%">
                        <br />
                        ¿Desea incluir comentarios en los artículos de la orden de compra?
                        <br />
                        <div style="position: absolute; right: 5px; top: 70px;">
                            <asp:Button ID="btComentariosSi" runat="server" Text="Si" CssClass="Boton_01"
                                        Height="25px" Width="50px" onclick="btComentariosSi_Click" />
                            <asp:Button ID="btComentariosNo" runat="server" Text="No" CssClass="Boton_01"
                                        Height="25px" Width="50px" onclick="btComentariosNo_Click" 
                                        OnClientClick="ShowHiddenConfirmationDialog(false);" />
                            <input type="button" value="Cancelar" style="width: 80px;" class="Boton_01" 
                                    onclick="ShowHiddenConfirmationDialog(false);" />
                        </div>
                    </div>
                </Template>
            </ContentPane>
        </ig:WebDialogWindow>
    
    <asp:Literal ID="id_messages" runat="server">
    
    </asp:Literal>
    
    <div style="visibility:hidden; display:none;">
        <asp:TextBox ID="txtCambio" runat="server">0</asp:TextBox>
    </div>
    
    <table id="tblCargando" style="visibility:hidden;display:none; width:100%; height:100%">
        <tr style="width:100%; height:100%">
            <td align="center" valign="middle" style="width:100%; height:100%">
                <img src="../Imagenes/cargando3.gif" alt="cargando" />
                <span style="width:100%; height:100%"></span>
            </td>
        </tr>
    </table>
    
    <script language="javascript" type="text/javascript">

        var strFind = "";
        var typeFind = "";
        var numCol = 3;
        var objFinded;
        
        function btnBuscarItem_onclick() {
            var regExp;
            if (objFinded != undefined) {
                window.document.getElementById(objFinded.Id).style.backgroundColor = "";
            }
            //debugger;

            if (strFind != window.document.getElementById("txtBuscarItem").value || typeFind != window.document.getElementById("<%=cboOpcionBusqueda.ClientID%>").value) {
                strFind = window.document.getElementById("txtBuscarItem").value;
                typeFind = window.document.getElementById("<%=cboOpcionBusqueda.ClientID%>").value;
                regExp = new RegExp("" + strFind + "", "gi");
                if (typeFind == "-1") //todo
                    objFinded = igtbl_getGridById("ctl00_Principal_grid_Detalle").find(regExp);
                else if (typeFind == "0") //articulo
                    objFinded = igtbl_getGridById("ctl00_Principal_grid_Detalle").Bands[0].Columns[0].find(regExp);
                else if (typeFind == "1") //descripción
                    objFinded = igtbl_getGridById("ctl00_Principal_grid_Detalle").Bands[0].Columns[2].find(regExp);
                else if (typeFind == "R") //resto
                {
                    for (numCol; numCol < 21; numCol++) {
                        objFinded = igtbl_getGridById("ctl00_Principal_grid_Detalle").Bands[0].Columns[numCol].find(regExp);
                        if (objFinded != null) {
                            break;
                        }
                    }
                }

                if (objFinded == null) {
                    objFinded = undefined;
                    alert("Palabra no encontrada.");
                    numCol = 3;
                    return;
                }

                objFinded.scrollToView();
                window.document.getElementById(objFinded.Id).style.backgroundColor = "Yellow";
            }
            else {
                if (typeFind == "-1") //todo
                    objFinded = igtbl_getGridById("ctl00_Principal_grid_Detalle").findNext();
                else if (typeFind == "0") //articulo
                    objFinded = igtbl_getGridById("ctl00_Principal_grid_Detalle").Bands[0].Columns[0].findNext();
                else if (typeFind == "1") //descripción
                    objFinded = igtbl_getGridById("ctl00_Principal_grid_Detalle").Bands[0].Columns[2].findNext();
                else if (typeFind == "R") //resto
                {
                    for (numCol; numCol < 21; numCol++) {
                        objFinded = igtbl_getGridById("ctl00_Principal_grid_Detalle").Bands[0].Columns[numCol].findNext();
                        if (objFinded != null) {
                            break;
                        }
                    }
                }

                if (objFinded == null) {
                    objFinded = undefined;
                    alert("No hay mas resultados.");
                    numCol = 3;
                    return;
                }
                objFinded.scrollToView();
                window.document.getElementById(objFinded.Id).style.backgroundColor = "Yellow";
            }
        }

        function validar(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 13) {
                btnBuscarItem_onclick();
                return false;
            }
        }
        
        //  window.onload = Pausa();

        //    function Pausa() {
        //        alert("hola");
        //     showModelessDialog('PaginaEspera.aspx','','help:0;resizable:0;status:0;dialogWidth:300px;dialogHeight:150px;edge: Raised;unadorned:1 ');    
        // }

        function on() {
            //window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").children[2].children[0].children[2].innerText
            //window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[1].cells[0].children[0].innerHTML
            //3, 4, 5, 12, 15
            var int_reg = 1;
            //debugger;
            for (int_reg = 1; int_reg < window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows.length; int_reg++) {
                arr_cds[int_reg] = new Array();

                arr_cds[int_reg][0] = window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[0].children[0].innerHTML;
                //window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[1].cells[3].children[0].children[0].rows[0].cells[0].children[0].innerHTML
                arr_cds[int_reg][1] = window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[1].children[0].innerHTML;
                arr_cds[int_reg][2] = window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[2].children[0].innerHTML;
                arr_cds[int_reg][3] = window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[10].children[0].innerHTML;
                arr_cds[int_reg][4] = window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[13].children[0].innerHTML;
                arr_cds[int_reg][5] = window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[8].children[0].innerHTML;
            }
            //        for (int_reg = 0; int_reg < window.document.getElementById("ctl00$Principal$grid_Detalle_pager").children.length; int_reg++) 
            //        {
            //            window.document.getElementById("ctl00$Principal$grid_Detalle_pager").children[int_reg].onclick = function(evt) {
            //                bit_Pantalla = true;



            //                var int_reg = 0;
            //                for (int_reg = 0; int_reg < window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").children[2].children.length; int_reg++) {

            //                    if ((arr_cds[int_reg][0] != window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[0].children[0].innerHTML) ||
            //                         (arr_cds[int_reg][1] != window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[1].children[0].innerHTML) ||
            //                         (arr_cds[int_reg][2] != window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[2].children[0].innerHTML) ||
            //                         (arr_cds[int_reg][3] != window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[9].children[0].innerHTML) ||
            //                         (arr_cds[int_reg][4] != window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[12].children[0].innerHTML)
            //                ) {
            //                        window.document.getElementById("ctl00_Principal_txtCambio").value = "1";
            //                        break;
            //                    }
            //                }



            //                if (typeof evt == 'undefined')
            //                    evt = window.event;
            //                igtbl_pageGrid(evt, 'ctl00xPrincipalxgridxDetalle', evt.srcElement.outerText)
            //            };
            //        }
            return true;
        }

       

        on();

        window.onbeforeunload = function(evt) {
            var message = 'Sus cambios no han sido guardados ¿Desea salir?\n';
            if (typeof evt == 'undefined')
                evt = window.event;

            //        alert(window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").children[2].children[0].children[3].children[0].innerHTML);

            var int_reg = 1;
            for (int_reg = 1; int_reg < window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows.length; int_reg++) {

                if ((arr_cds[int_reg][0] != window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[0].children[0].innerHTML) ||
                (arr_cds[int_reg][1] != window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[1].children[0].innerHTML) ||
                (arr_cds[int_reg][2] != window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[2].children[0].innerHTML) ||
                (arr_cds[int_reg][3] != window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[10].children[0].innerHTML) ||
                (arr_cds[int_reg][4] != window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[13].children[0].innerHTML ||
                (arr_cds[int_reg][5] != window.document.getElementById("G_ctl00xPrincipalxgridxDetalle").rows[int_reg].cells[3].children[0].children[0].rows[0].cells[8].children[0].innerHTML))
                ) {
                    window.document.getElementById("ctl00_Principal_txtCambio").value = "1";
                    if (bit_Pantalla == true) {
                        bit_Pantalla = false;
                        return;
                    }

                    if (evt)
                        evt.returnValue = message;
                    return message;
                }
            }

            if (bit_Pantalla == true) {
                bit_Pantalla = false;
                return;
            }

            if (window.document.getElementById("ctl00_Principal_txtCambio").value == "1") {
                if (evt)
                    evt.returnValue = message;
                return message;
            }
            return;
        }

        window.onresize = function(evt) {
            var altura = document.documentElement.clientHeight - 370;
            //alert("resize");
            if (window.document.getElementById("ctl00xPrincipalxgridxDetalle_main") != undefined && altura > 50)
                window.document.getElementById("ctl00xPrincipalxgridxDetalle_main").style.height = altura + "px";
        }

        var altura = document.documentElement.clientHeight - 370;
        if (window.document.getElementById("ctl00xPrincipalxgridxDetalle_main") != undefined && altura > 50)
            window.document.getElementById("ctl00xPrincipalxgridxDetalle_main").style.height = altura + "px";

        // Ejecutar el metodo manejador del evento click del boton 'btMostrarOcultarDetPedidosPendientes' del lado del cliente.
        btMostrarOcultarDetPedidosPendientes_onclick();
    
    </script>
    
    <script language="javascript" src="../FuncionesJS/wz_tooltip.js" type="text/javascript">
    
    </script>
    
</asp:Content>