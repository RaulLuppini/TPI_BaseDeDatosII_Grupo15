using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_ECOMMERCE_21_B
{
    public partial class altaProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("login.aspx", false);
            }

            if (!IsPostBack)
            {
                bool esModificacion = Session["modificarId"] != null;
                lblTitulo.Text = esModificacion ? "Modificar Producto" : "Alta de Producto";
                btnAceptar.Text = esModificacion ? "Guardar Cambios" : "Aceptar";



                negocioMarca negocio = new negocioMarca();
                List<Marca> marcas = negocio.listar();
                ddlMarcas.DataSource = marcas;
                ddlMarcas.DataTextField = "Nombre";
                ddlMarcas.DataValueField = "Id";
                ddlMarcas.DataBind();



                negocioCategoria negocioCategoria = new negocioCategoria();
                List<Categoria> categorias = negocioCategoria.listarCategoria();
                ddlCategoria.DataSource = categorias;
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataBind();



                if (Session["modificarId"] != null)
                {
                    int id = (int)Session["modificarId"];
                    negocioProducto negocioProducto = new negocioProducto();
                    Producto producto = negocioProducto.obtenerPorId(id);

                    if (producto != null)
                    {
                        txtCodigo.Text = producto.Codigo;
                        txtNombre.Text = producto.Nombre;
                        txtDescripcion.Text = producto.Descripcion;
                        txtPrecioCompra.Text = producto.PrecioCompra.ToString();
                        txtPorcentajeGanancia.Text = producto.PorcentajeGanancia.ToString("F2") + " %";


                        txtPrecioVenta.Text = producto.PrecioVenta.ToString();
                        txtStockActual.Text = producto.StockActual.ToString();
                        txtStockMinimo.Text = producto.StockMinimo.ToString();
                        CheckBoxEstado.Checked = producto.Estado;

                        
                        ddlMarcas.SelectedValue = producto.IdMarca.Id.ToString();
                        ddlCategoria.SelectedValue = producto.IdCategoria.Id.ToString();
                        negocioImagen negocioImg = new negocioImagen();
                        List<Imagen> imagenes = negocioImg.ObetenerimagenesId(producto.Id);

                        Session["imagenes"] = imagenes.Select(i => i.Url).ToList();
                        rptImagenes.DataSource = imagenes;
                        rptImagenes.DataBind();

                    }
                }
            }

        }

        protected void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                    return;


                if (string.IsNullOrWhiteSpace(txtCodigo.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    lblError.Text = "⚠️ Código y Nombre son obligatorios.";
                    return;
                }

                if (!decimal.TryParse(txtPrecioCompra.Text, out decimal precioCompra) ||
                    //!decimal.TryParse(txtPorcentajeGanancia.Text, out decimal porcentajeGanancia) ||
                    !decimal.TryParse(txtPrecioVenta.Text, out decimal precioVenta))
                {
                    lblError.Text = "⚠️ Los precios y porcentaje deben ser valores numéricos.";
                    return;
                }
                if (precioVenta < precioCompra)
                {
                    lblError.Text = "⚠️ El precio de venta no puede ser menor al de compra.";
                    return;
                }

                if (!int.TryParse(txtStockActual.Text, out int stockActual) ||
                    !int.TryParse(txtStockMinimo.Text, out int stockMinimo))
                {
                    lblError.Text = "⚠️ El stock debe ser un número entero.";
                    return;
                }

               
                if (string.IsNullOrWhiteSpace(ddlMarcas.SelectedValue) || !int.TryParse(ddlMarcas.SelectedValue, out int idMarca))
                {
                    lblError.Text = "⚠️ La marca seleccionada no es válida.";
                    return;
                }

                if (string.IsNullOrWhiteSpace(ddlCategoria.SelectedValue) || !int.TryParse(ddlCategoria.SelectedValue, out int idCategoria))
                {
                    lblError.Text = "⚠️ La categoría seleccionada no es válida.";
                    return;
                }

                
                List<string> imagenes = Session["imagenes"] as List<string> ?? new List<string>();
                if (Session["modificarId"] == null && imagenes.Count < 1)
                {
                    lblError.Text = "⚠️ El producto debe tener al menos una imagen cargada.";
                    return;
                }

               
                Producto nuevo = new Producto
                {
                    Codigo = txtCodigo.Text,
                    Nombre = txtNombre.Text,
                    IdMarca = new Marca(idMarca, ddlMarcas.SelectedItem.Text),
                    IdCategoria = new Categoria(idCategoria, ddlCategoria.SelectedItem.Text),
                    Descripcion = txtDescripcion.Text,
                    PrecioCompra = precioCompra,
                    //PorcentajeGanancia = porcentajeGanancia,
                    PrecioVenta = precioVenta,
                    StockActual = stockActual,
                    StockMinimo = stockMinimo,
                    Estado = CheckBoxEstado.Checked
                };

                negocioProducto negocio = new negocioProducto();

                if (Session["modificarId"] != null)
                {
                    nuevo.Id = (int)Session["modificarId"];
                    negocio.modificarProducto(nuevo);
                    negocioImagen negocioImg = new negocioImagen();
                    negocioImg.eliminarPorProducto(nuevo.Id);
                    List<Imagen> listaImagenes = imagenes.Select(url => new Imagen { Url = url,IdProducto=nuevo.Id }).ToList();
                    negocioImg.agregarImagenes(listaImagenes);
                    Session.Remove("modificarId");

                }
                else
                {
                    lblError.Text = $"DEBUG → MarcaId: {ddlMarcas.SelectedValue}, CategoriaId: {ddlCategoria.SelectedValue}";


                    negocio.agregarProducto(nuevo);

                    // Asociar imágenes
                    negocioImagen negocioImg = new negocioImagen();
                    List<Imagen> listaImagenes = imagenes.Select(url => new Imagen { Url = url, IdProducto = nuevo.Id }).ToList();
                    negocioImg.agregarImagenes(listaImagenes);
                }

                Session.Remove("imagenes");
                Response.Redirect("gestionProductos.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = "❌ Error al guardar: " + ex.Message;
            }
        }

        
        


        protected void actualizarPreview(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (!string.IsNullOrWhiteSpace(txt.Text))
                imgPreview.ImageUrl = txt.Text;
            else
                imgPreview.ImageUrl = string.IsNullOrWhiteSpace(txt.Text) ? "~/img/placeholder.jpg" : txt.Text;

        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            List<string> imagenes = Session["imagenes"] as List<string> ?? new List<string>();
            string url = txtUrlImagen.Text.Trim();
            
            if (string.IsNullOrWhiteSpace(url) || !Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                imgPreview.ImageUrl = "~/img/placeholder.jpg";
                lblErrorImagen.CssClass = "text-danger mb-2";
                lblErrorImagen.Text = "⚠️ La URL no es válida. No se agregó ninguna imagen.";
                return; 
            }

           
            if (imagenes.Contains(url))
            {
                lblErrorImagen.CssClass = "text-warning mb-2";
                lblErrorImagen.Text = "⚠️ Esa imagen ya fue cargada.";
                return;
            }

            if (Session["imagenEditando"] != null)
            {
                string urlVieja = Session["imagenEditando"].ToString();
                int index = imagenes.IndexOf(urlVieja);
                if (index >= 0)
                {
                    imagenes[index] = url;
                }
                Session.Remove("imagenEditando");
                lblErrorImagen.CssClass = "text-success mb-2";
                lblErrorImagen.Text = "✅ Imagen modificada.";
            }
            else
            {
                imagenes.Add(url);
                lblErrorImagen.CssClass = "text-success mb-2";
                lblErrorImagen.Text = $"✅ Imagen agregada ({imagenes.Count}).";
            }
            Session["imagenes"] = imagenes;
            rptImagenes.DataSource = imagenes.Select(i => new Imagen { Url = i });
            rptImagenes.DataBind();

            txtUrlImagen.Text = "";
            imgPreview.ImageUrl = "~/img/placeholder.jpg";
            
        }

        private bool EsUrlImagenValida(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            // Verifica que sea una URL bien formada
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return false;

            // Opcional: restringir a http/https
            Uri uri = new Uri(url);
            if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
                return false;

            return true;
        }

        protected void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            string url = txtUrlImagen.Text.Trim();

            if (!EsUrlImagenValida(url))
            {
                imgPreview.ImageUrl = "~/img/placeholder.jpg";
                lblErrorImagen.CssClass = "text-danger mb-2";
                lblErrorImagen.Text = "⚠️ La URL no es válida. Se muestra imagen por defecto.";
            }
            else
            {
                // Validar que apunte a una extensión de imagen común
                string[] extensiones = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                if (extensiones.Any(ext => url.ToLower().EndsWith(ext)))
                {
                    imgPreview.ImageUrl = url;
                    lblErrorImagen.Text = "";
                }
                else
                {
                    imgPreview.ImageUrl = "~/img/placeholder.jpg";
                    lblErrorImagen.CssClass = "text-warning mb-2";
                    lblErrorImagen.Text = "⚠️ La URL no parece ser una imagen. Se muestra placeholder.";
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("gestionProductos.aspx");
        }

        protected void rptImagenes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "Modificar")
            {
                string url = e.CommandArgument.ToString();

                Session["imagenEditando"] = url;
                txtUrlImagen.Text = url;
                imgPreview.ImageUrl = url;
                lblErrorImagen.Text = "✏️ Editá la URL y volvé a cargarla.";
            }
        }
    }
}