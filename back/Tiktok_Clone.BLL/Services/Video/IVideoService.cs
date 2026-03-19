using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.BLL.Dtos.Video;
using Tiktok_Clone.DAL.Entities.Video;

namespace Tiktok_Clone.BLL.Services.Video
{
    public interface IVideoService
        :IGenericService<VideoEntity, String, VideoDTO, CreateVideoDTO, UpdateVideoDTO>
    {
    }
}
