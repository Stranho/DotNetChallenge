using MassTransit;
using Microsoft.AspNetCore.SignalR;
using SimpleChat.Hubs;
using SimpleChat.Shared;

namespace SimpleChat.Helpers
{
    public class ChatBotConsumer : IConsumer<ChatMessage>
    {
        public static IHubContext<ChatHub>? _context;

        public async Task Consume(ConsumeContext<ChatMessage> context)
        {
            ChatMessage chatMessage = context.Message;
            if(_context != null)
                await _context.Clients.All.SendAsync("ReceiveBotNotification", chatMessage.Message, chatMessage.IsError, chatMessage.FromUser);
        }
    }
}
