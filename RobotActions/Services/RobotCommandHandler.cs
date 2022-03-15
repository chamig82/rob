using SimulationLib.Enums;
using SimulationLib.Exceptions;
using SimulationLib.Utils;

namespace SimulationLib.Services
{
    public class RobotCommandHandler : IRobotCommandHandler
    {
        public void PlaceRobotOnTheTable(IRobot robot, int xCoord, int yCoord, string direction,
            IPlacementValidationService validationService, ICommandBuilder commandBuilder, ICommandService commandService)
        {
            if (!robot.IsOnTheTable && robot.XCoordinate != -1)
                throw new RobotNotPlacedException();

            Direction outDirection;
            if (string.IsNullOrEmpty(direction))
            {
                outDirection = robot.Orientation;
            }
            else
            {
                DirectionUtil.TryParseDirection(direction.ToUpper(),
                    out outDirection);
            }

            var placeCommand = commandBuilder.CreatePlaceCommand(robot, xCoord, yCoord, outDirection, validationService);
            commandService.SetCommand(placeCommand);
            placeCommand.CheckSafetyToExecute();
            commandService.Invoke();
        }

        public void ReportRobotPosition(IRobot robot, IPlacementValidationService validationService, ICommandBuilder commandBuilder, ICommandService commandService)
        {
            if (!robot.IsOnTheTable)
                throw new RobotNotPlacedException();

            var reportCommand = commandBuilder.CreateReportCommand(robot, validationService);
            commandService.SetCommand(reportCommand);
            commandService.Invoke();
        }

        public void MoveRobot(IRobot robot, int numberOfUnits, IPlacementValidationService validationService, ICommandBuilder commandBuilder, ICommandService commandService)
        {
            if (!robot.IsOnTheTable)
                throw new RobotNotPlacedException();

            var moveCommand = commandBuilder.CreateMoveCommand(robot, numberOfUnits, validationService);
            commandService.SetCommand(moveCommand);
            moveCommand.CheckSafetyToExecute();
            commandService.Invoke();
        }

        public void TurnLeft(IRobot robot, IPlacementValidationService validationService, ICommandBuilder commandBuilder, ICommandService commandService)
        {
            if (!robot.IsOnTheTable)
                throw new RobotNotPlacedException();

            var leftCommand = commandBuilder.CreateLeftCommand(robot, validationService);
            commandService.SetCommand(leftCommand);
            commandService.Invoke();
        }

        public void TurnRight(IRobot robot, IPlacementValidationService validationService, ICommandBuilder commandBuilder, ICommandService commandService)
        {
            if (!robot.IsOnTheTable)
                throw new RobotNotPlacedException();

            var rightCommand = commandBuilder.CreateRightCommand(robot, validationService);
            commandService.SetCommand(rightCommand);
            commandService.Invoke();
        }
    }
}
