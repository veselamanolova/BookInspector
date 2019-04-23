
namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using BookInspector.Data.Models;
    using System.Collections.Generic;
    using BookInspector.Data.Context;
    using BookInspector.Services.Contracts;

    public class UserService : IUserService
    {
        private readonly BookInspectorContext _context;

        public UserService(BookInspectorContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public User Register(string name)
        {
            var user = _context.User.Where(u => u.Name == name).SingleOrDefault();

            Validator.IfIsNotInRange<ArgumentException>(name);
            Validator.IfNotNull<ArgumentException>(user, $"User {name} already exist.");

            user = new User() { Name = name };
            _context.User.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User FindByName(string name)
        {
            var user = _context.User.Where(x => x.Name.Equals(name)).SingleOrDefault();
            Validator.IfNull<ArgumentException>(user, $"User {name} does not exist.");

            return user;
        }

        public IReadOnlyCollection<User> GetUsers(int skip, int take)
        {
            return _context.User
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public User DeteleUser(string name)
        {
            var user = _context.User.Where(u => u.Name.Equals(name)).SingleOrDefault();
            Validator.IfNull<ArgumentException>(user, $"User {name} does not exist.");
            
            _context.User.Remove(user);
            _context.SaveChanges();
            return user;
        }

        public User Modify(string name, string newUsername)
        {
            var user = _context.User.Where(u => u.Name.Equals(name)).SingleOrDefault();
            user.Name = newUsername;

            Validator.IfNull<ArgumentException>(user, $"User {name} does not exist.");

            _context.User.First(u => u.Name.Equals(name)).Name = newUsername;
            _context.SaveChanges();
            return user;
        }
    }
}

