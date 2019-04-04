
namespace BookInspector.Console
{
    using Autofac;
    using System.Linq;
    using System.Reflection;
    using BookInspector.Console.Contracts;

    class Builder
    {
        public void AppBuilder()
        {
            var appBuilder = new ContainerBuilder();
            appBuilder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces();

            // appBuilder.RegisterType<.>().As<.>().SingleInstance();

            
            var commands = Assembly.GetExecutingAssembly()
                .DefinedTypes.Where(
                    typeInfo => typeInfo.ImplementedInterfaces.Contains(typeof(ICommand)))
                .ToList();

            foreach (var command in commands)
                appBuilder.RegisterType(command.AsType()).Named<ICommand>(command.Name.ToLower());
            
            var container = appBuilder.Build();
            var engine = container.Resolve<IRun>();
            engine.Run();
        }
    }
}
