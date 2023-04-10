namespace SimpleChat.Bot.Services
{
    public interface IInfoHelper
    {
        bool HasCode(string code);
        decimal GetStock(string code);
    }
}
