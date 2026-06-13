using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGrease.Css.Ast.Selectors;

namespace TP_ECOMMERCE_21_B
{
    public partial class productoUnico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!IsPostBack)
            {
                labelMessage.Visible = false;
                string idproduct= Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idproduct))
                {
                    int id = int.Parse(idproduct);
                    labelHidden.Value=id.ToString();
                    Producto p = getCurrentproduct(id);
                    ttlp.Text = String.Format(p.Nombre);
                    labelDescripcionText.Text = String.Format(p.Descripcion);
                    labelPrecioNormal.Text = String.Format(p.PrecioVenta.ToString());
                    imgProducto.ImageUrl = p.Imagenes[0].Url;
                    numLabel.Text = "0";
                }
            }
        }

        protected void click_buttonRest(object sender, EventArgs e)
        {

            if (!IsPostBack) { 
            
            }

            int num = int.Parse(numLabel.Text);

            if( num <= 0 || string.IsNullOrEmpty(numLabel.Text))
            {
           

            }
            else
            {
                buttonRest.Enabled= true;
                int rest = num - 1;
                string newvalue=rest.ToString();
                numLabel.Text = newvalue;
            }
        }

        protected void click_buttonPlus(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

            }

            int num = int.Parse(numLabel.Text);

            if (num < 0 || string.IsNullOrEmpty(numLabel.Text))
            {
           

            }
            else
            {
                buttonPlus.Enabled = true;
                int rest = num + 1;
                string newvalue = rest.ToString();
                numLabel.Text = newvalue;
            }

        }

        protected void click_buttonAdd(object sender, EventArgs e)
        {
            int num = int.Parse(numLabel.Text);
            if (num <= 0)
            {
                ShowMessage("No puede ser 0 la cantidad del producto", false);
                return;

            }
           
          bool res = addProductToCar();
            if (res) {
                ShowMessage("Se ha agregado tu producto al carrito", true);
            }
        }


        protected bool addProductToCar()
        {
            // Obtener lista de Session
            List<Producto> items = Session["items"] as List<Producto>;

            // Crear producto nuevo
            Producto product = getNewProduct();

            // Si la lista NO existe → crearla
            if (items == null)
            {
                items = new List<Producto>();
                items.Add(product);
                Session["items"] = items;
                Response.Redirect("carritoWithMaster.aspx");
                return true;
            }

            // Si la lista sí existe → buscar si el producto ya está
            bool encontrado = false;

            foreach (Producto item in items)
            {
                if (item.Id == product.Id)
                {
                    item.cantidad += product.cantidad; // sumar cantidad
                    encontrado = true;
                    break;
                }
            }

            // Si no está → agregarlo
            if (!encontrado)
            {
                items.Add(product);
               
            }

            // Guardar cambios
            if (items.Count <= 0)
            {
                return false;
            }
            Session["items"] = items;
                return true;
        }



        protected Producto getCurrentproduct(int id)
        {
            negocioProducto negoProduct = new negocioProducto();
            Producto product = negoProduct.obtenerPorId(id);
            if(product != null)
            {
                return product;
            }
            return null;
        }

        protected Producto getNewProduct()
        {
            Producto product = new Producto();
            product.Id = int.Parse(labelHidden.Value);
            product.Nombre = ttlp.Text;
            product.Descripcion = labelDescripcionText.Text;
            product.PrecioVenta = decimal.Parse(labelPrecioNormal.Text);
            product.cantidad = int.Parse(numLabel.Text);
            product.Imagenes = new List<Imagen>();
            Imagen img = new Imagen();
            img.Url = imgProducto.ImageUrl;
            product.Imagenes.Add(img);

            return product;
        }

        protected void ShowMessage(string message, bool success)
        {
            // Color según estado
            string cssClass = success ? "message-box message-success" : "message-box message-error";

            labelMessage.Text = message;
            labelMessage.CssClass = cssClass;
            labelMessage.Visible = true;

            // Script para mostrar y desaparecer el mensaje
            string script = @"
        setTimeout(function() {
            var lbl = document.getElementById('" + labelMessage.ClientID + @"');
            if(lbl){
                lbl.style.opacity = '1';
                setTimeout(function(){
                    lbl.style.opacity = '0';
                }, 2500);
            }
        }, 100);";

            ScriptManager.RegisterStartupScript(this, GetType(), "msg" + Guid.NewGuid(), script, true);
        }

        protected void btnSeguirComprando_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ecommerce.aspx");
        }

        protected void btnIrAlCarrito_Click(object sender, EventArgs e)
        {
            Response.Redirect("carritoWithMaster.aspx");
        }
    }
}