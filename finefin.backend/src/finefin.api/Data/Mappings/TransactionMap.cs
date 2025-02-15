using finefin.api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace finefin.api.Data.Mappings
{
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("TB_TRANSACTION").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("TRN_ID");
            builder.Property(x => x.CreatedAt).HasColumnName("TRN_CREATED_AT");
            builder.Property(x => x.UpdatedAt).HasColumnName("TRN_UPDATED_AT");
            builder.Property(x => x.Type).HasColumnName("TRN_TYPE").HasMaxLength(50);
            builder.Property(x => x.Amount).HasColumnName("TRN_AMOUNT");
            builder.Property(x => x.DueDate).HasColumnName("TRN_DUEDATE");
            builder.Property(x => x.IsCompleted).HasColumnName("TRN_COMPLETED");
            builder.Property(x => x.IsFixed).HasColumnName("TRN_FIXED");
            builder.Property(x => x.IsRecurring).HasColumnName("TRN_RECURRING");
            builder.Property(x => x.RecurrencyId).HasColumnName("TRN_RECURRENCY_ID");
            builder.Property(x => x.WalletId).HasColumnName("TRN_WALLET_ID");

            builder.HasOne(t => t.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(t => t.WalletId);

            builder.HasOne(t => t.Recurrency)
                .WithMany(r => r.Transactions)
                .HasForeignKey(t => t.RecurrencyId)
                .IsRequired(false); // SÓ PRA EXPLICITAR, VISTO QUE O EF JÁ DETERMINA DEVIDO À PROPRIEDADE NULLABLE
        }
    }
}
