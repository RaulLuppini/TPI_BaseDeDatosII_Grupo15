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
    public partial class gestionCategoria : System.Web.UI.Page
    {
       public void cargarListaMarcas(string filtro = "")
        {
            var negocio = new negocioMarca();
            var lista = negocio.listar();

            if (!string.IsNullOrWhiteSpace(filtro))
                lista = lista.Where(m => m.Nombre.ToLower().Contains(filtro.ToLower())).ToList();

            GridViewMarca.DataSource = lista;
            GridViewMarca.DataBind();
        }
        public void cargarListaCategoria(string filtro = "")
        {
            var negocio = new negocioCategoria();
            var lista = negocio.listarCategoria();

            if (!string.IsNullOrWhiteSpace(filtro))
                lista = lista.Where(c => c.Nombre.ToLower().Contains(filtro.ToLower())).ToList();

            GridViewCategoria.DataSource = lista;
            GridViewCategoria.DataBind();
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
            cargarListaCategoria();
            cargarListaMarcas();
            }
        }

        protected void GridViewCategoria_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewCategoria.PageIndex = e.NewPageIndex;
            cargarListaCategoria(); 
        }

        protected void GridViewMarca_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewMarca.PageIndex = e.NewPageIndex;
            cargarListaMarcas(); 
        }

        
        
        protected void GridViewCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(GridViewCategoria.SelectedDataKey.Value);
            string nombre = GridViewCategoria.SelectedRow.Cells[1].Text;
            txtCategoria.Text = nombre;
            Session["idCategoriaSeleccionada"] = id;
        }

        protected void GridViewMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(GridViewMarca.SelectedDataKey.Value);
            string nombre = GridViewMarca.SelectedRow.Cells[1].Text;
            txtMarca.Text = nombre;
            Session["idMarcaSeleccionada"] = id;
        }

        protected void btnFiltrarCategoria_Click(object sender, EventArgs e)
        {
            cargarListaCategoria(txtFiltroCategoria.Text.Trim());
        }

        protected void btnFiltrarMarca_Click(object sender, EventArgs e)
        {
            cargarListaMarcas(txtFiltroMarca.Text.Trim());
        }

        protected void btnAgregarC_Click(object sender, EventArgs e)
        {
            string nombreCategoria = txtCategoria.Text.Trim();
            lblMensajeC.ForeColor = System.Drawing.Color.Red;

            if (string.IsNullOrEmpty(nombreCategoria))
            {
                lblMensajeC.Text = "⚠️ Debe ingresar un nombre para la categoría.";
                return;
            }

            var negocioCat = new negocioCategoria();
            var listaCat = negocioCat.listarCategoria();

            bool existeCategoria = listaCat.Any(c => c.Nombre.Equals(nombreCategoria, StringComparison.OrdinalIgnoreCase));
            if (existeCategoria)
            {
                lblMensajeC.Text = "⚠️ Ya existe una categoría con ese nombre.";
                return;
            }

            negocioCat.agregarCategoria(new Categoria { Nombre = nombreCategoria });
            cargarListaCategoria();
            txtCategoria.Text = "";
            lblMensajeC.ForeColor = System.Drawing.Color.Green;
            lblMensajeC.Text = "✅ Categoría agregada correctamente.";

        }

        protected void btnModificarC_Click(object sender, EventArgs e)
        {
            if (Session["idCategoriaSeleccionada"] == null)
            {
                lblMensajeC.Text = "⚠️ Debe seleccionar una categoría para modificar.";
                lblMensajeC.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string nuevoNombre = txtCategoria.Text.Trim();
            if (string.IsNullOrEmpty(nuevoNombre))
            {
                lblMensajeC.Text = "⚠️ El nombre de la categoría no puede estar vacío.";
                lblMensajeC.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int id = (int)Session["idCategoriaSeleccionada"];
            var negocioCat = new negocioCategoria();
            var listaCat = negocioCat.listarCategoria();

            bool nombreDuplicado = listaCat.Any(c => c.Nombre.Equals(nuevoNombre, StringComparison.OrdinalIgnoreCase) && c.Id != id);
            if (nombreDuplicado)
            {
                lblMensajeC.Text = "⚠️ Ya existe otra categoría con ese nombre.";
                lblMensajeC.ForeColor = System.Drawing.Color.Red;
                return;
            }

            negocioCat.modificarCategoria(id, nuevoNombre);
            cargarListaCategoria();
            txtCategoria.Text = "";
            Session["idCategoriaSeleccionada"] = null;
            lblMensajeC.ForeColor = System.Drawing.Color.Green;
            lblMensajeC.Text = "✅ Categoría modificada correctamente.";
        }

        protected void btnEliminarC_Click(object sender, EventArgs e)
        {
            if (GridViewCategoria.SelectedDataKey == null)
            {
                lblMensajeC.Text = "⚠️ Debe seleccionar una categoría para eliminar.";
                lblMensajeC.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int idCategoria = Convert.ToInt32(GridViewCategoria.SelectedDataKey.Value);
            var negocioProd = new negocioProducto();

            if (negocioProd.ExistenProductosPorCategoria(idCategoria))
            {
                lblMensajeC.Text = "⚠️ No se puede eliminar la categoría porque tiene productos asociados.";
                lblMensajeC.ForeColor = System.Drawing.Color.Red;
                return;
            }

            var negocioCat = new negocioCategoria();
            negocioCat.eliminarCategoria(idCategoria);
            cargarListaCategoria();
            lblMensajeC.ForeColor = System.Drawing.Color.Green;
            lblMensajeC.Text = "✅ Categoría eliminada correctamente.";
        }

        protected void CancelarC_Click(object sender, EventArgs e)
        {
            txtCategoria.Text = "";
            Session["idCategoriaSeleccionada"] = null;
            lblMensajeC.Text = "";
            cargarListaCategoria();
        }

        protected void btnAgregarM_Click(object sender, EventArgs e)
        {

            string nombreMarca = txtMarca.Text.Trim();
            lblMensajeM.ForeColor = System.Drawing.Color.Red;

            if (string.IsNullOrEmpty(nombreMarca))
            {
                lblMensajeM.Text = "⚠️ Debe ingresar un nombre para la marca.";
                return;
            }

            var negocioMarca = new negocioMarca();
            var listaMarca = negocioMarca.listar();

            bool existeMarca = listaMarca.Any(m => m.Nombre.Equals(nombreMarca, StringComparison.OrdinalIgnoreCase));
            if (existeMarca)
            {
                lblMensajeM.Text = "⚠️ Ya existe una marca con ese nombre.";
                return;
            }

            negocioMarca.agregarMarca(new Marca { Nombre = nombreMarca });
            cargarListaMarcas();
            txtMarca.Text = "";
            lblMensajeM.ForeColor = System.Drawing.Color.Green;
            lblMensajeM.Text = "✅ Marca agregada correctamente.";

        }

        protected void btnModificarM_Click(object sender, EventArgs e)
        {
            if (Session["idMarcaSeleccionada"] == null)
            {
                lblMensajeM.Text = "⚠️ Debe seleccionar una marca para modificar.";
                lblMensajeM.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string nuevoNombre = txtMarca.Text.Trim();
            if (string.IsNullOrEmpty(nuevoNombre))
            {
                lblMensajeM.Text = "⚠️ El nombre de la marca no puede estar vacío.";
                lblMensajeM.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int id = (int)Session["idMarcaSeleccionada"];
            var negocioMarca = new negocioMarca();
            var listaMarca = negocioMarca.listar();

            bool nombreDuplicado = listaMarca.Any(m => m.Nombre.Equals(nuevoNombre, StringComparison.OrdinalIgnoreCase) && m.Id != id);
            if (nombreDuplicado)
            {
                lblMensajeM.Text = "⚠️ Ya existe otra marca con ese nombre.";
                lblMensajeM.ForeColor = System.Drawing.Color.Red;
                return;
            }

            negocioMarca.modificarMarca(id, nuevoNombre);
            cargarListaMarcas();
            txtMarca.Text = "";
            Session["idMarcaSeleccionada"] = null;
            lblMensajeM.ForeColor = System.Drawing.Color.Green;
            lblMensajeM.Text = "✅ Marca modificada correctamente.";

        }

        protected void btnEliminarM_Click(object sender, EventArgs e)
        {
            if (GridViewMarca.SelectedDataKey == null)
            {
                lblMensajeM.Text = "⚠️ Debe seleccionar una marca para eliminar.";
                lblMensajeM.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int idMarca = Convert.ToInt32(GridViewMarca.SelectedDataKey.Value);
            var negocioProd = new negocioProducto();

            if (negocioProd.ExistenProductosPorMarca(idMarca))
            {
                lblMensajeM.Text = "⚠️ No se puede eliminar la marca porque tiene productos asociados.";
                lblMensajeM.ForeColor = System.Drawing.Color.Red;
                return;
            }

            var negocioMarca = new negocioMarca();
            negocioMarca.eliminarMarca(idMarca);
            cargarListaMarcas();
            lblMensajeM.ForeColor = System.Drawing.Color.Green;
            lblMensajeM.Text = "✅ Marca eliminada correctamente.";
        }

        protected void btnCancelarM_Click(object sender, EventArgs e)
        {
            txtMarca.Text = "";
            Session["idMarcaSeleccionada"] = null;
            lblMensajeM.Text = "";
            cargarListaMarcas();
        }
    }
}