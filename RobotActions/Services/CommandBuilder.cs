using SimulationLib.Commands;
using SimulationLib.Enums;

namespace SimulationLib.Services;

public class CommandBuilder : ICommandBuilder
{
    public ICommand CreatePlaceCommand(IRobot robot, int xCoord, int yCoord, Direction direction, IPlacementValidationService validationService)
    {
        return new PlaceCommand(robot, xCoord, yCoord, direction, validationService);
    }

    public ICommand CreatePlaceCommand(IRobot robot, int xCoord, int yCoord, IPlacementValidationService validationService)
    {
        return new PlaceCommand(robot, xCoord, yCoord, robot.Orientation, validationService);
    }

    public ICommand CreateReportCommand(IRobot robot, IPlacementValidationService validationService)
    {
        return new ReportCommand(robot, validationService);
    }

    public ICommand CreateMoveCommand(IRobot robot, int numberOfUnits, IPlacementValidationService validationService)
    {
        return new MoveCommand(robot, numberOfUnits, validationService);
    }

    public ICommand CreateLeftCommand(IRobot robot, IPlacementValidationService validationService)
    {
        return new LeftCommand(robot, validationService);
    }

    public ICommand CreateRightCommand(IRobot robot, IPlacementValidationService validationService)
    {
        return new RightCommand(robot, validationService);
    }
}