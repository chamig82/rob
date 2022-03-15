using SimulationLib.Services;


namespace SimulationLib.Commands
{
    public class ReportCommand :ICommand
    {
        private readonly IRobot _robot;
        private IPlacementValidationService _validationService;


        public ReportCommand(IRobot rob, IPlacementValidationService validationService)
        {
            _robot = rob;
            _validationService = validationService;
        }
        
        public void ExecuteCommand()
        {
            _robot.ReportPosition();
        }

        public void CheckSafetyToExecute()
        {
            throw new NotImplementedException();
        }
    }
}
