namespace SimpleChat.Bot.Models
{
    public class CsvData
    {
        public string Symbol { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
    }
}
