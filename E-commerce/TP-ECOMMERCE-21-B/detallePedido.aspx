<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="detallePedido.aspx.cs" Inherits="TP_ECOMMERCE_21_B.detallePedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Detalle del Pedido</h3>
    <p><strong>Numero de Pedido:</strong>
        <asp:Label ID="lblNumeroPedido" runat="server"></asp:Label></p>
    <p><strong>Fecha:</strong>
        <asp:Label ID="lblFecha" runat="server"></asp:Label></p>
    <p><strong>Nombre:</strong>
        <asp:Label ID="lblNombreUsuario" runat="server"></asp:Label></p>
    <p><strong>Apellido:</strong>
        <asp:Label ID="lblApellidoUsuario" runat="server"></asp:Label></p>




    <asp:GridView ID="GridViewDetalle" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
            <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
            <asp:BoundField DataField="cantidadProducto" HeaderText="Cantidad" />
            <asp:BoundField DataField="SubTotal" HeaderText="SubTotal" DataFormatString="{0:C}" />
        </Columns>
    </asp:GridView>
    <p><strong>Precio total:</strong> <asp:Label ID="lblPrecioTotal" runat="server"></asp:Label></p>
    <p><strong>Metodo de Pago:</strong> <asp:Label ID="lblMetodoPago" runat="server"></asp:Label></p>
    <p><strong>Estado:</strong><asp:Label ID="lblEstado" runat="server"></asp:Label></p>
    
   
</asp:Content>
