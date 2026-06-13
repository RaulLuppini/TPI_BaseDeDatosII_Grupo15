<%@ Import Namespace="dominio" %>
<%@ Import Namespace="negocio" %>
<%@ Import Namespace="accesoAdatos" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="pedidos.aspx.cs" Inherits="TP_ECOMMERCE_21_B.WebForm1" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Content/pedidos.css") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="global-container">
        <% negocioPedido np = new negocioPedido();
           Usuario user = (Usuario)Session["usuario"]; 
            if(user == null)
            {%>
                <h1>No tienes pedidos realizados</h1>
           <%}else {%> 
               <%List<Pedido> pedidos = np.getPedidos(user.Id);
                   int index = 0;%>
                  <h1>PEDIDOS</h1>
                   <%foreach (Pedido pedido in pedidos) {%>
                      <div class="container-pedido">
                         <h2>#<%=pedido.Id%></h2>
                          <div class="container-titulos">
                               <p>Estatus: </p>
                              <p>Metodo de pago: </p>
                              <p>Productos: </p>
                              <p id="totaltext">Total: </p>
                          </div>

                          <div class="container-values">
                              <p><%=pedido.Estado%></p>
                              <p><%=pedido.MetodoDePago %></p>
                              <div class="container-productos-pedido">
                              <%foreach (DetallePedido detalle in pedido.DetallePedidos)
                                  { %>
                                  <p><%=detalle.nombreProducto %> x <%=detalle.cantidadProducto%></p>
                              <%} %>
                                 </div>
                              <p><%=pedido.PrecioTotal%></p>
                              </div>
                          </div>
               <%index++;%>
                 <% }%>
               <%}%>

       
    </div>

       <script src="<%= ResolveUrl("~/Scripts/WebForms/pedidos.js") %>"></script>
</asp:Content>
