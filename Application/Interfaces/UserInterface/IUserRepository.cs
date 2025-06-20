using CarDealership.Domain.Entities;
using System.Threading.Tasks;

namespace CarDealership.Application.Interfaces.Userinterface
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmailAsync(string email);
        Task AddAsync(User user);
        Task<User> GetByEmailAsync(string email);
    }
}
