using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace TP_ECOMMERCE_21_B
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string msg = Request.QueryString["msg"];
                if (msg == "loginRequired")
                {
                    lblErrorLogin.Text = "Debes iniciar sesión para comprar.";
                    lblErrorLogin.Visible = true;
                }
            }
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string clave = txtClave.Text.Trim();

            negocioUsuario negocio = new negocioUsuario();
            Usuario usuario = negocio.Login(email, clave);

            if (usuario != null)
            {
                Session["usuario"] = usuario;
                Session["user"] = usuario;

                if (usuario.RolUsuario == "admin")
                {
                    Response.Redirect("gestionProductos.aspx", false);
                    return;
                }

                string returnUrl = Request.QueryString["returnUrl"];
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    Response.Redirect(returnUrl, false);
                    return;
                }

                // Si no hay returnUrl pero venía del carrito
                if (Session["redirectCarrito"] != null && (bool)Session["redirectCarrito"])
                {
                    Session["redirectCarrito"] = null; // limpiar el flag
                    Response.Redirect("FormularioPago.aspx", false);
                    return;
                }

                // Por defecto
                Response.Redirect("Ecommerce.aspx", false);
            }
            else
            {
                lblErrorLogin.Text = "Email o contraseña incorrectos.";
            }
        }




    }
}