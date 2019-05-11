
namespace BookInspector.SERVICES.Contracts
{
    using BookInspector.DATA.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync(); 
    }
}