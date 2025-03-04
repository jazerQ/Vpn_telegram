using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Clients 
    {
        public List<Client> clients { get; set; }
    }
    public class Client 
    {
        public Guid id { get; set; }
        public string flow { get; set; }
        public string email { get; set; }
        public int limitIp { get; set; }
        public int totalGB { get; set; }
        public long expiryTime { get; set; }
        public bool enable { get; set; }
        public string tgId { get; set; } = "";
        public string subId { get; set; } = "";
        public int reset { get; set; } = 0;

    }
}
