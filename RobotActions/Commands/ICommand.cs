using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLib.Commands
{
    public interface ICommand
    {
        public void ExecuteCommand();
        public void CheckSafetyToExecute();

    }
}
