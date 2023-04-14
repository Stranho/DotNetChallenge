using Microsoft.AspNetCore.SignalR;
using SimpleChat.Models;

namespace SimpleChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageAsync(RoomMessage message, string userName)
        {
            await Clients.Group(message.RoomId.ToString()).SendAsync("ReceiveMessage", message, userName);
        }

        public async Task ChatNotificationAsync(string message, int roomId, string roomName)
        {
            await Clients.Group(roomId.ToString()).SendAsync("ReceiveChatNotification", message, roomId, roomName);
        }

        public async Task JoinRoomAsync(int roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
    }
}
