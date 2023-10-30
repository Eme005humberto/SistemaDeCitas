using SistemaDeCitas.Shared;
using System.Net.Http.Json;

namespace IUCliente.Services
{
    public class CitasService: ICitasService
{

        //agregar el servicio para poder hacer solicitudes http

        private readonly HttpClient _http;


        //Creamos un constructor para poder inyectar nuestro servicio
        public CitasService(HttpClient httpClient)
        {
            _http = httpClient;//Agregamos el servicio para recibir solicitudes http
        }
        //Nos trae nuestro metodo lista
        public async Task<List<CitasDTO>> ListarCitas()
        {
            //Vamos a ejecutar esta api la respuesta sera en formato JSON
            var resultado = await _http.GetFromJsonAsync<ResponseAPI<List<CitasDTO>>>("api/Citas/Lista");//Direccion de mi API

            //Validamos si la operacion fue correct nos devuelve el valor osea la lista
            if (resultado!.Correcto) //Si el resultado va a contener un valor por lo cual si es correcto
            {
                return resultado.Valor;
            }
            else
            {
                throw new Exception(resultado.Mensaje); //Nos devuelve el error
            }
        }
    }
}
