namespace Tiktok_Clone.BLL.Dtos.Comment
{
    public class CreateCommentDTO
    {
        public string Text { get; set; } = String.Empty;

        public Guid VideoId { get; set; }
        public Guid? ParentCommentId { get; set; }
    }
}
