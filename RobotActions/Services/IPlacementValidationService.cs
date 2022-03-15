using SimulationLib;

namespace SimulationLib.Services
{
    public interface IPlacementValidationService
    {
        void ValidatePosition(int x, int y);

        public void SetXCoordinateLimit(int xLimit);
        public void SetYCoordinateLimit(int yLimit);

    }
}