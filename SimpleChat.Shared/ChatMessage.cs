namespace SimpleChat.Shared
{
    public class ChatMessage
    {
        public string Message { get; set; }
        public bool IsError { get; set; }
        public string FromUser { get; set; }

        public ChatMessage()
        {
            Message = string.Empty;
            IsError = false;
            FromUser = string.Empty;
        }

        public ChatMessage(string message, bool isError = false, string fromUser = "")
        {
            Message = message;
            IsError = isError;
            FromUser = fromUser;
        }
    }
}