using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExceptions
{
    public class DriverNotFoundException : Exception
    {
        public DriverNotFoundException(string message) : base(message)
        {

        }
    }
}
