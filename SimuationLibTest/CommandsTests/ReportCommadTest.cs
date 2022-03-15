using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SimulationLib;
using SimulationLib.Commands;
using SimulationLib.Services;

namespace SimulationLibTest.CommandsTests
{
    [TestFixture]
    public class ReportCommandTest
    {
        private ReportCommand _reportCommand;
        private Mock<IRobot> _robotMock;
        private Mock<IPlacementValidationService> _placementValidationServiceMock;

        [SetUp]
        public void Setup()
        {
            _robotMock = new Mock<IRobot>();
            _placementValidationServiceMock = new Mock<IPlacementValidationService>();
            _reportCommand = new ReportCommand(_robotMock.Object, _placementValidationServiceMock.Object);
        }

        [Test]
        public void ExecuteCommand_Should_Call_The_Robot_To_Report_Position()
        {
            _reportCommand.ExecuteCommand();
            _robotMock.Verify(x => x.ReportPosition(),Times.Once);
        }
    }
}
