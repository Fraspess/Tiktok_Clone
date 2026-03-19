using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.BLL.Dtos.HashTag;
using Tiktok_Clone.DAL.Entities.HashTags;

namespace Tiktok_Clone.BLL.Services.HashTag
{
    public interface IHashTagService : IGenericService<HashTagEntity, String, HashTagDTO, CreateHashTagDTO, UpdateHashTagDTO>
    {
    }
}
