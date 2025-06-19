using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class VechileNotFoundException: Exception
    {
        public VechileNotFoundException(string message) : base(message)
        { 
           
        }
    }
}
