using System;
using System.Collections.Generic;

namespace SistemaDeCitas.Models;

public partial class TblPaciente
{
    public int IdPaciente { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public DateTime FechaCita { get; set; }

    public int IdCita { get; set; }

    public virtual TblCita IdCitaNavigation { get; set; } = null!;
}
