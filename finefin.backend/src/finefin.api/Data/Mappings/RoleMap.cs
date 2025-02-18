using finefin.api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace finefin.api.Data.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("TB_ROLES").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("RLE_ID");
            builder.Property(x => x.CreatedAt).HasColumnName("RLE_CREATED_AT");
            builder.Property(x => x.UpdatedAt).HasColumnName("RLE_UPDATED_AT");
            builder.Property(x => x.Name).HasColumnName("RLE_NAME");

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
