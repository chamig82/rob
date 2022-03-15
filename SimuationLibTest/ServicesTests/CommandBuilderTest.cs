using Moq;
using NUnit.Framework;
using SimulationLib;
using SimulationLib.Commands;
using SimulationLib.Enums;
using SimulationLib.Services;

namespace SimulationLibTest.ServicesTests
{
    [TestFixture]
    public  class CommandBuilderTest
    {
        private CommandBuilder _commandBuilder;
        
        [SetUp]
        public void Setup()
        {
            _commandBuilder = new CommandBuilder();
        }
        
        [Test]
        public void CreatePlaceCommand_Should_Return_PlaceCommand()
        {
            var result = _commandBuilder.CreatePlaceCommand(new Robot(),It.IsAny<int>(),
                It.IsAny<int>(),It.IsAny<Direction>(),new PlacementValidationService());
            Assert.IsInstanceOf<PlaceCommand>(result);
        }

        [Test]
        public void CreateReportCommand_Should_Return_ReportCommand()
        {
            var result = _commandBuilder.CreateReportCommand(new Robot(),new PlacementValidationService());
            Assert.IsInstanceOf<ReportCommand>(result);
        }

        [Test]
        public void CreateMoveCommand_Should_Return_MoveCommand()
        {
            var result = _commandBuilder.CreateMoveCommand(new Robot(),It.IsAny<int>(), new PlacementValidationService());
            Assert.IsInstanceOf<MoveCommand>(result);
        }

        [Test]
        public void CreateLeftCommand_Should_Return_LeftCommand()
        {
            var result = _commandBuilder.CreateLeftCommand(new Robot(),new PlacementValidationService());
            Assert.IsInstanceOf<LeftCommand>(result);
        }

        [Test]
        public void CreateRightCommand_Should_Return_RightCommand()
        {
            var result = _commandBuilder.CreateRightCommand(new Robot(), new PlacementValidationService());
            Assert.IsInstanceOf<RightCommand>(result);
        }



    }
}
