<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="gestionPedido.aspx.cs" Inherits="TP_ECOMMERCE_21_B.gestionPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Administrá los pedidos del sistema</h3>
    <p>Elegi el estado del pedido para cambiar.</p>
    <div class="mb-3 d-flex align-items-center">

        <asp:TextBox ID="txtFiltroNumero" runat="server" CssClass="form form-control mb-2" placeholder="Número de Pedido:"></asp:TextBox>

        <asp:DropDownList ID="ddlFiltroEstado" runat="server" CssClass="form form-control mb-2">
            <asp:ListItem Text="Todos" Value="" />
            <asp:ListItem Text="Pago en proceso" Value="Pago en proceso" />
            <asp:ListItem Text="Activo" Value="Activo" />
            <asp:ListItem Text="En preparación" Value="En preparación" />
            <asp:ListItem Text="Enviado" Value="Enviado" />
            <asp:ListItem Text="Cancelado" Value="Cancelado" />
        </asp:DropDownList>
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar"  CssClass="btn btn-secondary ms-2 " OnClick="btnBuscar_Click"  />
    </div>
    <asp:GridView ID="GridViewPedido" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" PageSize="10" AllowPaging="true" OnPageIndexChanging="GridViewPedido_PageIndexChanging"
        OnRowCommand="GridViewPedido_RowCommand" OnRowDataBound="GridViewPedido_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnVerDetalle" runat="server" Text="Ver detalle"
                        CommandName="VerDetalle" CommandArgument='<%# Eval("Id") %>'
                        CssClass="btn btn-primary" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Id" HeaderText="Numero de Pedido" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre" />
            <asp:BoundField DataField="ApellidoUsuario" HeaderText="Apellido" />
            <asp:BoundField DataField="IdUsuario" HeaderText="ID Usuario" Visible="false" />
            <asp:BoundField DataField="PrecioTotal" HeaderText="Precio Total" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Estado" HeaderText="Estado Actual" />
            <asp:BoundField DataField="MetodoDePago" HeaderText="Método de Pago" />

            <asp:TemplateField HeaderText="Nuevo Estado">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlEstado" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnCambiarEstado" runat="server" Text="Cambiar"
                        CommandName="CambiarEstado" CommandArgument='<%# Eval("Id") %>'
                        CssClass="btn btn-primary btn-sm" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
