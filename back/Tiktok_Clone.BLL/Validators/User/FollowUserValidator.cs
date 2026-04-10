using FluentValidation;
using Tiktok_Clone.BLL.Commands.User;

namespace Tiktok_Clone.BLL.Validators.User
{
    public class FollowUserValidator : AbstractValidator<FollowUserCommand>
    {
        public FollowUserValidator()
        {
            RuleFor(x => x)
                .Must(x => x.FollowerId != x.FollowingId).WithMessage("Ви не можете підписатись самі на себе");
            RuleFor(f => f.FollowerId).NotEmpty();
            RuleFor(f => f.FollowingId).NotEmpty();
        }
    }
}
