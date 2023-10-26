using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaDeCitas.Models;
using SistemaDeCitas.Shared;
using Microsoft.EntityFrameworkCore;
namespace SistemaDeCitas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        //vamos a utilizar nuestro servicio de datos
        private readonly SistemaCitasContext dbContext;

        //Voy a crear un constructor
        public CitasController(SistemaCitasContext dbContext)
        {
            this.dbContext = dbContext;//Llamamos a mi variable de base de datos
            //De esta manera ya tenemos inyectado nuestro servicio de base de datos en nuestro clase 
            //de controlador
        }

        //Nos va a devolver la lista de Citas
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> ListarCitas()
        {
            var responseApi = new ResponseAPI<List<CitasDTO>>();
            var ListaCitasDTO = new List<CitasDTO>();

            try
            {
                //Voy a crear un iteam por cada elemento que este recorriendo en la lista
                foreach(var item in await dbContext.TblCitas.ToListAsync())
                {
                    //Luego empezamos a traer los datos de mi CitasDTO para mostralos
                    ListaCitasDTO.Add(new CitasDTO
                    {
                        //Mostramos los datos
                        IdCita = item.IdCita,
                        Descripcion = item.Descripcion
                    });
                }
                responseApi.Correcto = true;
                responseApi.Valor = ListaCitasDTO;//Valor que vamos a devolver es la Lista
            }catch(Exception ex)
            {
                responseApi.Correcto =false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

    }
}
