<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseFailure.aspx.cs" Inherits="TP_ECOMMERCE_21_B.PurchaseFailure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="color: #c0392b;">❌ Ocurrió un problema con tu pago</h2>
    <p>Tu transacción no fue aprobada. Puede deberse a un error en los datos ingresados o a una falla en la conexión con el procesador de pagos.</p>
    <p>Te recomendamos revisar los datos de tu tarjeta o intentar con otro medio de pago.</p>
    <a href="FormularioPago.aspx" class="btn btn-danger">Volver al formulario de pago</a>

</asp:Content>
