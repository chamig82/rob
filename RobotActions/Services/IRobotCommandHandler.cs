namespace SimulationLib.Services;

public interface IRobotCommandHandler
{
    void PlaceRobotOnTheTable(IRobot robot, int xCoord, int yCoord, string direction,
        IPlacementValidationService validationService, ICommandBuilder commandBuilder, ICommandService commandService);

    void ReportRobotPosition(IRobot robot, IPlacementValidationService validationService, ICommandBuilder commandBuilder, ICommandService commandService);
    void MoveRobot(IRobot robot, int numberOfUnits, IPlacementValidationService validationService, ICommandBuilder commandBuilder, ICommandService commandService);
    void TurnLeft(IRobot robot, IPlacementValidationService validationService, ICommandBuilder commandBuilder, ICommandService commandService);
    void TurnRight(IRobot robot, IPlacementValidationService validationService, ICommandBuilder commandBuilder, ICommandService commandService);
}