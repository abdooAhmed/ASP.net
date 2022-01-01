using e_c_Project.BL;
using e_c_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {


        private readonly IBaseRepository<Author> _baseRepository;
        
        private readonly IWebHostEnvironment WebEnvironment;
        public AuthorsController( IWebHostEnvironment e, IBaseRepository<Author> baseRepository)
        {
            _baseRepository = baseRepository;
            WebEnvironment = e;
           

        }

        [HttpPost]
        [Route("AddActors")]
        public async Task<ActionResult> AddActors(Author author)
        {
            author.AuthorId = Guid.NewGuid().ToString();
            var f = await _baseRepository.Add(author);
            if (f)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("UploadActorFile")]
        public ActionResult UploadActorFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string uploadDir = Path.Combine(WebEnvironment.WebRootPath, "images/" + "authors" + "/");
                string filePath = Path.Combine(uploadDir, file.FileName + ".jpg");
                using (var strem = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(strem);
                }
                var path = "~/images/" + "films" + "/" + "/" + file.FileName;
                return Ok(new { path });
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
