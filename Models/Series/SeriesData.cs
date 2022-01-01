using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Models.Series
{
    public class SeriesData
    {
        public string seriesId { get; set; }
        [Required]
        public string name { set; get; }
        [Required]
        public string country { set; get; }
        [Required]
        public string time { set; get; }
        [Required]
        public DateTime CreateDate { set; get; }
        [Required]
        public string Describtion { set; get; }
        [Required]
        public int rate { set; get; }
        [Required]
        public IFormFile image { set; get; }
        [Required]
        public string TrailerURl { get; set; }
        public string type { get; set; }
        public string imageUrl { set; get; }
    }
}
