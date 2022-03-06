using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV6.Modelos
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Nombre de categoría")]
        public string Nombre { get; set; }
        [Required]
        public bool Estado { get; set; }
    }
}
