namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using BookInspector.Data.Models;
    using System.Collections.Generic;
    using BookInspector.Services.Interfaces;
    using BookInspector.Data.Context;    

    public class UserService : IUserService
    {

        private readonly BookInspectorContext context;

        public UserService(BookInspectorContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public User Register(string name)
        {
            
            if (this.context.User.Any(u => u.Name == name))
            {
                throw new ArgumentException($"User {name} already exists");
            }

            var user = new User() { Name = name };
            this.context.User.Add(user);
            this.context.SaveChanges();

            return user;
        }

        public User FindByName(string name)
        {
            return this.context.User
                .FirstOrDefault(u => u.Name == name);
        }

        public IReadOnlyCollection<User> GetUsers(int skip, int take)
        {
            return this.context.User
                .Skip(skip)
                .Take(take)
                .ToList();
        }
    }
}


   