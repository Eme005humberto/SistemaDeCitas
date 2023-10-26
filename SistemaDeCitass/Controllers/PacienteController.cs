using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeCitas.Shared;
using Microsoft.EntityFrameworkCore;
using SistemaDeCitas.Models;

namespace SistemaDeCitas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        //vamos a utilizar nuestro servicio de datos
        private readonly SistemaCitasContext dbContext;

        //Voy a crear un constructor
        public PacienteController(SistemaCitasContext dbContext)
        {
            this.dbContext = dbContext;//Llamamos a mi variable de base de datos
            //De esta manera ya tenemos inyectado nuestro servicio de base de datos en nuestro clase 
            //de controlador
        }
        //Metodo para devolver la la lista de Pacientes
        //Nos va a devolver la lista de Citas
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> ListarCitas()
        {
            var responseApi = new ResponseAPI<List<PacientesDTO>>();
            var ListaPacientesDTO = new List<PacientesDTO>();

            try
            {
                //Voy a crear un iteam por cada elemento que este recorriendo en la lista
                foreach (var item in await dbContext.TblPacientes.Include(c => c.IdCitaNavigation).ToListAsync())
                {
                    //Luego empezamos a traer los datos de mi CitasDTO para mostralos
                    ListaPacientesDTO.Add(new PacientesDTO
                    {
                        //Mostramos los datos
                        IdPaciente = item.IdPaciente,
                        NombreCompleto = item.NombreCompleto,
                        Telefono = item.Telefono,
                        FechaCita = item.FechaCita,
                        citasDTO = new CitasDTO
                        {
                            IdCita = item.IdCitaNavigation.IdCita,
                            Descripcion = item.IdCitaNavigation.Descripcion
                        }
                    });
                }
                responseApi.Correcto = true;
                responseApi.Valor = ListaPacientesDTO;//Valor que vamos a devolver es la Lista
            }
            catch (Exception ex)
            {
                responseApi.Correcto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        //Vamoa a encontrar un paciente
        [HttpGet]
        [Route("Lista/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<PacientesDTO>();
            var PacientesDTO = new PacientesDTO();

            try
            {
                //Vamos a buscar un empleado por Id
                var dbEmpleado = await dbContext.TblPacientes.FirstOrDefaultAsync(x=>x.IdPaciente == id);

                //Luego validamos si existe ese paciente traemos sus datos
                if(dbEmpleado != null)
                {
                    PacientesDTO.IdPaciente = dbEmpleado.IdPaciente;
                    PacientesDTO.NombreCompleto = dbEmpleado.NombreCompleto;
                    PacientesDTO.Telefono = dbEmpleado.Telefono;
                    PacientesDTO.FechaCita = dbEmpleado.FechaCita;

                    responseApi.Correcto = true;
                    responseApi.Valor = PacientesDTO;//Valor que vamos a devolver los datos encontrados
                }
                else
                {
                    responseApi.Correcto = false;
                    responseApi.Mensaje = "No encontrando";
                }
                
            }
            catch (Exception ex)
            {
                responseApi.Correcto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        //Metodo para agregar Paciente
        [HttpPost]
        [Route("GuardarPaciente")]
        public async Task<IActionResult> Guardar(PacientesDTO pacientes)
        {
            var responseApi = new ResponseAPI<PacientesDTO>();

            try
            {
                //Vamos a guardar los datos en nuestro modelo
                var dbEmpleado = new TblPaciente
                {
                    //Agregamos los datos de mi modelo
                    NombreCompleto = pacientes.NombreCompleto,
                    Telefono = pacientes.Telefono,
                    FechaCita = pacientes.FechaCita,
                    IdCita = pacientes.IdCita

                };
                dbContext.TblPacientes.Add(dbEmpleado);//Vamos a guardar estos cambios en nuestra tabla
                responseApi.Correcto = true;
                responseApi.Mensaje = PacientesDTO;





            }
            catch (Exception ex)
            {
                responseApi.Correcto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }
}
