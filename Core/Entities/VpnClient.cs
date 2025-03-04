using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class VpnClient
    {
        public Guid id { get; set; }
        public string flow { get; set; } = string.Empty;
        public long telegramId { get; set; } 
        public TelegramUser? TelegramUser { get; set; }
        public int limitIp { get; set; }
        public int totalGB { get; set; }
        public long expiryTime { get; set; }
        public bool enable { get; set; }
        public string tgId { get; set; } = "";
        public string subId { get; set; } = "";
        public int reset { get; set; } = 0;
        public bool isPrimaryUser { get; set; }
    }
}
