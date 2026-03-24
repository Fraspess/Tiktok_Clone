using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Tiktok_Clone.BLL.Dtos.Comment;
using Tiktok_Clone.DAL.Entities.Comment;

namespace Tiktok_Clone.BLL.Services.Comment
{
    public interface ICommentService : IGenericService<CommentEntity, String, CommentDTO, CreateCommentDTO, UpdateCommentDTO>
    {
    }
}
