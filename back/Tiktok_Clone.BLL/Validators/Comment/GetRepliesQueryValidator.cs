using FluentValidation;
using Tiktok_Clone.BLL.Queries.Comment;

namespace Tiktok_Clone.BLL.Validators.Comment
{
    public class GetRepliesQueryValidator : AbstractValidator<GetRepliesQuery>
    {
        public GetRepliesQueryValidator()
        {
            RuleFor(v => v.ParentCommentId)
                .NotEmpty().WithMessage("Parent comment id ???");

            RuleFor(p => p.PaginationSettings.PageNumber)
               .Must(page => page > 0).WithMessage("Номер сторінки має бути більше ніж 0")
               .Must(page => page < int.MaxValue).WithMessage("З дубу рухнув?");

            RuleFor(p => p.PaginationSettings.PageSize)
                .Must(size => size > 0).WithMessage("Розмір сторінки має бути більше ніж 0");
        }
    }
}
