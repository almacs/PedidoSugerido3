<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vSeleccionCliente.aspx.cs" Inherits="PedidoSugeridoLamosa.Reportes.vSeleccionCliente" %>


<%@ Register Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>

<%@ Register Assembly="Infragistics35.Web.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
    
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
    
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.ExcelExport.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
    
<%@ Register Assembly="Infragistics35.WebUI.Shared.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Shared" TagPrefix="ish" %>
                                

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Clientes</title>
    <link href="~/Estiloscss/Estilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        function btnAgregar_onclick() 
        {
            window.frames["frameClientes"].location = "about:blank";
            var URL = "vClientes.aspx"
            window.frames["frameClientes"].location = URL;
            $find('<%=dialogClientes.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
        }

        function btnAceptarDialog_onclick() 
        {

            $find('<%=dialogClientes.ClientID%>').set_windowState($IG.DialogWindowState.Hidden);
            window.frames["frameClientes"].location = "about:blank";
        }

        function btnQuitar_onclick() 
        {
            var int_reg = 0;
            var revisa_uno = 0;
            var id_cliente = "";
            var numRows = igtbl_getGridById("grid_Clientes").Rows.rows.length;
            
            if (numRows == 0)
            {
                alert("Selecciona al menos un Cliente a Quitar");
                return false;
            }


            for (int_reg = 0; int_reg < numRows; int_reg++) {
                if (igtbl_getGridById("grid_Clientes").Rows.rows[int_reg - revisa_uno].cells[0].getValue() == "true") 
                {
                    igtbl_getGridById("grid_Clientes").Rows.rows[int_reg - revisa_uno].remove();
                    revisa_uno++;
                }
            }

            if (revisa_uno == 0) {
                alert("Selecciona al menos un Cliente a Quitar");
                return false;
            }

        }
        
        function btnAceptar_onclick()
        {
            var int_reg = 0;
            var id_cliente = "";
            var nom_cliente = "";
            var desc_comp = "";
            var numRows = igtbl_getGridById("grid_Clientes").Rows.rows.length;

            if (numRows == 0) {
                alert("Agrega al Menos Un Cliente");
                return false;
            }
            /*if (window.parent.document.getElementById("text_tipo").value != 6 && numRows > 1)
            {
                alert("El tipo de usuario, únicamente permite seleccionar un cliente. Favor de seleccionar el cliente del usuario.");
                return false;
            }*/

            for (int_reg = 0; int_reg < numRows; int_reg++) {
                id_cliente += igtbl_getGridById("grid_Clientes").Rows.rows[int_reg].cells[5].getValue() + "|";
                nom_cliente += igtbl_getGridById("grid_Clientes").Rows.rows[int_reg].cells[4].getValue() + "|";
                desc_comp += igtbl_getGridById("grid_Clientes").Rows.rows[int_reg].cells[3].getValue() + "|";

            }
            window.parent.dialogArguments.document.getElementById("ctl00_Principal_txtIdcliente").value = id_cliente;
            window.parent.dialogArguments.document.getElementById("ctl00_Principal_txtClienteDesc").value = nom_cliente;
            window.parent.dialogArguments.document.getElementById("ctl00_Principal_txtCompDesc").value = desc_comp;

            window.parent.dialogArguments.document.getElementById("ctl00_Principal_ddlEntrega").value = "";
            window.parent.dialogArguments.document.getElementById("ctl00_Principal_txtEntrega").value = "";

            window.parent.close();
        }
        
        function window_onload()
        {
            var id_cliente = window.parent.dialogArguments.document.getElementById("ctl00_Principal_txtIdcliente").value;
            var nom_cliente = window.parent.dialogArguments.document.getElementById("ctl00_Principal_txtClienteDesc").value;
            var desc_comp = window.parent.dialogArguments.document.getElementById("ctl00_Principal_txtCompDesc").value;
            
            if (id_cliente != "")
            {
                for (int_reg = 0; int_reg < id_cliente.split("|").length - 1; int_reg++) 
                {
                    /*window.igtbl_getGridById("grid_Clientes").Rows.addNew();
                    window.igtbl_getGridById("grid_Clientes").Rows.rows[int_reg].cells[1].setValue(nom_cliente.split("|")[int_reg]);
                    window.igtbl_getGridById("grid_Clientes").Rows.rows[int_reg].cells[2].setValue(desc_comp.split("|")[int_reg]);
                    window.igtbl_getGridById("grid_Clientes").Rows.rows[int_reg].cells[3].setValue(id_cliente.split("|")[int_reg]);*/

                    window.igtbl_getGridById("grid_Clientes").Rows.addNew();
                    window.igtbl_getGridById("grid_Clientes").Rows.rows[int_reg].cells[1].setValue(id_cliente.split("|")[int_reg].split("-")[0]);
                    window.igtbl_getGridById("grid_Clientes").Rows.rows[int_reg].cells[2].setValue(id_cliente.split("|")[int_reg].split("-")[1]);
                    window.igtbl_getGridById("grid_Clientes").Rows.rows[int_reg].cells[3].setValue(desc_comp.split("|")[int_reg]);
                    window.igtbl_getGridById("grid_Clientes").Rows.rows[int_reg].cells[4].setValue(nom_cliente.split("|")[int_reg]);
                    window.igtbl_getGridById("grid_Clientes").Rows.rows[int_reg].cells[5].setValue(id_cliente.split("|")[int_reg]);
                }
            }
        }
    
    </script>
</head>
<body style="margin:0px; width:100%;" onload="window_onload();">
    <form id="form1" runat="server">
            <asp:ScriptManager ID="sm" runat="server">

        </asp:ScriptManager>     
    <div style="width:100%">
        <table width="100%">
                            <tr>
                                <td class="textos_01">
                                    Clientes:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="button" id="btnAceptar" class="Boton_01" value="Aceptar" onclick="return btnAceptar_onclick();" />
                                    <input type="button" id="btnAgregar" class="Boton_01" value="Agregar" onclick="return btnAgregar_onclick();" />
                                    <input type="button" id="btnQuitar" class="Boton_01" value="Quitar" onclick="return btnQuitar_onclick();" />
                                </td>
                            </tr>

                            <tr>
                                <td class="textos_01">
                                    <igtbl:UltraWebGrid ID="grid_Clientes" runat="server" Height="200px" Width="90%"
                                    EnableAppStyling="False" >
                                    <Bands>
                                        <igtbl:UltraGridBand>
                                            <Columns>
                                            </Columns>
                                        </igtbl:UltraGridBand>
                                    </Bands>
                                    <DisplayLayout Name="UltraWebGrid2" AllowDeleteDefault="Yes" AllowUpdateDefault="RowTemplateOnly"
                                        NoDataMessage="" RowHeightDefault="20px" SelectTypeRowDefault="Single" TableLayout="Fixed"
                                        Version="3.00" CellClickActionDefault="RowSelect" LoadOnDemand="Xml" AllowAddNewDefault="Yes"
                                        AllowColSizingDefault="Free" AllowSortingDefault="OnClient" CellPaddingDefault="1"
                                        HeaderClickActionDefault="SortSingle" RowSelectorsDefault="No" RowSizingDefault="Free">
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
                                        <ActivationObject BorderColor="Black" BorderWidth="" BorderStyle="Dotted">
                                        </ActivationObject>
                                        <AddNewRowDefault View="Top" Visible="No">
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
                            <tr>
                                <td style="text-align:center;">
                                    
                                </td>
                            </tr>
                        </table>
                        
                        
        <ig:WebDialogWindow ID="dialogClientes" runat="server" Height="350px" Width="580px"
            Modal="true" WindowState="Hidden" InitialLocation="Centered" Font-Size="10px">
            <ContentPane BackColor="#FAFAFA">
                <Template>
                    <iframe id="frameClientes" src="about:blank" width="99%" height="99%" frameborder="0"></iframe>
                </Template>
            </ContentPane>
        </ig:WebDialogWindow>
        
        
    </div>
    </form>
</body>
</html>
