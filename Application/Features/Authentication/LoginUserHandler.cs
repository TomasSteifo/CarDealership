using MediatR;
using CarDealership.Application.Interfaces.Userinterface;
using CarDealership.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace CarDealership.Application.Features.Authentication
{
    public class LoginUserHandler
        : IRequestHandler<LoginUserCommand, LoginResultDto>
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordHasher<User> _hasher;
        private readonly IJwtTokenService _jwt;

        public LoginUserHandler(
            IUserRepository repo,
            IPasswordHasher<User> hasher,
            IJwtTokenService jwt)
        {
            _repo = repo;
            _hasher = hasher;
            _jwt = jwt;
        }

        public async Task<LoginResultDto> Handle(
            LoginUserCommand cmd,
            CancellationToken ct)
        {
            var user = await _repo.GetByEmailAsync(cmd.Email);
            if (user == null)
                return new LoginResultDto
                {
                    Success = false,
                    Errors = new[] { "Invalid credentials." }
                };

            var verify = _hasher.VerifyHashedPassword(
                user, user.PasswordHash, cmd.Password);

            if (verify == PasswordVerificationResult.Failed)
                return new LoginResultDto
                {
                    Success = false,
                    Errors = new[] { "Invalid credentials." }
                };

            return new LoginResultDto
            {
                Success = true,
                Token = _jwt.GenerateToken(user)
            };
        }
    }
}
