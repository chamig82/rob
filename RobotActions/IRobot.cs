using SimulationLib.Enums;

namespace SimulationLib
{
    public interface IRobot
    {
        int XCoordinate { get; set; }
        int YCoordinate { get; set; }
        bool IsOnTheTable { get;}
        Direction Orientation { get; set; }
        int GetNewXCoordinate(int numberOfUnits);
        int GetNewYCoordinate(int numberOfUnits);
        void Move(int x, int y);
        void PlaceOnTheTable(int x, int y, Direction direction);
        void ReportPosition();
        void TurnLeft();
        void TurnRight();
    }
}