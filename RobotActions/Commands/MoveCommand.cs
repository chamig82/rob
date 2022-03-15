using SimulationLib.Services;


namespace SimulationLib.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly IRobot _robot;
        private readonly int _numberOfUnits;
        private readonly IPlacementValidationService _validationService;
        private int _newXCoordinate;
        private int _newYCoordinate;

        public MoveCommand(IRobot rob, int numberOfUnits, IPlacementValidationService validationService)
        {
            _robot = rob;
            _numberOfUnits = numberOfUnits;
            _validationService = validationService;
        }

        public void CheckSafetyToExecute()
        {
             _newXCoordinate = GetNewXCoordinate();
             _newYCoordinate = GetNewYCoordinate();

            _validationService.ValidatePosition(_newXCoordinate, _newYCoordinate);
        }

        public void ExecuteCommand()
        { 
            _robot.Move(_newXCoordinate, _newYCoordinate);           
        }

        private int GetNewXCoordinate()
        {
            return _robot.GetNewXCoordinate(_numberOfUnits);
        }

        private int GetNewYCoordinate()
        {
            return _robot.GetNewYCoordinate(_numberOfUnits);
        }
    }
}
