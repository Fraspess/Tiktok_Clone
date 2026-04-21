using AutoMapper;
using Tiktok_Clone.BLL.Dtos.Comment;
using Tiktok_Clone.DAL.Entities.Comment;

namespace Tiktok_Clone.BLL.MapperProfiles.Comment
{
    public class CommentMapperProfile : Profile
    {
        public CommentMapperProfile()
        {
            CreateMap<CommentEntity, CommentDTO>()
                .ForMember(d => d.RepliesCount, o => o.MapFrom(c => c.Replies.Count))
                .ForMember(d => d.Owner, o => o.MapFrom(c => $"@{c.Author!.UserName}"))
                .ForMember(d => d.LikesCount, o => o.MapFrom(c => c.CommentLikes.Count));
        }
    }
}
