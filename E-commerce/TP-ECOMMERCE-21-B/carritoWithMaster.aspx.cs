using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace TP_ECOMMERCE_21_B
{
    public partial class carritoWithMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                CargarProductos();
               
            }
        }

        private void CargarProductos()
        {
            List<Producto> products = (List<Producto>)Session["items"];

            if (products == null || products.Count == 0)
            {
                textcart.Text = "No tiene articulos en el carrito";
                btnIniciarCompra.Visible= false;
                repRepetidor.DataSource = null;
                repRepetidor.DataBind();

                lblTotalCarrito.Text = "Total de la compra: $0,00";
                return;
            }

            repRepetidor.DataSource = products;
            repRepetidor.DataBind();
            btnIniciarCompra.Visible = true;

            decimal total = products.Sum(p => p.PrecioVenta * p.cantidad);
            lblTotalCarrito.Text = $"Total de la compra: ${total:N2}";
        }


        protected void btnVolverCatalogo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ecommerce.aspx");
        }

        protected void btnIniciarCompra_Click(object sender, EventArgs e)
        {

            Usuario user = (Usuario)Session["usuario"]; 

            if (user != null) { 
            Response.Redirect("FormularioPago.aspx");
            }

            Session["redirectCarrito"] = true;
            Response.Redirect("login.aspx");

        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            var btn = (ImageButton)sender;
            int id = int.Parse(btn.CommandArgument);

            List<Producto> products = (List<Producto>)Session["items"];

            // Eliminar el producto
            Producto item = products.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                
                if (item.cantidad > 1)
                    item.cantidad -= 1;
                else
                    products.Remove(item);
            }

            // Actualizar sesión
            Session["items"] = products;

            // Rebindea
            CargarProductos();
        }
    }
}