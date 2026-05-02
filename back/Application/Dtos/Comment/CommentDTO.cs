namespace Application.Dtos.Comment
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;

        public int RepliesCount { get; set; }

        public string Owner { get; set; } = string.Empty;

        public int LikesCount { get; set; }
    }
}