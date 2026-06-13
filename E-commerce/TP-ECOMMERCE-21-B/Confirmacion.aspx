<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Confirmacion.aspx.cs" Inherits="TP_ECOMMERCE_21_B.Confirmacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <h2>¡Gracias por tu compra!</h2>
    <p>Tu pedido fue registrado correctamente. Nos pondremos en contacto para coordinar la entrega.</p>
    <asp:Button ID="btnVolverInicio" runat="server" Text="Volver al catálogo" CssClass="btn btn-primary" OnClick="btnVolverInicio_Click" />
</asp:Content>


