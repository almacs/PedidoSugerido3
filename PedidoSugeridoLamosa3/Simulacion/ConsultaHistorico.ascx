<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConsultaHistorico.ascx.cs" Inherits="PedidoSugeridoLamosa.Simulacion.ConsultaHistorico" %>
<link href="../Estiloscss/Estilos.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.tableHeader 
{
    font-family:Arial;
    font-size: 12px;
    font-weight: bold;
    background-color: #666666;
    color: White;
}
.tableListRow
{
    font-family:Arial;
    font-size: 12px;
}
.tableListRowAltern
{
    background-color: #E5E5E5;
    font-family:Arial;
    font-size: 12px;
}
</style>
<asp:Button ID="btnExportar" runat="server" OnClick="btnExportar_Click" Text="Exportar" CssClass="Boton_01" visible="false"/>
<asp:GridView ID="gvGrid" runat="server" AutoGenerateColumns="false">
<Columns>
    <asp:BoundField DataField="ItemID" HeaderText="ItemID" />
    <asp:BoundField DataField="ItemCustomerID" HeaderText="Cliente" />
    <asp:BoundField DataField="ItemDesc" HeaderText="Descripción" />
    <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unidad" />
    <asp:BoundField DataField="Month_H1" HeaderText="Mes 1" />
    <asp:BoundField DataField="Month_H2" HeaderText="Mes 2" />
    <asp:BoundField DataField="Month_H3" HeaderText="Mes 3" />
    <asp:BoundField DataField="Month_H4" HeaderText="Mes 4" />
    <asp:BoundField DataField="Month_H5" HeaderText="Mes 5" />
    <asp:BoundField DataField="Month_H6" HeaderText="Mes 6" />
</Columns>
<HeaderStyle CssClass="tableHeader" />
<RowStyle CssClass="tableListRow" />
<AlternatingRowStyle CssClass="tableListRowAltern" />
</asp:GridView>
