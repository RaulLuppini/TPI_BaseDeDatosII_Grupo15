using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class DetallePedido
    {

        public int idPedido { get; set; }

        public int idProducto {  get; set; }

        public string nombreProducto { get; set; }

        public int cantidadProducto { get; set; }

        public decimal precioUnitario { get; set; }
        public decimal SubTotal
        {
            get { return precioUnitario * cantidadProducto; }
        }

        public decimal precioRebajado { get; set; }

    }
}
