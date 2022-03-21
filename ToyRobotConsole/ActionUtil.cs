namespace ToyRobotConsole
{
    public enum CommandAction
    {
        PLACE,
        MOVE,
        LEFT,
        RIGHT,
        REPORT
    }

    public static class ActionUtil
    { public static bool TryParseAction(string input, out CommandAction action)
        {
            return Enum.TryParse<CommandAction>(input, out action);
        }
    }
}
