using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Models.Inputs
{
    public class StockInput
    {
        public string User {  get; set; }
        public string RoomId {  get; set; }
        public string StockCode { get; set; }
    }
}
