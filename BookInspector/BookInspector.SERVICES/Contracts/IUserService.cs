
namespace BookInspector.SERVICES.Contracts
{   
    using BookInspector.SERVICES.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {        
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO> GetUser(string id);
        Task EditUser(string id, string name, string email, List<string> userRoles);
    }
}