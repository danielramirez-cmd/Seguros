using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seguros.IServices;
using Seguros.Services;
using SegurosAPI.DTOs;

namespace Seguros.Controllers
{

    public class PolizaController : Controller
    {

        private readonly IPolizaServices _polizaServices;

        public PolizaController(IPolizaServices polizaServices)
        {
            // Inyección de dependencias
            string baseurl = "https://localhost:7028";
            _polizaServices = polizaServices;
        }

        public async Task<IActionResult> Index()
        {
            List<PolizaDTO> Lista = await _polizaServices.GetAllPoliza();
            return View(Lista);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PolizaDTOCreate poliza)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _polizaServices.CreatePoliza(poliza);
                    TempData["SuccessMessage"] = "Poliza agregada exitosamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Hubo un error al agregar la poliza";
            }
            return View(poliza);
        }


        public async Task<IActionResult> Details(int id)
        {
            var poliza = await _polizaServices.GetPolizaById(id); // Llama al servicio para obtener la poliza por ID

            if (poliza == null)
            {
                TempData["ErrorMessage"] = "La poliza no existe no existe.";
                return RedirectToAction("Index");
            }

            return View(poliza);
        }

        public async Task<IActionResult> Edit(int id)
        {
            PolizaDTO polizaDTO = await _polizaServices.GetPolizaById(id); // Llama al servicio para obtener la poliza por ID
            if (polizaDTO == null)
            {
                TempData["ErrorMessage"] = "Poliza no encontrado.";
                return RedirectToAction("Index");
            }

            return View(polizaDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PolizaDTO polizaDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _polizaServices.UpdatePoliza(polizaDTO);
                    TempData["SuccessMessage"] = "Poliza actualizado exitosamente.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Hubo un error al actualizar la Poliza.";
            }

            return View(polizaDTO);
        }



        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var poliza = await _polizaServices.GetPolizaById(id); // Llama al servicio para obtener la categoria por ID
            if (poliza == null)
            {
                TempData["ErrorMessage"] = "Poliza no existe.";
                return RedirectToAction("Index");
            }
            return View(poliza); // Muestra la vista de confirmación
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _polizaServices.DeletePoliza(id);
                TempData["SuccessMessage"] = "Poliza eliminado exitosamente.";
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Hubo un error al eliminar la poliza.";
            }

            return RedirectToAction("Index");
        }



    }
}
