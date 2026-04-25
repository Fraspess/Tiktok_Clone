using Application.Dtos.Video;
using AutoMapper;
using Domain.Entities.Video;

namespace Application.MapperProfiles.Video
{
    public class VideoMapperProfile : Profile
    {
        public VideoMapperProfile()
        {
            Guid? currentUserId = null;

            CreateMap<VideoEntity, VideoDTO>()
                .ForMember(d => d.HashTags,
                    o => o.MapFrom(s => s.HashTags.Select(h => h.HashTag.Tag)))
                .ForMember(
                    d => d.LikeCount,
                    o => o.MapFrom(s => s.Likes.Count))

                .ForMember(
                    d => d.CommentsCount,
                    o => o.MapFrom(v => v.Comments.Count))

                .ForMember(
                    d => d.FavoriteCount,
                    o => o.MapFrom(v => v.Favorites.Count))

                .ForMember(dest => dest.IsLiked,
                       opt => opt.MapFrom(src => src.Likes.Any(l => l.UserId == currentUserId)))

                .ForMember(dest => dest.IsFavorited,
                       opt => opt.MapFrom(src => src.Favorites.Any(f => f.UserId == currentUserId)));


            CreateMap<VideoEntity, SimpleVideoDTO>()
                .ForMember(d => d.HashTags,
                    o => o.MapFrom(s => s.HashTags.Select(h => h.HashTag.Tag)));

        }
    }
}
