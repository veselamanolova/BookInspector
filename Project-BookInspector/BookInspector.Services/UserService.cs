
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
            Validator.IfExist<ArgumentException>(name);

            var user = new User() { Name = name };
            _context.User.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User FindByName(string name)
        {
            return _context.User
                .FirstOrDefault(u => u.Name == name);
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
            var user = _context.User
                .Where(x => x.Name.Equals(name))
                .FirstOrDefault();

            _context.User.Remove(user);
            _context.SaveChanges();

            return user;
        }

        public User Modify(string oldVal, string newVal)
        {
            _context.User.FirstOrDefault(u => u.Name.Equals(oldVal)).Name = newVal;
            _context.SaveChanges();
            return _context.User
                .FirstOrDefault(u => u.Name == newVal);
        }
    }
}


   