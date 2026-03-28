using FluentValidation;
using Tiktok_Clone.BLL.Commands.User;

namespace Tiktok_Clone.BLL.Validators.User
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email не може бути порожнім")
                .EmailAddress().WithMessage("Невірний формат email");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Пароль не може бути пустим")
                .MinimumLength(6).WithMessage("Пароль повинен містити не менше  ніж 6 символів");

            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Ім'я користувача не може бути порожнім")
                .MinimumLength(3).WithMessage("Ім'я користувача повинно містить не менше ніж 3 символів");
        }
    }
}
