using Finance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Persistence.Configurations
{
    public class AccountConfigurations : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Description).HasMaxLength(64);
            builder.Property(a => a.Color).HasMaxLength(32);

            builder.Property(a => a.InitialBalance).HasPrecision(16, 6);
            builder.Property(a => a.Balance).HasPrecision(16, 6);
        }
    }
}
