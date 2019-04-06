namespace BookInspector.Console.Commands.UserCommands
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.Services.Contracts;

    public class AddUser : ICommand
    {
        private readonly IUserService userService;

        public AddUser(IUserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            if (!args.Any())
            {
                throw new ArgumentException("Please provide a username as first parameter");
            }

            var user = this.userService.Register(args[0]);

            return $"User {user.Name}, Id = {user.UserId} registered";
        }
               
    }
}
