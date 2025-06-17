using System;

namespace Chat.Domain.Models
{
    public class StockModel
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public string Volume { get; set; }
        public string User { get; set; }
        public string RoomId { get; set; }
    }
}
