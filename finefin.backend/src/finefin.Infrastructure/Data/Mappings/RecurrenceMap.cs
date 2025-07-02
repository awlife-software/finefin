using finefin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace finefin.Infrastructure.Data.Mappings
{
    public class RecurrenceMap : IEntityTypeConfiguration<Recurrence>
    {
        public void Configure(EntityTypeBuilder<Recurrence> builder)
        {
            builder.ToTable("TB_RECURRENCE").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("RCR_ID");
            builder.Property(x => x.CreatedAt).HasColumnName("RCR_CREATED_AT");
            builder.Property(x => x.UpdatedAt).HasColumnName("RCR_UPDATED_AT");
            builder.Property(x => x.Type).HasColumnName("RCR_TYPE").HasMaxLength(50);
            builder.Property(x => x.Occurrences).HasColumnName("RCR_OCCURRENCES");

            
        }
    }
}
