using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_ECOMMERCE_21_B
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["usuario"] == null)
            {

                Response.Redirect("Ecommerce.aspx");

            }
            // Cancelación de compra
            if (Request.QueryString["action"] == "cancelar" && Request.QueryString["id"] != null)
            {
                int idPedido = int.Parse(Request.QueryString["id"]);
                negocioPedido np = new negocioPedido();

             
                np.actualizarEstado(idPedido, "Cancelado");

                
                np.verificarDetallePedido(idPedido, false);

             
                Response.Redirect("pedidos.aspx");
            }
        }
    }
}