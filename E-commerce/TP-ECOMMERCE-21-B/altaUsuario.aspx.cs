using dominio;
using negocio;
using service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_ECOMMERCE_21_B
{
    public partial class altaUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                lblTitulo.Text = Session["modificarId"] != null ? "Modificar Usuario" : "Alta de Usuario";
                btnAceptar.Text = Session["modificarId"] != null ? "Guardar Cambios" : "Aceptar";
                bool esAdmin = Session["modoAdmin"] != null;

                ddlRol.Visible = esAdmin;
                chkEstado.Visible = esAdmin;
                lblEstado.Visible = esAdmin;
                phLinkLogin.Visible = !esAdmin;


                if (esAdmin)
                {
                    btnAceptar.Text = "Aceptar";
                    ddlRol.Items.Add("admin");
                    ddlRol.Items.Add("cliente");
                    chkEstado.Checked = true;
                }
                else
                {
                    btnAceptar.Text = "Registrarse";
                }

                lblTitulo.Text = Session["modificarUsuarioId"] != null ? "Modificar Usuario" : "Alta de Usuario";
                btnAceptar.Text = Session["modificarUsuarioId"] != null ? "Guardar Cambios" : "Aceptar";

                if (Session["modificarUsuarioId"] != null)
                {
                    int id = (int)Session["modificarUsuarioId"];
                    negocioUsuario negocio = new negocioUsuario();
                    Usuario u = negocio.buscarPorId(id);

                    if (u != null)
                    {
                        txtEmail.Text = u.Email;
                        txtNombre.Text = u.Nombre;
                        txtApellido.Text = u.Apellido;
                        txtDireccion.Text = u.Direccion;
                        txtCodigoPostal.Text = u.CodigoPostal;
                        txtTelefono.Text = u.Telefono;
                        txtClave.Text = u.Contraseña;

                        if (esAdmin)
                        {
                            ddlRol.SelectedValue = u.RolUsuario;
                            chkEstado.Checked = u.Estado;
                        }
                    }
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Usuario nuevo = new Usuario();
            nuevo.Email = txtEmail.Text.Trim();
            nuevo.Nombre = txtNombre.Text.Trim();
            nuevo.Apellido = txtApellido.Text.Trim();
            nuevo.Dni = txtDni.Text.Trim();
            nuevo.Direccion = txtDireccion.Text.Trim();
            nuevo.CodigoPostal = txtCodigoPostal.Text.Trim();
            nuevo.Telefono = txtTelefono.Text.Trim();
            nuevo.Contraseña = txtClave.Text.Trim();

            bool esAdmin = Session["modoAdmin"] != null;

            if (esAdmin)
            {
                nuevo.RolUsuario = ddlRol.SelectedValue;
                nuevo.Estado = chkEstado.Checked;
            }
            else
            {
                nuevo.RolUsuario = "cliente";
                nuevo.Estado = true;
            }

            negocioUsuario negocio = new negocioUsuario();
            List<Usuario> usuarios = negocio.listarUsuarios();

            string emailNuevo = nuevo.Email.ToLower();
            string dniNuevo = nuevo.Dni;
            int? idActual = Session["modificarUsuarioId"] as int?;

            bool emailRepetido = usuarios.Any(u => u.Email.ToLower() == emailNuevo && u.Id != idActual);
            bool dniRepetido = usuarios.Any(u => u.Dni == dniNuevo && u.Id != idActual);

            if (emailRepetido)
            {
                lblError.Text = "⚠️ El email ya está registrado por otro usuario.";
                return;
            }

            if (dniRepetido)
            {
                lblError.Text = "⚠️ El DNI ya está registrado por otro usuario.";
                return;
            }

            if (Session["modificarUsuarioId"] != null)
            {
                nuevo.Id = (int)Session["modificarUsuarioId"];
                negocio.modificarUsuario(nuevo);
            }
            else
            {
                negocio.agregarUsuario(nuevo);

                if (nuevo.RolUsuario == "cliente")
                {
                    try
                    {
                        string asunto = "Bienvenido a E-commerce SIGNOS";
                        string cuerpo = $@"<h1>✔ ¡Hola {nuevo.Nombre}!</h1>
                   <h2>Tu registro ha sido exitoso</h2>
                   <hr />
                   <p>Ya podés comenzar a comprar tus productos favoritos en nuestra tienda.</p>
                   <p>Si tenés dudas, escribinos a soporte@signos.com</p>
                   <br />
                   <h4>¡Gracias por confiar en nosotros!</h4>";

                        emailService servicio = new emailService();
                        servicio.armarCorreo(nuevo.Email, asunto, cuerpo);
                        servicio.enviarMail();
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = "❌ Error al enviar el correo: " + ex.Message;
                        return;
                    }
                }
            }

            Session.Remove("modoAdmin");
            Session.Remove("modificarUsuarioId");

            if (Session["usuario"] != null && ((Usuario)Session["usuario"]).RolUsuario == "admin")
            {
                Response.Redirect("gestionUsuario.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }


    }
}
