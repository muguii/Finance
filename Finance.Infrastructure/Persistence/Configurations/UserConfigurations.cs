using Finance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Persistence.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(a => a.Login).HasMaxLength(64);
            builder.Property(a => a.Email).HasMaxLength(64);
            builder.Property(a => a.Name).HasMaxLength(64);
            builder.Property(a => a.LastName).HasMaxLength(128);
            builder.Property(a => a.Gender).HasMaxLength(16);
            builder.Property(a => a.Telephone).HasMaxLength(16);

            builder.HasOne(u => u.Address)
                   .WithOne()
                   .HasForeignKey<Address>(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Accounts)
                   .WithOne()
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
