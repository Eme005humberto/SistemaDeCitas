using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeCitas.Shared
{
    public class CitasDTO
    {
        //Con esto vamos a compartir informacion al cliente
        public int IdCita { get; set; }

        public string Descripcion { get; set; } = null!;
    }
}
