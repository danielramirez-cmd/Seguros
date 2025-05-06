using SegurosAPI.DTOs;

namespace SegurosAPI.IService
{
    public interface ICatalogoService
    {

        Task<List<CatalogoDTO>> GetAllCatalogo();
        Task<CatalogoDTO> GetCatalogoById(int id);
        Task AddCatalogo(CatalogoDTOCreate catalogo);
        Task UpdateCatalogo(CatalogoDTO catalogo);
        Task DeleteCatalogo(int id);




    } // fin interfaz
} // fin namespace
