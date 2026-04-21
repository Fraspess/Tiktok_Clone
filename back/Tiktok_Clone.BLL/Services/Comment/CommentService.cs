using AutoMapper;
using AutoMapper.QueryableExtensions;
using Tiktok_Clone.BLL.Dtos.Comment;
using Tiktok_Clone.BLL.Exceptions;
using Tiktok_Clone.BLL.Extensions;
using Tiktok_Clone.BLL.Features.Comment.Create;
using Tiktok_Clone.BLL.Pagination;
using Tiktok_Clone.DAL.Entities.Comment;
using Tiktok_Clone.DAL.UnitOfWork;

namespace Tiktok_Clone.BLL.Services.Comment
{
    public class CommentService(IUnitOfWork _uow, IMapper _mapper) : ICommentService
    {
        public async Task CreateCommentAsync(CreateCommentDTO dto, Guid ownerId)
        {
            // якщо є батьківський коментар то це відповідь на якійсь коментар. треба провірити чи існує коментар на який відповідають
            if (dto.ParentCommentId is not null)
            {
                var comment = await _uow.Comments.GetByIdAsync(dto.ParentCommentId.Value)
                    ?? throw new ValidationException("Коментарій не знайдено");
                var newComment = new CommentEntity { Text = dto.Text, ParentCommentId = dto.ParentCommentId.Value, UserId = ownerId, VideoId = dto.VideoId };
                await _uow.Comments.CreateAsync(newComment);
            }
            else
            {
                var comment = new CommentEntity { Text = dto.Text, UserId = ownerId, VideoId = dto.VideoId };
                await _uow.Comments.CreateAsync(comment);
            }
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(Guid commentId, Guid userId)
        {
            var comment = await _uow.Comments.GetByIdAsync(commentId)
                ?? throw new ValidationException("Коментарій не знайдено");
            if (comment.UserId == userId)
            {
                await _uow.Comments.DeleteAsync(comment);
            }
            else
            {
                throw new NotAllowedException("Ви не маєте прав на це");
            }
            await _uow.SaveChangesAsync();
        }

        public async Task<PagedResult<CommentDTO>> GetCommentsAsync(Guid videoId, PaginationSettings settings)
        {
            return await _uow.Comments
                .GetCommentsByVideoId(videoId)
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .ToPagedResultAsync(settings);
        }

        public async Task<PagedResult<CommentDTO>> GetRepliesAsync(Guid parentCommentId, PaginationSettings settings)
        {
            return await _uow.Comments
                 .GetRepliesAsync(parentCommentId)
                 .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                 .ToPagedResultAsync(settings);
        }

        public async Task ToggleLikeAsync(Guid commentId, Guid userId)
        {
            var comment = await _uow.Comments.GetByIdAsync(commentId)
                ?? throw new NotFoundException("Коментарій не знайдено");

            var isExists = comment.CommentLikes.FirstOrDefault(c => c.UserId == userId);
            if (isExists is null)
            {
                isExists = new CommentLikeEntity() { CommentId = commentId, UserId = userId };
                comment.CommentLikes.Add(isExists);
                await _uow.SaveChangesAsync();
            }
            else
            {
                comment.CommentLikes.Remove(isExists);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
