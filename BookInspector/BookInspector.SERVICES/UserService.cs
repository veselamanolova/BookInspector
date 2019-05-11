using BookInspector.DATA;
using BookInspector.DATA.Models;
using BookInspector.SERVICES.Contracts;
using BookInspector.SERVICES.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookInspector.SERVICES
{
    public class UserService:IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }        

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var usersList = await _context.Users
                .ToListAsync();

            List<UserDTO> userDTOs = new List<UserDTO>(); 

            foreach (var user in usersList)
            {
                userDTOs.Add(new UserDTO()
                {
                    Id = user.Id,
                    Name = user.UserName, 
                    Email = user.Email
                });
            }

            return userDTOs; 
        }
    }
}
