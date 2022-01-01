
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Models.Series
{
    public class serie
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public string seriesId { set; get; }
        [Required]
        public string name { set; get; }
        
        public string country { set; get; }
        [Required]
        public string time { set; get; }
        public DateTime CreateDate { set; get; }
        [Required]
        public string Describtion { set; get; }
        public int rate { set; get; }
        public string image { set; get; }
        public string TrailerURl { get; set; }
        [NotMapped]
        public List<int> Types { get; set; }
        public virtual ICollection<serieType> serieTypes { get; set; }
        public virtual ICollection<AuthorToSeries> AuthorToSeries { get; set; }
        public virtual ICollection<Episode> episodes { get; set; }
    }
}
