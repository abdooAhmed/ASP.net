﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Models.Account
{
    public class Login
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }
}
