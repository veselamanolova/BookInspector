
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
            Validator.IfNullOrEmpty<ArgumentNullException>(name);
            Validator.IfIsNotInRange<ArgumentException>(name);
            Validator.IfExist<ArgumentException>(
                _context.User.Select(x => x.Name).ToList(), name, $"User {name} already exist.");

            var user = new User() { Name = name };
            _context.User.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User FindByName(string name)
        {
            Validator.IfNotExist<ArgumentException>(
                _context.User.Select(x => x.Name).ToList(), name, $"User {name} does not exist.");

            return _context.User.Single(u => u.Name == name);
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
            Validator.IfNotExist<ArgumentException>(
                _context.User.Select(x => x.Name).ToList(), name, $"User {name} does not exist.");

            var user = _context.User.Where(x => x.Name.Equals(name)).First();
            _context.User.Remove(user);
            _context.SaveChanges();
            return user;
        }

        public User Modify(string name, string newUsername)
        {
            Validator.IfNotExist<ArgumentException>(
                _context.User.Select(x => x.Name).ToList(), name, $"User {name} does not exist.");

            _context.User.First(u => u.Name.Equals(name)).Name = newUsername;
            _context.SaveChanges();
            return _context.User
                .FirstOrDefault(u => u.Name == newUsername);
        }
    }
}

