namespace Core.Entities
{
    public class UserPayments
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public DateTime ExpireDate { get; set; }
        public long TelegramId { get; set; }
        public TelegramUser? TelegramUser { get; set; }
    }
}