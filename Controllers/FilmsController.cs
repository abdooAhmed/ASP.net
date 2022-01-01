using e_c_Project.BL;
using e_c_Project.Data;
using e_c_Project.Models;
using e_c_Project.Models.Films;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        
        private readonly IBaseRepository<film> _baseRepository;
        private readonly IBaseRepository<Author> _ActorRepository;
        private readonly IBaseRepository<filmSeriesType> _TypesRepository;
        private readonly IBaseRepository<filmtype> _FilmToTypesRepository;
        private readonly IBaseRepository<FilmServer> _FilmWatchRepository;
        private readonly IBaseRepository<AuthorToFilm> _FilmToActorRepository;

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment WebEnvironment;
        public FilmsController(IBaseRepository<AuthorToFilm> FilmToActorRepository, IBaseRepository<FilmServer> FilmWatchRepository, IBaseRepository<film> baseRepository, IBaseRepository<Author> ActorRepository, IBaseRepository<filmtype> FilmToTypesRepository, IBaseRepository<filmSeriesType> TypesRepository, IWebHostEnvironment e, ApplicationDbContext context)
        {
            _baseRepository = baseRepository;
            _FilmToActorRepository = FilmToActorRepository;
            _TypesRepository = TypesRepository;
            WebEnvironment = e;
            _FilmWatchRepository = FilmWatchRepository;
            _context = context;
            _ActorRepository = ActorRepository;
            _FilmToTypesRepository = FilmToTypesRepository;

        }



        



        [HttpGet]
        [Route("FilmsHome")]
        public async Task<ActionResult> FilmsHome()
        {
            
            var f = await _baseRepository.GetAll();
            return Ok(f);
        }

        [Authorize]
        [HttpGet]
        [Route("AddFilm")]
        public async Task<ActionResult> AddFilm()
        {
            var f = await _TypesRepository.GetAll();
            return Ok(f);
        }

        [Authorize]
        [HttpPost]
        [Route("AddFilm")]
        public async Task<ActionResult> AddFilm(film film)
        {
            film.filmId = Guid.NewGuid().ToString();
            await _baseRepository.Add(film);

            foreach(var s in film.Types)
            {
                if (s == 0)
                {
                    continue;
                }
                var ids = await _TypesRepository.GetByID(s);
                var type = new filmtype
                {
                    film = film,
                    type = ids,
                    typeId = ids.typeID,
                };
                await _FilmToTypesRepository.Add(type);
            }
            
            return Ok(new { film.filmId});
        }

        


        [HttpGet]
        [Route("SelectActor")]
        public async Task<IActionResult> SelectActor()
        {
            var actors = await _ActorRepository.GetAll();
            return Ok(actors);
        }



        [Route("SelectActor")]
        [HttpPost]
        public async Task<IActionResult> SelectActor(List<string> ids)
        {
            var x = ids.Count;
            
            var Filmid = ids[x-1];
            ids.RemoveAt(x - 1);
            var film = await _baseRepository.GetByID(Filmid);
            List<AuthorToFilm> filmToActors= new List<AuthorToFilm>();
            foreach(var id in ids)
            {
                var actor = await _ActorRepository.GetByID(id);
                var filmToActor = new AuthorToFilm { Author = actor, authorId = id, film = film, filmId = Filmid };
                filmToActors.Add(filmToActor);
                
            }
            if (await _FilmToActorRepository.AddRange(filmToActors))
            {
                return Ok(true);
            }
            else
            {
                return BadRequest();
            }
            return BadRequest();
        }



        
        
        [HttpGet("{id}")]
        public async Task<ActionResult<film>> FilmHome(string id)
        {
            var film = await _baseRepository.GetByID(b=>b.filmId == id,new[] { "AuthorToFilms", "filmtypes" } );
            foreach(var fToT in film.AuthorToFilms)
            {
                var f = await  _FilmToActorRepository.GetByID(b => b.authorId == fToT.authorId, new[] { "Author"});
                
            }
            foreach(var ttot in film.filmtypes)
            {
                var f = await _FilmToTypesRepository.GetByID(b => b.typeId == ttot.typeId, new[] { "type" });
            }
            String shortDateTime = film.CreateDate.ToString("dd/MM/yyyy");
            film.CreateDate = Convert.ToDateTime(shortDateTime);
            return film;
        }


        [HttpPost]
        [Route("AddServe")]
        public async Task<ActionResult<bool>> AddServe( List<FilmServer> filmWatch)
        {
            var film = await _baseRepository.GetByID(filmWatch[0].filmId);
            List<FilmServer> server = new List<FilmServer>();
            for (var i = 0;i<filmWatch.Count;i++)
            {
                server.Add(new FilmServer() { film = film, Link = filmWatch[i].Link, ServerName = filmWatch[i].ServerName, filmId = filmWatch[i].filmId, LinkId = Guid.NewGuid().ToString() });

            }
            var status = await _FilmWatchRepository.AddRange(server);
            return status;
        }

        [HttpPost]
        [Route("UploadFile")]
        public  ActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string uploadDir = Path.Combine(WebEnvironment.WebRootPath, "images/" + "films" + "/");
                string filePath = Path.Combine(uploadDir, file.FileName + ".jpg");
                using(var strem = new FileStream(filePath,FileMode.Create))
                {
                    file.CopyTo(strem);
                }
                var path = "~/images/" + "films" + "/" + "/" + file.FileName;
                return Ok(new { path });
            }
            catch(Exception e)
            {
                return BadRequest();
            }
            
        }


        

        
        [HttpGet("{id}/watch")]

        public async Task<ICollection<FilmServer>> GetServers(string id)
        {
            var film = await _baseRepository.GetByID(b => b.filmId == id, new[] { "filmWatches" });
            
            return film.filmWatches;
        }



    }
}
