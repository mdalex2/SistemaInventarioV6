﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioV6.Modelos
{
    public class Bodega
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        [Display(Name ="Nombre bodega")]
        public string Nombre { get; set; }
        
        [Required]
        [MaxLength(100)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        
        [Required]
        public bool Estado { get; set; }
    }
}
