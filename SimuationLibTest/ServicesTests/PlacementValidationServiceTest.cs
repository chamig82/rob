using NUnit.Framework;
using SimulationLib.Exceptions;
using SimulationLib.Services;


namespace SimulationLibTest.ServicesTests
{
    [TestFixture]
    public class PlacementValidationServiceTest
    {
        private PlacementValidationService _placementValService;
        
        [SetUp]
        public void Setup()
        {
            _placementValService = new PlacementValidationService();
            _placementValService.XCoordinateLimit = 6;
            _placementValService.YCoordinateLimit = 6;
        }

        #region ValidatePosition test cases

        [Test]
        public void ValidatePosition_Should_Throw_Exception_When_Invalid_XPosition_Given
            ([Values(-1,7)] int x)
        {
            Assert.Throws<InvalidPositionException> 
                (()=>_placementValService.ValidatePosition(x ,5));
        }

        [Test]
        public void ValidatePosition_Should_Throw_Exception_When_Invalid_YPosition_Given
            ([Values(-1, 7)] int y)
        {
            Assert.Throws<InvalidPositionException>
                (() => _placementValService.ValidatePosition(5, y));
        }

        [Test]
        public void ValidatePosition_Should_Not_Throw_Exception_When_Valid_XYPosition_Given
            ([Range(0, 6)] int x, [Range(0, 6)] int y)
        {
            Assert.DoesNotThrow(() => _placementValService.ValidatePosition(x, y));
        }

        #endregion

        [Test]
        public void SetXCoordinateLimit_Should_Set_XCoordinateLimit()
        {
            _placementValService.SetXCoordinateLimit(6);
            Assert.AreEqual(6,_placementValService.XCoordinateLimit);
        }

        [Test]
        public void SetYCoordinateLimit_Should_Set_YCoordinateLimit()
        {
            _placementValService.SetYCoordinateLimit(6);
            Assert.AreEqual(6, _placementValService.YCoordinateLimit);
        }

    }
}
