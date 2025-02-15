using finefin.api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace finefin.api.Data.Mappings
{
    public class WalletMap : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("TB_WALLET").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("WLT_ID");
            builder.Property(x => x.CreatedAt).HasColumnName("WLT_CREATED_AT");
            builder.Property(x => x.UpdatedAt).HasColumnName("WLT_UPDATED_AT");
            builder.Property(x => x.Type).HasColumnName("WLT_TYPE").HasMaxLength(50);
            builder.Property(x => x.Name).HasColumnName("WLT_NAME").HasMaxLength(200);
            builder.Property(x => x.Color).HasColumnName("WLT_COLOR").HasMaxLength(50);
            builder.Property(x => x.Balance).HasColumnName("WLT_BALANCE");
            builder.Property(x => x.LastEditDate).HasColumnName("WLT_LAST_EDIT_DATE");
            builder.Property(x => x.UserId).HasColumnName("WLT_USER_ID");

            builder.HasOne(x => x.User)
                .WithMany(x => x.Wallets)
                .HasForeignKey(x => x.UserId);
        }
    }
}
