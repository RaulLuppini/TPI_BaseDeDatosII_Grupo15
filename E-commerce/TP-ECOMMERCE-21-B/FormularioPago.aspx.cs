using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_ECOMMERCE_21_B
{
    public partial class FormularioPago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            


            if (!IsPostBack)
            {
                string tipo = rbEnvio.SelectedValue;
                panelDireccion.Visible = tipo == "Retiro";
                panelTelefono.Visible = tipo == "Coordinar";

                
                
                    panelTarjeta.Visible = false;
                    panelTransferencia.Visible = false;
                    panelMercadoPago.Visible = false;
                


                Usuario usuario = Session["usuario"] as Usuario;
                if (usuario != null)
                {
                    txtNombre.Text = usuario.Nombre;
                    txtApellido.Text = usuario.Apellido;
                    txtEmail.Text = usuario.Email;
                }

                List<Producto> carrito = Session["items"] as List<Producto>;
                if (carrito != null && carrito.Count > 0)
                {
                    RepeaterCarrito.DataSource = carrito;
                    RepeaterCarrito.DataBind();
                    decimal total = carrito.Sum(p => p.PrecioVenta * p.cantidad);
                    lblTotal.Text = $"Total a pagar: ${total}";
                }
                else
                {
                    lblTotal.Text = "No hay productos en el carrito.";
                }
            }



        }

        protected void btnConfirmarPago_Click(object sender, EventArgs e)
        {
            List<Producto> carrito = Session["items"] as List<Producto>;
            if (carrito == null || carrito.Count == 0)
            {
                Response.Redirect("carritoWithMaster.aspx");
                return;
            }

            negocioPedido np = new negocioPedido();
            Pedido pedido = new Pedido();
            Usuario user = (Usuario)Session["usuario"];
            pedido.IdUsuario = user.Id;
            pedido.PrecioTotal= carrito.Sum(p => p.PrecioVenta * p.cantidad);

            if (pedido.PrecioTotal <= 0)
            {
                labelMessage.Text = "⚠️ El precio total no fue calculado correctamente.";
                return;
            }

            //pedido.Estado = "Pagado";
            pedido.MetodoDePago = rbPago.SelectedValue;

            int numPedido = np.AgregarPedido(pedido);

            pedido.DetallePedidos = getDetallePedido(numPedido);
            foreach(var detalle in pedido.DetallePedidos)
            {
              bool response =  np.AgregarDetalleDePedido(detalle);

                if (!response)
                {


                np.verificarDetallePedido(numPedido, response);
                }
            }

            if (rbPago.SelectedValue == "mercadopago")
            {
                string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
                service.MercadoPago mpService = new service.MercadoPago(baseUrl);

                string initPoint = mpService.CrearPreferencia(
                    "Compra en SIGNOS",
                    1,
                    pedido.PrecioTotal,
                    $"{baseUrl}/PurchaseConfirmation.aspx?id={numPedido}"
                );

               
                Session["items"] = null;

                Response.Redirect(initPoint);
                return;
            }

            try
            {
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                string email = txtEmail.Text;
                string metodoPago = rbPago.SelectedValue;

                service.emailService servicioEmail = new service.emailService();
                string asunto = "Confirmación de compra - E-commerce SIGNOS";

                StringBuilder cuerpo = new StringBuilder();
                cuerpo.AppendLine($"<h1>✔ Gracias por tu compra, {nombre}!</h1>");
                cuerpo.AppendLine("<h2>Resumen del pedido:</h2>");
                cuerpo.AppendLine("<ul>");
                foreach (var p in carrito)
                {
                    cuerpo.AppendLine($"<li>{p.Nombre} x{p.cantidad} = ${p.PrecioVenta * p.cantidad}</li>");
                }
                cuerpo.AppendLine("</ul>");
                cuerpo.AppendLine($"<h3>Total: ${pedido.PrecioTotal}</h3>");
                cuerpo.AppendLine($"<p>Método de pago: {metodoPago}</p>");
                cuerpo.AppendLine("<hr />");
                cuerpo.AppendLine("<p>Tu pedido está siendo procesado. ¡Gracias por confiar en SIGNOS!</p>");

                servicioEmail.armarCorreo(email, asunto, cuerpo.ToString());
                servicioEmail.enviarMail();
            }
            catch (Exception ex)
            {
               
                throw ex;
            }

            Session["items"] = null;
            Response.Redirect("Confirmacion.aspx");
        }

        protected List<DetallePedido> getDetallePedido(int numPedido)
        { 
            List<DetallePedido> detallePedidos = new List<DetallePedido>();
            List<Producto> items = (List<Producto>)Session["items"];
           
           /* if (items.Count < 0 || items == null) {
                return null;
            }*/
            if (items == null || items.Count == 0)
            {
                return new List<DetallePedido>();
            }

            foreach (var item in items) { 
                DetallePedido detalleP = new DetallePedido();
                detalleP.idProducto=item.Id;
                detalleP.idPedido = numPedido;
                detalleP.cantidadProducto = item.cantidad;
                detalleP.precioUnitario = item.PrecioVenta;
                detalleP.precioRebajado = 0;
                detallePedidos.Add(detalleP);
            }
            return detallePedidos;
        }


        protected void rbPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            string metodo = rbPago.SelectedValue;

            panelTarjeta.Visible = metodo == "tarjeta";
            panelTransferencia.Visible = metodo == "transferencia";
            panelMercadoPago.Visible = metodo == "mercadopago";
        }
        protected void rbEnvio_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo = rbEnvio.SelectedValue;

            panelDireccion.Visible = tipo == "Retiro";
            panelTelefono.Visible = tipo == "Coordinar";
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ecommerce.aspx");
        }
    }
}