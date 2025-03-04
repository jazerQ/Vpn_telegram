using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.Models
{
    public class AddClientToInboundModel
    {
        //"id": 5,
	    public int id { get; set; }
        public string settings { get; set; }
        private AddClientToInboundModel(string customSettings) 
        {
            id = 1;
            settings = customSettings;
        }
        public static AddClientToInboundModel GetSimpleClient(string name) 
        {
            Client client = new Client
            {
                id = Guid.NewGuid(),
                email = name,
                expiryTime = ((DateTimeOffset)DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds(),
                flow = "xtls-rprx-vision",
                enable = true,
                limitIp = 1,
                totalGB = 1,
                subId = SubIdService.Generate()
            };

            List<Client> clients = new List<Client>() { client };
            Clients clients1 = new Clients { clients = clients };
            string clientsJson = JsonConvert.SerializeObject(clients1);
            return new AddClientToInboundModel(clientsJson);
        }
        public static AddClientToInboundModel GetPrimaryClient(string name) 
        {
            Client client = new Client
            {
                id = Guid.NewGuid(),
                email = name,
                flow = "xtls-rprx-vision",
                expiryTime = ((DateTimeOffset)DateTime.Now.AddDays(30)).ToUnixTimeSeconds(),
                enable = true,
                limitIp = 2,
                totalGB = 0,
                subId = SubIdService.Generate()
            };
            List<Client> clients = new List<Client>() { client };
            Clients clients1 = new Clients { clients = clients };
            string clientsJson = JsonConvert.SerializeObject(clients1);
            return new AddClientToInboundModel(clientsJson);
        }
    }
}
