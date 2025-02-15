using finefin.api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace finefin.api.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("TB_USERS")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("USR_ID");
            builder.Property(x => x.CreatedAt).HasColumnName("USR_CREATED_AT");
            builder.Property(x => x.UpdatedAt).HasColumnName("USR_UPDATED_AT");
            builder.Property(x => x.Name).HasColumnName("USR_NAME").HasMaxLength(200);
            builder.Property(x => x.Email).HasColumnName("USR_EMAIL").HasMaxLength(250);
            builder.Property(x => x.Password).HasColumnName("USR_PASSWORD");
        }
    }
}
