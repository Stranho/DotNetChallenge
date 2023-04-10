namespace SimpleChat.Models
{
    public class PrivateMessage
    {
        public int Id { get; set; }
        public string FromUserId { get; set; } = string.Empty;
        public string ToUserId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public virtual ApplicationUser? FromUser { get; set; }
        public virtual ApplicationUser? ToUser { get; set; }
    }
}
