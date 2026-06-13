<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioPago.aspx.cs" Inherits="TP_ECOMMERCE_21_B.FormularioPago" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-md-6">
                <h1>Detalle productos</h1>
                <asp:Label runat="server" ID="labelMessage" />
                <asp:Repeater ID="RepeaterCarrito" runat="server">
                    <ItemTemplate>
                        <div class="div-contenedor-producto">
                            <input type="hidden" name="hfId" class="hfId" value="" />
                            <div class="div-img-carrito">
                                <img src="<%# Eval("Imagenes[0].Url") %>" style="max-width: 100px; height: auto;" />

                            </div>

                            <div class="div-producto-info">
                                <p><%# Eval("Nombre") %></p>
                                <p><%# Eval("Descripcion") %></p>
                                <strong>
                                    <label>Cantidad:</label>
                                </strong>
                                <p><%# Eval("cantidad")%></p>
                            </div>

                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <hr />
                <asp:Label ID="lblTotal" runat="server" CssClass="total-pago" />
            </div>


            <div class="col-md-6">
                <h4>Formulario de pago</h4>

                <div class="mb-3">
                    <label for="rbEnvio">Tipo de envío</label>

                    <asp:RadioButtonList ID="rbEnvio" runat="server" CssClass="form-check" AutoPostBack="true" OnSelectedIndexChanged="rbEnvio_SelectedIndexChanged">
                        <asp:ListItem Text="Retiro por el local" Value="Retiro" />
                        <asp:ListItem Text="Coordinar entrega con el vendedor" Value="Coordinar" />
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="rfvEnvio" runat="server"
                        ControlToValidate="rbEnvio"
                        InitialValue=""
                        ErrorMessage="⚠️ Debés seleccionar un tipo de envío."
                        Display="Dynamic"
                        CssClass="text-danger" />

                    <asp:Panel ID="panelDireccion" runat="server" Visible="false">
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" Placeholder="Dirección del local" ReadOnly="true" Text="Av. Siempreviva 123" />
                    </asp:Panel>


                    <asp:Panel ID="panelTelefono" runat="server" Visible="false">
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" Placeholder="Teléfono de contacto" />
                    </asp:Panel>
                </div>
                <hr />
                <hr />


                <div class="mb-3">
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Placeholder="Nombre" />
                </div>
                <div class="mb-3">
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" Placeholder="Apellido" />
                </div>
                <div class="mb-3">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email" TextMode="Email" />
                </div>



                <div class="mb-3">
                    <label for="rbPago">Método de pago</label>
                    <asp:RadioButtonList ID="rbPago" runat="server" AutoPostBack="false" OnSelectedIndexChanged="rbPago_SelectedIndexChanged">
                        <asp:ListItem Text="Tarjeta de crédito" Value="tarjeta" />
                        <asp:ListItem Text="Transferencia bancaria" Value="transferencia" />
                        <asp:ListItem Text="MercadoPago" Value="mercadopago" />
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="rfvMetodoPago" runat="server"
                        ControlToValidate="rbPago"
                        InitialValue=""
                        ErrorMessage="⚠️ Seleccioná un método de pago."
                        Display="Dynamic"
                        CssClass="text-danger" />
                </div>

                <asp:Panel ID="panelTarjeta" runat="server" Visible="false">
                    <asp:TextBox ID="txtNumeroTarjeta" runat="server" Placeholder="Número de tarjeta" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="reqNumeroTarjeta" runat="server"
                        ControlToValidate="txtNumeroTarjeta"
                        ErrorMessage="⚠️ El número de tarjeta es obligatorio."
                        CssClass="text-danger" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="regexNumeroTarjeta" runat="server"
                        ControlToValidate="txtNumeroTarjeta"
                        ValidationExpression="^\d{13,19}$"
                        ErrorMessage="⚠️ El número de tarjeta debe tener entre 13 y 19 dígitos."
                        CssClass="text-danger" Display="Dynamic" />
                    <asp:TextBox ID="txtNombreTitular" runat="server" Placeholder="Nombre del titular" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="reqNombreTitular" runat="server"
                        ControlToValidate="txtNombreTitular"
                        ErrorMessage="⚠️ El nombre del titular es obligatorio."
                        CssClass="text-danger" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="regexNombreTitular" runat="server"
                        ControlToValidate="txtNombreTitular"
                        ValidationExpression="^[a-zA-Z\s]+$"
                        ErrorMessage="⚠️ Solo se permiten letras y espacios."
                        CssClass="text-danger" Display="Dynamic" />
                    <asp:TextBox ID="txtVencimiento" runat="server" Placeholder="MM/AA" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="reqVencimiento" runat="server"
                        ControlToValidate="txtVencimiento"
                        ErrorMessage="⚠️ La fecha de vencimiento es obligatoria."
                        CssClass="text-danger" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="regexVencimiento" runat="server"
                        ControlToValidate="txtVencimiento"
                        ValidationExpression="^(0[1-9]|1[0-2])\/\d{2}$"
                        ErrorMessage="⚠️ Formato inválido. Use MM/AA."
                        CssClass="text-danger" Display="Dynamic" />
                    <asp:TextBox ID="txtCVV" runat="server" Placeholder="CVV" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="reqCVV" runat="server"
                        ControlToValidate="txtCVV"
                        ErrorMessage="⚠️ El CVV es obligatorio."
                        CssClass="text-danger" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="regexCVV" runat="server"
                        ControlToValidate="txtCVV"
                        ValidationExpression="^\d{3,4}$"
                        ErrorMessage="⚠️ El CVV debe tener 3 o 4 dígitos."
                        CssClass="text-danger" Display="Dynamic" />
                </asp:Panel>

                <asp:Panel ID="panelTransferencia" runat="server" Visible="false">
                    <p>CBU: 0000003100012345678901</p>
                    <p>Alias: SIGNOS.ECOMMERCE</p>
                </asp:Panel>

                <asp:Panel ID="panelMercadoPago" runat="server" Visible="false">
                    <p>Serás redirigido a MercadoPago para completar el pago.</p>
                </asp:Panel>



                <div class="mb-3">
                    <asp:Button ID="btnConfirmarPago" runat="server" CssClass="btn btn-primary" Text="Confirmar pago" OnClick="btnConfirmarPago_Click" CausesValidation="true" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

