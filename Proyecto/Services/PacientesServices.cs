using SistemaDeCitas.Shared;
using System.Net.Http.Json;

namespace IUCliente.Services
{
    public class PacientesServices : IPacientesService
    {
        //En esta clase Vamos a implementar el servicio

        //agregar el servicio para poder hacer solicitudes http

        private readonly HttpClient _http;


        //Creamos un constructor para poder inyectar nuestro servicio
        public PacientesServices(HttpClient httpClient)
        {
            _http = httpClient;//Agregamos el servicio para recibir solicitudes http
        }
        public async Task<List<PacientesDTO>> ListarPacientes()
        {
            //Vamos a ejecutar esta api la respuesta sera en formato JSON
            var resultado = await _http.GetFromJsonAsync<ResponseAPI<List<PacientesDTO>>>("api/Paciente/Lista");//Direccion de mi API

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
        public async Task<PacientesDTO> Buscar(int id)
        {
            //Vamos a ejecutar esta api la respuesta sera en formato JSON
            var resultado = await _http.GetFromJsonAsync<ResponseAPI<PacientesDTO>>($"api/Paciente/Lista/{id}");//Direccion de mi API

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
        public async Task<int> Guardar(PacientesDTO pacientes)
        {
            //Vamos a ejecutar esta api la respuesta sera en formato JSON
            var resultado = await _http.PostAsJsonAsync<PacientesDTO>("api/Paciente/GuardarPaciente",pacientes);//Direccion de mi API
            var response = await resultado.Content.ReadFromJsonAsync<ResponseAPI<int>>();//Vamos
            //a leer el resultado en un JSON

            //Validamos si la operacion fue correct nos devuelve el valor osea la lista
            if (response!.Correcto) //Si el resultado va a contener un valor por lo cual si es correcto
            {
                return response.Valor;
            }
            else
            {
                throw new Exception(response.Mensaje); //Nos devuelve el error
            }
        }
        
        public async Task<int> Modificar(PacientesDTO pacientes)
        {
            //Vamos a ejecutar esta api la respuesta sera en formato JSON
            var resultado = await _http.PutAsJsonAsync<PacientesDTO>($"api/Paciente/EditarPaciente/{pacientes.IdPaciente}",pacientes);//Direccion de mi API
            var response = await resultado.Content.ReadFromJsonAsync<ResponseAPI<int>>();//Vamos
            //a leer el resultado en un JSON

            //Validamos si la operacion fue correct nos devuelve el valor osea la lista
            if (response!.Correcto) //Si el resultado va a contener un valor por lo cual si es correcto
            {
                return response.Valor;
            }
            else
            {
                throw new Exception(response.Mensaje); //Nos devuelve el error
            }
        }
        public async Task<bool> Eliminar(int id)
        {
            //Vamos a ejecutar esta api la respuesta sera en formato JSON
            var resultado = await _http.DeleteAsync($"api/Paciente/EliminarPaciente/{id}");//Direccion de mi API
            var response = await resultado.Content.ReadFromJsonAsync<ResponseAPI<int>>();//Vamos
            //a leer el resultado en un JSON

            //Validamos si la operacion fue correct nos devuelve el valor osea la lista
            if (response!.Correcto) //Si el resultado va a contener un valor por lo cual si es correcto
            {
                return response.Correcto;//Devolvemos un correcto por que es bool
            }
            else
            {
                throw new Exception(response.Mensaje); //Nos devuelve el error
            }
        }
    }
}
