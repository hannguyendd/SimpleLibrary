using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleLibrary.Domain.Entities;

namespace SimpleLibrary.Infrastructure.Configurations;

public class BorrowedBookConfiguration : IEntityTypeConfiguration<BorrowedBook>
{
    public void Configure(EntityTypeBuilder<BorrowedBook> builder)
    {
        builder.HasKey(e => new { e.AccountId, e.BookId, e.BorrowedAt });

        builder.HasOne(e => e.Book)
            .WithMany(e => e.BorrowedBooks)
            .HasForeignKey(e => e.BookId);
    }
}
