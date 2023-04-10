using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleChat.Bot.Services;
using SimpleChat.Shared;

namespace SimpleChat.Bot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBot : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IInfoHelper _infoHelper;

        public ChatBot(IPublishEndpoint publishEndpoint, IInfoHelper infoHelper)
        {
            _publishEndpoint = publishEndpoint;
            _infoHelper = infoHelper;
        }

        [HttpPost]
        [Route("SendMessage")]
        public IActionResult SendMessage([FromBody] ChatMessage chatMessage)
        {
            ChatMessage sendMessage = new("Invalid Code", false, chatMessage.FromUser);
            string message = chatMessage.Message;

            if(message.StartsWith("/Hello"))
            {
                sendMessage.Message = "Hi, how are you?";
            }
            else if (message.StartsWith("/stock="))
            {
                string code = message[7..];
                if(_infoHelper.HasCode(code))
                {
                    sendMessage.Message = $"{code} quote is ${_infoHelper.GetStock(code)} per share";
                }
                else
                {
                    sendMessage.Message = $"Code {code} note found!";
                    sendMessage.IsError = false;
                }
            }

            _publishEndpoint.Publish<ChatMessage>(sendMessage);
            Console.WriteLine("Message Sent");
            return Ok("Received");
        }
    }
}
