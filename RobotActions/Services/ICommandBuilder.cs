using SimulationLib.Commands;
using SimulationLib.Enums;

namespace SimulationLib.Services;

public interface ICommandBuilder
{
    ICommand CreatePlaceCommand(IRobot robot, int xCoord, int yCoord, Direction direction, IPlacementValidationService validationService);
    ICommand CreatePlaceCommand(IRobot robot, int xCoord, int yCoord, IPlacementValidationService validationService);
    ICommand CreateReportCommand(IRobot robot, IPlacementValidationService validationService);
    ICommand CreateMoveCommand(IRobot robot, int numberOfUnits, IPlacementValidationService validationService);
    ICommand CreateLeftCommand(IRobot robot, IPlacementValidationService validationService);
    ICommand CreateRightCommand(IRobot robot, IPlacementValidationService validationService);
}