<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchasePending.aspx.cs" Inherits="TP_ECOMMERCE_21_B.PurchasePending" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="color: #f39c12;">⏳ Tu pago está pendiente</h2>
    <p>Elegiste un medio de pago que requiere confirmación manual (como PagoFácil, Rapipago o transferencia).</p>
    <p>Una vez que se acredite, te enviaremos la confirmación por e-mail y procesaremos tu pedido.</p>
    <a href="Ecommerce.aspx" class="btn btn-warning">Volver al inicio</a>
    
</asp:Content>
