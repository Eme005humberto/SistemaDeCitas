using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeCitas.Shared;
using Microsoft.EntityFrameworkCore;
using SistemaDeCitas.Models;
using SistemaDeCitas.Services;

namespace SistemaDeCitas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {

        //vamos a utilizar nuestro servicio de datos
        private readonly SistemaCitasContext dbContext;
        private readonly GenerarPdf _generarPdf;

        //Voy a crear un constructor
        public PacienteController(SistemaCitasContext dbContext,GenerarPdf generarPdf)
        {
            this.dbContext = dbContext;//Llamamos a mi variable de base de datos
            //De esta manera ya tenemos inyectado nuestro servicio de base de datos en nuestro clase 
            //de controlador
            this._generarPdf = generarPdf;//Estoy usando el servicio para general pdf
        }

        //Metodo para general el pdf de los pacientes
        [HttpPost]
        public IActionResult GenerarPdf(PacientesDTO pacientes)
        {
            string htmlContent = "";
            byte[] pdfBytes = _generarPdf.PdfGenerar(htmlContent);
            return File(pdfBytes, "application/pdf", "Reporte Generado.pdf");
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
                var dbEmpleado = await dbContext.TblPacientes.FirstOrDefaultAsync(x => x.IdPaciente == id);

                //Luego validamos si existe ese paciente traemos sus datos
                if (dbEmpleado != null)
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
            var responseApi = new ResponseAPI<int>();

            try
            {
                //Vamos a guardar los datos en nuestro modelo
                var dbPaciente = new TblPaciente
                {
                    //Agregamos los datos de mi modelo
                    NombreCompleto = pacientes.NombreCompleto,
                    Telefono = pacientes.Telefono,
                    FechaCita = pacientes.FechaCita,
                    IdCita = pacientes.IdCita
                };
                //Luego vamos a guardar los datos en dbContex
                dbContext.TblPacientes.Add(dbPaciente);//Vamos a guardar estos cambios en nuestra tabla
                await dbContext.SaveChangesAsync();
                //Validamos si se creo nuestro paciente correctamente
                if (dbPaciente.IdPaciente != 0)
                {
                    responseApi.Correcto = true;
                    responseApi.Valor = dbPaciente.IdPaciente;//Me devuelve el valor con los datos del cliente
                    //y su IdPaciente
                }
                else
                {
                    responseApi.Correcto = false;
                    responseApi.Mensaje = "No se ha guardado los datos del paciente";
                }
            }
            catch (Exception ex)
            {
                responseApi.Correcto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        //Metodo para modificar Paciente
        [HttpPut]
        [Route("EditarPaciente/{id}")]
        public async Task<IActionResult> Modificar(PacientesDTO pacientes, int id)
        {
            var responseApi = new ResponseAPI<int>();
             

            try
            {
                //Vamos a buscar a ese paciente por medio del Id
                var dbPaciente = await dbContext.TblPacientes.FirstOrDefaultAsync(e => e.IdPaciente == id);
               
              
                //Validamos si el paciente es diferente de null
                if (dbPaciente != null)
                {
                    //Si es diferente procedemos a modificar
                    dbPaciente.NombreCompleto = pacientes.NombreCompleto;
                    dbPaciente.Telefono = pacientes.Telefono;
                    dbPaciente.FechaCita = pacientes.FechaCita;

                    //Procedemos a guardar los cambios
                    dbContext.TblPacientes.Update(dbPaciente);
                    //Guardamos los cambios
                    await dbContext.SaveChangesAsync();


                    //Mandamos un mensaje para decirle a la Api que todo ha salido bien
                    responseApi.Correcto = true;
                    responseApi.Valor = dbPaciente.IdPaciente;
                }
                else
                {
                    responseApi.Correcto = false;
                    responseApi.Mensaje = "No se han actualizado los datos del paciente";
                }
            }
            catch (Exception ex)
            {
                responseApi.Correcto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        //Metodo para eliminar Paciente
        [HttpDelete]
        [Route("EliminarPaciente/{id}")]
        public async Task<IActionResult> Eliminar( int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                //Vamos a buscar a ese paciente por medio del Id
                var dbPaciente = await dbContext.TblPacientes.FirstOrDefaultAsync(e => e.IdPaciente == id);


                //Validamos si el paciente es diferente de null
                if (dbPaciente != null)
                {
                    //Procedemos a eliminar
                    dbContext.TblPacientes.Remove(dbPaciente);
                    //Guardamos los cambios
                    await dbContext.SaveChangesAsync();


                    //Mandamos un mensaje para decirle a la Api que todo ha salido bien
                    responseApi.Correcto = true;
                }
                else
                {
                    responseApi.Correcto = false;
                    responseApi.Mensaje = "No se han eliminado los datos del paciente";
                }
            }
            catch (Exception ex)
            {
                responseApi.Correcto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }
    }
}
