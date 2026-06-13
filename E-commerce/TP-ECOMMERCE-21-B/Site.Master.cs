using dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TP_ECOMMERCE_21_B
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string currentPage = Path.GetFileName(Request.Url.AbsolutePath).ToLower();
                bool esRegistroPublico = currentPage.Contains("altausuario") && Session["usuario"] == null;


                // Mostrar menú solo si estás en gestionProductos.aspx
               
                lnkCarrito.Visible = !(
                currentPage.Contains("carritowithmaster") ||
                currentPage.Contains("gestion") ||
                currentPage.Contains("altaproducto") ||
                currentPage.Contains("admin") ||
                currentPage.Contains("login") ||
                currentPage.Contains("altausuario"));


                Usuario usuario = Session["usuario"] as Usuario;

                if (usuario!=null && !esRegistroPublico)
                {
                    lblUsuario.Text = $"Hola, {usuario.Nombre}";
                    pnlUsuario.Visible = true;
                    btnLogin.Visible = false;
                    if(usuario.RolUsuario=="Admin" || usuario.RolUsuario=="admin")
                    {
                        phGestionLinks.Visible = true;
                        userLinks.Visible = false;
                    }
                    else
                    {
                        phGestionLinks.Visible = false;
                        userLinks.Visible = true;
                    }
                    
                }
                else
                {
                    pnlUsuario.Visible = false;
                    btnLogin.Visible = !currentPage.Contains("login") && !esRegistroPublico;
                    phGestionLinks.Visible = false;
                    userLinks.Visible = false;
                }



            }

        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Ecommerce.aspx");
        }








    }
}
