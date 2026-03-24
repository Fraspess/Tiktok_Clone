using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.DAL.Entities.HashTags;

namespace Tiktok_Clone.DAL.Entities.Video
{
    public class VideoHashTagEntity
    {
        public Guid VideoId { get; set; }
        public VideoEntity Video { get; set; } = null!;

        public Guid HashTagId { get; set; }
        public HashTagEntity HashTag { get; set; } = null!;
    }
}
