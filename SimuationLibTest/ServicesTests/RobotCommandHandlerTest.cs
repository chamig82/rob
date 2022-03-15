using Moq;
using NUnit.Framework;
using SimulationLib;
using SimulationLib.Commands;
using SimulationLib.Enums;
using SimulationLib.Exceptions;
using SimulationLib.Services;

namespace SimulationLibTest.ServicesTests
{
    [TestFixture]
    public  class RobotCommandHandlerTest
    {
        private RobotCommandHandler _commandHandler;
        private Mock<ICommandBuilder> _commandBuilderMock;
        private Mock<ICommandService> _commandServiceMock;
        private Mock<IPlacementValidationService> _placementValidationServiceMock;
        private Mock<ICommand> _commandMock;

        [SetUp]
        public void Setup()
        {
            _commandHandler = new RobotCommandHandler();
            _commandBuilderMock = new Mock<ICommandBuilder>();
            _commandServiceMock = new Mock<ICommandService>();
            _placementValidationServiceMock = new Mock<IPlacementValidationService>();
            _commandMock = new Mock<ICommand>();
        }

        #region PlaceRobotOnTheTable

        [Test]
        public void PlaceRobotOnTheTable_Should_Call_CommandBuilder_To_Create_PlaceCommand_When_Robot_Is_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            rob.XCoordinate = 4;
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, true);
            
            _commandBuilderMock.Setup(x => x.CreatePlaceCommand(rob, It.IsAny<int>(),
                It.IsAny<int>(), It.IsAny<Direction>(), _placementValidationServiceMock.Object)).Returns(_commandMock.Object);
           
            
            //Act
            _commandHandler.PlaceRobotOnTheTable(rob, It.IsAny<int>(),
                It.IsAny<int>(),"NORTH", _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object);

            //Assert
            _commandBuilderMock.Verify(x => x.CreatePlaceCommand(rob, It.IsAny<int>(),
                It.IsAny<int>(), It.IsAny<Direction>(), _placementValidationServiceMock.Object));

        }

        [Test]
        public void PlaceRobotOnTheTable_Should_Call_CommandService_To_Set_And_Invoke_Command_When_Robot_Is_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            rob.XCoordinate = 4;
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, true);

            _commandBuilderMock.Setup(x => x.CreatePlaceCommand(rob, It.IsAny<int>(),
                It.IsAny<int>(), It.IsAny<Direction>(), It.IsAny<IPlacementValidationService>())).Returns(_commandMock.Object);


            //Act
            _commandHandler.PlaceRobotOnTheTable(rob, It.IsAny<int>(),
                It.IsAny<int>(), "NORTH", _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object);

            //Assert
            _commandServiceMock.Verify(x => x.SetCommand(_commandMock.Object));
            _commandServiceMock.Verify(x => x.Invoke());

        }

        [Test]
        public void PlaceRobotOnTheTable_Should_Check_Safety_To_Place_Robot_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            rob.XCoordinate = 4;
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, true);

            _commandBuilderMock.Setup(x => x.CreatePlaceCommand(rob, It.IsAny<int>(),
                It.IsAny<int>(), It.IsAny<Direction>(), It.IsAny<IPlacementValidationService>())).Returns(_commandMock.Object);


            //Act
            _commandHandler.PlaceRobotOnTheTable(rob, It.IsAny<int>(),
                It.IsAny<int>(), "NORTH", _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object);

            //Assert
            _commandMock.Verify(x => x.CheckSafetyToExecute());

        }

        [Test] public void PlaceRobotOnTheTable_Should_Set_Currrent_Orientation_Of_The_Robot_If_Orientation_Not_Passed_And_Robot_Is_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            rob.XCoordinate = 4;
            rob.Orientation = Direction.SOUTH;
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, true);

            _commandBuilderMock.Setup(x => x.CreatePlaceCommand(rob, It.IsAny<int>(),
                It.IsAny<int>(), It.IsAny<Direction>(), It.IsAny<IPlacementValidationService>())).Returns(_commandMock.Object);

            //Act
            _commandHandler.PlaceRobotOnTheTable(rob, It.IsAny<int>(),
                It.IsAny<int>(), "", _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object);

            //Assert
            Assert.AreEqual(Direction.SOUTH,rob.Orientation);
        }

        [Test]
        public void PlaceRobotOnTheTable_Should_Throw_RobotNotPlacedException_When_Orientation_Not_Passed_And_Robot_Is_Not_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, false);
            
            _commandBuilderMock.Setup(x => x.CreatePlaceCommand(rob, It.IsAny<int>(),
                It.IsAny<int>(), It.IsAny<Direction>(), It.IsAny<IPlacementValidationService>())).Returns(_commandMock.Object);

            //Act
            
            Assert.Throws<RobotNotPlacedException>(() =>
                _commandHandler.PlaceRobotOnTheTable(rob, It.IsAny<int>(),
                    It.IsAny<int>(), "", _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object));
        }

        #endregion


        #region ReportRobotPosition
        [Test]
        public void ReportRobotPosition_Should_Call_CreateReportCommand_When_Robot_Is_On_The_Table()
        {
            var rob = new Robot();
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, true);

            _commandHandler.ReportRobotPosition(rob, _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object);

            _commandBuilderMock.Verify(x => x.CreateReportCommand(rob,It.IsAny<IPlacementValidationService>()));
        }

        [Test]
        public void ReportRobotPosition_Should_Call_CommandService_To_Set_And_Invoke_Command_When_Robot_Is_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, true);

            //Act
            _commandHandler.ReportRobotPosition(rob, _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object);

            //Assert
            _commandServiceMock.Verify(x => x.SetCommand(It.IsAny<ReportCommand>()));
            _commandServiceMock.Verify(x => x.Invoke());
        }

        [Test]
        public void ReportRobotPosition_Should_Throw_RobotNotPlacedException_When_Robot_Is_Not_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, false);

            Assert.Throws<RobotNotPlacedException>(() =>
            _commandHandler.ReportRobotPosition(rob, _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object));
        }

        #endregion

        #region MoveRobot
        [Test]
        public void MoveRobot_Should_Call_CreateMoveCommand_When_Robot_Is_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, true);
            _commandBuilderMock.Setup(x => x.CreateMoveCommand(rob, 1, It.IsAny<IPlacementValidationService>()))
                .Returns(_commandMock.Object);

            //Act
            _commandHandler.MoveRobot(rob, 1,_placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object);

            //Assert
            _commandBuilderMock.Verify(x => x.CreateMoveCommand(rob, 1,It.IsAny<IPlacementValidationService>()));
        }

        [Test]
        public void MoveRobot_Should_Call_CommandService_To_Set_And_And_Invoke_Command_When_Robot_Is_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, true);
            _commandBuilderMock.Setup(x => x.CreateMoveCommand(rob, 1, It.IsAny<IPlacementValidationService>()))
                .Returns(_commandMock.Object);

            //Act
            _commandHandler.MoveRobot(rob, 1, _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object);

            //Assert
            _commandServiceMock.Verify(x => x.SetCommand(It.IsAny<MoveCommand>()));
            _commandServiceMock.Verify(x => x.Invoke());
        }

        [Test]
        public void MoveRobot_Should_Check_Safety_For_The_Move_When_Robot_Is_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, true);
            _commandBuilderMock.Setup(x => x.CreateMoveCommand(rob, 1, It.IsAny<IPlacementValidationService>()))
                .Returns(_commandMock.Object);

            //Act
            _commandHandler.MoveRobot(rob, 1, _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object);

            //Assert
            _commandMock.Verify(x => x.CheckSafetyToExecute());
        }

        [Test]
        public void MoveRobot_Should_Throw_RobotNotPlacedException_When_Robot_Is_Not_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, false);
            _commandBuilderMock.Setup(x => x.CreateMoveCommand(rob, 1, It.IsAny<IPlacementValidationService>()))
                .Returns(_commandMock.Object);

            Assert.Throws<RobotNotPlacedException>(() =>
                _commandHandler.MoveRobot(rob, 1, _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object));
        }



        #endregion

        #region TurnLeft
        [Test]
        public void TurnLeft_Should_Call_CreateLeftCommand_When_Robot_Is_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, true);
            _commandBuilderMock.Setup(x => x.CreateLeftCommand(rob, It.IsAny<IPlacementValidationService>()))
                .Returns(_commandMock.Object);

            //Act
            _commandHandler.TurnLeft(rob, _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object);

            //Assert
            _commandBuilderMock.Verify(x => x.CreateLeftCommand(rob, It.IsAny<IPlacementValidationService>()));
        }

        [Test]
        public void TurnLeft_Should_Call_CommandService_To_Set_And_And_Invoke_Command_When_Robot_Is_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, true);
            _commandBuilderMock.Setup(x => x.CreateLeftCommand(rob, It.IsAny<IPlacementValidationService>()))
                .Returns(It.IsAny<LeftCommand>());
           
            //Act
            _commandHandler.TurnLeft(rob, _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object);

            //Assert
            _commandServiceMock.Verify(x => x.SetCommand(It.IsAny<LeftCommand>()));
            _commandServiceMock.Verify(x => x.Invoke());
        }

        [Test]
        public void TurnLeft_Should_Throw_RobotNotPlacedException_When_Robot_Is_Not_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, false);
            _commandBuilderMock.Setup(x => x.CreateLeftCommand(rob, It.IsAny<IPlacementValidationService>()))
                .Returns(_commandMock.Object);

            Assert.Throws<RobotNotPlacedException>(() =>
                _commandHandler.TurnLeft(rob, _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object));
        }



        #endregion

        #region TurnRight
        [Test]
        public void TurnRight_Should_Call_CreateRightCommand_When_Robot_Is_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, true);
            _commandBuilderMock.Setup(x => x.CreateRightCommand(rob, It.IsAny<IPlacementValidationService>()))
                .Returns(_commandMock.Object);

            //Act
            _commandHandler.TurnRight(rob, _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object);

            //Assert
            _commandBuilderMock.Verify(x => x.CreateRightCommand(rob, It.IsAny<IPlacementValidationService>()));
        }

        [Test]
        public void TurnRight_Should_Call_CommandService_To_Set_And_And_Invoke_Command_When_Robot_Is_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, true);
            _commandBuilderMock.Setup(x => x.CreateRightCommand(rob, It.IsAny<IPlacementValidationService>()))
                .Returns(It.IsAny<LeftCommand>());

            //Act
            _commandHandler.TurnRight(rob, _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object);

            //Assert
            _commandServiceMock.Verify(x => x.SetCommand(It.IsAny<RightCommand>()));
            _commandServiceMock.Verify(x => x.Invoke());
        }

        [Test]
        public void TurnRight_Should_Throw_RobotNotPlacedException_When_Robot_Is_Not_On_The_Table()
        {
            //Arrange
            var rob = new Robot();
            typeof(Robot).GetProperty(nameof(Robot.IsOnTheTable)).SetValue(rob, false);
            _commandBuilderMock.Setup(x => x.CreateRightCommand(rob, It.IsAny<IPlacementValidationService>()))
                .Returns(_commandMock.Object);

            Assert.Throws<RobotNotPlacedException>(() =>
                _commandHandler.TurnRight(rob, _placementValidationServiceMock.Object, _commandBuilderMock.Object, _commandServiceMock.Object));
        }



        #endregion

    }
}
