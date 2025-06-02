using LibraryManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.DataConfiguartions
{
    public class BorrowingTransactionConfiguration : IEntityTypeConfiguration<BorrowingTransaction>
    {
        public void Configure(EntityTypeBuilder<BorrowingTransaction> builder)
        {
            builder.Property(bt => bt.BorrowedDate)
                   .IsRequired();

            builder.HasOne(bt => bt.Book)
                   .WithMany(b => b.BorrowingTransactions)
                   .HasForeignKey(bt => bt.BookId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
