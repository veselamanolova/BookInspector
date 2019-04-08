
namespace BookInspector.Console.Commands
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class ShowAllUsers : ICommand
    {
        private readonly IUserService _userService;

        public ShowAllUsers(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            return string.Join(", ", _userService.GetUsers(int.Parse(args[0]), int.Parse(args[1])).Select(x => x.Name));
        }
    }
}
