<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArtDesc.aspx.cs" Inherits="PedidoSugeridoLamosa.Simulacion.ArtDesc" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>

<%@ Register Assembly="Infragistics35.Web.v11.1, Version=11.1.20111.2020, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Artículos Descontinuados</title>
    <link href="../Estiloscss/EstiloTooltip.css" rel="Stylesheet" type="text/css" />
    <link href="../Estiloscss/Estilos.css" rel="Stylesheet" type="text/css" />
    <!--<link href="../Estiloscss/menu_Lamosa.css" rel="Stylesheet" type="text/css" />-->
    <!--<link href="../Estiloscss/pro-line-down-fly_lamosa/menu3.css" rel="Stylesheet" type="text/css" />-->
</head>
<body style="width:100%; height:550px; margin:0px;">
    <form id="form1" runat="server"  style="width:100%; height:100%;">
    <div  style="width:100%; height:100%;">
        <table border="0px" cellpadding="0px" cellspacing="0px" style="width:100%; height:100%;">
            <tr style="height: 10px">
                <td style="height: 10px">
                </td>
            </tr>
            <tr style="height: 30px;">
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="lblTitulo1" onmouseover="this.T_DESCPANTALLA=true; this.T_BGIMG='../Imagenes/help2.gif'; this.T_HEIGHT=200; this.T_WIDTH=400; this.T_ABOVE=false; this.T_CLICKCLOSE=true; this.T_STICKY=true; return escape('<b>Quitar Artículos Descontinuados</b><p></p>En esta pantalla se podrá quitar de la Simulación, aquellos artículos descontinuados.');">
                                <asp:Label ID="lblTitulo" runat="server" Text="Quitar Artículos Descontinuados">
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
            <tr>
                <td height="30px">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="Boton_01"
                        Width="200px" onclick="btnGuardar_Click"/>
                </td>
            </tr>
            <tr>
                    <td>
                        <igtbl:UltraWebGrid ID="grdArticulosDesc" runat="server" 
                            EnableAppStyling="False" Height="500px" Width="100%">
                            <bands>
                                <igtbl:UltraGridBand>
                                    <Columns>
                                        <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Bit_Descontinuado" 
                                            Width="120px" DataType="System.Boolean" Type="CheckBox">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                            <ValueList DisplayMember="Bit_Descontinuado">
                                            </ValueList>
                                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                                            </CellStyle>
                                            <Header Caption="Seleccionado" Fixed="True">
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Cve_Articulo_Cliente" 
                                            Width="120px">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                            <ValueList DisplayMember="Cve_Articulo_Cliente">
                                            </ValueList>
                                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                                            </CellStyle>
                                            <Header Caption="Artículo Cliente" Fixed="True">
                                            <RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Desc_Articulo" 
                                            Width="300px">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                            <ValueList DisplayMember="Desc_Articulo">
                                            </ValueList>
                                            <CellStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True">
                                            </CellStyle>
                                            <Header Caption="Descripción del Artículo" Fixed="True">
                                                <RowLayoutColumnInfo OriginX="1" />
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="2" />
                                            </Footer>
                                        </igtbl:UltraGridColumn>
                                        <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Cve_Articulo" Hidden="true"
                                            Width="120px">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                            <ValueList DisplayMember="Cve_Articulo">
                                            </ValueList>
                                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                                            </CellStyle>
                                            <Header Caption="Artículo Rev" Fixed="True">
                                            <RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
                                            </Header>
                                            <Footer>
                                                <RowLayoutColumnInfo OriginX="1" />
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
                                NoDataMessage="" RowHeightDefault="20px" SelectTypeRowDefault="None"
                                Version="3.00"
                                AllowSortingDefault="No" CellPaddingDefault="1"
                                AutoGenerateColumns="False"
                                >
                                <FrameStyle Width="100%" Height="500px">
                                </FrameStyle>
                                <RowAlternateStyleDefault BackColor="#E5E5E5">
                                </RowAlternateStyleDefault>
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
                            </DisplayLayout>
                        </igtbl:UltraWebGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        
                    </td>
                </tr>
        </table>
    </div>
    <asp:Literal ID="id_messages" runat="server">
    
    </asp:Literal>
    
    <script language="javascript" src="../FuncionesJS/wz_tooltip.js" type="text/javascript">
    
    </script>
    </form>
</body>
</html>
