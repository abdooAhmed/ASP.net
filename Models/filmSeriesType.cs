using e_c_Project.Models.Films;
using e_c_Project.Models.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Models
{
    public class filmSeriesType
    {
        [Key]
        public int typeID { set; get; }
        public string type { set; get; }
        public virtual ICollection<filmtype> filmtypes { get; set; }
        public virtual ICollection<serieType> serieTypes { get; set; }
    }
}
