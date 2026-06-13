<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="altaProducto.aspx.cs" Inherits="TP_ECOMMERCE_21_B.altaProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblTitulo" runat="server" CssClass="h2 text-center mb-4" />

    <div class="row">
        <!-- Columna izquierda -->
        <div class="col-md-6">
            <div class="mb-3">
                <p><strong>Codigo:</strong>  <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" Placeholder="Código" /></p>             
            </div>
            <div class="mb-3">
                <p><strong>Nombre:</strong><asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Placeholder="Nombre" /></p>                
            </div>

            <asp:UpdatePanel ID="updListas" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="mb-3">
                        <p><strong>Categoria:</strong> <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control" /></p>                       
                    </div>
                    <div class="mb-3">
                        <p><strong>Marca:</strong> <asp:DropDownList ID="ddlMarcas" runat="server" CssClass="form-control" /></p>                       
                    </div>
                </ContentTemplate>

            </asp:UpdatePanel>

            <div class="mb-3">
                <p><strong>Descripcion:</strong> <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" Placeholder="Descripción" /></p>   
            </div>
            <div class="mb-3">
                <p><strong>Precio de Compra:</strong> <asp:TextBox ID="txtPrecioCompra" runat="server" CssClass="form-control" Placeholder="Precio Compra" /></p>              
            </div>
            <div class="mb-3">
                <p><strong>Porcentaje de Ganancia:</strong><asp:TextBox ID="txtPorcentajeGanancia" runat="server" CssClass="form-control" ReadOnly="true" /></p>
            </div>
            <div class="mb-3">
                <p><strong>Precio de Venta:</strong>  <asp:TextBox ID="txtPrecioVenta" runat="server" CssClass="form-control" Placeholder="Precio Venta" /></p>              
            </div>
            <div class="mb-3">
                <p><strong>Stock Actual:</strong> <asp:TextBox ID="txtStockActual" runat="server" CssClass="form-control" Placeholder="Stock Actual" /></p>               
            </div>
            <div class="mb-3">
                <p><strong>Stock Minimo:</strong><asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control" Placeholder="Stock Mínimo" /></p>
                
            </div>
            <div class="mb-3">
                <asp:CheckBox ID="CheckBoxEstado" runat="server" Checked="true" Enabled="false" Text="&nbsp;&nbsp;Activo" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblError" runat="server" CssClass="text-danger" />


                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="Aceptar_Click" CssClass="btn btn-success" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="btn btn-secondary" />
            </div>
        </div>

        <!-- Columna derecha -->
        <div class="col-md-6">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>

                    <div class="mb-3">
                        <asp:Label ID="lblErrorImagen" runat="server" CssClass="text-danger mb-2" />
                    </div>
                    <label for="txtUrlImagen" class="form-label"><strong>URL Imagen:</strong></label>
                    <div class="mb-3 d-flex">


                        <asp:TextBox ID="txtUrlImagen" runat="server" AutoPostBack="false" CssClass="form-control me-2" placeholder="Url" />
                        <asp:Button ID="btnVistaPrevia" runat="server" Text="Vista previa" CssClass="btn btn-info me-2" OnClick="btnVistaPrevia_Click" />
                        <asp:Button ID="btnCargar" runat="server" Text="Cargar Imagen" CssClass="btn btn-success" OnClick="btnCargar_Click" />
                    </div>


                    <div class="mb-3">
                        <asp:Image ID="imgPreview" runat="server" Width="200px" Height="200px" CssClass="img-thumbnail" />
                        <asp:Repeater ID="rptImagenes" runat="server" OnItemCommand="rptImagenes_ItemCommand">
                            <ItemTemplate>
                                <div class="d-inline-block text-center me-2">
                                   <img src='<%# Eval("Url") %>' class="img-thumbnail" width="100" onerror="this.onerror=null;this.src='/img/placeholder.jpg';" />


                                    <asp:Button ID="btnModificarImagen" runat="server" Text="Modificar" CommandName="Modificar" CommandArgument='<%# Eval("Url") %>' Visible='<%# Session["modificarId"] != null %>' CssClass="btn btn-sm btn-danger mt-1" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>


                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
