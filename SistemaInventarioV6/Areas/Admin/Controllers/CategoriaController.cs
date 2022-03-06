using Microsoft.AspNetCore.Mvc;
using SistemaInventarioV6.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioV6.Modelos;

namespace SistemaInventarioV6.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public CategoriaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return  View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Categoria categoria = new Categoria();
            if (id == null)
            {
                return View(categoria);
            }
            categoria = await _unidadTrabajo.Categoria.Obtener(id.GetValueOrDefault());
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (categoria.Id == 0)
                {
                    await _unidadTrabajo.Categoria.Agregar(categoria);
                } else
                {
                    await _unidadTrabajo.Categoria.Actualizar(categoria);
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }


        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Categoria.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var categoriaDb = await _unidadTrabajo.Categoria.Obtener(id);
            if (categoriaDb == null)
            {
                return Json(new { success = false, message = "Error al borrar, no se encontró el registro." });
            } else
            {
                _unidadTrabajo.Categoria.Remover(categoriaDb);
                await _unidadTrabajo.Guardar();
            }
            return Json(new { success = true, message = "Categoria eliminada con éxito" });
        }
        #endregion
    }
}
