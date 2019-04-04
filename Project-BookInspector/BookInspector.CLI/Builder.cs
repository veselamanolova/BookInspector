
namespace BookInspector.CLI
{
    using Autofac;
    using System.Linq;
    using System.Reflection;
    using BookInspector.CLI.Contracts;

    public class Builder
    {
        public void BuildApp()
        {
            var appBuilder = new ContainerBuilder();
            appBuilder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces();
        }
    }
}