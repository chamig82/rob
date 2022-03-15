using System;
using System.IO;
using NUnit.Framework;
using SimulationLib;
using SimulationLib.Enums;
using SimulationLib.Exceptions;

namespace SimulationLibTest 
{
    public class RobotTest
    {
        private Robot _robot;

        [SetUp]
        public void Setup()
        {
            _robot = new Robot();
        }

        #region PlaceOnTheTable tests

        [Test]
        public void PlaceOnTheTable_Should_Place_Robot_On_The_Table()
        {
            //Arrange
            int x = 2;
            int y = 5;
            Direction orientation = Direction.EAST;
            
            //Act
            _robot.PlaceOnTheTable(x,y,orientation);

            //Assert
            Assert.AreEqual(2,_robot.XCoordinate);
            Assert.AreEqual(5,_robot.YCoordinate);
            Assert.AreEqual(Direction.EAST,_robot.Orientation);
            Assert.AreEqual(true,_robot.IsOnTheTable);
        }

        [Test]
        public void PlaceOnTheTable_Should_Place_Robot_On_The_New_Location_If_Already_On_The_Table()
        {
            //Arrange
            int x = 2;
            int y = 5;
            Direction orientation = Direction.EAST;
            _robot.XCoordinate = 1;
            _robot.YCoordinate = 4;
            _robot.Orientation = Direction.SOUTH;

            //Act
            _robot.PlaceOnTheTable(x, y, orientation);

            //Assert
            Assert.AreEqual(2, _robot.XCoordinate);
            Assert.AreEqual(5, _robot.YCoordinate);
            Assert.AreEqual(Direction.EAST, _robot.Orientation);
            Assert.AreEqual(true, _robot.IsOnTheTable);
        }

        #endregion

        #region Move tests

        [Test]
        public void Move_Should_Place_The_Robot_On_The_New_Position()
        {
            //Arrange
            int moveToX = 5;
            int moveToY = 6;
          
            _robot.XCoordinate = 1;
            _robot.XCoordinate = 2;
            _robot.Orientation = Direction.WEST;

            //Act
            _robot.Move(moveToX, moveToY);

            //Assert
            Assert.AreEqual(moveToX, _robot.XCoordinate);
            Assert.AreEqual(moveToY, _robot.YCoordinate);
        }

        [Test]
        public void Move_Should_Not_Change_Orientation()
        {
            //Arrange
            int moveToX = 5;
            int moveToY = 6;

            _robot.XCoordinate = 1;
            _robot.XCoordinate = 2;
            _robot.Orientation = Direction.WEST;

            //Act
            _robot.Move(moveToX, moveToY);

            //Assert
            Assert.AreEqual(Direction.WEST, _robot.Orientation);
        }

        #endregion

        #region GetNewXCoordinate tests

        [Test]
        public void GetNewXCoordinate_Should_Increase_XValue_By_NumberOfUnits_When_Orientation_Of_The_Robot_Is_East()
        {
            int incrment = 1;
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.EAST;

            var result =_robot.GetNewXCoordinate(incrment);

            Assert.AreEqual(3,result);
        }
        
        [Test]
        public void GetNewXCoordinate_Should_Decrease_XValue_By_NumberOfUnits_When_Orientation_Of_The_Robot_Is_West()
        {
            int incrment = 1;
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.WEST;

            var result = _robot.GetNewXCoordinate(incrment);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetNewXCoordinate_Should_Not_Change_XValue_By_NumberOfUnits_When_Orientation_Of_The_Robot_Is_North_Or_South
            ([Values(Direction.NORTH, Direction.SOUTH)] Direction direction)
        {
            int incrment = 1;
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = direction;

            var result = _robot.GetNewXCoordinate(incrment);

            Assert.AreEqual(2, result);
        }

        [Test]
        public void GetNewXCoordinate_Should_Throw_Exception_When_Invalid_Orientation_Given()
        {
            int incrment = 1;
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.DEFAULT;

            Assert.Throws<InvalidOrientationException>(()=>_robot.GetNewXCoordinate(incrment)) ;
        }
        #endregion

        #region GetNewYCoordinate
        [Test]
        public void GetNewYCoordinate_Should_Increase_YValue_By_NumberOfUnits_When_Orientation_Of_The_Robot_Is_North()
        {
            int incrment = 1;
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.NORTH;

            var result = _robot.GetNewYCoordinate(incrment);

            Assert.AreEqual(6, result);
        }

        [Test]
        public void GetNewYCoordinate_Should_Decrease_YValue_By_NumberOfUnits_When_Orientation_Of_The_Robot_Is_South()
        {
            int incrment = 1;
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.SOUTH;

            var result = _robot.GetNewYCoordinate(incrment);

            Assert.AreEqual(4, result);
        }

        [Test]
        public void GetNeYXCoordinate_Should_Not_Change_YValue_By_NumberOfUnits_When_Orientation_Of_The_Robot_Is_West_Or_East
            ([Values(Direction.WEST, Direction.EAST)] Direction direction)
        {
            int incrment = 1;
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = direction;

            var result = _robot.GetNewYCoordinate(incrment);

            Assert.AreEqual(5, result);
        }

        [Test]
        public void GetNewYCoordinate_Should_Throw_Exception_When_Invalid_Orientation_Given()
        {
            int incrment = 1;
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.DEFAULT;

            Assert.Throws<InvalidOrientationException>(() => _robot.GetNewYCoordinate(incrment));
        }

        #endregion

        #region TurnLeft tests
        [Test]

        public void TurnLeft_Should_Not_Change_XY_Cordinates()
        {
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.EAST;

            _robot.TurnLeft();

            Assert.AreEqual(2, _robot.XCoordinate);
            Assert.AreEqual(5, _robot.YCoordinate);
        }

        [Test]
        public void TurnLeft_Should_Set_Orientation_Of_The_Robot_As_North_When_Current_Orientation_Is_East()
        {
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.EAST;

             _robot.TurnLeft();

            Assert.AreEqual(Direction.NORTH, _robot.Orientation);
        }

        [Test]
        public void TurnLeft_Should_Set_Orientation_Of_The_Robot_As_South_When_Current_Orientation_Is_West()
        {
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.WEST;

            _robot.TurnLeft();

            Assert.AreEqual(Direction.SOUTH, _robot.Orientation);
        }

        [Test]
        public void TurnLeft_Should_Set_Orientation_Of_The_Robot_As_East_When_Current_Orientation_Is_South()
        {
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.SOUTH;

            _robot.TurnLeft();

            Assert.AreEqual(Direction.EAST, _robot.Orientation);
        }

        [Test]
        public void TurnLeft_Should_Set_Orientation_Of_The_Robot_As_West_When_Current_Orientation_Is_North()
        {
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.NORTH;

            _robot.TurnLeft();

            Assert.AreEqual(Direction.WEST, _robot.Orientation);
        }

        [Test]
        public void TurnLeft_Should_Throw_InvalidOrientationException_When_Invalid_Orientation_Is_Set()
        {
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.DEFAULT;
            

            Assert.Throws<InvalidOrientationException>(() => _robot.TurnLeft());
        }

        #endregion

        #region TurnRight tests
        [Test]
        public void TurnRight_Should_Not_Change_XY_Cordinates()
        {
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.EAST;

            _robot.TurnRight();

            Assert.AreEqual(2, _robot.XCoordinate);
            Assert.AreEqual(5, _robot.YCoordinate);
        }

        [Test]
        public void TurnRight_Should_Set_Orientation_Of_The_Robot_As_West_When_Current_Orientation_Is_South()
        {
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.SOUTH;

            _robot.TurnRight();

            Assert.AreEqual(Direction.WEST, _robot.Orientation);
        }

        [Test]
        public void TurnRight_Should_Set_Orientation_Of_The_Robot_As_North_When_Current_Orientation_Is_West()
        {
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.WEST;

            _robot.TurnRight();

            Assert.AreEqual(Direction.NORTH, _robot.Orientation);
        }

        [Test]
        public void TurnRight_Should_Set_Orientation_Of_The_Robot_As_East_When_Current_Orientation_Is_North()
        {
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.NORTH;

            _robot.TurnRight();

            Assert.AreEqual(Direction.EAST, _robot.Orientation);
        }

        [Test]
        public void TurnRight_Should_Set_Orientation_Of_The_Robot_As_South_When_Current_Orientation_Is_East()
        {
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.EAST;

            _robot.TurnRight();

            Assert.AreEqual(Direction.SOUTH, _robot.Orientation);
        }

        [Test]
        public void TurnRight_Should_Throw_InvalidOrientationException_When_Invalid_Orientation_Is_Set()
        {
            _robot.XCoordinate = 2;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.DEFAULT;

            Assert.Throws<InvalidOrientationException>(() => _robot.TurnRight());
        }
        #endregion

        #region ReportPosition
        [Test]
        public void ReportPosition_Should_Write_Position_To_Console()
        {
            //Arrange
            _robot.XCoordinate = 3;
            _robot.YCoordinate = 5;
            _robot.Orientation = Direction.EAST;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //Act
            _robot.ReportPosition();
            
            //Assert
            var expectedResult = "Table position: x=3, y=5, orientation=EAST\r\n";
            var output = stringWriter.ToString();
            Assert.AreEqual(expectedResult, output);
        }
        #endregion
    }
}