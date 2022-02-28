using Microsoft.EntityFrameworkCore;
using SistemaInventarioV6.AccesoDatos.Data;

namespace SistemaInventarioV6.Data
{
    public class SysDBContext : ApplicationDbContext
    {
        public SysDBContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }

}
