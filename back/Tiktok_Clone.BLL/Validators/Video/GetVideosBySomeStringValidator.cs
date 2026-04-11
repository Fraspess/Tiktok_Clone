using FluentValidation;
using Tiktok_Clone.BLL.Queries.Video;

namespace Tiktok_Clone.BLL.Validators.Video
{
    public class GetVideosBySomeStringValidator : AbstractValidator<GetVideosBySomeStringQuery>
    {
        public GetVideosBySomeStringValidator()
        {
            RuleFor(v => v.SomeString)
                .NotEmpty().WithMessage("Query не може бути порожній");
        }
    }
}
