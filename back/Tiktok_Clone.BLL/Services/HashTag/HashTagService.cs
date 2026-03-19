using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.BLL.Dtos.HashTag;
using Tiktok_Clone.DAL.Entities.HashTags;
using Tiktok_Clone.DAL.Repositories;

namespace Tiktok_Clone.BLL.Services.HashTag
{
    public class HashTagService : GenericService<HashTagEntity, String, HashTagDTO, CreateHashTagDTO, UpdateHashTagDTO>, IHashTagService
    {
        public HashTagService(IGenericRepository<HashTagEntity, string> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
