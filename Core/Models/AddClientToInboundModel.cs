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
        public static AddClientToInboundModel GetClient(Client client) 
        {
            List<Client> clients = new List<Client>() { client };
            Clients clients1 = new Clients { clients = clients };
            string clientsJson = JsonConvert.SerializeObject(clients1);
            return new AddClientToInboundModel(clientsJson);
        }
    }
}
