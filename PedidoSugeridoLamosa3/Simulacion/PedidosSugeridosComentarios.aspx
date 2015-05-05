<%@ Page Title="Pedido Sugerido Comentarios" Language="C#" MasterPageFile="~/PedidoSugeridoLamosa.Master" 
    AutoEventWireup="true" CodeBehind="PedidosSugeridosComentarios.aspx.cs" Inherits="PedidoSugeridoLamosa.Simulacion.PedidosSugeridosComentarios" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>

<%@ Register Assembly="Infragistics35.Web.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>

<asp:Content ID="PedidoSugeridoComentarios" ContentPlaceHolderID="Principal" runat="server">
    
    <div id="divMain" style="width: 100%;">
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tbody>
                <tr>
                    <td style="height: 10px">
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="lblTitulo1" onmouseover="this.T_DESCPANTALLA=true; this.T_BGIMG='../Imagenes/help2.gif'; this.T_HEIGHT=200; this.T_WIDTH=400; this.T_ABOVE=false; this.T_CLICKCLOSE=true; this.T_STICKY=true; return escape('<b>Incluir comentarios en los artículos</b><p></p>En esta sección usted puede agregar algún comentario a cada artículo de la orden de compra.');">
                                    <asp:Label ID="lblTitulo" runat="server" Text="Incluir comentarios en los artículos">
                                    </asp:Label>
                                    <br/>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lbDesFechaSimulacion" CssClass="textos_Login" runat="server">
                                    </asp:Label>
                                    <br/>
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
                <tr align="right">
                    <td height="30px">
                        <asp:Button ID="btExportarFormatoOC" runat="server" Text="Exportar Formato OC" CssClass="Boton_01"
                            Width="200px" onclick="btExportarFormatoOC_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <igtbl:UltraWebGrid ID="gvArtComentarios" runat="server" 
                            EnableAppStyling="False" Height="500px" Width="100%">
                            <bands>
                                <igtbl:UltraGridBand>
                                    <Columns>
                                        <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Cve_Articulo_Cliente" 
                                            Width="75px">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                            <ValueList DisplayMember="Cve_Articulo_Cliente">
                                            </ValueList>
                                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                                            </CellStyle>
                                            <Header Caption="Artículo Cliente" Fixed="True">
                                            </Header>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Desc_Articulo" 
                                            Width="220px">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                            <ValueList DisplayMember="Desc_Articulo">
                                            </ValueList>
                                            <CellStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True">
                                            </CellStyle>
                                            <Header Caption="Descripción del Artículo" Fixed="True">
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Comentarios" 
                                            FieldLen="40" Width="400px">
                                            <HeaderStyle BackColor="#0033CC" HorizontalAlign="Center" 
                                                VerticalAlign="Middle" Wrap="True" Height="25px" />
                                            <ValueList DisplayMember="Comentarios">
                                            </ValueList>
                                            <CellStyle ForeColor="#0033CC" HorizontalAlign="Left" VerticalAlign="Middle" 
                                                Wrap="True">
                                            </CellStyle>
                                            <Header Caption="Comentarios">
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                    </Columns>
                                    <RowTemplateStyle BackColor="White" BorderColor="White" BorderStyle="Ridge">
                                        <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" 
                                            WidthTop="3px" />
                                    </RowTemplateStyle>
                                    <addnewrow view="NotSet" visible="NotSet">
                                    
                                    </addnewrow>
                                </igtbl:UltraGridBand>
                            </bands>
                            <DisplayLayout Name="UltraWebGrid2" AllowDeleteDefault="No" AllowUpdateDefault="RowTemplateOnly"
                                NoDataMessage="" RowHeightDefault="20px" SelectTypeRowDefault="Single" TableLayout="Fixed"
                                Version="3.00" CellClickActionDefault="RowSelect" LoadOnDemand="Xml"
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
                <tr>
                    <td>
                        
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    
    <asp:Literal ID="id_messages" runat="server">
    
    </asp:Literal>
    
    <script language="javascript" src="../FuncionesJS/wz_tooltip.js" type="text/javascript">
    
    </script>
    
</asp:Content>
