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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack) {

                if (Session["usuario"] == null) {

                    Response.Redirect("Ecommerce.aspx");

                }

                Usuario user = (Usuario)Session["usuario"];
                nombreUser.Text = user.Nombre + " "+ user.Apellido;
                string textid = "ID Usuario: ";
                IDusuario.Text = textid + user.Id.ToString();
                emailText.Text= " " + user.Email;
                TlfText.Text = " " + user.Telefono;
                CurrentPassword.Attributes.Add("required", "required");
                NewPassword.Attributes.Add("required", "required");


            }

        }

        protected void changePassword_Click(object sender, EventArgs e)
        {
            string current = CurrentPassword.Text.Trim();
            string newPassword = NewPassword.Text.Trim();
            
            
            if(newPassword=="" || current == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                   "alert('No se aceptan caracteres vacios');", true);
            }


            Usuario user = (Usuario)Session["usuario"];
            int id = user.Id;

            negocioUsuario nu = new negocioUsuario();
            string res = nu.UpdatePassword(id, current, newPassword);

            if (res == "OK")
            {

                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Contraseña actualizada correctamente');", true);

            }

                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Error: {res}');", true);

        }
    }
}