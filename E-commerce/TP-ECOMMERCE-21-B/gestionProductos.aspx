<%@ Page Title="Gestion de Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="gestionProductos.aspx.cs" Inherits="TP_ECOMMERCE_21_B.gestionProductos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>

        <h3>Administrá los productos del sistema</h3>
        <p>Seleccioná un producto para modificar, eliminar, dar de baja o alta.</p>

        <div class="mb-3 d-flex flex-wrap gap-2">
            <asp:TextBox ID="txtFiltroProducto" runat="server" CssClass="form-control" placeholder="Buscar por nombre o código..." />

            <asp:DropDownList ID="ddlFiltroCategoria" runat="server" CssClass="form-select">
                <asp:ListItem Text="Todas las categorías" Value="" />
            </asp:DropDownList>

            <asp:DropDownList ID="ddlFiltroMarca" runat="server" CssClass="form-select">
                <asp:ListItem Text="Todas las marcas" Value="" />
            </asp:DropDownList>

            <asp:Button ID="btnFiltrarProducto" runat="server" Text="Filtrar" CssClass="btn btn-secondary" OnClick="btnFiltrarProducto_Click" />
        </div>

        <asp:GridView ID="GridViewProductos" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridViewProductos_PageIndexChanging"
            CssClass="table table-bordered"
            AutoGenerateColumns="False"
            DataKeyNames="Id"
            OnSelectedIndexChanged="GridViewProductos_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" />
                <asp:BoundField DataField="Codigo" HeaderText="Código" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />

                <asp:TemplateField HeaderText="Marca">
                    <ItemTemplate>
                        <%# Eval("IdMarca.Nombre") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Categoria">
                    <ItemTemplate>
                        <%# Eval("IdCategoria.Nombre") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="PrecioCompra" HeaderText="Precio Compra" />
                <asp:BoundField DataField="PorcentajeGanancia" HeaderText="Ganancia (%)" />
                <asp:BoundField DataField="PrecioVenta" HeaderText="Precio Venta" />
                <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" />
                <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo" />
                <asp:TemplateField HeaderText="Estado" Visible="false">
                    <ItemTemplate>
                        <%# (bool)Eval("Estado") ? "Activo" : "Baja" %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>


        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnBaja" runat="server" Text="Baja" OnClick="btnBaja_Click" CssClass="btn btn-warning" />
        <asp:Button ID="btnAlta" runat="server" Text="Alta" OnClick="btnAlta_Click" CssClass="btn btn-success" />
        <% //<asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" CssClass="btn btn-danger" />%>
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="btn btn-secondary" />


    </main>
</asp:Content>
