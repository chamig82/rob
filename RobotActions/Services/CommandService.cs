using SimulationLib.Commands;
using SimulationLib.Exceptions;

namespace SimulationLib.Services
{
    public class CommandService : ICommandService
    {
        private ICommand _command;
        public void SetCommand(ICommand commandAction)
        {
            _command = commandAction;
        }

        public void Invoke()
        {
            try
            {
                _command.ExecuteCommand();
            }
            catch (Exception ex)
            {
                throw new ExecuteCommandException($"Execute command failed - {ex.Message}");
            }
        }
    }
}
