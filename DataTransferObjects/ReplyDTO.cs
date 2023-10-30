namespace ForumProject.DataTransferObjects
{
    public class ReplyDTO
    {
        public int Id { get; set; }
        public string Body {  get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostId { get; set; }
        public UserDTO User { get; set; }
    }
}
