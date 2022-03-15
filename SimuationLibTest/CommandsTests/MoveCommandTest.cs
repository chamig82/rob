using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SimulationLib;
using SimulationLib.Commands;
using SimulationLib.Enums;
using SimulationLib.Services;

namespace SimulationLibTest.CommandsTests
{
    [TestFixture]
    public class MoveCommandTest
    {
        private MoveCommand _moveCommand;
        private Mock<IRobot> _robotMock;
        private Mock<IPlacementValidationService> _placementValidationServiceMock;

        [SetUp]
        public void Setup()
        {
            _robotMock = new Mock<IRobot>();
            _placementValidationServiceMock = new Mock<IPlacementValidationService>();
            _moveCommand = new MoveCommand(_robotMock.Object,It.IsAny<int>(),
                _placementValidationServiceMock.Object);
        }

        [Test]
        public void ExecuteCommand_Should_Call_The_Robot_To_Move()
        {
            _moveCommand.ExecuteCommand();
            _robotMock.Verify(x => x.Move(It.IsAny<int>(), It.IsAny<int>()),Times.Once);
        }

        [Test]
        public void ValidateSafetyToExecute_Should_Make_A_Call_To_ValidatePosition()
        {
            _moveCommand.CheckSafetyToExecute();
            _placementValidationServiceMock.Verify(x => x.ValidatePosition(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

    }
}
