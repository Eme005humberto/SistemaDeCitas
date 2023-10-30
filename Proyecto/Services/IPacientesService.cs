using SistemaDeCitas.Shared;
namespace IUCliente.Services
{
    public interface IPacientesService
    {
        //Vamos a crear los metodos
        Task<List<PacientesDTO>> ListarPacientes();//Metodo para listar
        Task<PacientesDTO> Buscar(int id);//Metodo para buscar por Id
        Task<int> Guardar(PacientesDTO pacientes);//Metodo para guardar 
        Task<int> Modificar(PacientesDTO pacientes);//Metodo para modificar
        Task<bool> Eliminar(int id);//Metodo para eliminar

    }
}
