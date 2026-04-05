namespace Tiktok_Clone.BLL.Dtos.Video
{
    public class VideoDTO
    {
        public Guid Id { get; set; }
        public string VideoFileName { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        public List<string> HashTags { get; set; } = new List<string>();

        public int LikeCount { get; set; }

        public int CommentsCount { get; set; }

        public int FavoriteCount { get; set; }

        public bool IsFavorited { get; set; }
        public bool IsLiked { get; set; }


    }
}
