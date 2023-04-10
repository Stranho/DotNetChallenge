using Microsoft.AspNetCore.SignalR;
using SimpleChat.Models;

namespace SimpleChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageAsync(RoomMessage message, string userName)
        {
            await Clients.All.SendAsync("ReceiveMessage", message, userName);
        }

        public async Task ChatNotificationAsync(string message, int roomId, string roomName)
        {
            await Clients.All.SendAsync("ReceiveChatNotification", message, roomId, roomName);
        }
    }
}
