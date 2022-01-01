using e_c_Project.BL;
using e_c_Project.Models;
using e_c_Project.Models.Series;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly IBaseRepository<serie> _baseRepository;
        private readonly IBaseRepository<Author> _ActorRepository;
        private readonly IBaseRepository<filmSeriesType> _TypesRepository;
        private readonly IBaseRepository<serieType> _serieToTypesRepository;
        private readonly IBaseRepository<Episode> _EpisodeRepository;
        private readonly IBaseRepository<EpisodeServer> _episodeWatchRepository;
        private readonly IBaseRepository<AuthorToSeries> _SeriesToActorRepository;
        private readonly IWebHostEnvironment WebEnvironment;

        public SeriesController(IBaseRepository<Episode> EpisodeRepository,IBaseRepository<AuthorToSeries> FilmToActorRepository, IBaseRepository<EpisodeServer> FilmWatchRepository, IBaseRepository<serie> baseRepository, IBaseRepository<Author> ActorRepository, IBaseRepository<serieType> FilmToTypesRepository, IBaseRepository<filmSeriesType> TypesRepository, IWebHostEnvironment e)
        {
            _baseRepository = baseRepository;
            _SeriesToActorRepository = FilmToActorRepository;
            _TypesRepository = TypesRepository;
            WebEnvironment = e;
            _episodeWatchRepository = FilmWatchRepository;
            _EpisodeRepository = EpisodeRepository;
            _ActorRepository = ActorRepository;
            _serieToTypesRepository = FilmToTypesRepository;
        }

        [Authorize]
        [HttpGet]
        [Route("AddSeries")]
        public async Task<ActionResult> AddSeries()
        {
            var f = await _TypesRepository.GetAll();
            return Ok(f);
        }


        [Authorize]
        [HttpPost]
        [Route("AddSeries")]
        public async Task<ActionResult> AddSeries(serie serie)
        {
            serie.seriesId = Guid.NewGuid().ToString();
            await _baseRepository.Add(serie);

            foreach (var s in serie.Types)
            {
                if (s == 0)
                {
                    continue;
                }
                var ids = await _TypesRepository.GetByID(s);
                var type = new serieType
                {
                    serie = serie,
                    type = ids,
                    typeId = ids.typeID,
                };
                await _serieToTypesRepository.Add(type);
            }

            return Ok(new { serie.seriesId });
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

            var Filmid = ids[x - 1];
            ids.RemoveAt(x - 1);
            var film = await _baseRepository.GetByID(Filmid);
            List<AuthorToSeries> AuthorToSeries = new List<AuthorToSeries>();
            foreach (var id in ids)
            {
                var actor = await _ActorRepository.GetByID(id);
                var AuthorToSerie = new AuthorToSeries { Author = actor, authorId = id, serie = film, serieId = Filmid };
                AuthorToSeries.Add(AuthorToSerie);

            }
            if (await _SeriesToActorRepository.AddRange(AuthorToSeries)) 
            {
                return Ok(true);
            }
            else
            {
                return BadRequest(false);
            }
            
        }


        [HttpPost]
        [Route("UploadFile")]
        public ActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string uploadDir = Path.Combine(WebEnvironment.WebRootPath, "images/" + "series" + "/");
                string filePath = Path.Combine(uploadDir, file.FileName + ".jpg");
                using (var strem = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(strem);
                }
                var path = "~/images/" + "series" + "/" + "/" + file.FileName;
                return Ok(new { path });
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }




        [HttpGet]
        [Route("AddEpisods")]
        public async Task<IActionResult> AddEpisods()
        {
            var series = await _baseRepository.GetAll();
            var data = new List<seriesData>();
            foreach(var serie in series )
            {
                data.Add(new seriesData { seriesId = serie.seriesId, name = serie.name });
            }
            return Ok(data);
        }




        [HttpGet("AddEpisode/{id}")]
        public async Task<IActionResult> AddEpisode(string id)
        {
            var series = await _baseRepository.GetByID(b => b.seriesId == id, new[] { "episodes" });
            if(series.episodes.Count == 0)
            {
                var episod = new Episode() { Episode_Id = Guid.NewGuid().ToString() ,Episode_num=1,SerieId=id,serie=series};
                var status = await _EpisodeRepository.Add(episod);
                return Ok(new { episod.Episode_Id });
            }
            else if(series == null)
            {
                return BadRequest(false);
            }
            else
            {
                series.episodes.OrderBy(x => x.Episode_num);
                var lastEpisod = series.episodes.LastOrDefault();
                var episod = new Episode() { Episode_Id = Guid.NewGuid().ToString(), Episode_num = lastEpisod.Episode_num+1, SerieId = id, serie = series };
                var status = await _EpisodeRepository.Add(episod);
                return Ok(new { episod.Episode_Id });
            }
            

        }


        [HttpPost]
        [Route("AddEpisodeServe")]
        public async Task<ActionResult<bool>> AddEpisodeServe(List<EpisodeServer> episodeServers)
        {
            var episode = await _EpisodeRepository.GetByID(episodeServers[0].episodeId);
            List<EpisodeServer> server = new List<EpisodeServer>();
            for (var i = 0; i < episodeServers.Count; i++)
            {
                server.Add(new EpisodeServer() { episode = episode, Link = episodeServers[i].Link, ServerName = episodeServers[i].ServerName, episodeId = episodeServers[i].episodeId, LinkId = Guid.NewGuid().ToString() });

            }
            var status = await _episodeWatchRepository.AddRange(server);
            return status;
        }




        [HttpGet]
        [Route("SeriesHome")]
        public async Task<ActionResult> SeriesHome()
        {

            var f = await _baseRepository.GetAll();
            return Ok(f);
        }



        [HttpGet("SeriesView/{id}")]
        public async Task<ActionResult<serie>> SeriesView(string id)
        {
            var series = await _baseRepository.GetByID(b => b.seriesId == id, new[] { "AuthorToSeries", "serieTypes", "episodes" });
            
            foreach (var fToT in series.AuthorToSeries)
            {
                var f = await _SeriesToActorRepository.GetByID(b => b.authorId == fToT.authorId, new[] { "Author" });

            }
            foreach (var ttot in series.serieTypes)
            {
                var f = await _serieToTypesRepository.GetByID(b => b.typeId == ttot.typeId, new[] { "type" });
            }
            
            return series;
        }


        [HttpGet("{id}/watchEpisode")]

        public async Task<ICollection<EpisodeServer>> watchEpisode(string id)
        {
            var series = await _EpisodeRepository.GetByID(b => b.Episode_Id == id, new[] { "episodeServers" });

            return series.episodeServers;
        }







    }

    public class seriesData
    {
        public string seriesId { set; get; }
        
        public string name { set; get; }

    } 
}
