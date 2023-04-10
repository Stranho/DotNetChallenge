using SimpleChat.Bot.Models;
using CsvHelper;
using System.Globalization;

namespace SimpleChat.Bot.Services
{
    public class InfoHelper : IInfoHelper
    {
        private const string PathToCSV = "aapl.us.csv";
        private List<CsvData> _csvData = new();

        private void LoadData()
        {
            _csvData = new();
            CsvReader csvReader = new(new StreamReader(PathToCSV, System.Text.Encoding.UTF8), CultureInfo.InvariantCulture);
            _csvData = csvReader.GetRecords<CsvData>().ToList();
        }

        public bool HasCode(string code)
        {
            if (_csvData.Count == 0)
                LoadData();
            return _csvData.Any(x => x.Symbol == code);
        }

        public decimal GetStock(string code)
        {
            if (_csvData.Count == 0)
                LoadData();
            return _csvData.Where(w => w.Symbol == code).Select(s => s.Open).FirstOrDefault();
        }
    }
}
