using Application.Dtos.User;

namespace Application.Dtos.Video
{
    public class SimpleVideoDTO
    {
        public Guid Id { get; set; }
        public string VideoFileName { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        public List<string> HashTags { get; set; } = new List<string>();
        public UserAuthorDTO? Author { get; set; }
    }
}
