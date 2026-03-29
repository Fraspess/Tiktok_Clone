namespace Tiktok_Clone.BLL.Dtos.Video
{
    public class VideoDTO
    {
        public string VideoFileName { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        public List<string> HashTags { get; set; } = new List<string>();


    }
}
