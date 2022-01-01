using e_c_Project.Models;
using Microsoft.AspNetCore.Mvc;
using e_c_Project.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_c_Project.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace e_c_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        
        // GET: api/<AccountController>
        public AccountController(UserManager<ApplicationUser> userMngr, SignInManager<ApplicationUser> signInMngr, ApplicationDbContext con)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            _context = con;
            

        }

        [HttpPost]
        [Route("Login")]
        public async Task<bool> Login(Login login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);
            if (user!=null)
            {
                
                var result = await signInManager.PasswordSignInAsync(login.Username, login.Password, isPersistent: login.RememberMe,lockoutOnFailure:false);
               
                var x = User.Identity.IsAuthenticated;

                return result.Succeeded;
            }
            return false;


        }

        
        [HttpGet]
        [Route("CheckAthorication")]
        public async Task<bool> CheckAthorication()
        {
            var x = User.Identity.IsAuthenticated;
            return x;
        }

        // POST api/<AccountController> 
        [HttpPost]
        [Route("Register")]
        public async Task<bool> RegisterAsync(AccountRegister account)
        {
            var user = new ApplicationUser
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                PhoneNumber = account.Phone,
                city = account.City,
                Street = account.Street,
                area = account.Area,
                UserName = account.FirstName + account.LastName,
                birthData = account.BirthDate,
                GenderId = 1,
                Gender = _context.genders.Where(c=>c.GenderId == account.Gender).FirstOrDefault()
            };
            var result = await userManager.CreateAsync(user, account.Password);
            if (result.Succeeded)
            {
                var i = result.Succeeded;
                await signInManager.SignInAsync(user, isPersistent: false);
                return true;
            }
            else
            {
                var i = result.Succeeded;
                return false;
            }
            
        }

        // PUT api/<AccountController>/5
        [HttpGet("{id}")]
        public ActionResult getAccount(int id)
        {
            int i = id;
            int x = i;
            return Ok();
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
