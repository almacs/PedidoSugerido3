<%@ Page Title="Alta usuario" Language="C#" MasterPageFile="~/PedidoSugeridoLamosa.Master" AutoEventWireup="true" 
                            CodeBehind="AltaUsr.aspx.cs" Inherits="PedidoSugeridoLamosa.Usuario.AltaUsr" %>
                            
<%@ Register Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>

<%@ Register Assembly="Infragistics35.Web.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
    
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
    
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.ExcelExport.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
    
<%@ Register Assembly="Infragistics35.WebUI.Shared.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Shared" TagPrefix="ish" %>
                                
<asp:Content ID="Content1" ContentPlaceHolderID="Principal" runat="server">

    <script src="../FuncionesJS/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="../FuncionesJS/htmlTabControl.js" type="text/javascript"></script>
    <script type="text/javascript">


        var beforeClose = true;
        var clickcancel = true;
        var comboLoaded = false;

        function ok(event) {

            var usr = $("#id_usr").val();
            var pass = $("#text_pass").val();
            var nom = $("#text_nombre").val();
            var apPaterno = $("#text_paterno").val();
            var apMaterno = $("#text_materno").val();
            var mail = $("#text_mail").val();
            var tipo = $("#text_tipo").val();
            var clienteCia = document.getElementsByName("ctl00$Principal$UltraWebGrid1$ctl00$txtIdcliente")[0].value;
            //var cliente = clienteCia.split('-')[0];
            //var cia = clienteCia.split('-')[1];
            var cliente = clienteCia;
            var cia = "";
            var dir_entrega = $("#ctl00_Principal_UltraWebGrid1_ctl00_ddlEntrega").val();
            var estatus = $("#text_estatus").val();
            var ousr = $("#ousr").val();

            var opass = $("#opass").val();
            var onom = $("#onombre").val();
            var oapPaterno = $("#opaterno").val();
            var oapMaterno = $("#omaterno").val();
            var omail = $("#omail").val();
            var otipo = $("#otipo").val();
            var ocliente = $("#ocliente").val();
            var odir_entrega = $("#oentrega").val();
            var oestatus = $("#oStatus").val();


            if (estatus == "") {
                estatus = 1;
            }
            if (tipo == "") {
                tipo = 3;
            }

            //Para cuando sea Amdmin o asesor
            if (tipo == 4) {
                dir_entrega = "0";
                cliente = "0";
                cia = "0";
            }

            //Para cuando venga vacio algun campo.
            if (usr == "" || pass == "" || nom == "" || apMaterno == "" || apMaterno == "" || mail == "" || cliente == "" || dir_entrega == "" || estatus == "") {
                alert("No es posible dar de alta al usuario, faltan campos que son requeridos");
                return false;
            }

            var re = /^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,3})$/
            if (!re.exec(mail)) {
                alert("Sintáxis de dirección incorrecta. Ejem: usuario@empresa.com");
                return false;
            }

            var er_cp = /^(?=(?:.*?\d){1})(?=(?:.*?[A-Za-z]){2})\w{8,}$/     //3 numeros o cadena vacia /^(?=.*\d{2})(?=.*[a-zA-Z]{2}).{8,}$/
            if (!er_cp.test(pass)) {
                alert('Se requieren como minimo 8 caracteres , dos letras y al menos un número para la contraseña.')
                return false
            }

            if (dir_entrega == "") {
                dir_entrega = window.document.getElementById("cmbEntrega").value;
            }

            if (confirm("Deseas guardar cambios")) {
                if (usr != ousr) {
                    InsertUsuario(usr, pass, nom, apPaterno, apMaterno, mail, tipo, cliente, dir_entrega, estatus, cia);

                }
                else if (usr == ousr && (pass != opass || nom != onom || apPaterno != oapPaterno || apMaterno != oapMaterno || mail != omail || tipo != otipo || cliente != ocliente || dir_entrega != odir_entrega || estatus != oestatus)) {
                    //alert("updatear");
                    // alert(cliente + " ," + cia);
                    UpdateUsuario(usr, pass, nom, apPaterno, apMaterno, mail, tipo, cliente, dir_entrega, estatus, cia);
                }
            }
            $("#<%=BtnRefresh.ClientID%>").click();
        }


        function relaodGrid() {
            $("#<%=BtnRefresh.ClientID%>").click();
        }

        function cancel(event) {
            var usr = $("#id_usr").val();
            var pass = $("#text_pass").val();
            var nom = $("#text_nombre").val();
            var apPaterno = $("#text_paterno").val();
            var apMaterno = $("#text_materno").val();
            var mail = $("#text_mail").val();
            var tipo = $("#text_tipo").val();
            var cliente = $("#ctl00_Principal_UltraWebGrid1_ctl00_txtIdcliente").val();
            var dir_entrega = $("#ctl00_Principal_UltraWebGrid1_ctl00_ddlEntrega").val();
            var estatus = $("#text_estatus").val();

            var ousr = $("#ousr").val();
            var opass = $("#opass").val();
            var onom = $("#onombre").val();
            var oapPaterno = $("#opaterno").val();
            var oapMaterno = $("#omaterno").val();
            var omail = $("#omail").val();
            var otipo = $("#otipo").val();
            var ocliente = $("#ocliente").val();
            var odir_entrega = $("#oentrega").val();
            var oestatus = $("#oStatus").val();

            var edit = true;
            beforeClose = false;
            if (usr != ousr || pass != opass || nom != onom || apPaterno != oapPaterno || apMaterno != oapMaterno || mail != omail || tipo != otipo || estatus != oestatus || dir_entrega != odir_entrega || cliente != ocliente) {
                if (!confirm('¿Está seguro de cerrar pantalla sin hacer cambios?')) {
                    false;
                }
            }
            if (edit) {
                clickcancel = false;
                igtbl_gRowEditButtonClick(event);
                clickcancel = true;
                beforeClose = true;
            }
        }

        function del(event) {
            if (confirm('¿Está seguro de eliminar el usuario?')) {
                var usr = $("#id_usr").val();
                var pass = $("#text_pass").val();
                var nom = $("#text_nombre").val();
                var apPaterno = $("#text_paterno").val();
                var apMaterno = $("#text_materno").val();
                var mail = $("#text_mail").val();
                var tipo = $("#text_tipo").val();
                var clienteCia = document.getElementsByName("ctl00$Principal$UltraWebGrid1$ctl00$txtIdcliente")[0].value;
                //var cliente = clienteCia.split('-')[0];
                //var cia = clienteCia.split('-')[1];
                var cliente = clienteCia;
                var cia = "";

                var dir_entrega = $("#ctl00_Principal_UltraWebGrid1_ctl00_ddlEntrega").val();
                var estatus = 3//$("#text_estatus").val();
                var ousr = $("#ousr").val();

                var opass = $("#opass").val();
                var onom = $("#onombre").val();
                var oapPaterno = $("#opaterno").val();
                var oapMaterno = $("#omaterno").val();
                var omail = $("#omail").val();
                var otipo = $("#otipo").val();
                var ocliente = $("#ocliente").val();
                var odir_entrega = $("#oentrega").val();
                var oestatus = $("#oStatus").val();

                UpdateUsuario(usr, pass, nom, apPaterno, apMaterno, mail, tipo, cliente, dir_entrega, estatus, cia);
                relaodGrid();
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

        function Change(ComboValue, selected_value) {
            document.getElementsByName("ctl00$Principal$UltraWebGrid1$ctl00$txtIdcliente")[0].value = ComboValue;
            if (comboLoaded == false)
                return;
            $("#cmbEntrega").html("");
            GetEntrega(ComboValue, selected_value);
        }

        function AfterRowTemplateOpen(event) {
            Change($("#ctl00_Principal_UltraWebGrid1_ctl00_txtIdcliente").val(), $("#oentrega").val());
            if (($("#text_tipo").val()) == 4) {
                window.document.getElementById("trNomEntrega").style.visibility = "hidden";
                window.document.getElementById("trCliente").style.visibility = "hidden";
            }
            else {
                window.document.getElementById("trNomEntrega").style.visibility = "visible";
                window.document.getElementById("trCliente").style.visibility = "visible";
            }
            comboLoaded = true;
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

        function BeforeRowTemplateOpen(gridName, rowId) {
            var row = igtbl_getRowById(rowId);
            var edit = window.document.getElementById("id_usr");
            comboLoaded = false;
            if (row.getCell(0).getValue() == null) {
                edit.disabled = false;
            }
            else {
                edit.disabled = true;
            }
        }


        function onchangedelCombo() {
            if (($("#text_tipo").val()) == 4)//si es administrador, oculta los rows de cliente y NomEntrega
            {
                window.document.getElementById("trNomEntrega").style.visibility = "hidden";
                window.document.getElementById("trCliente").style.visibility = "hidden";
            }
            else {
                window.document.getElementById("trNomEntrega").style.visibility = "visible";
                window.document.getElementById("trCliente").style.visibility = "visible";
            }
            window.document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_txtIdcliente").value = "";
            window.document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_txtClienteDesc").value = "";
            window.document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_txtCompDesc").value = "";
            window.document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_ddlEntrega").value = "";
            window.document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_txtEntrega").value = "";
        }


        function openClientes() {


            window.frames["frameClientes"].location = "about:blank";

            var URL = "vSeleccionCliente.aspx"
            
            window.frames["frameClientes"].location = URL;

            $find('<%=dialogClientes.ClientID%>').set_windowState($IG.DialogWindowState.Normal);

        }

        function btnAceptarDialogCliente_onclick(id_cliente, nom_cliente, desc_comp)
        {
            window.document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_txtIdcliente").value = id_cliente;
            window.document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_txtClienteDesc").value = nom_cliente;
            window.document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_txtCompDesc").value = desc_comp;
            window.document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_ddlEntrega").value = "";
            window.document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_txtEntrega").value = "";
            $find('<%=dialogClientes.ClientID%>').set_windowState($IG.DialogWindowState.Hidden);
            window.frames["frameClientes"].location = "about:blank";
        }


        function openSucursales() {
            window.frames["frameSucursales"].location = "about:blank";
            var id_clienteS = document.getElementsByName("ctl00$Principal$UltraWebGrid1$ctl00$txtIdcliente")[0].value;

            if (id_clienteS == "")
            {
                alert("Favor de Seleccionar el Cliente");
                return;
            }

            var id_cliente = id_clienteS.split('-')[0];
            var id_cia = id_clienteS.split('-')[1];
            var id_entrega = window.document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_ddlEntrega").value;
            var URL = "UsuarioEntrega.aspx";
            URL += "?id_cliente=" + id_clienteS;
            URL += "&ids_entrega=" + id_entrega;
            window.frames["frameSucursales"].location = URL;

            $find('<%=dialogSucursales.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
        }

        function checkIdSucursal(id_sucursal) {
            var int_reg = 0;

            var ids = window.document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_ddlEntrega").value.split(",");

            for (int_reg = 0; int_reg < ids.length - 1; int_reg++) {

                if (id_sucursal == ids[int_reg])
                    return true;
            }
            return false;
        }


        function btnAceptarDialog_onclick() {
            $find('<%=dialogSucursales.ClientID%>').set_windowState($IG.DialogWindowState.Hidden);
            window.frames["frameSucursales"].location = "about:blank";
        }



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
            if (idList == 1) //Nuevo
            {
                window.document.forms[0].reset();
                igtbl_addNew("<%=UltraWebGrid1.ClientID%>", 0).editRow();
            }
            else if (idList == 2)//Exportar
            {
                $find('<%=WebDialogWindow1.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
            }
        }
    
    </script>
    <div> 
        <asp:ScriptManager ID="sm" runat="server">
            <Scripts>
                <asp:ScriptReference Path= "CallWebServiceMethods.js" />
            </Scripts>
            <Services>
                <asp:ServiceReference Path="WebService.asmx" />
            </Services>
        </asp:ScriptManager>  
        
        
                    
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%" >
            <tr>
                <td style="height:10px" colspan= "3"></td>
            </tr>
            <tr style="height:30px;">
                <td style="width:10px; background-color:#eee;"></td>
                <td  colspan= "3"  class ="lblTitulo1" onmouseover="return escape('Permite dar de alta a usuarios y administradores nuevos ligandolos a un cliente, tambien modificar ciertos datos del usuario.');">
                    <asp:Label ID="lblTitulo" runat="server"  Text="Alta de usuarios" ></asp:Label><br/>
                </td> 
                 <td class= "ms-descriptiontextToolTip" align="right">
                    <img id="imgHelp" onmouseover="this.T_PADDING=2; this.T_INFORMATION=true; this.T_BGIMG=''; this.T_BGCOLOR='#FFFFDD'; this.T_HEIGHT=0; this.T_ABOVE=true; this.T_LEFT=true;  return escape('Muestra la descripcion de la pantalla');"
                                            style="cursor: hand" onclick="return help_onclick();" alt="" src="../Imagenes/Information2_.jpg" name="imgHelp" /> 
                </td>
            </tr>
            <tr>
                <td style="height:10px" colspan= "3"></td>
            </tr>
            <tr>
                <td style="width:10px;" rowspan="2"></td>
                <td rowspan="2" valign="top" class="leftarea" style="width:100px">
                    <div id="navcontainer">
                        <ul id="navlist">
                            <li><input class="Boton_01" type="button" ID="LAddNew" value="Nuevo usuario" onclick="javascript:ListItemSelected(1,'')" style="width: 120px" /></li>
                        </ul>
                    </div>
                </td><td style="width:20px;" rowspan="2">&nbsp;</td>
                <td>           
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
                        <asp:Button ID="BtnRefresh" CssClass="hidden" OnClick="UltraWebGrid1_InitializeLayout" runat="server" Text="Boton" />
                        <table>
                            <tbody>
                                <tr>
                                    <td style=" height:40px" class="textos_01">  
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False" OnPageIndexChanged="cambio_pagina" > 
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <Columns>
                                                    </Columns>
                                                    <RowEditTemplate >
                                                    <table style="font-family: Arial; text-align: left">
                                                        <tr>
                                                            <td valign="top">
                                                         <table>
                                                              <tr>
                                                                <td class ="textos_01">
                                                                 * Clave usuario:
                                                                </td >
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="ousr" columnkey="Clave usuario" />
                                                                   <input id="id_usr" maxlength="30" columnkey="Clave usuario" style="width: 150px;" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class ="textos_01">
                                                                  * Contraseña:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="opass" columnkey="Contraseña" />
                                                                     <input id="text_pass" maxlength="30" columnkey="Contraseña" style="width: 150px;" type= "password" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class ="textos_01">
                                                                  * Nombre:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="onombre" columnkey="Nombre" />
                                                                    <input id="text_nombre" maxlength="20" columnkey="Nombre" style="width: 150px;" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class ="textos_01">
                                                                  * Ap. paterno:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="opaterno" columnkey="Ap_paterno" />
                                                                    <input id="text_paterno" maxlength="12" columnkey="Ap_paterno" style="width: 150px;" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class ="textos_01">
                                                                  * Ap. materno:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="omaterno" columnkey="Ap_materno" />
                                                                    <input id="text_materno" maxlength="12" columnkey="Ap_materno" style="width: 150px;" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01">
                                                                  * Correo electr&oacute;nico:
                                                                </td>
                                                                <td class="textos_01">
                                                                    <input type="hidden" id="omail" columnkey="Correo" />
                                                                    <input id="text_mail" maxlength="100" columnkey="Correo" style="width: 150px;" type="text" />
                                                                </td>
                                                            </tr>
                                                           </table>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                  <tr>
                                                                <td class ="textos_01">
                                                                  * Tipo:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="otipo" columnkey="id_tipo" />
                                                                    <select id="text_tipo"  columnkey="id_tipo" onchange="onchangedelCombo()">
                                                                        <option value="3">Cliente</option>
                                                                        <option value="4">Administrador</option>
                                                                        <option value="5">Consulta</option>
                                                                        <option value="6">Asesor</option>
                                                                    </select> 
                                                                </td>
                                                            </tr>
                                                            <tr id="trCliente">
                                                                <td class ="textos_01">
                                                                  * Cliente:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="ocliente" columnkey="id_cliente" /> 
                                                                    <input type="hidden" id="hdncompania" columnkey="id_cia" /> 
                                                                    <asp:TextBox ID="txtIdcliente" runat="server" columnkey="id_cliente" CssClass="hidden" />
                                                                    <asp:TextBox ID="txtCompDesc" runat="server"  columnkey="id_cia" CssClass="hidden" />
                                                                    <asp:TextBox ID="txtClienteDesc" runat="server" ReadOnly="true" columnkey="Cliente" Width="380px" ></asp:TextBox><img src="../Imagenes/tbfilter.gif" alt="seleccionar" style="cursor:pointer;" onclick="return openClientes();" />
                                                                    
                                                                    
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr id="trNomEntrega">
                                                                <td class ="textos_01">
                                                                 * Direcci&oacute;n entrega:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <asp:TextBox ID="ddlEntrega" runat="server" CssClass="hidden" columnkey="id_entrega"/>
                                                                    <asp:TextBox ID="txtEntrega" runat="server" ReadOnly="true" Width="380px" columnkey="Sucursal"></asp:TextBox><img src="../Imagenes/tbfilter.gif" alt="seleccionar" style="cursor:pointer;" onclick="return openSucursales();" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01">
                                                                  * Estatus:
                                                                </td>
                                                                <td class="textos_01">
                                                                    <input id="oStatus" type="hidden" columnkey="id_estatus" />
                                                                    <select id="text_estatus"  columnkey="id_estatus">
                                                                       <option value="1">ACTIVO</option>
                                                                       <option value="2">INACTIVO</option>
                                                                    </select> 
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td class="textos_01">
                                                                  (*)Campos requeridos
                                                                </td>
                                                                <td class="textos_01">
                                                                   
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <p align="center">
                                                                        <input id="Button1" class="Boton_01" style="width: 75px;" type="button" 
                                                                            value="Guardar" onclick="ok(event)"></input>
                                                                        <input id="Button2" onclick="cancel(event)" class="Boton_01" style="width: 75px;" type= "button"
                                                                            value="Cancelar"> </input>
                                                                        <input id="Button3" onclick="del(event)" class="Boton_01" style="width: 75px;" type= "button"
                                                                            value="Eliminar"> </input>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                             </table>
                                                    </RowEditTemplate>
                                                    <RowTemplateStyle Height="180px" BackColor="White" BorderColor="White" BorderStyle="Ridge"></RowTemplateStyle>
                                                    <AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                            <DisplayLayout Name="UltraWebGrid1" AllowDeleteDefault="Yes" AllowUpdateDefault="RowTemplateOnly"
                                                NoDataMessage="" RowHeightDefault="20px" SelectTypeRowDefault="Single" TableLayout="Fixed" Version="3.00"
                                                CellClickActionDefault="RowSelect" LoadOnDemand="Xml" AllowAddNewDefault="Yes"
                                                AllowColSizingDefault="Free" AllowSortingDefault="OnClient" CellPaddingDefault="1"
                                                 RowSelectorsDefault="No" RowSizingDefault="Free">
                                                <RowAlternateStyleDefault BackColor= "#E5E5E5"></RowAlternateStyleDefault>
                                                <Pager AllowPaging="True" PageSize="20">
                                                    <PagerStyle Font-Size= "11px"  Font-Names="Arial" BackColor= "#666666" ForeColor="White" Height="20px" BorderStyle= "Solid" BorderColor="Black" BorderWidth= "1px"/>
                                                </Pager>
                                                <EditCellStyleDefault BackColor= "silver" ></EditCellStyleDefault>
                                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px"></FooterStyleDefault>
                                                <HeaderStyleDefault BackColor="#666666"   BorderColor="Black" BorderStyle="Solid" Font-Bold="True" ForeColor="White"> </HeaderStyleDefault>
                                                <RowSelectorStyleDefault BorderStyle="Solid"></RowSelectorStyleDefault>
                                                <RowStyleDefault BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                                 Font-Names="Verdana" Font-Size="8pt" ForeColor="Black"></RowStyleDefault>
                                                <SelectedRowStyleDefault BackColor="#FFFFB3" ForeColor="Black" Font-Bold="True"></SelectedRowStyleDefault>
                                                <AddNewBox Hidden="true"></AddNewBox>
                                                <ActivationObject BorderColor="Black" BorderWidth="" BorderStyle="Dotted"></ActivationObject>
                                                <AddNewRowDefault View="Top" Visible="No"></AddNewRowDefault>
                                                <FilterOptionsDefault>
                                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                                                         BorderWidth="1px" CustomRules="overflow:auto;" 
                                                                         Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
                                                    </FilterDropDownStyle>
                                                    <FilterHighlightRowStyle BackColor="#999999" ForeColor="White"></FilterHighlightRowStyle>
                                                </FilterOptionsDefault>
                                                <ClientSideEvents BeforeRowTemplateOpenHandler="BeforeRowTemplateOpen" AfterRowTemplateOpenHandler="AfterRowTemplateOpen" BeforeRowTemplateCloseHandler ="BeforeRowTemplateCloseHandler" />
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid>
                                    </td>
                                </tr>
                            </tbody>
                    </table>
                  </igmisc:WebAsyncRefreshPanel>
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
                </td>
            </tr>
            <tr>
                <td colspan ="3">
                    <igtblexp:UltraWebGridExcelExporter ID="uwgRutas" WorksheetName="Rutas" DownloadName="Reporte.XLS" runat='server'>
                    </igtblexp:UltraWebGridExcelExporter>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="UserInSession" runat="server" />
        <asp:HiddenField ID="HddUser" runat="server" />
        <asp:HiddenField ID="Sucursalhdd" runat="server" />
        <asp:UpdatePanel runat="server" ID="UpdatePanel2" ChildrenAsTriggers="True" UpdateMode="Always" RenderMode="Inline">
            <ContentTemplate>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="hddSecurityConstants" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <ig:WebDialogWindow ID="dialogSucursales" runat="server" Height="450px" Width="800px"
            Modal="true" WindowState="Hidden" InitialLocation="Centered" Font-Size="10px">
            <ContentPane BackColor="#FAFAFA">
                <Template>
                    <iframe id="frameSucursales" src="about:blank" width="99%" height="99%" frameborder="0"></iframe>
                </Template>
            </ContentPane>
        </ig:WebDialogWindow>
        
        <ig:WebDialogWindow ID="dialogClientes" runat="server" Height="450px" Width="700px"
            Modal="true" WindowState="Hidden" InitialLocation="Centered" Font-Size="10px">
            <ContentPane BackColor="#FAFAFA">
                <Template>
                    <iframe id="frameClientes" src="about:blank" width="99%" height="99%" frameborder="0"></iframe>
                </Template>
            </ContentPane>
        </ig:WebDialogWindow>
    </div>
    <script language="javascript" src="../FuncionesJS/wz_tooltip.js" type="text/javascript"></script>
</asp:Content>
