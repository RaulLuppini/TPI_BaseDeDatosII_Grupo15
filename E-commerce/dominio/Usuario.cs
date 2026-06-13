using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contraseña { get; set; }
        public string Telefono { get; set; }
        public string RolUsuario { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; } 
        public string CodigoPostal { get; set; } 
        public bool Estado { get; set; }

    }
}
