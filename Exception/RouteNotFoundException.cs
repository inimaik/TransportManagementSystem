using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExceptions
{
    public class RouteNotFoundException : Exception
    {
        public RouteNotFoundException(string message) : base(message)
        {

        }
    }
}
