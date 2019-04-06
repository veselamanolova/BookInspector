
namespace BookInspector.App
{
    using Autofac;
    using System.Linq;
    using System.Reflection;
    using BookInspector.App.Contracts;
    using BookInspector.App.Providers;

    public class Builder
    {
        public void AppBuilder()
        {
            var appBuilder = new ContainerBuilder();
            appBuilder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces();

            appBuilder.RegisterType<CommandParser>().As<ICommandParser>().SingleInstance();
            appBuilder.RegisterType<CommandProcessor>().As<ICommandProcessor>().SingleInstance();
            appBuilder.RegisterType<ConsoleReader>().As<IReader>().SingleInstance();
            appBuilder.RegisterType<ConsoleWriter>().As<IWriter>().SingleInstance();
            
            var commands = Assembly.GetExecutingAssembly()
                .DefinedTypes.Where(
                    typeInfo => typeInfo.ImplementedInterfaces.Contains(typeof(ICommand)))
                .ToList();
            foreach (var command in commands)
            {
                appBuilder.RegisterType(command.AsType()).Named<ICommand>(command.Name.ToLower());
            }
            
            var container = appBuilder.Build();
            var engine = container.Resolve<IRun>();
            engine.Run();
        }
    }
}