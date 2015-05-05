<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="PedidoSugeridoLamosa.Reportes.Usuario" %>

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


        function ObtenerDato(Clave, Nombre, id_cliente, nom_cliente, id_entrega, nom_entrega, desc_comp)
        {
            window.parent.dialogArguments.document.getElementById("ctl00_Principal_txtNomUsuario").value = Nombre;
            window.parent.dialogArguments.document.getElementById("ctl00_Principal_HddUser").value = Clave;
            window.parent.dialogArguments.document.getElementById("ctl00_Principal_txtIdcliente").value = id_cliente;
            window.parent.dialogArguments.document.getElementById("ctl00_Principal_txtClienteDesc").value = nom_cliente;
            window.parent.dialogArguments.document.getElementById("ctl00_Principal_txtCompDesc").value = desc_comp;
            window.parent.dialogArguments.document.getElementById("ctl00_Principal_ddlEntrega").value = id_entrega;
            window.parent.dialogArguments.document.getElementById("ctl00_Principal_txtEntrega").value = nom_entrega;
            window.parent.close();
            
        }
    
    </script>
</head>
<body style="margin:0px; width:100%;">
    <form id="form1" runat="server" style="margin:0px;">
    <div style="width:100%" >
        <table width="100%" cellpadding="0px" cellspacing="0px" border="0px">                            
                <tr>
                    <td class="textos_01">
                        <igtbl:UltraWebGrid ID="grid_Sucursales" runat="server" Height="440px" Width="100%"
                        EnableAppStyling="False"
                            oninitializelayout="grid_Sucursales_InitializeLayout" >
                        <Bands>
                        
                            <igtbl:UltraGridBand>
                                <Columns>                                            
                                    <igtbl:TemplatedColumn Width="150">
                                        <HeaderTemplate>Clave Usuario</HeaderTemplate>
                                        <CellTemplate><a href="javascript:ObtenerDato('<%# DataBinder.Eval(Container, "Dataitem.Clave usuario")%>','<%# DataBinder.Eval(Container, "Dataitem.Nombre_Completo")%>','<%# DataBinder.Eval(Container, "Dataitem.id_cliente")%>','<%# DataBinder.Eval(Container, "Dataitem.nom_cliente")%>','<%# DataBinder.Eval(Container, "Dataitem.id_entrega")%>','<%# DataBinder.Eval(Container, "Dataitem.Sucursal")%>','<%# DataBinder.Eval(Container, "Dataitem.id_cia")%>');"><%# DataBinder.Eval(Container, "Dataitem.Clave usuario")%></a></CellTemplate>
                                    </igtbl:TemplatedColumn>
                                    <igtbl:TemplatedColumn Width="370">
                                        <HeaderTemplate>Nombre Usuario</HeaderTemplate>                                                    
                                        <CellTemplate><%# DataBinder.Eval(Container, "Dataitem.Nombre_Completo")%></CellTemplate>
                                    </igtbl:TemplatedColumn>
                                </Columns>
                            </igtbl:UltraGridBand>
                        </Bands>
                        <DisplayLayout Name="UltraWebGrid2" AllowDeleteDefault="Yes"  AutoGenerateColumns="false" AllowUpdateDefault="RowTemplateOnly"
                            NoDataMessage="" RowHeightDefault="20px" SelectTypeRowDefault="Single" TableLayout="Fixed"
                            Version="3.00" CellClickActionDefault="RowSelect" LoadOnDemand="Xml" AllowAddNewDefault="Yes"
                            AllowColSizingDefault="Free" AllowSortingDefault="OnClient" CellPaddingDefault="1"
                            HeaderClickActionDefault="SortSingle" RowSelectorsDefault="No" RowSizingDefault="Free" >
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
