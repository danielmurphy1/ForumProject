namespace ForumProject.DataTransferObjects
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Replies { get; set; }
        public int Views {  get; set; }
        public int BoardId {  get; set; }
        public UserDTO User { get; set; }
        public IEnumerable<ReplyDTO> ReplyMessages { get; set; }    
    }
}
