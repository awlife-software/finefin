using finefin.api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace finefin.api.Data.Mappings
{
    public class RecurrencyMap : IEntityTypeConfiguration<Recurrency>
    {
        public void Configure(EntityTypeBuilder<Recurrency> builder)
        {
            builder.ToTable("TB_RECURRENCY").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("RCR_ID");
            builder.Property(x => x.CreatedAt).HasColumnName("RCR_CREATED_AT");
            builder.Property(x => x.UpdatedAt).HasColumnName("RCR_UPDATED_AT");
            builder.Property(x => x.Type).HasColumnName("RCR_TYPE").HasMaxLength(50);
            builder.Property(x => x.Occurrences).HasColumnName("RCR_OCCURRENCES");

            
        }
    }
}
