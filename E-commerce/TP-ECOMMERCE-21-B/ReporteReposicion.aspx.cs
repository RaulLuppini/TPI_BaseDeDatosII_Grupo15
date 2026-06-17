using System;
using negocio;

namespace TP_ECOMMERCE_21_B
{
    public partial class ReporteReposicion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarReporte();
            } 
        }

        private void CargarReporte()
        {
            try
            {
                negocioProducto negocio = new negocioProducto();
          
                dgvFaltantes.DataSource = negocio.listarProductosAReponer();
                dgvFaltantes.DataBind();
            }
            catch (Exception ex)
            {
                
                lblMensaje.Text = "Ocurrió un error al cargar el reporte: " + ex.Message;
            }
        }
    }
}