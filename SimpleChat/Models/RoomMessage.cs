namespace SimpleChat.Models
{
    public class RoomMessage
    {
        public long Id { get; set; }
        public int RoomId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
