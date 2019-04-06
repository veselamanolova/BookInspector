﻿
namespace BookInspector.Console.Commands
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BookInspector.App.Contracts;
    using BookInspector.Services.Contracts;

    public class FindUser : ICommand
    {
        private readonly IUserService userService;

        public FindUser(IUserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public string Execute(IReadOnlyList<string> args)
        {
            if (!args.Any())
            {
                throw new ArgumentException("Please provide a username as first parameter");
            }

            var user = this.userService.FindByName(args[0]);
            if (user == null)
            {
                return $"User {args[0]} does not exist";
            }

            return $"{user.Name}, Id: {user.UserId}";
        }
    }
}
