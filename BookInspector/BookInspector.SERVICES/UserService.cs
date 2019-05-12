using BookInspector.DATA;
using BookInspector.DATA.Models;
using BookInspector.SERVICES.Contracts;
using BookInspector.SERVICES.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookInspector.SERVICES
{
    public class UserService: IUserService
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


        public async Task<UserDTO> GetUser(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id); 

            if(user == null)
            {
                return null; 
            }
            else
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                return new UserDTO()
                {
                    Id = user.Id,
                    Name = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                };                
            }        
        }


        public async Task EditUser(string id, string name, string email, List<string> newUserRoles)
        {
            ApplicationUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return; 
            }          

            else
            {
                user.UserName = name;
                user.Email = email;
                await _userManager.UpdateAsync(user);

                List<string> existingRoles = (await _userManager.GetRolesAsync(user)).ToList();

                foreach (var newUserRole in newUserRoles)
                {
                    if (!existingRoles.Contains(newUserRole))
                    {
                       await _userManager.AddToRoleAsync(user, newUserRole);
                    }
                }

                foreach (var existingRole in existingRoles)
                {
                    if (!newUserRoles.Contains(existingRole))
                    {
                        await _userManager.RemoveFromRoleAsync(user, existingRole);
                    }
                }                
            }
        }
    }
}
