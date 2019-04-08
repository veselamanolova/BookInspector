
namespace BookInspector.Console.Commands
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class DeleteUser : ICommand
    {
        private readonly IUserService _userService;

        public DeleteUser(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            if (!args.Any())
            {
                throw new ArgumentException("Please provide a username");
            }

            var user = _userService.DeteleUser(args[0]);
            if (user == null)
            {
                return $"User {args[0]} does not exist";
            }

            return $"{user.Name}, Id: {user.UserId} was deleted!";
        }
    }
}
