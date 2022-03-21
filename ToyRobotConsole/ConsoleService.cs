using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimulationLib;
using SimulationLib.Exceptions;
using SimulationLib.Services;
using ToyRobotConsole.Exceptions;



namespace ToyRobotConsole
{
    public class ConsoleService {
 
        private IPlacementValidationService _placementValidationService;
        private ICommandService _commandService;
        private IRobotCommandHandler _commandHandler;
        private ICommandBuilder _commandBuilder;
        private IUserCommandValidator _userCommandValidator;
        private IRobot _robot;
        private int _numberOfUnits;

        public ConsoleService()
        {
            ResolveServices();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("AppConfig.json", optional: true)
                .Build();
            SetConfigurations(configuration);
            
        }


        public void StartService()
        {
            DisplayInstructions();

            bool run = true;               
          
            _robot = new Robot();
            _robot.XCoordinate = -1;
            _robot.YCoordinate = -1;
            while (run)
            {
                try
                {
                    var userInput = Console.ReadLine();
                    _userCommandValidator.ValidateEmptyUserInput(userInput);
                    var userInputArgs = userInput.Split(new string[] { " ", "," }, StringSplitOptions.None);

                    var action = GetCommandAction(userInput, userInputArgs);

                    switch (action)
                        {
                            case CommandAction.PLACE:
                                _userCommandValidator.ValidatePlaceCommand(_robot.IsOnTheTable, userInputArgs);
                                PerformPlaceAction(userInputArgs);
                                break;

                            case CommandAction.REPORT:
                                _userCommandValidator.ValidateReportCommand(userInputArgs);
                                PerformReportAction();
                                break;

                            case CommandAction.MOVE:
                                _userCommandValidator.ValidateMoveCommand(userInputArgs);
                                PerformMoveAction();
                                break;

                            case CommandAction.LEFT:
                                _userCommandValidator.ValidateLeftCommand(userInputArgs);
                                PerformLeftAction();
                                break;

                            case CommandAction.RIGHT:
                                _userCommandValidator.ValidateRightCommand(userInputArgs);
                                 PerformRightAction();
                                break;
                            default:
                                Console.WriteLine("Unrecognized command");
                                break;
                        }
                }
                catch (InvalidUserCommandException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (RobotNotPlacedException ex)
                {
                    Console.WriteLine("Robot not placed on the table, Use PLACE command");
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unhandled exception {ex.Message}");
                }
            }
        }

        #region Private methods
        private static void DisplayInstructions()
        {
            Console.WriteLine("Starting toy robot simulation console....");
            Console.WriteLine("Instructions:");
            Console.WriteLine("To place the robot on the table : PLACE X,Y,<Direction>)");
            Console.WriteLine("<Direction> Options: NORTH|SOUTH|EAST|WEST");
            Console.WriteLine("To Move the robot : MOVE");
            Console.WriteLine("To turn left : LEFT");
            Console.WriteLine("To turn right : RIGHT");
            Console.WriteLine("To report the location of the robot : REPORT");
            Console.WriteLine();
            Console.WriteLine("Enter commands to control the robot");
        }

        private void SetConfigurations(IConfigurationRoot configuration)
        {
            SetTableSize(configuration);
            int.TryParse(configuration["MoveIncrement"], out _numberOfUnits);
        }

        private void SetTableSize(IConfigurationRoot configuration)
        {
            var length = configuration["TableSize:length"];
            var width = configuration["TableSize:width"];

            int xLimit;
            int yLimit;
            int.TryParse(length,out xLimit);
            int.TryParse(width, out yLimit);
            _placementValidationService.SetXCoordinateLimit(xLimit);
            _placementValidationService.SetYCoordinateLimit(yLimit);
        }

        private void PerformRightAction()
        {
            try
            {
                _commandHandler.TurnRight(_robot, _placementValidationService, _commandBuilder, _commandService);
            }
            catch (ExecuteCommandException e)
            {
                Console.WriteLine($"Failed to execute RIGHT Command - {e.Message}");
            }
        }

        private void PerformLeftAction()
        {
            try
            {
                _commandHandler.TurnLeft(_robot, _placementValidationService, _commandBuilder, _commandService);
            }
            catch (ExecuteCommandException e)
            {
                Console.WriteLine($"Failed to execute LEFT Command - {e.Message}");
            }
        }

        private void PerformMoveAction()
        {
            try
            {
                _commandHandler.MoveRobot(_robot,_numberOfUnits,_placementValidationService, _commandBuilder, _commandService);
            }
            catch (InvalidPositionException ex)
            {
                Console.WriteLine($"Not safe to move - {ex.Message}");
                Console.WriteLine("Command MOVE will not be executed");
            }
            catch (ExecuteCommandException e)
            {
                Console.WriteLine($"Failed to execute MOVE Command - {e.Message}");
            }
        }

        private void PerformReportAction()
        {
            try
            {
                _commandHandler.ReportRobotPosition(_robot, _placementValidationService, _commandBuilder,_commandService);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to execute REPORT Command - {e.Message}");
            }
        }

        private void PerformPlaceAction(string[] userInputArgs)
        {
            try
            {
                int xCoord, yCoord;
                int.TryParse(userInputArgs[1], out xCoord);
                int.TryParse(userInputArgs[2], out yCoord);
                var direction = string.Empty;
                if (userInputArgs.Length != 3)
                {
                    direction = userInputArgs[3];
                    _userCommandValidator.ValidateDirection(direction);
                }
                _commandHandler.PlaceRobotOnTheTable(_robot,
                    xCoord, yCoord, direction, _placementValidationService, _commandBuilder, _commandService);
            }
            catch (InvalidPositionException ex)
            {
                Console.WriteLine($"Not a safe position to place - {ex.Message}");
                Console.WriteLine("Command PLACE will not be executed");
            }
            catch (ExecuteCommandException e)
            {
                Console.WriteLine($"Failed to execute PLACE Command - {e.Message}");
            }
        }


        private CommandAction GetCommandAction(string? userInput, string[] userInputArgs)
        {
            var userInputCommand = userInputArgs[0]?.ToString().ToUpper();

            CommandAction action;
            _userCommandValidator.ValidateCommandAction(userInputCommand);

            ActionUtil.TryParseAction(userInputCommand, out action);
            return action;
        }

        private void ResolveServices()
        {
            var provider = IServiceCollectionExtension.RegisterServices();

            _robot = provider.GetService<IRobot>();
            _placementValidationService = provider.GetService<IPlacementValidationService>();
            _commandService = provider.GetService<ICommandService>();
            _userCommandValidator = provider.GetService<IUserCommandValidator>();
            _commandBuilder = provider.GetService<ICommandBuilder>();
            _commandHandler = provider.GetService<IRobotCommandHandler>();
        }

        #endregion
    }

}
