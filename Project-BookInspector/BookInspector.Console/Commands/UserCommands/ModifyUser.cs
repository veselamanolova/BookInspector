
namespace BookInspector.Console.Commands
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class ModifyUser : ICommand
    {
        private readonly IUserService _userService;

        public ModifyUser(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            if (!args.Any())
            {
                throw new ArgumentException("Please provide a username as first parameter");
            }

            var user = _userService.Modify(args[0], args[1]);
            if (user == null)
            {
                return $"User {args[0]} does not exist";
            }

            return $"{args[0]}, Id: {user.UserId} was Changed to {user.Name}, Id: {user.UserId}.";
        }
    }
}