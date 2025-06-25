using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class TripNotFoundException : Exception
    {
        public TripNotFoundException(string message) : base(message)
        {

        }
      
    }
}
