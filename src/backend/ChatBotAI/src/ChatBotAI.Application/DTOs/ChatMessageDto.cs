namespace ChatBotAI.Application.DTOs
{
    public class ChatMessageDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool? IsLiked { get; set; }
    }
}
