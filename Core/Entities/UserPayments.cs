using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class UserPayments
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime ExpireDate { get; set; }
        public long TelegramId { get; set; }
        public TelegramUser? TelegramUser { get; set; }

    }
}
