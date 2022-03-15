using Microsoft.Extensions.DependencyInjection;
using SimulationLib;
using SimulationLib.Services;

namespace ToyRobotConsole
{
    public static class IServiceCollectionExtension    {
        public static ServiceProvider RegisterServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<IUserCommandValidator, UserCommandValidator>();
            services.AddScoped<IRobot, Robot>();
            services.AddScoped<IPlacementValidationService, PlacementValidationService>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<ICommandBuilder, CommandBuilder>();
            services.AddScoped<IRobotCommandHandler, RobotCommandHandler>();

            return services.BuildServiceProvider();
        }
    }
}
