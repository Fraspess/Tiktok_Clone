using FluentValidation;
using Tiktok_Clone.BLL.Queries.Video;

namespace Tiktok_Clone.BLL.Validators.Video
{
    public class GetForYouPageVideosQueryValidator : AbstractValidator<GetForYouPageVideosQuery>
    {
        public GetForYouPageVideosQueryValidator()
        {
            RuleFor(p => p.PaginationSettings.PageNumber)
                .Must(page => page > 0).WithMessage("Номер сторінки має бути більше ніж 0")
                .Must(page => page < int.MaxValue).WithMessage("З дубу рухнув?");

            RuleFor(p => p.PaginationSettings.PageSize)
                .Must(size => size > 0).WithMessage("Розмір сторінки має бути більше ніж 0");
        }
    }
}
