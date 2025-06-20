using MediatR;
using System.Collections.Generic;

namespace CarDealership.Application.Features.Authentication
{
    public class LoginUserCommand : IRequest<LoginResultDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResultDto
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
