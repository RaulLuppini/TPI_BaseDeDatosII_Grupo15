<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="TP_ECOMMERCE_21_B.WebForm2" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Content/profile.css") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container-infoUser">
    <div id="container-NombreId">
        <strong><asp:label runat="server" ID="nombreUser"></asp:label></strong>
        <asp:label runat="server" ID="IDusuario"></asp:label>
    </div>
        <div id="container-infoDatos">
            <div class="contactoUser">
             <p>Correo: </p>
            <asp:label runat="server"  ID="emailText"></asp:label>
            </div>

            <div class="contactoUser">
                <p>Telefono: </p>
            <asp:Label runat="server" ID="TlfText"> </asp:Label>
                </div>
        </div>

        
     <div id="container-password">

         <div class="div-password">
             <p>Contrasena Actual</p>
             <asp:Textbox runat="server" ID="CurrentPassword" TextMode="Password"> </asp:Textbox>
         </div>

         <div class="div-password">
              <p>Nueva Contrasena</p>
          <asp:Textbox runat="server" ID="NewPassword" TextMode="Password"> </asp:Textbox>
         </div>

         <asp:Button ID="changePassword" runat="server" Text="Confirmar" CssClass="btn btn-primary" Onclick="changePassword_Click"/>
     </div>

</div>
     <script src="<%= ResolveUrl("~/Scripts/WebForms/profile.js") %>"></script>
</asp:Content>
