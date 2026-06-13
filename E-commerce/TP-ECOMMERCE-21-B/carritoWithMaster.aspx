<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="carritoWithMaster.aspx.cs" Inherits="TP_ECOMMERCE_21_B.carritoWithMaster" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Content/carrito.css") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" CssClass="contentCarrito">

        <asp:Label runat="server" ID="textcart" />
        <asp:Repeater runat="server" ID="repRepetidor">
            <ItemTemplate>

                <div class="div-contenedor-producto">
                    <input type="hidden" name="hfId" class="hfId" value="" />
                    <div class="div-img-carrito">
                        <img src="<%# Eval("Imagenes[0].Url")%>">
                    </div>

                    <div class="div-producto-info">
                        <p><%# Eval("Nombre") %></p>
                        <p><%# Eval("Descripcion") %></p>
                        <p><%# Eval("PrecioVenta", "{0:N2}") %></p>
                        <p>Subtotal: $<%# (Convert.ToDecimal(Eval("PrecioVenta")) * Convert.ToInt32(Eval("cantidad"))).ToString("N2") %></p>
                        <strong>
                            <label>Cantidad:</label>
                        </strong>
                        <p><%# Eval("cantidad")%></p>
                    </div>
                    <div class="div-button-eliminar">

                        <asp:ImageButton
                            runat="server"
                            ID="btnDelete"
                            ImageUrl="~/img/trash.jpg"
                            CssClass="imagebtDelete"
                            CommandName="productId"
                            CommandArgument='<%# Eval("Id")%>'
                            OnClick="btnDelete_Click" />

                    </div>
                </div>

            </ItemTemplate>
        </asp:Repeater>
        <asp:Label ID="lblTotalCarrito" runat="server" CssClass="h4 text-success mt-3" />
        <div class="mt-5">
            <asp:Button ID="btnIniciarCompra" runat="server" Text="Iniciar Compra" CssClass="btn btn-success" OnClick="btnIniciarCompra_Click" />
            <asp:Button ID="btnVolverCatalogo" runat="server" Text="Volver al catálogo" CssClass="btn btn-secondary" OnClick="btnVolverCatalogo_Click" />
        </div>
    </asp:Panel>
    <script src="<%= ResolveUrl("~/Scripts/WebForms/carrito.js") %>"></script>
</asp:Content>
