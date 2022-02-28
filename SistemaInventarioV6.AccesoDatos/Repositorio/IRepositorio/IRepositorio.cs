using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV6.AccesoDatos.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task<T> Obtener(int id);

        Task<IEnumerable<T>> ObtenerTodos(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null
            );

        Task<T> ObtenerPrimero(
           Expression<Func<T, bool>> filter = null,
           string incluirPropiedades = null
           );

        Task Agregar(T entity);

        Task Remover(int id);

        void Remover(T entity);

        void RemoverRango(IEnumerable<T> entity);
    }

}

