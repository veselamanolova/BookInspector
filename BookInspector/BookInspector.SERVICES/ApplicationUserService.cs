using BookInspector.DATA;
using BookInspector.DATA.Models;
using BookInspector.SERVICES.Contracts;
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


        public UserService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));          
        }        

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _context.Users               
                .ToListAsync();
        }
    }
}
