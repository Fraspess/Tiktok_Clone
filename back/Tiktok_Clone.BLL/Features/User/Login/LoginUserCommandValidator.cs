using FluentValidation;

namespace Tiktok_Clone.BLL.Features.User.Login
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.login)
                .NotEmpty().WithMessage("Логін не може бути порожнім");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Пароль не може бути порожнім")
                .MinimumLength(6).WithMessage("Пароль повинен містить не менше ніж 6 символів");
        }
    }
}
