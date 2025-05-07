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

       
        public async Task<IActionResult> Details(int id)
        {
            var categorias = await _catalogosServices.GetCatalogoById(id); // Llama al servicio para obtener la categoria por ID

            if (categorias == null)
            {
                TempData["ErrorMessage"] = "La categoria no existe no existe.";
                return RedirectToAction("Index");
            }

            return View(categorias); 
        }

        public async Task<IActionResult> Edit(int id)
        {
            CatalogoDTO categoriaDTO = await _catalogosServices.GetCatalogoById(id); // Llama al servicio para obtener la categoria por ID
            if (categoriaDTO == null)
            {
                TempData["ErrorMessage"] = "Catalogo no encontrado.";
                return RedirectToAction("Index");
            }

            return View(categoriaDTO);
        }

        // Acción para procesar el formulario de edición de producto
        [HttpPost]
        public async Task<IActionResult> Edit(CatalogoDTO categoriaDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _catalogosServices.UpdateCatalogo(categoriaDTO);
                    TempData["SuccessMessage"] = "Catalogo actualizado exitosamente.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Hubo un error al actualizar el Catalogo.";
            }

            return View(categoriaDTO);
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var categoria = await _catalogosServices.GetCatalogoById(id); // Llama al servicio para obtener la categoria por ID

            if (categoria == null)
            {
                TempData["ErrorMessage"] = "Catalogo no existe.";
                return RedirectToAction("Index");
            }
            return View(categoria); // Muestra la vista de confirmación
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _catalogosServices.DeleteCatalogo(id); // Llama al servicio para eliminar la categoria
                TempData["SuccessMessage"] = "Catalogo eliminado exitosamente.";
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Hubo un error al eliminar el catalogo.";
            }

            return RedirectToAction("Index");
        }

    }
}
