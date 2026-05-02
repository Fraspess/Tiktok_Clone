using Application.Dtos.Comment;
using AutoMapper;
using Domain.Entities.Comment;

namespace Application.MapperProfiles.Comment
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