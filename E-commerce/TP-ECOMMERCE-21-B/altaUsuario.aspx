<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="altaUsuario.aspx.cs" Inherits="TP_ECOMMERCE_21_B.altaUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <asp:Label ID="lblTitulo" runat="server" CssClass="h2 text-center mb-4" />


                <div class="card p-4 shadow-sm">

                    <asp:ValidationSummary ID="vsErrores" runat="server" CssClass="alert alert-danger" HeaderText="Por favor corregí los siguientes errores:" DisplayMode="BulletList" />

                    <div class="mb-1">
                        <p><strong>E-mail:</strong><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email" /></p>                        
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="El email es obligatorio" CssClass="text-danger" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Formato de email inválido" CssClass="text-danger" Display="Dynamic" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" />
                    </div>

                    <div class="mb-1">
                        <p><strong>Nombre:</strong> <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Placeholder="Nombre" /></p>                       
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="El nombre es obligatorio" CssClass="text-danger" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revNombre" runat="server"
                            ControlToValidate="txtNombre"
                            ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"
                            ErrorMessage="⚠️ Solo se permiten letras en el nombre"
                            ForeColor="Red" Display="Dynamic" />
                    </div>

                    <div class="mb-1">
                        <p><strong>Apellido:</strong> <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" Placeholder="Apellido" /></p>                 
                        <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido" ErrorMessage="El apellido es obligatorio" CssClass="text-danger" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ControlToValidate="txtApellido"
                            ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"
                            ErrorMessage="⚠️ Solo se permiten letras en el Apellido"
                            ForeColor="Red" Display="Dynamic" />
                    </div>

                    <div class="mb-1">
                        <p><strong>DNI:</strong><asp:TextBox ID="txtDni" runat="server" CssClass="form-control" Placeholder="DNI" /></p>                        
                        <asp:RequiredFieldValidator ID="rfvDni" runat="server" ControlToValidate="txtDni" ErrorMessage="El DNI es obligatorio" CssClass="text-danger" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revDni" runat="server" ControlToValidate="txtDni" ErrorMessage="Formato inválido (Debe contener entre 7 y 9 numeros solamente )" CssClass="text-danger" Display="Dynamic" ValidationExpression="^\d{7,8}$" />
                    </div>


                    <div class="mb-1">
                        <p><strong>Direccion:</strong><asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" Placeholder="Dirección" /></p>                 
                        <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" ControlToValidate="txtDireccion" ErrorMessage="La dirección es obligatoria" CssClass="text-danger" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revDireccion" runat="server"
                            ControlToValidate="txtDireccion"
                            ValidationExpression="^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s\.,\-]+$"
                            ErrorMessage="⚠️ La dirección contiene caracteres inválidos."
                            ForeColor="Red" Display="Dynamic" />
                    </div>

                    <div class="mb-1">
                        <p><strong>Codigo Postal:</strong><asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" Placeholder="Código Postal" /></p>                        
                        <asp:RequiredFieldValidator ID="rfvCP" runat="server" ControlToValidate="txtCodigoPostal" ErrorMessage="El código postal es obligatorio" CssClass="text-danger" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revCP" runat="server" ControlToValidate="txtCodigoPostal" ErrorMessage="Solo números (4 a 10 dígitos)" CssClass="text-danger" Display="Dynamic" ValidationExpression="^\d{4,10}$" />
                    </div>

                    <div class="mb-1">
                        <p><strong>Telefono:</strong><asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" Placeholder="Teléfono" /></p>                        
                        <asp:RequiredFieldValidator ID="rfvTel" runat="server" ControlToValidate="txtTelefono" ErrorMessage="El teléfono es obligatorio" CssClass="text-danger" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revTel" runat="server" ControlToValidate="txtTelefono" ErrorMessage="Formato inválido (solo números, guiones o espacios)" CssClass="text-danger" Display="Dynamic" ValidationExpression="^[\d\s\-]{6,20}$" />
                    </div>

                    <div class="mb-1">
                        <p><strong>Contraseña:</strong> <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Contraseña" /></p>                       
                        <asp:RequiredFieldValidator ID="rfvClave" runat="server" ControlToValidate="txtClave" ErrorMessage="La contraseña es obligatoria" CssClass="text-danger" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revClave" runat="server" ControlToValidate="txtClave" ErrorMessage="Debe tener al menos 6 caracteres" CssClass="text-danger" Display="Dynamic" ValidationExpression=".{6,}" />
                    </div>

                    <div class="mb-1">
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control" Visible="false" />
                    </div>

                    <div class="mb-1">
                        <asp:Label ID="lblEstado" runat="server" Text="Estado:" CssClass="form-label" Visible="false" />
                        <asp:CheckBox ID="chkEstado" runat="server" CssClass="form-check-input ms-2" Visible="false" />
                    </div>

                    <div class="d-grid">
                         <asp:Label ID="lblError" runat="server" CssClass="text-danger" />
                        <asp:Button ID="btnAceptar" runat="server" Text="Registrarse" CssClass="btn btn-success" OnClick="btnAceptar_Click" />
                    </div>

                    <asp:PlaceHolder ID="phLinkLogin" runat="server">
                        <div class="mt-3 text-center">
                            ¿Ya tenés cuenta?
                        <a href="Login.aspx" class="link-primary">Iniciar sesión</a>
                        </div>
                    </asp:PlaceHolder>



                    <div class="mt-3 text-center">
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </div>
        </div>
    </main>


</asp:Content>

