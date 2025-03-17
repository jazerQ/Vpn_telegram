using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserPaymentsRepository
    {
        private readonly TelegramDbContext _context;
        public UserPaymentsRepository(TelegramDbContext context)
        {
            _context = context;            
        }

        public async Task AddNewUserPayment(long telegramId)
        {
            UserPayments userPayments = new UserPayments() { Id =  Guid.NewGuid(), Status = true, ExpireDate = DateTime.UtcNow, TelegramId = telegramId};
            await _context.UserPayments.AddAsync(userPayments);
            await _context.SaveChangesAsync();
        }
        public async Task<UserPayments> GetUserById(long telegramId) 
        {
            var user = await _context.UserPayments.FirstOrDefaultAsync(up => up.TelegramId == telegramId) ?? throw new NullReferenceException("not found this entity");
            return user;
        }
    }
}
