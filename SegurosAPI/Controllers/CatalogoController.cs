using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SegurosAPI.DTOs;
using SegurosAPI.IService;
using SegurosAPI.Models;

namespace SegurosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        // inyeccion de dependencias 
        private readonly ICatalogoService _catalogoService;
        public CatalogoController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        #region "Obtener Catalogos"

        [HttpGet]
        public async Task<IActionResult> GetAllCatalogo()
        {
            var catalogos = await _catalogoService.GetAllCatalogo();

            if (catalogos == null)
            {
                return NotFound();
            }
            return Ok(catalogos);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalogoById(int id)
        {
            var catalogo = await _catalogoService.GetCatalogoById(id);
            if (catalogo == null)
            {
                return NotFound();
            }

            return Ok(catalogo);
        }


        #endregion

        #region "Agregar Catalogo"
        // POST api/<AssignmentController>
        [HttpPost("Insertar")]
        public async Task<ActionResult<CatalogoDTOCreate>> Create([FromBody] CatalogoDTOCreate catalogoDTO)
        {
            try
            {
                await _catalogoService.AddCatalogo(catalogoDTO);  

                return Ok(new { message = "Catalogo creado correctamente" }); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Existe un error al crear el catalogo: {ex.Message}" }); 
            }
        }
        #endregion

        #region "Actualizar Catalogo"
        [HttpPut("Actualizar")]
        public async Task<ActionResult<CatalogoModel>> UpdateCatalogo([FromBody] CatalogoDTO catalogo)
        {
            try
            {
                await _catalogoService.UpdateCatalogo(catalogo);  
                return Ok(new { message = "Catalogo Actualizado con éxito" });  // Returns 200 Ok with a success message.  
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error al actualizar el catalogo: {ex.Message}" });   // Returns 400 Bad Request with an error message.
            }
        }
        #endregion

        #region "Eliminar Catalogo"
        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult<CatalogoModel>> DeleteCatalogo(int id)
        {
            try
            {
                await _catalogoService.DeleteCatalogo(id);
                return Ok(new { message = " Catalogo Eliminado Correctamente " });  // Returns 200 Ok with a success message.  
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Hay un problema al eliminar el catalogo: {ex.Message}" });   // Returns 400 Bad Request with an error message.
            }
        }

        #endregion







    }
}
