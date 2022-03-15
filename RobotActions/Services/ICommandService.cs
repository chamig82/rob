using SimulationLib.Commands;
using SimulationLib.Enums;

namespace SimulationLib.Services
{
    public interface ICommandService
    {
        void Invoke();
        void SetCommand(ICommand commandAction);
    }
}