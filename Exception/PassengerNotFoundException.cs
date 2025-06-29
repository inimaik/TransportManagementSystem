using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExceptions
{
    public class PassengerNotFoundException : Exception
    {
        public PassengerNotFoundException(string message) : base(message)
        {

        }
    }
}
