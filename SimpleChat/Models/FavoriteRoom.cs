namespace SimpleChat.Models
{
    public class FavoriteRoom
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public ChatRoom? ChatRoom { get; set; }
    }
}
