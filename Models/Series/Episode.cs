using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Models.Series
{
    public class Episode
    {
        [Key]
        public string Episode_Id { get; set; }
        [Required]
        public int Episode_num { get; set; }

        public string SerieId { get; set; }

        public virtual serie serie { get; set; }

        public ICollection<EpisodeServer> episodeServers { get; set; }
    }
}
