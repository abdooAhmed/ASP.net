using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Models.Films
{
    public class FilmServer
    {
        [Key]
        public string LinkId { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public string ServerName { get; set; }
        
        [Required]
        
        public string filmId { get; set; }
        public virtual film film { get; set; }
        
    }
}
