<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseConfirmation.aspx.cs" Inherits="TP_ECOMMERCE_21_B.PurchaseConfirmation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
            <h1>✔ Gracias por tu compra</h1>
            <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-success d-block" />
            <asp:Repeater ID="RepeaterResumen" runat="server">
                <HeaderTemplate>
                    <h3>Resumen del pedido:</h3>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li><%# Eval("nombreProducto") %> x <%# Eval("cantidadProducto") %> = $<%# Eval("PrecioUnitario") %></li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
            <asp:Label ID="lblTotal" runat="server" CssClass="fw-bold" />
        </div>
    <a href="Ecommerce.aspx" class="btn btn-success">Volver al inicio</a>
</asp:Content>
