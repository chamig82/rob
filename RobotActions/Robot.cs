using SimulationLib.Enums;
using SimulationLib.Exceptions;

namespace SimulationLib
{
    public class Robot : IRobot
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public Direction Orientation { get; set; }
        public bool IsOnTheTable { get; private set; }

        public void PlaceOnTheTable(int x, int y, Direction direction)
        {
            XCoordinate = x;
            YCoordinate = y;
            Orientation = direction;
            IsOnTheTable = true;
        }

        public void Move(int x, int y)
        {
            XCoordinate = x;
            YCoordinate = y;
        }

        public int GetNewXCoordinate(int numberOfUnits)
        {
            switch (Orientation)
            {
                case Direction.EAST:
                    return XCoordinate + numberOfUnits;
                case Direction.WEST:
                    return XCoordinate - numberOfUnits;
                case Direction.NORTH:
                case Direction.SOUTH:
                    return XCoordinate;
                default:
                    throw new InvalidOrientationException("Invalid orientation");
            }

        }

        public int GetNewYCoordinate(int numberOfUnits)
        {
            switch (Orientation)
            {
                case Direction.NORTH:
                    return YCoordinate + numberOfUnits;
                case Direction.SOUTH:
                    return YCoordinate - numberOfUnits;
                case Direction.EAST:
                case Direction.WEST:
                    return YCoordinate;
                default:
                    throw new InvalidOrientationException("Invalid orientation");
            }
        }

        public void TurnLeft()
        {
            switch (Orientation)
            {
                case Direction.EAST:
                    Orientation = Direction.NORTH;
                    break;
                case Direction.WEST:
                    Orientation = Direction.SOUTH;
                    break;
                case Direction.NORTH:
                    Orientation = Direction.WEST;
                    break;
                case Direction.SOUTH:
                    Orientation = Direction.EAST;
                    break;
                default:
                    throw new InvalidOrientationException("Invalid orientation");
            }
        }

        public void TurnRight()
        {
            switch (Orientation)
            {
                case Direction.EAST:
                    Orientation = Direction.SOUTH;
                    break;
                case Direction.WEST:
                    Orientation = Direction.NORTH;
                    break;
                case Direction.NORTH:
                    Orientation = Direction.EAST;
                    break;
                case Direction.SOUTH:
                    Orientation = Direction.WEST;
                    break;
                default:
                    throw new InvalidOrientationException("Invalid orientation");
            }
        }

        public void ReportPosition()
        {
            Console.WriteLine($"Table position: x={XCoordinate}, y={YCoordinate}, orientation={Orientation}");
        }

    }
}
