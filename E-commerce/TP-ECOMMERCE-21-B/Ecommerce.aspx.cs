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
    public partial class Ecommerce : Page
    {
        public List<Producto> listaProducto
        {
            get { return Session["listaProducto"] as List<Producto>; }
            set { Session["listaProducto"] = value; }
        }
        private void BindRepeater(List<Producto> productos)
        {
            RepeaterProducto.DataSource = productos;
            RepeaterProducto.DataBind();
        }



        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                negocioProducto productoNegocio = new negocioProducto(); 
                listaProducto = productoNegocio.listar(); 
                negocioCategoria categoriaNegocio = new negocioCategoria(); 
                ddlFiltroCategoria.DataSource = categoriaNegocio.listarCategoria(); 
                ddlFiltroCategoria.DataTextField = "Nombre"; 
                ddlFiltroCategoria.DataValueField = "Nombre"; 
                ddlFiltroCategoria.DataBind(); 
                ddlFiltroCategoria.Items.Insert(0, new ListItem("Todas las categorías", "")); 
                negocioMarca marcaNegocio = new negocioMarca(); 
                ddlFiltroMarca.DataSource = marcaNegocio.listar(); 
                ddlFiltroMarca.DataTextField = "Nombre"; 
                ddlFiltroMarca.DataValueField = "Nombre"; 
                ddlFiltroMarca.DataBind(); 
                ddlFiltroMarca.Items.Insert(0, new ListItem("Todas las marcas", "")); 
                Session["cantidadMostrar"] = 9; 
                int cantidad = (int)Session["cantidadMostrar"]; 
                var productosLimitados = listaProducto.Take(cantidad).ToList(); 
                BindRepeater(productosLimitados); 
                btnMostrarMas.Visible = listaProducto.Count > cantidad;

            }
        }
        protected void btnComprar_Command(object sender, CommandEventArgs e)
        {
            int idProducto = Convert.ToInt32(e.CommandArgument);

            Usuario usuario = (Usuario)Session["usuario"];

            if (usuario != null && usuario.RolUsuario == "admin")
            {
                Response.Redirect("gestionProductos.aspx", false);
            }
            else
            {
                Response.Redirect("product.aspx?id=" + idProducto, false);
            }
        }



        protected void btnFiltrarProducto_Click(object sender, EventArgs e)
        {
            string filtroTexto = txtFiltroProducto.Text.Trim().ToLower();
            string filtroCategoria = ddlFiltroCategoria.SelectedValue;
            string filtroMarca = ddlFiltroMarca.SelectedValue;

            var productos = listaProducto;

            var listaFiltrada = productos.Where(p =>
                (string.IsNullOrEmpty(filtroTexto) ||
                 (p.Nombre != null && p.Nombre.ToLower().Contains(filtroTexto)) ||
                 (p.Codigo != null && p.Codigo.ToLower().Contains(filtroTexto))) &&
                (string.IsNullOrEmpty(filtroCategoria) || p.IdCategoria.Nombre == filtroCategoria) &&
                (string.IsNullOrEmpty(filtroMarca) || p.IdMarca.Nombre == filtroMarca)).ToList();

           
            bool filtrosVacios = string.IsNullOrEmpty(filtroTexto) && string.IsNullOrEmpty(filtroCategoria) && string.IsNullOrEmpty(filtroMarca);
            Session["listaFiltrada"] = filtrosVacios ? null : listaFiltrada;

            int cantidad = (int)Session["cantidadMostrar"];
            var productosLimitados = (filtrosVacios ? listaProducto : listaFiltrada).Take(cantidad).ToList();

            BindRepeater(productosLimitados);
            btnMostrarMas.Visible = (filtrosVacios ? listaProducto.Count : listaFiltrada.Count) > cantidad;
        }

        protected void btnMostrarMas_Click(object sender, EventArgs e)
        {
            int cantidadActual = (int)Session["cantidadMostrar"];
            cantidadActual += 9;
            
            Session["cantidadMostrar"] = cantidadActual;
            var productosBase = Session["listaFiltrada"] as List<Producto> ?? listaProducto;
            var productosMostrados = productosBase.Take(cantidadActual).ToList();
            BindRepeater(productosMostrados);

            btnMostrarMas.Visible = productosBase.Count > cantidadActual;
        }
    }
}