using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TelegramUserRepository
    {
        private readonly TelegramDbContext _context;
        public TelegramUserRepository(TelegramDbContext context) 
        {
            _context = context;
        }
    }
}
