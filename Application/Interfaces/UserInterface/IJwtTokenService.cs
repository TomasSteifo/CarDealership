using CarDealership.Domain.Entities;

namespace CarDealership.Application.Interfaces.Userinterface
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}