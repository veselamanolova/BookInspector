
namespace BookInspector.SERVICES.Contracts
{   
    using BookInspector.SERVICES.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {        
        Task<IEnumerable<UserDTO>> GetAllAsync(); 
    }
}