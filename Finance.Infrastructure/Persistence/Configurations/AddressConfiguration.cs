using Finance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Street).HasMaxLength(256);
            builder.Property(a => a.Number).HasMaxLength(16);
            builder.Property(a => a.PostalCode).HasMaxLength(16);
            builder.Property(a => a.District).HasMaxLength(64);
            builder.Property(a => a.City).HasMaxLength(64);
            builder.Property(a => a.State).HasMaxLength(64);
            builder.Property(a => a.Country).HasMaxLength(64);
        }
    }
}
