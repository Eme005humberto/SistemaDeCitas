using SistemaDeCitas.Shared;
//Aqui vamos a agregar nuestro servicios
namespace IUCliente.Services
{
    public interface ICitasService
    {
        //Vamos agregar los metodos con los que vamos a trabajar en mi servicio citas
        Task<List<CitasDTO>> ListarCitas();
    }
}
