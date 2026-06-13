using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Producto
    {
         public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion {  get; set; }
        public Marca IdMarca { get; set; }
        public Categoria IdCategoria { get; set; }
        public List<Imagen> Imagenes { get; set; }
        public int cantidad { get; set; }
        public decimal PrecioCompra {  get; set; }
        public decimal PorcentajeGanancia
        {
            get
            {
                if (PrecioCompra == 0)
                    return 0;
                return Math.Round(((PrecioVenta - PrecioCompra) / PrecioCompra) * 100, 2);
            }
        }


        public decimal PrecioVenta { get; set; }
        public int StockActual {  get; set; }  
        public  int StockMinimo { get; set; }
        public bool Estado {  get; set; }

    }
}
