using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventarioV6.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioV6.Modelos;
using SistemaInventarioV6.Modelos.ViewModels;

namespace SistemaInventarioV6.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _hostEnvironment; //para las imagenes del productoVM
        public ProductoController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment hostEnvironment)
        {
            _unidadTrabajo = unidadTrabajo;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return  View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var prod = from p in await _unidadTrabajo.Producto.ObtenerTodos() select new {p.Descripcion};


            ProductoVM productoVM = new ProductoVM()
            {
                
                Producto = new Producto(),
                CategoriaLista = from s in await _unidadTrabajo.Categoria.ObtenerTodos() 
                select  new SelectListItem
                {
                    Text = s.Nombre,
                    Value = s.Id.ToString()
                },
                MarcaLista = from s in await _unidadTrabajo.Marca.ObtenerTodos() select
                new SelectListItem
                {
                    Text = s.Nombre,
                    Value = s.Id.ToString()
                },
                PadreLista = from s in await  _unidadTrabajo.Producto.ObtenerTodos() 
                select new SelectListItem 
                { 
                    Text = s.Descripcion,
                    Value = s.Id.ToString()
                }
            };

            if (id == null)
            {
                return View(productoVM);
            }
            productoVM.Producto = await _unidadTrabajo.Producto.Obtener(id.GetValueOrDefault());
            if (productoVM.Producto == null)
            {
                return NotFound();
            }
            return View(productoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                //cargar imagen
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\productos");
                    var extension = Path.GetExtension(files[0].FileName);
                    if (productoVM.Producto.ImagenUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath, productoVM.Producto.ImagenUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads,fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    productoVM.Producto.ImagenUrl = @"\images\productos\" + fileName + extension;
                } else
                {
                    //si en el update no se cambia la imagen
                    //se deja la imagen anterior para evitar que quede null
                    if (productoVM.Producto.Id != 0)
                    {
                        Producto productoDb = await _unidadTrabajo.Producto.Obtener(productoVM.Producto.Id);
                        productoVM.Producto.ImagenUrl = productoDb.ImagenUrl;
                    }

                }
                //fin carga imagen

                if (productoVM.Producto.Id == 0)
                {
                    await _unidadTrabajo.Producto.Agregar(productoVM.Producto);
                } else
                {
                    await _unidadTrabajo.Producto.Actualizar(productoVM.Producto);
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction("Index");
            } else
            {
                
                productoVM.CategoriaLista = _unidadTrabajo.Categoria.ObtenerTodos(f => f.Estado == true)
                .Result.Select
                (s => new SelectListItem
                {
                    Text = s.Nombre,
                    Value = s.Id.ToString()
                });

                //para los casos select específicos no anda el.select hay que separarlo
                var lstMarcas = await _unidadTrabajo.Marca.ObtenerTodos(f => f.Estado == true);
                productoVM.MarcaLista = lstMarcas.Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                });
                
                productoVM.PadreLista = _unidadTrabajo.Producto.ObtenerTodos().Result.Select(s => new SelectListItem
                {
                    Text = s.Descripcion,
                    Value = s.Id.ToString()
                });

                if (productoVM.Producto.Id != 0)
                {
                    productoVM.Producto = await _unidadTrabajo.Producto.Obtener(productoVM.Producto.Id);
                }
            }
            return View(productoVM);
        }


        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Producto.ObtenerTodos( incluirPropiedades: "Categoria,Marca");
            return Json(new { data = todos });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var productoDb = await _unidadTrabajo.Producto.Obtener(id);
            if (productoDb == null)
            {
                return Json(new { success = false, message = "Error al borrar, no se encontró el registro." });
            } 
            
            //borrado de la imagen en disco
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, productoDb.ImagenUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _unidadTrabajo.Producto.Remover(productoDb);
            await _unidadTrabajo.Guardar();
            
            return Json(new { success = true, message = "Producto eliminado con éxito" });
        }
        #endregion
    }
}
