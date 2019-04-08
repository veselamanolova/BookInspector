

namespace BookInspector.App
{
    using Autofac;
    using System.Linq;
    using System.Reflection;
    using BookInspector.Services;
    using BookInspector.Data.Context;
    using BookInspector.App.Contracts;
    using BookInspector.App.Providers;
    using BookInspector.Services.Contracts;

    public class Builder
    {
        public void AppBuilder()
        {
            var appBuilder = new ContainerBuilder();
            appBuilder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces();

            appBuilder.RegisterType<BookInspectorContext>().AsSelf().InstancePerLifetimeScope();
            appBuilder.RegisterType<AuthorService>().As<IAuthorService>();
            appBuilder.RegisterType<PublisherService>().As<IPublisherService>();
            appBuilder.RegisterType<UserService>().As<IUserService>();
            appBuilder.RegisterType<CategoryService>().As<ICategoryService>();           
            appBuilder.RegisterType<RatingService>().As<IRatingService>();
            appBuilder.RegisterType<BookService>().As<IBookService>();


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