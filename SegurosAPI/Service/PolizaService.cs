using Microsoft.EntityFrameworkCore;
using SegurosAPI.Data;
using SegurosAPI.DTOs;
using SegurosAPI.IService;

namespace SegurosAPI.Service
{
    public class PolizaService : IPolizaService
    {
        private readonly ApplicationDBContext _context;

        public PolizaService(ApplicationDBContext context)
        {
            _context = context;
        }


        #region "Obtener Poliza"

        // obtener toda la lista 
        public async Task<List<PolizaDTO>> GetAllPoliza()
        {
            var Poliza = await _context.Set<PolizaDTO>()
            .FromSqlRaw("usp_ObtenerListaPoliza").ToListAsync();
            return Poliza;

        }

        // obtener por id
        public async Task<PolizaDTO> GetPolizaById(int id)
        {
            var polizas = await _context.Set<PolizaDTO>()
             .FromSqlInterpolated($"EXEC usp_ObtenerPolizaById @Id = {id}")
             .AsNoTracking()
             .ToListAsync();


            var poliza = polizas.FirstOrDefault();

            if (poliza == null)
                throw new ApplicationException($"Catalogo con ID {id} no encontrado.");

            return poliza;
        }



        #endregion

        #region "Agregar Catalogo"
        public async Task AddPoliza(PolizaDTOCreate poliza)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC usp_InsertarPoliza
            @IdCatalogo = {poliza.IdCatalogo},
            @Nombre = {poliza.Nombre},
            @ApPaterno = {poliza.ApPaterno},
            @ApMaterno = {poliza.ApMaterno},
            @Edad = {poliza.Edad},
            @PaisNacimiento = {poliza.PaisNacimiento},
            @Genero = {poliza.Genero},
            @Correo = {poliza.Correo},
            @Telefono = {poliza.Telefono},
            @FechaInicio = {poliza.FechaInicio},
            @FechaFinal = {poliza.FechaFinal},
            @MontoPrima = {poliza.MontoPrima},
            @Estatus = {poliza.Estatus}");
            await _context.SaveChangesAsync();
        }

        #endregion

        #region "Actualizar Catalogo"
        public async Task UpdatePoliza(PolizaUpdateDTO poliza)
        {

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC usp_UpdatePoliza
            @Id = {poliza.Id},
            @IdCatalogo = {poliza.IdCatalogo},
            @Nombre = {poliza.Nombre},
            @ApPaterno = {poliza.ApPaterno},
            @ApMaterno = {poliza.ApMaterno},
            @Edad = {poliza.Edad},
            @PaisNacimiento = {poliza.PaisNacimiento},
            @Genero = {poliza.Genero},
            @Correo = {poliza.Correo},
            @Telefono = {poliza.Telefono},
            @FechaInicio = {poliza.FechaInicio},
            @FechaFinal = {poliza.FechaFinal},
            @MontoPrima = {poliza.MontoPrima},
            @Estatus = {poliza.Estatus}");
            await _context.SaveChangesAsync();
        }

        #endregion

        #region "Eliminar Catalogo"
        public async Task DeletePoliza(int id)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC usp_EliminarPoliza
            @Id = {id}");
            await _context.SaveChangesAsync();
        }

        #endregion

    } // fin clase
} // fin namespace
