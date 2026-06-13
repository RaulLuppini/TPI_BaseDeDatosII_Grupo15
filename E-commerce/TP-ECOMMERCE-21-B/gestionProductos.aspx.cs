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
    public partial class gestionProductos : Page
    {
        private void cargarGrilla()
        {
            negocioProducto negocio = new negocioProducto();

            if (Session["modoAlta"] != null && (bool)Session["modoAlta"])
                GridViewProductos.DataSource = negocio.listarInactivos();
            else
                GridViewProductos.DataSource = negocio.listar();

            GridViewProductos.DataBind();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("login.aspx", false);
            }
            else
            {
                Usuario usuario = (Usuario)Session["usuario"];
                if (!usuario.RolUsuario.Equals("admin", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect("login.aspx", false);
                }
            }

            if (!IsPostBack)
            {
               
                if (Session["modoModificar"] != null && (bool)Session["modoModificar"] ||
                    Session["modoBaja"] != null && (bool)Session["modoBaja"] ||
                    Session["modoAlta"] != null && (bool)Session["modoAlta"] ||
                    Session["modoEliminar"] != null && (bool)Session["modoEliminar"])
                {
                    GridViewProductos.AutoGenerateSelectButton = true;
                }

               
                negocioCategoria negocioCat = new negocioCategoria();
                ddlFiltroCategoria.DataSource = negocioCat.listarCategoria();
                ddlFiltroCategoria.DataTextField = "Nombre";
                ddlFiltroCategoria.DataValueField = "Id";
                ddlFiltroCategoria.DataBind();
                ddlFiltroCategoria.Items.Insert(0, new ListItem("Todas las categorías", ""));

                negocioMarca negocioMarca = new negocioMarca();
                ddlFiltroMarca.DataSource = negocioMarca.listar();
                ddlFiltroMarca.DataTextField = "Nombre";
                ddlFiltroMarca.DataValueField = "Id";
                ddlFiltroMarca.DataBind();
                ddlFiltroMarca.Items.Insert(0, new ListItem("Todas las marcas", ""));

                
                cargarGrilla();
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Session.Remove("modificarId");
            Session.Remove("modoModificar");
            Session.Remove("modoBaja");
            Session.Remove("modoAlta");
            Session.Remove("modoEliminar");
            Response.Redirect("altaProducto.aspx");
        }

        protected void GridViewProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32(GridViewProductos.SelectedDataKey.Value);
            negocioProducto negocio = new negocioProducto();

            if (Session["modoModificar"] != null && (bool)Session["modoModificar"])
            {
                Session["modificarId"] = idProducto;
                Session.Remove("modoModificar");
                Response.Redirect("altaProducto.aspx");
            }
            else if (Session["modoBaja"] != null && (bool)Session["modoBaja"])
            {
                negocio.darDeBaja(idProducto);
                Session.Remove("modoBaja");
                Response.Redirect("gestionProductos.aspx");
            }
            else if (Session["modoAlta"] != null && (bool)Session["modoAlta"])
            {
                negocio.darDeAlta(idProducto);
                Session.Remove("modoAlta");
                Response.Redirect("gestionProductos.aspx");
            }

            else if (Session["modoEliminar"] != null && (bool)Session["modoEliminar"])
            {
               
                negocio.eliminarProducto(idProducto);

                Session.Remove("modoEliminar");
                Response.Redirect("gestionProductos.aspx");
            }


        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Session["modoModificar"] = true;
            Response.Redirect("gestionProductos.aspx"); 

        }
        /*
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Session["modoEliminar"] = true;
            Response.Redirect("gestionProductos.aspx");

        }
        */
        protected void btnBaja_Click(object sender, EventArgs e)
        {
            Session["modoBaja"] = true;
            Response.Redirect("gestionProductos.aspx"); 

        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            Session["modoAlta"] = true;
            Response.Redirect("gestionProductos.aspx");

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            
            Session.Remove("modoAlta");
            Session.Remove("modoBaja");
            Session.Remove("modoModificar");

            Response.Redirect("gestionProductos.aspx"); 
        }
        private void aplicarFiltroProductos()
        {
            string texto = txtFiltroProducto.Text.Trim().ToLower();
            string idCategoria = ddlFiltroCategoria.SelectedValue;
            string idMarca = ddlFiltroMarca.SelectedValue;

            negocioProducto negocio = new negocioProducto();
            List<Producto> lista = negocio.listar();

            var filtrados = lista.Where(p =>
                (string.IsNullOrEmpty(texto) ||
                    (!string.IsNullOrEmpty(p.Nombre) && p.Nombre.ToLower().Contains(texto)) ||
                    (!string.IsNullOrEmpty(p.Codigo) && p.Codigo.ToLower().Contains(texto))
                ) &&
                (string.IsNullOrEmpty(idCategoria) || p.IdCategoria.Id.ToString() == idCategoria) &&
                (string.IsNullOrEmpty(idMarca) || p.IdMarca.Id.ToString() == idMarca)
            ).ToList();

            GridViewProductos.DataSource = filtrados;
            GridViewProductos.DataBind();
        }
        protected void GridViewProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewProductos.PageIndex = e.NewPageIndex; 
            aplicarFiltroProductos(); 
        }
        protected void btnFiltrarProducto_Click(object sender, EventArgs e)
        {
            GridViewProductos.PageIndex = 0; 
            aplicarFiltroProductos();
        }
    }
}
