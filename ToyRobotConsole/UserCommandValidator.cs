using ToyRobotConsole.Exceptions;


namespace ToyRobotConsole
{
    public class UserCommandValidator : IUserCommandValidator
    {
        public void ValidateEmptyUserInput(string userInput)
        {
            if (string.IsNullOrEmpty(userInput) || userInput.Length == 0)
            {
                throw new InvalidUserCommandException("Command is empty");
            }
        }
        
        public void ValidateCommandAction(string userInputCommand)
        {
            CommandAction action;
            if (!ActionUtil.TryParseAction(userInputCommand, out action))
            {
                throw new InvalidUserCommandException("Unrecognized command");
                
            }
        }
        public void ValidatePlaceCommand(bool isPlacedOnTable, string[] userInputArgs)
        {
            if ((!isPlacedOnTable && userInputArgs.Length != 4) ||
                                        (isPlacedOnTable && userInputArgs.Length > 4) ||
                                        (isPlacedOnTable && userInputArgs.Length < 3))
            {
                throw new InvalidUserCommandException("Invalid arguments for PLACE command. Give x,y and direction");
            }

            int xCoord;
            if (!int.TryParse(userInputArgs[1], out xCoord))
            {
                throw new InvalidUserCommandException("X is not a valid integer");
            }

            int yCoord;
            if (!int.TryParse(userInputArgs[2], out yCoord))
            {
                throw new InvalidUserCommandException("Y is not a valid integer");
            }
        }


        public void ValidateDirection(string dir)
        {
            var validInputDirections = new string[]{"NORTH","SOUTH","EAST","WEST"};
            if (!validInputDirections.Contains(dir.ToUpper()))
            {
                throw new InvalidUserCommandException("Direction is not valid");
            }
        }

        public void ValidateReportCommand(string[] userInputArgs)
        {
            if (userInputArgs.Length != 1)
            {
                throw new InvalidUserCommandException("Invalid arguments for REPORT command");
            }
        }

        public void ValidateMoveCommand(string[] userInputArgs)
        {
            if (userInputArgs.Length != 1)
            {
                throw new InvalidUserCommandException("Invalid arguments for MOVE command");
            }
        }

        public void ValidateLeftCommand(string[] userInputArgs)
        {
            if (userInputArgs.Length != 1)
            {
                throw new InvalidUserCommandException("Invalid arguments for LEFT command");
            }
           
        }

        public void ValidateRightCommand(string[] userInputArgs)
        {
            if (userInputArgs.Length != 1)
            {
                throw new InvalidUserCommandException("Invalid arguments for RIGHT command");
            }
        }
    }
}
