using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string area { get; set; }
        public string birthData { get; set; }
        public string city { get; set; }
        
        public int GenderId { set; get; }
        public Gender Gender { get; set; }
    }
}
