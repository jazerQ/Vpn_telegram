using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions;
using Core.Entities;

namespace Application
{
    public class UserPaymentsService : IUserPaymentsService
    {
        private readonly IUserPaymentsRepository _repository;
        public UserPaymentsService(IUserPaymentsRepository repository)
        {
            _repository = repository;
        }
        public async Task AddNewUserPayment(long telegramId)
        {
            await _repository.AddNewUserPayment(telegramId);
        }

        public async Task<UserPayments> Task(long telegramId)
        {
            return await _repository.GetUserById(telegramId);
        }
    }
}
