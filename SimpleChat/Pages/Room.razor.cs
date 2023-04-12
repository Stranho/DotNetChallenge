using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Components;
using SimpleChat.Models;
using Microsoft.JSInterop;
using Flurl.Http;
using Flurl;
using SimpleChat.Shared;
using Newtonsoft.Json;

namespace SimpleChat.Pages
{
    public partial class Room
    {
        [CascadingParameter] public HubConnection hubConnection { get; set; }
        [CascadingParameter] private int CurrentRoomId { get; set; }

        private List<RoomMessage> messages = new();
        [Parameter] public string CurrentMessage { get; set; } = string.Empty;
        [Parameter] public string CurrentUserId { get; set; } = string.Empty;
        private const string BotAddress = "https://localhost:5000/";

        private async Task SubmitAsync()
        {
            if (!string.IsNullOrEmpty(CurrentMessage) && !string.IsNullOrEmpty(roomId))
            {
                StateHasChanged();
                //Save Message to DB
                RoomMessage chatHistory = new()
                {
                    Message = CurrentMessage,
                    RoomId = Convert.ToInt32(roomId),
                    Timestamp = DateTime.Now,
                    UserId = CurrentUserId
                };

                if (CurrentMessage.StartsWith("/"))
                {
                    ChatMessage chatMessage = new() { Message = CurrentMessage, IsError = false, FromUser = CurrentUserId };
                    var response = await BotAddress.AppendPathSegment("api/ChatBot/SendMessage")
                        .WithHeader("Content-Type", "application/json")
                        .AllowAnyHttpStatus()
                        .PostJsonAsync(chatMessage);
                    CurrentMessage = string.Empty;
                    StateHasChanged();
                    return;
                }

                await RoomService.SaveMessageAsync(chatHistory);
                await hubConnection.SendAsync("SendMessageAsync", chatHistory, CurrentUserId);
                CurrentMessage = string.Empty;
                StateHasChanged();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            CurrentUserId = user.Claims.Where(a => a.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Select(a => a.Value).FirstOrDefault();

            CurrentRoomId = Convert.ToInt32(roomId);
            _chatRoom = await RoomService.GetChatRoom(CurrentRoomId);
            AppData.RoomId = CurrentRoomId;

            if (_chatRoom != null)
            {
                messages = await RoomService.GetRoomMessages(CurrentRoomId);
            }
            hubConnection ??= new HubConnectionBuilder().WithUrl(_navigationManager.ToAbsoluteUri("/chathub")).WithAutomaticReconnect().Build();
            if (hubConnection.State == HubConnectionState.Disconnected)
            {
                await hubConnection.StartAsync();
            }
            hubConnection.On<RoomMessage, string>("ReceiveMessage", async (message, userName) =>
            {
                messages.Add(new RoomMessage { Message = message.Message, Timestamp = message.Timestamp, UserId = userName });
                await hubConnection.SendAsync("ChatNotificationAsync", $"New Message on {_chatRoom?.Name}", CurrentRoomId, _chatRoom?.Name);
                StateHasChanged();
            });
        }
    }
}
