using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV6.Modelos
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Número de serie")]
        [MaxLength(30)]
        public string NumeroSerie { get; set; }

        [Required]
        [Display(Name = "Descripción del producto")]
        [MaxLength(60)]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "Precio")]
        [Range(1,10000)]
        public double Precio { get; set; }

        [Required]
        [Display(Name = "Costo")]
        [Range(1, 10000)]
        public double Costo { get; set; }

        [Display(Name = "Imagen del producto")]
        public string? ImagenUrl { get; set; }

        [Required]
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; }

        [Required]
        public int MarcaId { get; set; }
        [ForeignKey("MarcaId")]
        public Marca? Marca { get; set; }

        //recursividad
        public int? PadreId { get; set; }
        public virtual Producto? Padre { get; set; }
    }
}
