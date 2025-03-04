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
    public class VpnClientConfiguration : IEntityTypeConfiguration<VpnClient>
    {
        public void Configure(EntityTypeBuilder<VpnClient> builder)
        {
            builder.HasKey(vpn => vpn.id);
            builder.HasOne(vpn => vpn.TelegramUser).WithOne(tg => tg.VpnClient).HasForeignKey<VpnClient>(vpn => vpn.telegramId);
        }
    }
}
