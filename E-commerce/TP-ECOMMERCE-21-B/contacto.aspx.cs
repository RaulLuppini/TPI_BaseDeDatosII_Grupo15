using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_ECOMMERCE_21_B
{
    public partial class contacto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string email = txtEmail.Text.Trim();
            string mensaje = txtMensaje.Text.Trim();

            
            try
            {
                service.emailService servicioEmail = new service.emailService();
                string asunto = "Consulta desde la pagina de contacto";
                StringBuilder cuerpo = new StringBuilder();
                cuerpo.AppendLine("<h2>Nuevo mensaje de contacto</h2>");
                cuerpo.AppendLine($"<p><strong>Nombre:</strong> {nombre}</p>");
                cuerpo.AppendLine($"<p><strong>Email:</strong> {email}</p>");
                cuerpo.AppendLine($"<p><strong>Mensaje:</strong><br/>{mensaje}</p>");


                servicioEmail.armarCorreo("utnprogramacionprueba@gmail.com", asunto, cuerpo.ToString());
                servicioEmail.enviarMail();

                lblConfirmacion.Text = "Gracias por tu mensaje, " + nombre + ". Te responderemos pronto.";
                lblConfirmacion.Visible = true;

                txtNombre.Text = "";
                txtEmail.Text = "";
                txtMensaje.Text = "";
            }
            catch (Exception ex)
            {
                lblConfirmacion.Text = "❌ Error al enviar el mensaje: " + ex.Message;
                lblConfirmacion.Visible = true;

            }
        }
    }
}