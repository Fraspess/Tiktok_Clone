using AutoMapper;
using Tiktok_Clone.BLL.Dtos.Video;
using Tiktok_Clone.DAL.Entities.Video;

namespace Tiktok_Clone.BLL.MapperProfiles.Video
{
    public class VideoMapperProfile : Profile
    {
        public VideoMapperProfile()
        {

            CreateMap<VideoEntity, VideoDTO>()
                .ForMember(d => d.HashTags,
                    o => o.MapFrom(s => s.HashTags.Select(h => h.HashTag.Tag)))
                .ForMember(d => d.LikeCount,
                o => o.MapFrom(s => s.Likes.Count));
        }
    }
}
