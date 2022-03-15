using Moq;
using NUnit.Framework;
using SimulationLib.Commands;
using SimulationLib.Exceptions;
using SimulationLib.Services;

namespace SimulationLibTest.ServicesTests
{
    [TestFixture]
    public  class CommandServiceTest
    {
        private CommandService _commandService;
        private Mock<ICommand> _commandMock;
        
        [SetUp]
        public void Setup()
        {
           _commandService = new CommandService();
           _commandMock = new Mock<ICommand>();
        }

  
    
        #region Invoke method tests
        [Test]
        public void Invoke_Should_Execute_Command_When_Command_Is_Set()
        {
            _commandService.SetCommand(_commandMock.Object);
            _commandService.Invoke();
            _commandMock.Verify(x => x.ExecuteCommand(),Times.Once);
        }
        [Test]
        public void Invoke_Should_Throw_ExecuteCommandException_When_Command_Is_Not_Set()
        {
            Assert.Throws<ExecuteCommandException>(() => _commandService.Invoke());
        }
        #endregion



    }
}
