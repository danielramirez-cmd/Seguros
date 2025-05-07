using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seguros.IServices;
using SegurosAPI.DTOs;


namespace Seguros.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ICatalogosServices _catalogosServices;

        public CatalogoController(ICatalogosServices catalogosServices)
        {
            // Inyección de dependencias
            string baseurl = "https://localhost:7028";
            _catalogosServices = catalogosServices;
        }

        public async Task<IActionResult> Index()
        {
            List<CatalogoDTO> Lista = await _catalogosServices.GetAllCatalogo();
            return View(Lista);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CatalogoDTOCreate categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _catalogosServices.CreateCatalogo(categoria);
                    TempData["SuccessMessage"] = "Catalogo agregada exitosamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Hubo un error al agregar el catalogo";
            }
            return View(categoria);
        }


    }
}
