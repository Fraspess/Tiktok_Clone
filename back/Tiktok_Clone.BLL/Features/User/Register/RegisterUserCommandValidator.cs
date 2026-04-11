using FluentValidation;

namespace Tiktok_Clone.BLL.Features.User.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email не може бути порожнім")
                .EmailAddress().WithMessage("Невірний формат email");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль не може бути пустим")
                .MinimumLength(6).WithMessage("Пароль повинен містити не менше  ніж 6 символів");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Ім'я користувача не може бути порожнім")
                .MinimumLength(3).WithMessage("Ім'я користувача повинно містить не менше ніж 3 символів");
        }
    }
}
