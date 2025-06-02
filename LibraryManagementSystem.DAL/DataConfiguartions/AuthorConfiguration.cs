using LibraryManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.DataConfiguartions
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author>builder)
        {
            builder.HasIndex(a => a.FullName)
                   .IsUnique();

            builder.HasIndex(a => a.Email)
                   .IsUnique();

            builder.Property(a => a.FullName)
                   .IsRequired();

            builder.Property(a => a.Email)
                   .IsRequired();
        }
    }
}
