<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vClientes.aspx.cs" Inherits="PedidoSugeridoLamosa.Usuario.vClientes" %>

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
        function btnAceptarDialog_onclick()
        {
            var int_reg = 1;
            var id_cliente = "";
            var desc_cliente = "";
            var desc_compania = "";
            var revisa_uno = false;

            if (window.document.getElementById("G_gridxClientes") == undefined) {
                alert("Selecciona al menos un Cliente");
                return false;
            }
            
            
            for (int_reg = 1; int_reg < window.document.getElementById("G_gridxClientes").rows.length; int_reg++) {
                if (window.document.getElementById("G_gridxClientes").rows[int_reg].cells[0].children[0].children[0].checked == true) 
                {
                    id_cliente = window.document.getElementById("G_gridxClientes").rows[int_reg].cells[5].children[0].innerHTML;
                    desc_cliente = window.document.getElementById("G_gridxClientes").rows[int_reg].cells[4].children[0].innerHTML
                    desc_compania = window.document.getElementById("G_gridxClientes").rows[int_reg].cells[3].children[0].innerHTML;
                    revisa_uno = true;
                    
                    var regExp = new RegExp("" + id_cliente + "", "gi");
                    objFinded = window.parent.igtbl_getGridById("grid_Clientes").Bands[0].Columns[3].find(regExp);

                    if (objFinded == null) {
                        var numNextRow = window.parent.igtbl_getGridById("grid_Clientes").Rows.rows.length;

                        window.parent.igtbl_getGridById("grid_Clientes").Rows.addNew();
                        window.parent.igtbl_getGridById("grid_Clientes").Rows.rows[numNextRow].cells[1].setValue(id_cliente.split("-")[0]);
                        window.parent.igtbl_getGridById("grid_Clientes").Rows.rows[numNextRow].cells[2].setValue(id_cliente.split("-")[1]);
                        window.parent.igtbl_getGridById("grid_Clientes").Rows.rows[numNextRow].cells[4].setValue(desc_cliente);
                        window.parent.igtbl_getGridById("grid_Clientes").Rows.rows[numNextRow].cells[3].setValue(desc_compania);
                        window.parent.igtbl_getGridById("grid_Clientes").Rows.rows[numNextRow].cells[5].setValue(id_cliente);
                        
                    }
                }
            }

            if (revisa_uno == false) {
                alert("Selecciona al menos un Cliente");
                return false;
            }

            window.parent.btnAceptarDialog_onclick();
        }

        function lnkBuscar_onclick() 
        {
            if (window.document.getElementById("txtDescCliente").value == "" && window.document.getElementById("txtIdClienteD").value == "" && window.document.getElementById("txtIdClienteA").value == "")
            {
                alert("Colocar un Cliente a Buscar");
                return false;
            }
            if (window.document.getElementById("txtDescCliente").value == "") 
            {
                if ((window.document.getElementById("txtIdClienteD").value == "" || window.document.getElementById("txtIdClienteA").value == ""))
                {
                    alert("Colocar ambos Ids de Clientes a Buscar");
                    return false;
                }
            }
            else
            {
                if ((window.document.getElementById("txtIdClienteD").value != "" || window.document.getElementById("txtIdClienteA").value != "")) {
                    if ((window.document.getElementById("txtIdClienteD").value == "" || window.document.getElementById("txtIdClienteA").value == "")) {
                        alert("Colocar ambos Ids de Clientes a Buscar");
                        return false;
                    }
                }
            }
            window.document.getElementById("btnBuscar").click();
        }


        function ValNumero_onkeypress() {
            if (!EsNumero(window.event.keyCode)) {
                window.event.returnValue = false;
                return false;
            }
        }

        function EsNumero(keyCode) {
            //0 - 48,  9 - 57
            if (keyCode >= 48 & keyCode <= 57)
                return true;
            else
                return false;
        }
    
    </script>
</head>
<body style="margin:0px; width:100%;">
    <form id="form1" runat="server">
    <div style="width:100%">
        <table width="100%">
                            <tr>
                                <td>
                                    <input type="button" id="btnAceptarDialog" class="Boton_01" value="Aceptar" onclick="return btnAceptarDialog_onclick();" />
                                    <asp:Button runat="server" id="btnBuscar" CssClass="Hidden" 
                                        Text="Buscar" onclick="btnBuscar_Click" 
                                       /></td>
                            </tr>
                            <tr>
                                <td class="textos_01">
                                    Clientes: <asp:TextBox ID="txtDescCliente" runat="server" Width="300px" /> <!--<a href="#" onclick="return lnkBuscar_onclick();" >Buscar</a>-->
                                </td>
                            </tr>
                            <tr>
                                <td class="textos_01">
                                    Id Cliente de : <asp:TextBox ID="txtIdClienteD" runat="server" Width="130px" MaxLength="10" /> a <asp:TextBox ID="txtIdClienteA" runat="server" Width="130px" MaxLength="10" /> <a href="#" onclick="return lnkBuscar_onclick();" >Buscar</a>
                                </td>
                            </tr>
                            <tr>
                                <td class="textos_01">
                                    <igtbl:UltraWebGrid ID="grid_Clientes" runat="server" Height="200px" Width="100%"
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
    </div>
    </form>
</body>
</html>
