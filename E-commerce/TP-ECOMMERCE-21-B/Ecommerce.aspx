<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ecommerce.aspx.cs" Inherits="TP_ECOMMERCE_21_B.Ecommerce" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Content/default.css") %>" rel="stylesheet" />
</asp:Content>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %></h2>
        

        <div class="mb-3 d-flex flex-wrap gap-2">
            <asp:TextBox ID="txtFiltroProducto" runat="server" CssClass="form-control" placeholder="Buscar por nombre o código..." />
            <asp:DropDownList ID="ddlFiltroCategoria" runat="server" CssClass="form-select">
                <asp:ListItem Text="Todas las categorías" Value="" />
            </asp:DropDownList>
            <asp:DropDownList ID="ddlFiltroMarca" runat="server" CssClass="form-select">
                <asp:ListItem Text="Todas las marcas" Value="" />
            </asp:DropDownList>
            <asp:Button ID="btnFiltrarProducto" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnFiltrarProducto_Click" />
        </div>


        <div class="mb-3 carousel slide carousel-fixed-height bg-dark"
            id="carouselExampleControlsNoTouching"
            data-bs-touch="false"
            data-bs-ride="carousel"
            data-bs-interval="2000">
            <div class="carousel-inner">
                <div class="carousel-item active">
                    <img src="https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcS-16UNOVHe5nIZWa7zLh-ex6terwMLo5nz58LivszmFGL-38kQENQi49HzUOnsRcieNGQ4gL-e0z0CKl7mDYIGtyVs5zivW1MUFfH5R4E"
                        style="background-color: black; display: block; margin: 0 auto; max-height: 300px; object-fit: contain;" alt="...">
                </div>
                <div class="carousel-item">
                    <img src="https://www.megatone.net/images/Articulos/zoom2x/253/MKT0579LTA-1.jpg"
                        style="background-color: black; display: block; margin: 0 auto; max-height: 300px; object-fit: contain;" alt="...">
                </div>
                <div class="carousel-item">
                    <img src="https://www.megatone.net/images/Articulos/zoom2x/200/03/MKT0114DIN_3.jpg"
                        style="background-color: black; display: block; margin: 0 auto; max-height: 300px; object-fit: contain;" alt="...">
                </div>
            </div>
        </div>



        <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">

            <asp:Repeater ID="RepeaterProducto" runat="server">
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100">
                            <div id="carousel<%# Eval("Id") %>" class="carousel slide" data-bs-ride="carousel" aria-label="Imágenes del producto">
                                <div class="carousel-inner">
                                    <asp:Repeater ID="RepeaterImagenes" runat="server" DataSource='<%# Eval("Imagenes") %>'>
                                        <ItemTemplate>
                                            <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
                                                <img src='<%# Eval("Url") %>' class="d-block w-100 img-fluid" style="max-height: 300px; object-fit: cover;" alt="Imagen del producto" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <button class="carousel-control-prev" type="button" data-bs-target="#carousel<%# Eval("Id") %>" data-bs-slide="prev">
                                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                    <span class="visually-hidden">Anterior</span>
                                </button>
                                <button class="carousel-control-next" type="button" data-bs-target="#carousel<%# Eval("Id") %>" data-bs-slide="next">
                                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                    <span class="visually-hidden">Siguiente</span>
                                </button>
                            </div>

                            <div class="card-body">
                                <h5 class="card-Nombre"><%# Eval("Nombre") %></h5>
                                <p class="card-Descripcion"><%# Eval("Descripcion") %></p>
                                <p class="card-Precio"><%# string.Format("${0:N2}", Eval("PrecioVenta")) %></p>
                                <p class="card-StockActual">Stock disponible: <%# Eval("StockActual") %></p>



                                <asp:Button ID="btnComprar" runat="server" Text='<%# Convert.ToInt32(Eval("StockActual")) <=0 ? "Sin stock" : "Comprar ahora" %>' Enabled='<%# Convert.ToInt32(Eval("StockActual")) > 0 %>'
                                    
                                    CssClass="btn btn-primary w-100"
                                    CommandArgument='<%# Eval("Id") %>' CommandName="ComprarAhora" OnCommand="btnComprar_Command" />

                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="text-center">
            <asp:Button ID="btnMostrarMas" runat="server" Text="Mostrar más productos" CssClass="btn btn-dark" OnClick="btnMostrarMas_Click" />
        </div>
    </main>
    <footer class="bg-dark text-light py-4 mt-5 w-100">
        <div class="container-fluid px-5">
            <div class="row">

                <div class="col-md-3 mb-3">
                    <h5>Navegación</h5>
                    <ul class="list-unstyled">
                        <li><a href="Ecommerce.aspx" class="text-light">Inicio</a></li>  
                        <li><a href="contacto.aspx" class="text-light">Soporte</a></li>
                    </ul>
                </div>


                <div class="col-md-3 mb-3">
                    <h5>Medios de pago</h5>
                    <p>Visa, Mastercard, American Express, MercadoPago</p>
                </div>


                <div class="col-md-3 mb-3">
                    <h5>Envíos</h5>
                    <p>Correo Argentino, OCA, Retiro en tienda</p>
                </div>


                <div class="col-md-3 mb-3">
                    <h5>Contacto</h5>
                    <p>📞 +54 9 11 5107 6003</p>
                    <p>📧 EcommerceSignos@gmail.com</p>
                    <p>📍 Av. Corrientes 791, CABA</p>
                    <div>
                        <a href="#" class="text-light me-2">Instagram</a>
                        <a href="#" class="text-light">Facebook</a>
                    </div>
                </div>
            </div>
        </div>
    </footer>
</asp:Content>
