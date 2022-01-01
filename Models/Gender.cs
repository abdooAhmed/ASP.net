using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace e_c_Project.Models
{
    public class Gender
    {
        [Key]
        public int GenderId { set; get; }
        public string gender { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
