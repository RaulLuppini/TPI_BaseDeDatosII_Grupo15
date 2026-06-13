<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TP_ECOMMERCE_21_B.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <h2 class="text-center mb-4">Iniciar sesión</h2>
                <div class="card p-4 shadow-sm ">
                    <div class="mb-3 d-flex flex-column align-items-center">
                        <asp:TextBox ID="txtEmail" runat="server" Style="width:80%;" CssClass="form-control" Placeholder="Email" />
                    </div>
                    <div class="mb-3 d-flex flex-column align-items-center">
                        <asp:TextBox ID="txtClave" runat="server" Style="width:80%;" CssClass="form-control" TextMode="Password" Placeholder="Contraseña" />
                    </div>
                    <div class="d-grid d-flex flex-column align-items-center">
                        <asp:Button ID="btnLogin" runat="server" Style="width:80%;" Text="Ingresar" CssClass="btn btn-dark" OnClick="btnLogin_Click" />
                    </div>
                    <div class="mt-3 text-center">
                        ¿No tenés cuenta?
                       <a href="<%= ResolveUrl("~/altaUsuario.aspx") %>" class="link-primary">Registrate acá</a>

                    </div>


                    <div class="mt-3 text-center">
                        <asp:Label ID="lblErrorLogin" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>

