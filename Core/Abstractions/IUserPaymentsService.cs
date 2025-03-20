using Core.Entities;

namespace Application
{
    public interface IUserPaymentsService
    {
        Task AddNewUserPayment(long telegramId);
        Task<UserPayments> Task(long telegramId);
    }
}