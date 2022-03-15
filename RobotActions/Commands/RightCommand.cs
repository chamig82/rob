using SimulationLib.Services;


namespace SimulationLib.Commands
{
    public class RightCommand :ICommand
    {
        private readonly IRobot _robot;
        private IPlacementValidationService _validationService;


        public RightCommand(IRobot rob, IPlacementValidationService validationService)
        {
            _robot = rob;
            _validationService = validationService;
        }
        
        public void ExecuteCommand()
        {
            _robot.TurnRight();
        }

        public void CheckSafetyToExecute()
        {
            throw new NotImplementedException();
        }
    }
}
