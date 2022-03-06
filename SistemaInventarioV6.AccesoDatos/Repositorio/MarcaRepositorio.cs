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
    public class MarcaRepositorio : Repositorio<Marca>, IMarcaRepositorio
    {
        public readonly ApplicationDbContext _db;
        public MarcaRepositorio(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public async Task Actualizar(Marca marca)
        {
            var marcaDb = await _db.Marcas.FirstOrDefaultAsync(m => m.Id == marca.Id);
            if (marcaDb != null)
            {
                marcaDb.Nombre = marca.Nombre;
                marcaDb.Estado = marca.Estado;
            }
        }
    }
}
