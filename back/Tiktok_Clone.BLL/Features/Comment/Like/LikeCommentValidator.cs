using FluentValidation;

namespace Tiktok_Clone.BLL.Features.Comment.Like
{
    public class LikeCommentValidator : AbstractValidator<LikeCommentCommand>
    {
        public LikeCommentValidator()
        {
            RuleFor(c => c.CommentId).NotEmpty().WithMessage("Comment id не може бути пустим ");
        }
    }
}
