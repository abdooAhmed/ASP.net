using e_c_Project.Models.Films;
using e_c_Project.Models.Series;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Models
{
    public class Author
    {
        [Key]
        public string AuthorId { set; get; }
        
        public string author { get; set; }
        public string ImageURl { get; set; }
        public IFormFile Image { get; set; }
        
        public virtual ICollection<AuthorToFilm> AuthorToFilms { get; set; }
        public virtual ICollection<AuthorToSeries> AuthorToSeries { get; set; }

    }
}
