using CarDealership.Application.Interfaces.Userinterface;
using CarDealership.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CarDealership.Infrastructure.Persistence;

namespace CarDealership.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarDealershipDbContext _ctx;
        public UserRepository(CarDealershipDbContext ctx) =>
            _ctx = ctx;

        public Task<bool> ExistsByEmailAsync(string email) =>
            _ctx.Users.AnyAsync(u => u.Email == email);

        public Task AddAsync(User user) =>
            _ctx.Users.AddAsync(user).AsTask();

        public Task<User> GetByEmailAsync(string email) =>
            _ctx.Users.SingleOrDefaultAsync(u => u.Email == email);
    }
}
