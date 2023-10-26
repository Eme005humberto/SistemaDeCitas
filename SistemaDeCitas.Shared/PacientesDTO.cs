using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeCitas.Shared
{
    public class PacientesDTO
    {
        //Con esto vamos a compartir informacion al cliente vamos a tener restrinciones
        public int IdPaciente { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string NombreCompleto { get; set; } = null!;
        [Required(ErrorMessage = "Este campo es necesario")]
        public string Telefono { get; set; } = null!;
        [Required(ErrorMessage = "Agrege la fecha solicitada por el paciente")]
        public DateTime FechaCita { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Este campo es requerido")]//Que empieze desde el 1 hasta el valor mas alto posible 
        public int IdCita { get; set; }
        //Objeto TblCitaDTO
        public CitasDTO? citasDTO { get; set; }//Este campo va a permitir nulos

    }
}
