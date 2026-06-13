<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="gestionCategoria.aspx.cs" Inherits="TP_ECOMMERCE_21_B.gestionCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Administrá las categorias y marcas del sistema</h3>
    <p>Seleccioná una categoria o marca para modificar.</p>
    <div class="row">
        <div class="col-md-6">
            <h4>Categorías</h4>

            <asp:TextBox ID="txtFiltroCategoria" runat="server" CssClass="form-control mb-2" placeholder="Buscar categoría por nombre" />
            <asp:Button ID="btnFiltrarCategoria" runat="server" Text="Filtrar" CssClass="btn btn-sm btn-secondary mb-3"
                OnClick="btnFiltrarCategoria_Click" />

            <asp:GridView ID="GridViewCategoria" AutoGenerateColumns="false" AutoGenerateSelectButton="true" DataKeyNames="Id" CssClass="table table-bordered" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridViewCategoria_PageIndexChanging" OnSelectedIndexChanged="GridViewCategoria_SelectedIndexChanged" AutoPostBack="true">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                </Columns>
            </asp:GridView>
            <hr />
            <asp:TextBox ID="txtCategoria" runat="server" placeholder="Agregar Categoria"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCategoria" runat="server"
                ControlToValidate="txtCategoria"
                ErrorMessage="⚠️ Ingresá un nombre de categoría."
                Display="Dynamic" CssClass="text-danger" ValidationGroup="Categoria"  />

            <div class="d-flex flex-wrap gap-1 mt-3">
                <asp:Button ID="btnAgregarC" runat="server" Text="Agregar Categoria" OnClick="btnAgregarC_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnModificarC" runat="server" Text="Modificar Categoria" OnClick="btnModificarC_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnEliminarC" runat="server" Text="Eliminar Categoria" OnClick="btnEliminarC_Click"  CssClass="btn btn-danger"/>
                <asp:Button ID="CancelarC" runat="server" Text="Cancelar " OnClick="CancelarC_Click" CssClass="btn btn-secondary" />
                <asp:Label ID="lblMensajeC" runat="server" Text=""></asp:Label>
            </div>
            -
        </div>

        <div class="col-md-6">
            <h4>Marcas</h4>
            <asp:TextBox ID="txtFiltroMarca" runat="server" CssClass="form-control mb-2" placeholder="Buscar marca por nombre" />
            <asp:Button ID="btnFiltrarMarca" runat="server" Text="Filtrar" CssClass="btn btn-sm btn-secondary mb-3"
                OnClick="btnFiltrarMarca_Click" />

            <asp:GridView ID="GridViewMarca" AutoGenerateColumns="false" AutoGenerateSelectButton="true" DataKeyNames="Id" CssClass="table table-bordered" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridViewMarca_PageIndexChanging" OnSelectedIndexChanged="GridViewMarca_SelectedIndexChanged" AutoPostBack="true">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                </Columns>
            </asp:GridView>
            <hr />
            <asp:TextBox ID="txtMarca" runat="server" placeholder="Agregar Marca"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvMarca" runat="server"
                ControlToValidate="txtMarca"
                ErrorMessage="⚠️ Ingresá un nombre de marca."
                Display="Dynamic" CssClass="text-danger" ValidationGroup="Marca" />

            <div class="d-flex flex-wrap gap-1 mt-3">
                <asp:Button ID="btnAgregarM" runat="server" Text="Agregar Marca" OnClick="btnAgregarM_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnModificarM" runat="server" Text="Modificar Marca" OnClick="btnModificarM_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnEliminarM" runat="server" Text="Eliminar Marca" OnClick="btnEliminarM_Click" CssClass="btn btn-danger" />
                <asp:Button ID="btnCancelarM" runat="server" Text="Cancelar " OnClick="btnCancelarM_Click" CssClass="btn btn-secondary" />
                <asp:Label ID="lblMensajeM" runat="server" Text=""></asp:Label>
            </div>
           
        </div>

    </div>

    <hr />



</asp:Content>
