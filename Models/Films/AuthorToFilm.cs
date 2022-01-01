using e_c_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Models.Films
{
    public class AuthorToFilm
    {
        public string filmId { set; get; }
        public string authorId { set; get; }
        public film film { set; get; }
        public Author Author { set; get; }
    }
}
