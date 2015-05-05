<%@ Page Title="Relación cliente/proveedor" Language="C#" MasterPageFile="~/PedidoSugeridoLamosa.Master" AutoEventWireup="true" 
            CodeBehind="RelacionCP.aspx.cs" Inherits="PedidoSugeridoLamosa.Catalogos.RelacionCP" %>

<%@ Register Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>

<%@ Register Assembly="Infragistics35.Web.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
    
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
    
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.ExcelExport.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
    
<%@ Register Assembly="Infragistics35.WebUI.Shared.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Shared" TagPrefix="ish" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Principal" runat="server">

    <script src="../FuncionesJS/jquery-1.4.2.js" type="text/javascript">

    </script>
    
    <script src="../FuncionesJS/htmlTabControl.js" type="text/javascript">
    
    </script>
    
    <div id="divMain" style="width:100%; height:100%; vertical-align:top;">
        <asp:ScriptManager ID="sm" runat="server">
            <Scripts>
                <asp:ScriptReference Path= "CallWebServiceMethodsCat.js" />
            </Scripts>
            <Services>
                <asp:ServiceReference Path="../Usuario/WebService.asmx" />
            </Services>
        </asp:ScriptManager>
        
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%" >
            <tr><td style="height:10px"></td></tr>
            <tr>
                <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                    <tr>
                        <td style="width:10px; background-color:#eee;">
                        
                        </td>
                        <td class ="lblTitulo1" style="width:100%" onmouseover="this.T_DESCPANTALLA=true; this.T_BGIMG='../Imagenes/help2.gif'; this.T_HEIGHT=200; this.T_WIDTH=400; this.T_ABOVE=false; this.T_CLICKCLOSE=true; this.T_STICKY=true; return escape('<b>Relación Artículo Cliente-Proveedor </b><p></p>Esta pantalla se utiliza para crear una relación entre el código que usted maneja y el que maneja su proveedor para un mismo Artículo. Posteriormente usted puede Seleccionar el Artículo si desea que participe dentro de la simulación. También puede agregar nuevos artículos a la lista o Buscar un artículo por su Descripción. Si usted no maneja sistema de codificación, puede utilizar el botón “Copiar Ids” para asignar el mismo Código de su proveedor como código suyo. Al fiinalizar presionar el botón “Aceptar” para guardar los cambios.');">
                              <asp:Label ID="lblTitulo" runat="server" Text="Relación artículo cliente/proveedor" >
                              </asp:Label>
                              <br/>
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
                <td class="textos_01" style="width:100%">
                    <table border="0px" cellpadding="0px" cellspacing="0px" style="width:100%">
                        <tr style="height:35px;">
                            <td style="width:80px; text-align:left; vertical-align:middle;">
                                Descripción
                            </td>
                            <td style="width:420px; text-align:left; vertical-align:middle;">
                                <asp:TextBox ID="txtDesc" runat="server" style="text-transform: uppercase" value="" Width="100%" AutoPostBack="false">
                                </asp:TextBox>                                      
                            </td>
                            <td style="width:50px;">
                            
                            </td>
                            <td style="width:120px; text-align:left; vertical-align:middle;">
                                <input type="button" class="Boton_01" style="width:100px" onclick="window.document.getElementById('<%=btnBuscar.ClientID%>').click()" value="Buscar" OnClientClick="return btnCargar_onclick();" />
                            </td>
                            <td style="width:120px; text-align:left; vertical-align:middle;">
                                <input type="button" id="id_copy" class="Boton_01" style="width:100px" onclick="cambio()" value="Copiar Ids" />
                            </td>
                            <td style="width:120px; text-align:right; vertical-align:middle;">
                                <asp:Button ID="btnAceptar" runat="server" class="Boton_01" onclick="btnAceptar_Click" style="width: 100px;" Text="Aceptar" OnClientClick="return btnAceptar_onclick();" />
                            </td>
                            <td>
                             
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="textos_01" style="width:100%">
                    <table border="0px" cellpadding="0px" cellspacing="0px" style="width:100%">
                        <tr style="height:35px;">
                            <td style="width:250px; text-align:left; vertical-align:middle;">
                                <asp:CheckBox ID="chekCopyall" runat="server" onclick="selectAll(this)" EnableViewState="true" />
                                Seleccionar todos los art&iacute;culos
                            </td>
                            <td style="width:250px; text-align:right; vertical-align:middle;">
                                <a href="javascript:getPlantilla();" class="Boton_01" >Descargar Plantilla de Carga Masiva</a>
                            </td>
                            <td style="width:50px;">
                            
                            </td>
                            <td style="width:360px; text-align:center; vertical-align:middle;">
                                <asp:FileUpload ID="File_Cargar" CssClass="Boton_01" runat="server" title="Examinar" Width="100%"/> 
                            </td>
                            <td style="width:120px; text-align:right; vertical-align:middle;">
                                <asp:Button ID="btnCargar" runat="server" class="Boton_01" onclick="btnCargar_Click" style="width: 100px;" Text="Cargar" OnClientClick="return btnCargar_onclick();"/>
                            </td>
                            <td>
                             
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>           
                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%; height:100%;">
                        <tbody>
                            <tr>
                                <td>
                                    <script src="../FuncionesJS/jquery-1.4.2.js" type="text/javascript">
                                    
                                    </script>
                                    
                                    <script type="text/javascript">

                                        var beforeClose = true;
                                        var clickcancel = true;
                                        var band;
                                        var checkingAll = false;
                                        var checkingONE = false;
                                        var testcheck = false;


                                        function ok(event) {
                                            var art_generico = $("#id_art_generico").val();
                                            var art_cliente = $("#id_art_cliente").val();
                                            var desc = $("#id_desc").val();
                                            var oart_generico = $("#ogenerico").val();
                                            if (desc == " ") {
                                                alert("entro");
                                                desc = "";
                                            }
                                            if (confirm("Deseas guardar cambios")) {
                                                var clienteInSession = $("#<%=hddClienteInSession.ClientID%>").val();
                                                if (art_generico != oart_generico) {
                                                    //  alert("guardar");
                                                    InsertArtCliente(art_generico, art_cliente, desc, clienteInSession);

                                                }
                                                else {
                                                    // alert("updatear aspx " + art_cliente + "," + art_generico + "," + desc);
                                                    UpdateArtCliente(art_generico, art_cliente, clienteInSession);
                                                }
                                            }
                                            // $("#<%=BtnRefresh.ClientID%>").click();
                                        }

                                        function relaodGrid() {
                                            $("#<%=BtnRefresh.ClientID%>").click();
                                        }

                                        function cancel(event) {
                                            var art_generico = $("#id_art_generico").val();
                                            var art_cliente = $("#id_art_cliente").val();
                                            var desc = $("#id_desc").val();

                                            var oart_generico = $("#ogenerico").val();
                                            var oart_cliente = $("#oart_cliente").val();
                                            var odesc = $("#odesc").val();

                                            var edit = true;
                                            beforeClose = false;
                                            if (art_generico != oart_generico || art_cliente != oart_cliente || desc != odesc) {
                                                if (!confirm('¿Está seguro de cerrar pantalla sin hacer cambios?')) {
                                                }
                                            }
                                            if (edit) {
                                                clickcancel = false;
                                                igtbl_gRowEditButtonClick(event);
                                                clickcancel = true;
                                                beforeClose = true;
                                            }
                                        }



                                        function eliminar(event) {
                                            var art = $("#art1").val();
                                            var porce = $("#porcela").val();
                                            var desc = $("#desc").val();

                                            if (confirm('¿Desea eliminar registro?')) {
                                                clickcancel = false;
                                                igtbl_gRowEditButtonClick(event);
                                                clickcancel = false;
                                                beforeClose = false;
                                            }
                                            else {
                                                clickcancel = false;
                                                igtbl_gRowEditButtonClick(event);
                                                clickcancel = true;
                                                beforeClose = false;
                                            }
                                        }

                                        function BeforeRowTemplateCloseHandler(event) {
                                            if (clickcancel) {
                                                $("#btnCancelar").click();
                                            }
                                            return beforeClose;
                                            if (clickok) {
                                                $("#btnGuardar").click();
                                            }
                                            return beforeClose;
                                            if (clickeliminar) {
                                                $("#btnEliminar").click();
                                            }
                                            return beforeClose;
                                        }

                                        function selectAll(obj) {

                                            if (checkingONE == true) {

                                                if (obj.checked == false) {
                                                    testcheck = true;
                                                }
                                                return;
                                            }
                                            if (obj.checked == false) {
                                                document.getElementById('<%=txtCheckAllVerify.ClientID%>').value = '1';

                                            }
                                            else {
                                                document.getElementById('<%=txtCheckAllVerify.ClientID%>').value = '';
                                                alert("Solo se tomaran en cuenta los artículos que tengan capturado Artículo cliente.");
                                            }
                                            testcheck = true;
                                            document.getElementById('<%=hddClaveArtCliente.ClientID%>').value = $("#<%=hddClavesAllCliente.ClientID%>").val();
                                            checkingAll = true;
                                            var check = obj.checked;
                                            if (check == false) {
                                                document.getElementById('<%=hddClaveArtCliente.ClientID%>').value = '';

                                            }
                                            var x = document.getElementsByTagName("input");
                                            for (var i = 0; i < x.length; i++) {
                                                if (x[i].type == "checkbox" && x[i].id != obj.id) {
                                                    x[i].checked = check;
                                                }
                                            }
                                            checkingAll = false;
                                            //alert(document.getElementById('<%=txtCheckAllVerify.ClientID%>').value);

                                        }

                                        function cambio() {
                                            if ($("#<%=hddClaveArtCliente.ClientID%>").val().split(",").length == 0) {
                                                alert("Selecciona al menos un artículo a copiar su ID.");
                                                return;
                                            }
                                            if (confirm("Se copiarán las claves de Revestimientos a la clave del cliente de los artículos seleccionados. ¿Está seguro de esta acción?. (Esto no se verá afectado hasta dar click en 'Aceptar')")) {
                                                window.document.getElementById("<%=btnCopiarIds.ClientID%>").click();
                                            }
                                        }

                                        function btnAceptar_onclick() {

                                            if ($("#<%=hddClaveArtCliente.ClientID%>").val().split(",").length == 0) {
                                                alert("Selecciona al menos un artículo guardar.");
                                                return false;
                                            }
                                            else {
                                                window.document.getElementById("tblCargando").style.visibility = "visible";
                                                window.document.getElementById("tblCargando").style.display = "block";
                                                window.document.getElementById("divMain").style.display = "none";
                                            }
                                        }
                                        
                                        function btnCargar_onclick() {
                                            window.document.getElementById("tblCargando").style.visibility = "visible";
                                            window.document.getElementById("tblCargando").style.display = "block";
                                            window.document.getElementById("divMain").style.display = "none";
                                        }
                                        
                                        function AfterCellUpdate(tableName, itemName) {
                                            if (checkingAll == true)
                                                return;
                                            checkingONE = true;
                                            var cell = igtbl_getCellById(itemName);
                                            var colNo = cell.Column.Id.substring(cell.Column.Id.lastIndexOf('_') + 1);

                                            //                                                alert(cell.Column.Key);
                                            if (cell.Column.Key == 'Bit_Activo') {
                                                var row = igtbl_getRowById(itemName);
                                                var newCell = row.getCell(2);
                                                var codes = document.getElementById('<%=hddClaveArtCliente.ClientID %>'); //Hidden que va a contener todos los codigos que se seleccionen
                                                codes.value = validate(codes.value, newCell.getValue());
                                                //document.getElementById('showCodes').innerHTML = codes.value;
                                                //                                         chekCopyall
                                                //alert(cell.Column.Key + " , " + colNo + " , " + newCell.getValue() + "," + row.getCell(0).getValue());
                                                if (row.getCell(0).getValue() == 0 || row.getCell(0).getValue() == "false") {
                                                    //alert(document.getElementById('<%=txtCheckAllVerify.ClientID%>').value);
                                                    if (document.getElementById('<%=txtCheckAllVerify.ClientID%>').value != '1' && testcheck == true)
                                                        document.getElementById('<%=txtCheckAllVerify.ClientID%>').value = '2';
                                                    document.getElementById('<%=chekCopyall.ClientID%>').checked = false;
                                                }
                                                //alert($("#<%=hddClaveArtCliente.ClientID%>").val().split(",").length);
                                                //alert($("#<%=hddClavesAllCliente.ClientID%>").val().split(",").length);

                                                if ($("#<%=hddClaveArtCliente.ClientID%>").val().split(",").length == $("#<%=hddClavesAllCliente.ClientID%>").val().split(",").length) {
                                                    //darle check al todos....
                                                    document.getElementById('<%=chekCopyall.ClientID%>').checked = true;
                                                }
                                                checkingONE = false;
                                                //
                                            }
                                            function validate(savedCodes, code) {

                                                var codes = savedCodes ? savedCodes.split(',') : '';
                                                var newCodes = '';
                                                for (var x = 0; x < codes.length; x++) {
                                                    if (code == codes[x]) {//Si el Checkbox es encontrado significa que lo deselecciono
                                                        for (var j = 0; j < codes.length; j++) {
                                                            if (code != codes[j]) {
                                                                //Almacenamos toda la informacion de la lista menos el que encontramos
                                                                newCodes = newCodes + (newCodes ? "," : "") + codes[j];
                                                            }
                                                            else {
                                                                //alert("code:"  + code);
                                                                window.document.getElementById("<%=hddDes.ClientID%>").value += code + ",";
                                                            }
                                                        }
                                                        return newCodes;
                                                    }

                                                }
                                                //Si llega hasta este punto significa que no se ha encontrado el valor en la lista y entonces lo agregamos
                                                newCodes = savedCodes + (savedCodes ? "," : "") + code;
                                                return newCodes;
                                            }
                                        }
                                        </script>
                                     
                                    <table>
                                            <tr>
                                                <td>
                                                    <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" 
                                                        EnableAppStyling="False" Height="460px" Width="100%" 
                                                        onpageindexchanged="UltraWebGrid1_PageIndexChanged" 
                                                        onsortcolumn="UltraWebGrid1_SortColumn">
                                                        <bands>
                                                            <igtbl:UltraGridBand>
                                                                <Columns>
                                                                </Columns>
                                                                <addnewrow view="NotSet" visible="NotSet">
                                                                </addnewrow>
                                                            </igtbl:UltraGridBand>
                                                        </bands>
                                                        <displaylayout allowaddnewdefault="Yes" 
                                                            allowdeletedefault="Yes" allowsortingdefault="Yes" 
                                                            allowupdatedefault="RowTemplateOnly" cellclickactiondefault= "RowSelect" 
                                                            cellpaddingdefault="1"  loadondemand="Xml" HeaderClickActionDefault= "SortSingle"
                                                            name="UltraWebGrid1" nodatamessage="" rowheightdefault="20px" 
                                                            rowselectorsdefault="No" scrollbarview="Vertical" 
                                                            selecttyperowdefault="Single" StationaryMargins="Header" version="3.00">
                                                            <rowalternatestyledefault backcolor="#E5E5E5">
                                                            </rowalternatestyledefault>
                                                            <pager allowpaging="true" pagesize="200">
                                                                <PagerStyle BackColor="#666666" BorderColor="Black" BorderStyle="Solid" 
                                                                    BorderWidth="1px" Font-Names="Arial" Font-Size="11px" ForeColor="White" 
                                                                    Height="20px" />
                                                            </pager>
                                                            <editcellstyledefault backcolor="silver">
                                                            </editcellstyledefault>
                                                            <footerstyledefault backcolor="LightGray" borderstyle="Solid" borderwidth="1px">
                                                            </footerstyledefault>
                                                            <headerstyledefault backcolor="#666666" bordercolor="Black" borderstyle="Solid" 
                                                                font-bold="True" forecolor="White">
                                                            </headerstyledefault>
                                                            <rowselectorstyledefault borderstyle="Solid">
                                                            </rowselectorstyledefault>
                                                            <rowstyledefault backcolor="White" bordercolor="Black" borderstyle="Solid" 
                                                                borderwidth="1px" font-names="Verdana" font-size="8pt" forecolor="Black">
                                                            </rowstyledefault>
                                                            <selectedrowstyledefault backcolor="#FFFFB3" font-bold="True" forecolor="Black">
                                                            </selectedrowstyledefault>
                                                            <addnewbox hidden="true">
                                                            </addnewbox>
                                                            <activationobject bordercolor="Black" borderstyle="Dotted" borderwidth="">
                                                            </activationobject>
                                                            <addnewrowdefault view="Top" visible="No">
                                                            </addnewrowdefault>
                                                            <filteroptionsdefault>
                                                                <filterdropdownstyle backcolor="White" bordercolor="Silver" borderstyle="Solid" 
                                                                    borderwidth="1px" customrules="overflow:auto;" 
                                                                    font-names="Verdana,Arial,Helvetica,sans-serif" font-size="11px" width="200px">
                                                                </filterdropdownstyle>
                                                                <filterhighlightrowstyle backcolor="#999999" forecolor="White">
                                                                </filterhighlightrowstyle>
                                                            </filteroptionsdefault>
                                                            <framestyle height="460px" Width="100%">
                                                            </framestyle>
                                                            <clientsideevents AfterCellUpdateHandler="AfterCellUpdate" BeforeRowTemplateOpenHandler="BeforeRowTemplateOpen" CellClickHandler="CellClick" BeforeSortColumnHandler="BeforeSortColumn" />
                                                        </displaylayout>
                                                    </igtbl:UltraWebGrid>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                    </table>
                    <script type="text/javascript">

                        function onMouseOut(listbar, object, e) {
                        }
                        function onMouseOver(listbar, object, e) {
                            if (object == listbar.Groups[0].Items[0])
                                ig.getElementById(listbar.Id).style.filter = "Shadow(Color=#999999,Direction=45);";
                            else if (object == listbar.Groups[0].Items[1])
                                ig.getElementById(listbar.Id).style.filter = "Shadow(Color=#3366cc,Direction=45);";
                            else if (object == listbar.Groups[0].Items[2])
                                ig.getElementById(listbar.Id).style.filter = "Shadow(Color=#99cc99,Direction=45);";
                            else if (object == listbar.Groups[0].Items[3])
                                ig.getElementById(listbar.Id).style.filter = "Shadow(Color=#ccff66,Direction=45);";
                            else if (object == listbar.Groups[1].Items[0])
                                ig.getElementById(listbar.Id).style.filter = "Shadow(Color=#ff6666,Direction=45);";
                            else if (object == listbar.Groups[1].Items[1])
                                ig.getElementById(listbar.Id).style.filter = "Shadow(Color=#cc9966,Direction=45);";

                        }
                        function ListItemSelected(idList, varList) {
                            //var nuevo = document.getElementById("<%=hddSecurityConstants.ClientID %>").getAttribute('value').indexOf('1');
                            //   var exporta = document.getElementById("<%=hddSecurityConstants.ClientID %>").getAttribute('value').indexOf('6');

                            if (idList == 1) {//Nuevo
                                igtbl_addNew("<%=UltraWebGrid1.ClientID%>", 0).editRow();

                            } else if (idList == 2) {//Exportar
                                $find('<%=WebDialogWindow1.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                            }
                        }

                        function BeforeSortColumn(gridName, columnId) {
                            window.document.getElementById("<%=hddIDSortColumn.ClientID%>").value = columnId.split("_")[columnId.split("_").length - 1];
                            window.document.getElementById("<%=btnSort.ClientID%>").click();
                            //alert(columnId.split("_")[columnId.split("_").length - 1]);
                            return true;
                        }

                        function BeforeRowTemplateOpen(gridName, rowId) {


                            var row = igtbl_getRowById(rowId);
                            var edit = window.document.getElementById("id_desc");
                            var gene = window.document.getElementById("id_art_generico");
                            // alert(row.getCell(0).getValue());

                            if (row.getCell(1).getValue() == null && row.getCell(2).getValue() == null) {
                                //   alert("hablita");
                                edit.disabled = false;
                                gene.disabled = false;
                            }
                            else {
                                if (row.getCell(1).getValue() == null && row.getCell(2).getValue() != null) {
                                    // alert("deshabilita");
                                    edit.disabled = true;
                                    gene.disabled = true;
                                }
                            }
                        }

                        function CellClick() {
                            // alert("Click");
                        }

                        function getPlantilla() {
                            window.document.getElementById('<%=btnGetPlantilla.ClientID%>').click();
                        }                    
                    </script>
                </td>
            </tr>
        </table>
        <ig:WebDialogWindow ID="WebDialogWindow1" runat="server" InitialLocation="Centered"
                        Height="100px" Width="400px" Modal="true" WindowState="Hidden" Font-Size="10px">
                        <ContentPane BackColor="#FAFAFA">
                            <Template>
                                <div style="padding: 5px;">
                                    <table cellpadding="0" cellspacing="0" align="center" style="text-align: center; width: 100%">
                                        <tr>
                                            <td align="center" colspan="3">
                                                <asp:Panel ID="pnlExporta" runat="server" Width="300px">
                                                    <asp:DropDownList ID="ddlSeleccion" runat="server" CssClass="AreaBox_02" Width="180px">
                                                        <asp:ListItem>Documento Portable (PDF)</asp:ListItem>
                                                        <asp:ListItem>MS Word (DOC)</asp:ListItem>
                                                        <asp:ListItem>MS Excel (XLS)</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btnExporta" runat="server" CssClass="Boton_01"  Text="Exportar" Width="115px" />
                                                </asp:Panel>
                                                &nbsp; &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </Template>
                        </ContentPane>
                    </ig:WebDialogWindow>
                    
        <div style="visibility:hidden; display:none;">
            <asp:Button ID="btnGetPlantilla" runat="server" Text="Plantilla" 
        onclick="btnGetPlantilla_Click"/>
    <asp:Button ID="btn_Log" runat="server" Text="Log" 
        onclick="btn_Log_Click"/>
            <asp:Button ID="BtnRefresh" CssClass="" runat="server" Text="Refresh" 
            onclick="BtnRefresh_Click" />
            <asp:HiddenField ID="hddIDSortColumn" runat="server" />
            <asp:Button ID="btnSort" runat="server" CssClass="Boton_01" 
         Text="Sort" onclick="btnSort_Click" />
        <asp:Button ID="btnBuscar" runat="server" CssClass="Boton_01" 
        OnClick="btnBuscar_Click" Text="Buscar" />
                <asp:TextBox ID="txtCheckAllVerify" runat="server"></asp:TextBox>
                <asp:Button ID="btnCopiarIds" runat="server" Text="btnCopiarIds" onclick="btnCopiarIds_Click" />
                <asp:HiddenField ID="hddChanceID" runat="server" />
        <asp:HiddenField ID="HddUser" runat="server" />
        <asp:HiddenField ID="Sucursalhdd" runat="server" />
        <asp:UpdatePanel runat="server" ID="UpdatePanel2" ChildrenAsTriggers="True" UpdateMode="Always" RenderMode="Inline">
            <ContentTemplate>
                <asp:HiddenField ID="hddBanderaBusq" runat="server" />
                <asp:HiddenField ID="hddDes" runat="server" />
                <asp:HiddenField ID="hddClavesAllCliente" runat="server" />
                <asp:HiddenField ID="hddClaveArtCliente" runat="server" />
                <asp:HiddenField ID="hddSecurityConstants" runat="server" />
                <asp:HiddenField ID="hddClienteInSession" runat="server" />
                
            </ContentTemplate>
        </asp:UpdatePanel>

        </div>
    </div>
    
    <iframe id="DownloadFileIFrame" height="0px" width="0px">
    
    </iframe>
    
    
    <asp:Literal ID="id_mesaje" runat="server">
    
    </asp:Literal>
    
    <table id="tblCargando" style="visibility:hidden;display:none; width:100%; height:100%">
        <tr style="width:100%; height:100%">
            <td align="center" valign="middle" style="width:100%; height:100%">
                <img src="../Imagenes/cargando3.gif" alt="cargando" />
                <span style="width:100%; height:100%"></span>
            </td>
        </tr>
    </table>

    <script language="javascript" src="../FuncionesJS/wz_tooltip.js" type="text/javascript">
    
    </script>
    
    <script type="text/javascript">

        window.onload = window_onload;

        function window_onload() {
            window.document.getElementById("ctl00xPrincipalxUltraWebGrid1_main").style.height = (document.body.clientHeight - 385) + "px";
        }

        function txtDesc_onkeypress() {
            if (window.event.keyCode == 13) {

                window.document.getElementById('<%=btnBuscar.ClientID%>').click();
                return false;
            }
        }
    </script>
    
</asp:Content>
