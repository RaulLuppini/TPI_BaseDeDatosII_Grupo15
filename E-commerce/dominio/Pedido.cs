using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pedido
    {
        public int Id {  get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set;}
        public string ApellidoUsuario { get; set;}
        public decimal PrecioTotal { get; set; }
        public string Estado {  get; set; }
        public string MetodoDePago { get; set; }
        public List<DetallePedido> DetallePedidos { get; set; }

        public int IDEstado { get; set; }
        public int IDMetodoPago { get; set; }

    }
}
