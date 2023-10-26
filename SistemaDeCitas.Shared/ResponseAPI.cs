using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeCitas.Shared
{
    public class ResponseAPI<T>//Esto va hacer generico podemos retornar cualquier dato que queramos
    {
        public bool Correcto { get; set; }//Con esta operacion vamos a indicar si al momento
        //de agregar, modificar y eliminar fueron correctos
        public T? Valor { get; set; }//Con esto vamos a devolver la lista de pacientes 
        //o citas o Id del Paciente
        public string? Mensaje { get; set; }//Con esto enviamos un mensaje

    }
}
