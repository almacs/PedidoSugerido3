<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vSeleccionEntrega.aspx.cs" Inherits="PedidoSugeridoLamosa.Reportes.vSeleccionEntrega" %>


<%@ Register Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>

<%@ Register Assembly="Infragistics35.Web.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
    
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
    
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.ExcelExport.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
    
<%@ Register Assembly="Infragistics35.WebUI.Shared.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.Shared" TagPrefix="ish" %>
                                

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Puntos de Entrega</title>
    <link href="~/Estiloscss/Estilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function btnAceptarDialog_onclick()
        {
            var int_reg = 1;
            var id_entrega = "";
            var desc_entrega = "";
            var revisa_uno = false;
            for (int_reg = 1; int_reg < window.document.getElementById("G_gridxSucursales").rows.length; int_reg++) {
                if (window.document.getElementById("G_gridxSucursales").rows[int_reg].cells[0].children[0].children[0].checked == true) {
                    id_entrega += window.document.getElementById("G_gridxSucursales").rows[int_reg].cells[7].children[0].innerHTML + "|";
                    desc_entrega += window.document.getElementById("G_gridxSucursales").rows[int_reg].cells[6].children[0].innerHTML + "|";
                    revisa_uno = true;
                }
            }

            if (revisa_uno == false) {
                alert("Selecciona al menos una Sucursal de Entrega");
                return false;
            }
            window.parent.dialogArguments.document.getElementById("ctl00_Principal_ddlEntrega").value = id_entrega;
            window.parent.dialogArguments.document.getElementById("ctl00_Principal_txtEntrega").value = desc_entrega;

            window.parent.close();
        }
        
        function window_onload()
        {
            window.document.getElementById("G_gridxSucursales").rows[0].cells[0].innerHTML = "<input type='checkbox' id='chkTodos' onclick='checkAll(this);'>";
        }
        
        function checkAll(obj)
        {
            var int_reg = 1;
            for (int_reg = 1; int_reg < window.document.getElementById("G_gridxSucursales").rows.length; int_reg++) {
                window.document.getElementById("G_gridxSucursales").rows[int_reg].cells[0].children[0].children[0].checked = obj.checked;
            }
        }
    
    </script>
</head>
<body style="margin:0px; width:100%;" onload="window_onload();">
    <form id="form1" runat="server">
    <div style="width:100%">
        <table width="100%">
                            <tr>
                                <td class="textos_01">
                                    Sucursales:
                                </td>
                            </tr>
                             <tr>
                                <td style="">
                                    <input type="button" id="btnAceptarDialog" class="Boton_01" value="Aceptar" onclick="return btnAceptarDialog_onclick();" />
                                </td>
                            </tr>
                            <tr>
                                <td class="textos_01">
                                    <igtbl:UltraWebGrid ID="grid_Sucursales" runat="server" Height="200px" Width="90%"
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
                                        AllowColSizingDefault="Free" AllowSortingDefault="No" CellPaddingDefault="1"
                                        HeaderClickActionDefault="NotSet" RowSelectorsDefault="No" RowSizingDefault="Free">
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
                           
                        </table>
    </div>
    </form>
</body>
</html>
