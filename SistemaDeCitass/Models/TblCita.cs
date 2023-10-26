using System;
using System.Collections.Generic;

namespace SistemaDeCitas.Models;

public partial class TblCita
{
    public int IdCita { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<TblPaciente> TblPacientes { get; set; } = new List<TblPaciente>();
}
