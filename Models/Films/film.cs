
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Models.Films
{
    public class film
    {
        [Key]
        public string filmId { set; get; }
        [Required]
        public string name { set; get; }
        public string country { set; get; }
        [Required]
        public string time { set; get; }
        [Required]
        public string Describtion { set; get; }
        public int rate { set; get; }
        
        public DateTime CreateDate { set; get; }
        public string image { set; get; }
        public string TrailerURl { get; set; }
        [NotMapped]
        public List<int> Types { get; set; }
        public virtual ICollection<filmtype> filmtypes { get; set; }
        public virtual ICollection<AuthorToFilm> AuthorToFilms { get; set; }
        
        
        public virtual ICollection<FilmServer> filmWatches { get; set; }
    }
}
