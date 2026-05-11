using FluentValidation;

namespace Application.Features.User.ChangeUsername;

public class ChangeUsernameCommandValidator : AbstractValidator<ChangeUsernameCommand>
{
    public ChangeUsernameCommandValidator()
    {
        RuleFor(c => c.newUsername).NotEmpty().WithMessage("Нове ім'я користувача є обов'язковим")
            .MaximumLength(50).WithMessage("Ім'я користувача не може бути довшим ніж 50 символів")
            .MinimumLength(3).WithMessage("Ім'я користувача не може бути коротшим ніж 3 символи");
    }
}