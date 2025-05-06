using SegurosAPI.DTOs;

namespace SegurosAPI.IService
{
    public interface IPolizaService
    {
        Task<List<PolizaDTO>> GetAllPoliza();
        Task<PolizaDTO> GetPolizaById(int id);

        Task AddPoliza(PolizaDTOCreate poliza);
        Task UpdatePoliza(PolizaDTO poliza);
        Task DeletePoliza(int id);


    } // fin interfaz
} // fin namespace
