using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.BLL.Dtos.Comment;
using Tiktok_Clone.DAL.Entities.Comment;
using Tiktok_Clone.DAL.Repositories;

namespace Tiktok_Clone.BLL.Services.Comment
{
    public class CommentService : GenericService<CommentEntity, String, CommentDTO, CreateCommentDTO, UpdateCommentDTO>, ICommentService
    {
        public CommentService(IGenericRepository<CommentEntity, string> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
