using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Models.Series
{
    public class EpisodeServer
    {
        [Key]
        public string LinkId { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public string ServerName { get; set; }

        public string episodeId { get; set; }
        public virtual Episode episode { get; set; }
        
    }
}
