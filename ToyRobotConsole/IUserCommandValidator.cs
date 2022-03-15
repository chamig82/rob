namespace ToyRobotConsole
{
    public interface IUserCommandValidator
    {
        void ValidatePlaceCommand(bool isPlacedOnTable, string[] userInputArgs);
        void ValidateDirection(string dir);
        void ValidateReportCommand(string[] userInputArgs);
        void ValidateMoveCommand(string[] userInputArgs);
        void ValidateLeftCommand(string[] userInputArgs);
        void ValidateRightCommand(string[] userInputArgs);
        void ValidateCommandAction(string userInputCommand);
        void ValidateEmptyUserInput(string userInput);
    }
}