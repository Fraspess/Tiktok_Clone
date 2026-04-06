using AutoMapper;
using AutoMapper.QueryableExtensions;
using Tiktok_Clone.BLL.Dtos.Comment;
using Tiktok_Clone.BLL.Exceptions;
using Tiktok_Clone.BLL.Extensions;
using Tiktok_Clone.BLL.Pagination;
using Tiktok_Clone.DAL.Entities.Comment;
using Tiktok_Clone.DAL.Repositories.Comment;

namespace Tiktok_Clone.BLL.Services.Comment
{
    public class CommentService(ICommentRepository _repository, IMapper _mapper) : ICommentService
    {
        public async Task CreateCommentAsync(CreateCommentDTO dto, Guid ownerId)
        {
            // якщо є батьківський коментар то це відповідь на якійсь коментар. треба провірити чи існує коментар на який відповідають
            if (dto.ParentCommentId is not null)
            {
                var comment = await _repository.GetByIdAsync(dto.ParentCommentId.Value)
                    ?? throw new ValidationException("Коментарій не знайдено");
                var newComment = new CommentEntity { Text = dto.Text, ParentCommentId = dto.ParentCommentId.Value, UserId = ownerId, VideoId = dto.VideoId };
                await _repository.CreateAsync(newComment);
            }
            else
            {
                var comment = new CommentEntity { Text = dto.Text, UserId = ownerId, VideoId = dto.VideoId };
                await _repository.CreateAsync(comment);
            }
        }

        public async Task DeleteCommentAsync(Guid commentId, Guid userId)
        {
            var comment = await _repository.GetByIdAsync(commentId)
                ?? throw new ValidationException("Коментарій не знайдено");
            if (comment.UserId == userId)
            {
                await _repository.DeleteAsync(comment);
                return;
            }
            throw new NotAllowedException("Ви не маєте прав на це");
        }

        public async Task<PagedResult<CommentDTO>> GetCommentsAsync(Guid videoId, PaginationSettings settings)
        {
            return await _repository
                .GetCommentsByVideoId(videoId)
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .ToPagedResultAsync(settings);
        }

        public async Task<PagedResult<CommentDTO>> GetRepliesAsync(Guid parentCommentId, PaginationSettings settings)
        {
            return await _repository
                 .GetRepliesAsync(parentCommentId)
                 .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                 .ToPagedResultAsync(settings);
        }
    }
}
