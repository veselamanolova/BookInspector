﻿
namespace BookInspector.Console.Commands
{
    using System;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class FindUser : ICommand
    {
        private readonly IUserService _userService;

        public FindUser(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            var user = _userService.FindByName(args[0]);
            return $"{user.Name}, Id: {user.UserId}";
        }
    }
}
