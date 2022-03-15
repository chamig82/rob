using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLib.Exceptions
{
    public class ExecuteCommandException : Exception
    {
        public ExecuteCommandException(string message) : base(message)
        {

        }

    }
}
