using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotConsole.Exceptions
{
    public class InvalidUserCommandException : Exception
    {
        public InvalidUserCommandException(string message) : base(message)
        {

        }
    }
}
