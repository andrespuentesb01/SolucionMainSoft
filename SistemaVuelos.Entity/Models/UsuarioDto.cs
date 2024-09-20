using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnMain.Domain.Models
{
    public class UsuarioDto
    {
        [Required]
        [MaxLength(50)]
        public string mail { get; set; } 
        public string password { get; set; }    
    }
}
