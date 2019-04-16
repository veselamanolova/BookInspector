
namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using BookInspector.Data.Models;
    using System.Collections.Generic;
    using BookInspector.Data.Repository;
    using BookInspector.Services.Contracts;

    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public User Register(string name)
        {
            var user = _userRepository.All().Where(u => u.Name == name).SingleOrDefault();

            Validator.IfIsNotInRange<ArgumentException>(name);
            Validator.IfNotNull<ArgumentException>(user, $"User {name} already exist.");

            user = new User() { Name = name };
            _userRepository.Add(user);
            _userRepository.Save();
            return user;
        }

        public User FindByName(string name)
        {
            var user = _userRepository.All().Where(x => x.Name.Equals(name)).SingleOrDefault();
            Validator.IfNull<ArgumentException>(user, $"User {name} does not exist.");

            return user;
        }

        public IReadOnlyCollection<User> GetUsers(int skip, int take)
        {
            return _userRepository.All()
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public User DeteleUser(string name)
        {
            var user = _userRepository.All().Where(u => u.Name.Equals(name)).SingleOrDefault();
            Validator.IfNull<ArgumentException>(user, $"User {name} does not exist.");
            
            _userRepository.Remove(user);
            _userRepository.Save();
            return user;
        }

        public User Modify(string name, string newUsername)
        {
            var user = _userRepository.All().Where(u => u.Name.Equals(name)).SingleOrDefault();
            user.Name = newUsername;

            Validator.IfNull<ArgumentException>(user, $"User {name} does not exist.");

            _userRepository.All().First(u => u.Name.Equals(name)).Name = newUsername;
            _userRepository.Save();
            return user;
        }
    }
}

