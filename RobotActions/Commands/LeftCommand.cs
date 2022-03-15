using SimulationLib.Services;


namespace SimulationLib.Commands
{
    public class LeftCommand :ICommand
    {
        private readonly IRobot _robot;
        private IPlacementValidationService _validationService;


        public LeftCommand(IRobot rob, IPlacementValidationService validationService)
        {
            _robot = rob;
            _validationService = validationService;
        }
        
        public void ExecuteCommand()
        {
            _robot.TurnLeft();
        }

        public void CheckSafetyToExecute()
        {
           //Do nothing at the moment
        }
    }
}
