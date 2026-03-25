using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.DAL.Entities.HashTags;

namespace Tiktok_Clone.DAL.Entities.Video
{
    public class VideoHashTagEntity
    {
        public string VideoId { get; set; } = string.Empty;
        public VideoEntity Video { get; set; } = null!;

        public string HashTagId { get; set; } = string.Empty;
        public HashTagEntity HashTag { get; set; } = null!;
    }
}
