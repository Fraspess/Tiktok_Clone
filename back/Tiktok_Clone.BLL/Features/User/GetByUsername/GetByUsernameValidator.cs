using FluentValidation;

namespace Tiktok_Clone.BLL.Features.User.GetByUsername
{
    public class GetByUsernameValidator : AbstractValidator<GetUserByUsernameQuery>
    {
        public GetByUsernameValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Юзернейм не може бути пустим");
        }
    }
}
