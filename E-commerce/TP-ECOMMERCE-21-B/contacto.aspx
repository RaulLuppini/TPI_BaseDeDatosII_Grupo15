<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="contacto.aspx.cs" Inherits="TP_ECOMMERCE_21_B.contacto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    
        <main class="container py-5">
            <h2>Contacto</h2>
            <p>¿Tenés alguna consulta o sugerencia? Completá el formulario y te responderemos a la brevedad.</p>

            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label for="txtEmail" class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
            </div>

            <div class="mb-3">
                <label for="txtMensaje" class="form-label">Mensaje</label>
                <asp:TextBox ID="txtMensaje" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
            </div>

            <asp:Button ID="btnEnviar" runat="server" Text="Enviar consulta" CssClass="btn btn-primary" OnClick="btnEnviar_Click" />

            <asp:Label ID="lblConfirmacion" runat="server" CssClass="text-success mt-3 d-block" Visible="false" />
        </main>
   

</asp:Content>
