using SimulationLib.Enums;

namespace SimulationLib.Utils
{
    public class DirectionUtil
    {
        public static bool TryParseDirection(string input, out Direction direction)
        {
            return Enum.TryParse<Direction>(input, out direction);
        }
    }
}
