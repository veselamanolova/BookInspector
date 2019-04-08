
namespace BookInspector.Console.Commands
{
    using System;
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
            var user = _userService.DeteleUser(args[0]);
            return $"{user.Name}, Id: {user.UserId} was deleted!";
        }
    }
}
