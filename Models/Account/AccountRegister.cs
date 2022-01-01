
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Models.Account
{
    public class AccountRegister
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Street { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public int Gender { set; get; }
        public string BirthDate { get; set; }

    }
}
