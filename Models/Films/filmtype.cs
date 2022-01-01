using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Models.Films
{
    public class filmtype
    {
        public string filmId { set; get; }
        public int typeId { set; get; }
        public film film { set; get; }
        public filmSeriesType type { set; get; }
    }
}
