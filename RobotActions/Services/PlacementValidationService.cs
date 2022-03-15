using SimulationLib.Exceptions;

namespace SimulationLib.Services
{
    public class PlacementValidationService : IPlacementValidationService
    {      
        public int XCoordinateLimit { get; set; }
        public int YCoordinateLimit { get; set; }

        public void ValidatePosition(int x, int y)
        {
            if (x > XCoordinateLimit || y > YCoordinateLimit || x < 0 || y < 0)
                throw new InvalidPositionException($"Invalid position : x = {x}, y = {y}");
        }

        public void SetXCoordinateLimit(int maximumXValue)
        {
            XCoordinateLimit = maximumXValue;
        }
        public void SetYCoordinateLimit(int maximumYValue)
        {
            YCoordinateLimit = maximumYValue;
        }

    }
}
