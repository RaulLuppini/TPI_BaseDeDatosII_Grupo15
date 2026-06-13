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
    public partial class PurchaseConfirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idPedido;
                if (int.TryParse(Request.QueryString["id"],out idPedido))
                {
                    negocioPedido np = new negocioPedido();
                    Pedido pedido = np.ObtenerPedidoConUsuario(idPedido);
                    lblMensaje.Text = $"Pedido #{pedido.Id} confirmado correctamente";

                    RepeaterResumen.DataSource = pedido.DetallePedidos;
                    RepeaterResumen.DataBind();
                    lblTotal.Text =$"Total: ${pedido.PrecioTotal}";

                    Usuario user = new negocioUsuario().buscarPorId(pedido.IdUsuario); // <- en vez de Session["usuario"]

                    service.emailService servicioEmail = new service.emailService();
                    string asunto = "Confirmación de compra - E-commerce SIGNOS";
                    StringBuilder cuerpo = new StringBuilder();
                    cuerpo.AppendLine($"<h1>✔ Gracias por tu compra, {user.Nombre} {user.Apellido}!</h1>");
                    cuerpo.AppendLine("<h2>Resumen del pedido:</h2><ul>");
                    foreach(var d in pedido.DetallePedidos)
                    {
                        cuerpo.AppendLine($"<li>{d.nombreProducto} x{d.cantidadProducto} = ${d.SubTotal}</li>");
                    }
                    cuerpo.AppendLine("</ul>");
                    cuerpo.AppendLine($"<h3>Total: ${pedido.PrecioTotal}</h3>");
                    cuerpo.AppendLine($"<p>Método de pago: {pedido.MetodoDePago}</p>");
                    cuerpo.AppendLine("<hr /><p>Tu pedido está siendo procesado. ¡Gracias por confiar en SIGNOS!</p>");

                    servicioEmail.armarCorreo(user.Email, asunto, cuerpo.ToString());
                    servicioEmail.enviarMail();

                    
                    Session["items"] = null;
                }
               
            }
        }
    }
}