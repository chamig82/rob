using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLib.Exceptions
{
    public class InvalidOrientationException : Exception
    {
        public InvalidOrientationException(string message) : base(message)
        {

        }

    }
}
