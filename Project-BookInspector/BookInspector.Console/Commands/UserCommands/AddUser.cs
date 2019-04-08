
namespace BookInspector.Console.Commands
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class AddUser : ICommand
    {
        private readonly IUserService _userService;

        public AddUser(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            if (!args.Any())
            {
                throw new ArgumentException("Please provide a username as first parameter");
            }

            var user = _userService.Register(args[0]);

            return $"User {user.Name}, Id = {user.UserId} registered";
        }
               
    }
}
