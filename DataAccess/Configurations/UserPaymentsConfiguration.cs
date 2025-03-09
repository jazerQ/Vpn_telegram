using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class UserPaymentsConfiguration : IEntityTypeConfiguration<UserPayments>
    {
        public void Configure(EntityTypeBuilder<UserPayments> builder)
        {
            builder.HasKey(up => up.Id);
            builder.HasOne(up => up.TelegramUser)
                   .WithOne(tu => tu.UserPayments).HasForeignKey<UserPayments>(up => up.TelegramId);
        }
    }
}
