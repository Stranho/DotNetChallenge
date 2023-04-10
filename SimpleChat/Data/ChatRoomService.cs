using SimpleChat.Models;

namespace SimpleChat.Data
{
    public class ChatRoomService
    {
        private readonly ApplicationDbContext _context;
        public ChatRoomService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<ChatRoom>> GetChatRooms()
        {
            return Task.FromResult(_context.ChatRooms.ToList());
        }

        public Task<ChatRoom?> GetChatRoom(int roomId )
        {
            return Task.FromResult(_context.ChatRooms.Where(x => x.Id == roomId).FirstOrDefault());
        }

        public Task<List<RoomMessage>> GetRoomMessages(int roomId)
        {
            return Task.FromResult(_context.RoomMessages.Where(w => w.RoomId == roomId).OrderByDescending(o => o.Timestamp).Take(50).Reverse().ToList());
        }

        public void SaveNewRoom(ChatRoom room)
        {
            _context.ChatRooms.Add(room);
            _context.SaveChanges();
        }

        public async Task SaveMessageAsync(RoomMessage message)
        {
            await _context.AddAsync<RoomMessage>(message);
            await _context.SaveChangesAsync();
        }

        public Task<List<RoomCategory>> GetCategories()
        {
            return Task.FromResult(_context.RoomCategories.ToList());
        }
    }
}
