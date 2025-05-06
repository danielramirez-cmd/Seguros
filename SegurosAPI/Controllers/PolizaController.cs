using Microsoft.AspNetCore.Mvc;
using SegurosAPI.DTOs;
using SegurosAPI.IService;
using SegurosAPI.Models;
using SegurosAPI.Service;


namespace SegurosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolizaController : ControllerBase
    {
        private readonly IPolizaService _polizaService;

        public PolizaController(IPolizaService polizaService)
        {
            _polizaService = polizaService;
        }

        #region "Obtener Catalogos"

        [HttpGet]
        public async Task<IActionResult> GetAllCatalogo()
        {
            var catalogos = await _polizaService.GetAllPoliza();

            if (catalogos == null)
            {
                return NotFound();
            }
            return Ok(catalogos);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalogoById(int id)
        {
            var catalogo = await _polizaService.GetPolizaById(id);
            if (catalogo == null)
            {
                return NotFound();
            }

            return Ok(catalogo);
        }


        #endregion

        #region "Agregar Catalogo"
        // POST api/<AssignmentController>
        [HttpPost]
        public async Task<ActionResult<PolizaDTOCreate>> Create([FromBody] PolizaDTOCreate polizaDTO)
        {
            try
            {
                await _polizaService.AddPoliza(polizaDTO);

                return Ok(new { message = "Poliza creada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Existe un error al crear la polizza: {ex.Message}" });
            }
        }
        #endregion

        #region "Actualizar Catalogo"
        [HttpPut]
        public async Task<ActionResult<PolizaModel>> UpdateCatalogo([FromBody] PolizaDTO poliza)
        {
            try
            {
                await _polizaService.UpdatePoliza(poliza);  // Calls the service to update the policy.
                return Ok(new { message = "Poliza Actualizado con éxito" });  // Returns 200 Ok with a success message.  
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error al actualizar la poliza: {ex.Message}" });   // Returns 400 Bad Request with an error message.
            }
        }
        #endregion

        #region "Eliminar Catalogo"
        [HttpDelete("{id}")]
        public async Task<ActionResult<PolizaModel>> DeletePoliza(int id)
        {
            try
            {
                await _polizaService.DeletePoliza(id);  // Calls the service to delete the policy.
                return Ok(new { message = " Poliza Eliminado Correctamente " });  // Returns 200 Ok with a success message.  
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Hay un problema al eliminar la poliza: {ex.Message}" });   // Returns 400 Bad Request with an error message.
            }
        }

        #endregion





    }
}
