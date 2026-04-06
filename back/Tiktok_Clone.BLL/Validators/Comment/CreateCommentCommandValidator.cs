using FluentValidation;
using Tiktok_Clone.BLL.Commands.Comment;

namespace Tiktok_Clone.BLL.Validators.Comment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.Dto.Text)
                .NotEmpty().WithMessage("Коментарій не може бути пустим.")
                .MaximumLength(200).WithMessage("Коментарій не може бути більше ніж 200 символів.");

            RuleFor(x => x.Dto.VideoId)
                .NotEmpty().WithMessage("VideoId?????");
        }
    }
}
