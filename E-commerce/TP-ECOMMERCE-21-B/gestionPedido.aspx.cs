using dominio;
using negocio;
using service;
using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_ECOMMERCE_21_B
{
    public partial class gestionPedido : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo cultura = new System.Globalization.CultureInfo("es-AR");
            System.Threading.Thread.CurrentThread.CurrentCulture = cultura;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultura;
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
                cargarPedidos();
            }
        }

        private void cargarPedidos()
        {
            negocioPedido negocio = new negocioPedido();
            GridViewPedido.DataSource = negocio.listarPedido();
            GridViewPedido.DataBind();
        }


        protected void GridViewPedido_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Pedido pedido = (Pedido)e.Row.DataItem;
                DropDownList ddlEstado = (DropDownList)e.Row.FindControl("ddlEstado");

                if (ddlEstado != null)
                {
                    ddlEstado.Items.Clear();

                    bool esTransferencia = pedido.MetodoDePago != null &&
                        pedido.MetodoDePago.Trim().ToLower().Contains("transferencia");

                    if (esTransferencia)
                    {
                        ddlEstado.Items.Add("Pago aprobado");
                        ddlEstado.Items.Add("En preparación");
                        ddlEstado.Items.Add("Enviado");
                        ddlEstado.Items.Add("Entregado");
                        ddlEstado.Items.Add("Cancelado");
                    }
                    else
                    {
                        ddlEstado.Items.Add("En preparación");
                        ddlEstado.Items.Add("Enviado");
                        ddlEstado.Items.Add("Entregado");
                        ddlEstado.Items.Add("Cancelado");
                    }

                    
                    string estadoActual = pedido.Estado?.Trim() ?? "";
                    string estadoPreseleccionado = estadoActual;

                    if (esTransferencia)
                    {
                        if (estadoActual.Equals("Pago pendiente", StringComparison.OrdinalIgnoreCase))
                            estadoPreseleccionado = "Pago aprobado";
                        else if (estadoActual.Equals("Pago aprobado", StringComparison.OrdinalIgnoreCase))
                            estadoPreseleccionado = "En preparación";
                        else if (estadoActual.Equals("En preparación", StringComparison.OrdinalIgnoreCase))
                            estadoPreseleccionado = "Enviado";
                        else if (estadoActual.Equals("Enviado", StringComparison.OrdinalIgnoreCase))
                            estadoPreseleccionado = "Entregado";
                    }
                    else
                    {
                        if (estadoActual.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                            estadoPreseleccionado = "En preparación";
                        else if (estadoActual.Equals("En preparación", StringComparison.OrdinalIgnoreCase))
                            estadoPreseleccionado = "Enviado";
                        else if (estadoActual.Equals("Enviado", StringComparison.OrdinalIgnoreCase))
                            estadoPreseleccionado = "Entregado";
                    }

                    ListItem item = ddlEstado.Items.FindByText(estadoPreseleccionado);
                    if (item != null)
                        item.Selected = true;
                    else
                        ddlEstado.SelectedIndex = 0;
                }
            }
        }

        protected void GridViewPedido_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CambiarEstado")
            {
                GridViewRow row = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                DropDownList ddlEstado = (DropDownList)row.FindControl("ddlEstado");
                if (ddlEstado != null)
                {
                    string nuevoEstado = ddlEstado.SelectedValue;
                    int idPedido = Convert.ToInt32(e.CommandArgument);

                    negocioPedido negocio = new negocioPedido();
                    Pedido pedido = negocio.ObtenerPedidoConUsuario(idPedido);

                    
                    if (pedido.MetodoDePago != null &&
                        pedido.MetodoDePago.Trim().ToLower().Contains("transferencia") &&
                        nuevoEstado == "Activo")
                    {
                        
                        nuevoEstado = "Pago aprobado";
                    }

                    
                    negocio.actualizarEstado(idPedido, nuevoEstado);

                    
                    Usuario user = new negocioUsuario().buscarPorId(pedido.IdUsuario);
                    emailService servicioEmail = new emailService();
                    string asunto = $"Estado actualizado - Pedido #{pedido.Id}";

                    StringBuilder cuerpo = new StringBuilder();
                    cuerpo.AppendLine($"<h2>Hola {user.Nombre},</h2>");
                    cuerpo.AppendLine($"<p>Tu pedido <strong>#{pedido.Id}</strong> ha cambiado de estado.</p>");
                    cuerpo.AppendLine($"<p><strong>Nuevo estado:</strong> {nuevoEstado}</p>");
                    cuerpo.AppendLine("<hr /><p>Gracias por confiar en SIGNOS.</p>");

                    servicioEmail.armarCorreo(user.Email, asunto, cuerpo.ToString());
                    servicioEmail.enviarMail();

                    cargarPedidos();
                }
            }
            else if (e.CommandName == "VerDetalle")
            {
                int idPedido = Convert.ToInt32(e.CommandArgument);
                negocioPedido negocio = new negocioPedido();

                Pedido pedido = negocio.ObtenerPedidoConUsuario(idPedido);

                Response.Redirect("detallePedido.aspx?id=" + idPedido);
            }
        }



        private void aplicarFiltroPedidos()
        {
            int.TryParse(txtFiltroNumero.Text.Trim(), out int numeroPedido);
            string estado = ddlFiltroEstado.SelectedValue;

            negocioPedido negocio = new negocioPedido();
            var lista = negocio.listarPedido();

            if (numeroPedido > 0)
                lista = lista.Where(p => p.Id == numeroPedido).ToList();

            if (!string.IsNullOrEmpty(estado))
                lista = lista.Where(p => p.Estado == estado).ToList();

            GridViewPedido.DataSource = lista;
            GridViewPedido.DataBind();
        }

        protected void GridViewPedido_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPedido.PageIndex = e.NewPageIndex; 
            aplicarFiltroPedidos(); 
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            GridViewPedido.PageIndex = 0; 
            aplicarFiltroPedidos();
        }

        
    }
}