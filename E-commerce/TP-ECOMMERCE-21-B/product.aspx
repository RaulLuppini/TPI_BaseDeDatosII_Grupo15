<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="product.aspx.cs" Inherits="TP_ECOMMERCE_21_B.productoUnico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="labelHidden"/>
    <asp:Label runat="server" ID="labelMessage" />
    <div id="div-container-product">
    <div id="div-details">
    <div id="div-img">
      <asp:Image runat="server" ID="imgProducto" CssClass="imagen-producto" />
    </div>
    <div id="div-text">
       <h4><asp:Label runat="server" ID="ttlp"/></h4>
        <div>
            <strong><asp:Label runat="server" Text="Descripcion: " CssClass="noise"/></strong><asp:Label runat="server" ID="labelDescripcionText" Text="Es un producto utilizado para limpiar y cuidar el cabello. Su función principal es eliminar la grasa, el polvo y las partículas no deseadas que se acumulan en el pelo, haciéndolo más manejable."/>
        </div>
        <div>
            <strong><asp:Label runat="server" Text="Precio: "/></strong><asp:Label runat="server" ID="labelPrecioNormal"/>
             <asp:HiddenField ID="hfId" runat="server"/>
        </div>
    </div>
</div>
    <div id="div-buttonsCant">
<asp:Button runat="server" CssClass="buttonCant" Text="-" ID="buttonRest" OnClick="click_buttonRest" />
<asp:Label runat="server" CssClass="labelNum" ID="numLabel" Text="0"/>
<asp:Button runat="server" CssClass="buttonCant" Text="+" ID="buttonPlus" OnClick="click_buttonPlus" />
    </div>
    <asp:Button runat="server" Cssclass="buttonAddCart" Onclick="click_buttonAdd"  role="button" Text="Agregar al carrito"></asp:Button>
        </div>

    <div class="mt-3">
        <asp:Button ID="btnSeguirComprando" runat="server" Text="Seguir Comprando" CssClass="btn btn-secondary" OnClick="btnSeguirComprando_Click"  />
        <asp:Button ID="btnIrAlCarrito" runat="server" Text="Ir al carrito" CssClass="btn btn-primary" OnClick="btnIrAlCarrito_Click" />
    </div>

</asp:Content>
