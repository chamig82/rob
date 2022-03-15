using SimulationLib.Enums;
using SimulationLib.Services;


namespace SimulationLib.Commands
{
    public class PlaceCommand : ICommand
    {
        private readonly IRobot _robot;
        private readonly int _xCoordinate;
        private readonly int _yCoordinate;
        private readonly Direction _orientation;
        private IPlacementValidationService _validationService;
        public PlaceCommand(IRobot rob, int x, int y, Direction direction, IPlacementValidationService validationService)
        {
            _robot = rob;
            _xCoordinate = x;
            _yCoordinate = y;
            _orientation = direction;
            _validationService = validationService;
        }

        public void CheckSafetyToExecute()
        {
            _validationService.ValidatePosition(_xCoordinate, _yCoordinate);
        }

        public void ExecuteCommand()
        {  
             _robot.PlaceOnTheTable(_xCoordinate, _yCoordinate, _orientation);
        }
    }
}
