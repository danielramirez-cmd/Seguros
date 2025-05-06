using Microsoft.EntityFrameworkCore;
using SegurosAPI.Data;
using SegurosAPI.DTOs;
using SegurosAPI.IService;

namespace SegurosAPI.Service
{
    public class CatalogoService : ICatalogoService
    {
        // inyeccion de dependencias 

        private readonly ApplicationDBContext _context;

        public CatalogoService(ApplicationDBContext context)
        {
            _context = context;
        }


        #region "Obtener Catalogos"

        // obtener toda la lista 
        public async Task<List<CatalogoDTO>> GetAllCatalogo()
        {
            var catalogo = await _context.Set<CatalogoDTO>()
            .FromSqlRaw("usp_ObtenerListaCatalogos").ToListAsync();
            return catalogo;

        }

        // obtener por id
        public async Task<CatalogoDTO> GetCatalogoById(int id)
        {
            var catalogos = await _context.Set<CatalogoDTO>()
             .FromSqlInterpolated($"EXEC usp_ObtenerCatalogoById @Id = {id}")
             .AsNoTracking()
             .ToListAsync();


            var catalogo = catalogos.FirstOrDefault();

            if (catalogo == null)
                throw new ApplicationException($"Catalogo con ID {id} no encontrado.");

            return catalogo;
        }



        #endregion

        #region "Agregar Catalogo"
        public async Task AddCatalogo(CatalogoDTOCreate catalogo)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC usp_InsertarCatalogo 
            @Tipo = {catalogo.Tipo},  
            @Observaciones = {catalogo.Observaciones}");
            await _context.SaveChangesAsync();
        }

        #endregion
        
        #region "Actualizar Catalogo"
        public async Task UpdateCatalogo(CatalogoDTO catalogo)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC usp_UpdateCatalogo
            @Id = {catalogo.Id},
            @Tipo = {catalogo.Tipo},  
            @Observaciones = {catalogo.Observaciones}");
            await _context.SaveChangesAsync();
        }

        #endregion

        #region "Eliminar Catalogo"
        public async Task DeleteCatalogo(int id)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC usp_EliminarCatalogo
            @Id = {id}");
            await _context.SaveChangesAsync();
        }

        #endregion



    } // fin clase
} // fin namespace 
