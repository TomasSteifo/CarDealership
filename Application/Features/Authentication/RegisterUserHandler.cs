using MediatR;
using CarDealership.Application.Interfaces.Userinterface;
using CarDealership.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace CarDealership.Application.Features.Authentication
{
    public class RegisterUserHandler
        : IRequestHandler<RegisterUserCommand, RegisterResultDto>
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordHasher<User> _hasher;
        private readonly IJwtTokenService _jwt;

        public RegisterUserHandler(
            IUserRepository repo,
            IPasswordHasher<User> hasher,
            IJwtTokenService jwt)
        {
            _repo = repo;
            _hasher = hasher;
            _jwt = jwt;
        }

        public async Task<RegisterResultDto> Handle(
            RegisterUserCommand cmd,
            CancellationToken ct)
        {
            if (await _repo.ExistsByEmailAsync(cmd.Email))
                return new RegisterResultDto
                {
                    Success = false,
                    Errors = new[] { "Email already in use." }
                };

            var user = new User(
                cmd.FirstName, cmd.LastName, cmd.Email,
                cmd.Mobile, cmd.Address, cmd.Postcode,
                cmd.City, passwordHash: string.Empty);

            var hashed = _hasher.HashPassword(user, cmd.Password);
            user = new User(
                cmd.FirstName, cmd.LastName, cmd.Email,
                cmd.Mobile, cmd.Address, cmd.Postcode,
                cmd.City, hashed);

            await _repo.AddAsync(user);

            var token = _jwt.GenerateToken(user);
            return new RegisterResultDto
            {
                Success = true,
                Token = token
            };
        }
    }
}
