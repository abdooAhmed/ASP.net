using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Models.Series
{
    public class serieType
    {
        public string serieId { set; get; }
        public int typeId { set; get; }
        public serie serie { set; get; }
        public filmSeriesType type { set; get; }
    }
}
