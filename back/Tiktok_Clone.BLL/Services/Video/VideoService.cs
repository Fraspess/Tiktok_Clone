using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.BLL.Dtos.Video;
using Tiktok_Clone.DAL.Entities.Video;
using Tiktok_Clone.DAL.Repositories;

namespace Tiktok_Clone.BLL.Services.Video
{
    public class VideoService : GenericService<VideoEntity, String, VideoDTO, CreateVideoDTO, UpdateVideoDTO>, IVideoService
    {
        public VideoService(IGenericRepository<VideoEntity, string> repository, IMapper mapper) : base(repository, mapper)
        {
        }

    }
}
