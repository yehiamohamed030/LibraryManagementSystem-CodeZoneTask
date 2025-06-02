using LibraryManagementSystem.DAL.Data;
using LibraryManagementSystem.DAL.Enums;
using LibraryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.DataSeeders
{
    public static class BookSeeder
    {
        public static void SeedBooksData(AppDbContext context)
        {
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book { Title = "The Song Of Ice And Fire", Genre = Genre.History, Description = "A novel written by George R Martin, Game Of Thrones is based on this Novel", AuthorId = 1 },
                    new Book { Title = "Dummy Title", Genre = Genre.SciFi, Description = "Dummy Descripition", AuthorId = 2 },
                    new Book { Title = "Dummy Title 2", Genre = Genre.Adventure, Description = "Dummy Descripition 2", AuthorId = 3 }
                    );
                context.SaveChanges();
            }
        }
    }
}
