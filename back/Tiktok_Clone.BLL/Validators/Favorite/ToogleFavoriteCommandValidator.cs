using FluentValidation;
using Tiktok_Clone.BLL.Commands.Favorite;

namespace Tiktok_Clone.BLL.Validators.Favorite
{
    public class ToogleFavoriteCommandValidator : AbstractValidator<ToogleFavoriteCommand>
    {
        public ToogleFavoriteCommandValidator()
        {
            RuleFor(f => f.VideoId)
                .NotEmpty().WithMessage("Video id???");
        }
    }
}
