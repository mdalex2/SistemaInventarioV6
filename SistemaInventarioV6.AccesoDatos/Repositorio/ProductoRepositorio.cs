using Microsoft.EntityFrameworkCore;
using SistemaInventarioV6.AccesoDatos.Data;
using SistemaInventarioV6.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioV6.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV6.AccesoDatos.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly ApplicationDbContext _db;
        public ProductoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task Actualizar(Producto producto)
        {
            var productoDb = await _db.Productos.FirstOrDefaultAsync(p => p.Id == producto.Id);
            if (productoDb != null)
            {
                productoDb.Id = producto.Id;
                productoDb.Descripcion = producto.Descripcion;
                productoDb.NumeroSerie = producto.NumeroSerie;
                productoDb.CategoriaId = producto.CategoriaId;
                productoDb.MarcaId = producto.MarcaId;
                productoDb.Precio = producto.Precio;
                productoDb.Costo = producto.Costo;
                if (producto.ImagenUrl != null)
                {
                    productoDb.ImagenUrl = producto.ImagenUrl;
                }
                productoDb.PadreId = producto.PadreId == 0 ? null : producto.PadreId;           
                
            }
        }
    }
}
