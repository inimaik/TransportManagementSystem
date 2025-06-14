using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Routes
    {
        public int RouteID { get; set; }
        public string StartDestination { get; set; }
        public string EndDestination { get; set; }
        public decimal Distance { get; set; }
    }
}
