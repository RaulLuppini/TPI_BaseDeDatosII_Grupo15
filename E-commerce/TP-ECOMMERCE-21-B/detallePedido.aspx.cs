using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TP_ECOMMERCE_21_B
{
    public partial class detallePedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo cultura = new System.Globalization.CultureInfo("es-AR");
            System.Threading.Thread.CurrentThread.CurrentCulture = cultura;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultura;
            if (!IsPostBack)
            {
                int idPedido;
                if(int.TryParse(Request.QueryString["id"], out idPedido))
                {
                    negocioPedido negocio = new negocioPedido();
                   Pedido pedido = negocio.ObtenerPedidoConUsuario(idPedido);
                    
                    lblNumeroPedido.Text = pedido.Id.ToString();
                    lblFecha.Text = pedido.Fecha.ToString("dd/MM/yyyy HH:mm");
                    lblNombreUsuario.Text = pedido.NombreUsuario;
                    lblApellidoUsuario.Text = pedido.ApellidoUsuario;
                   
                    lblEstado.Text = pedido.Estado;
                    lblMetodoPago.Text = pedido.MetodoDePago;

                    GridViewDetalle.DataSource = pedido.DetallePedidos;
                    GridViewDetalle.DataBind();

                    lblPrecioTotal.Text = pedido.PrecioTotal.ToString("C");
                }
            }

        }
    }
}