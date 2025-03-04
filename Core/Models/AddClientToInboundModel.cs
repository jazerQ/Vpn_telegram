using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AddClientToInboundModel
    {
        //"id": 5,
	    public int id { get; set; }
        public string settings { get; set; } 
    }
}
