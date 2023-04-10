namespace SimpleChat.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public RoomType RoomType { get; set; }
        public RoomCategory? RoomCategory { get; set; }
    }
}
