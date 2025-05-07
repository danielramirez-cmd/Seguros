

using SegurosAPI.DTOs;

namespace Seguros.IServices
{
    public interface ICatalogosServices
    {
      Task<List<CatalogoDTO>> GetAllCatalogo();
      Task<CatalogoDTO> GetCatalogoById(int id);
      Task <bool> CreateCatalogo(CatalogoDTOCreate catalogo);




    } // fin ICatalogosservicios 
} // fin namespace
