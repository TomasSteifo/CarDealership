using FluentValidation;

namespace CarDealership.Application.Features.Authentication
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).NotEmpty().Matches(@"^(\+46|0)\d{9}$");
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Postcode).NotEmpty().Matches(@"^\d{5}$");
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords must match.");
        }
    }
}
