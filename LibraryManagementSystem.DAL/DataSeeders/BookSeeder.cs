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
                     new Book { Title = "The Song Of Ice And Fire", Genre = Genre.History, Description = "A novel written by George R. R. Martin; Game Of Thrones is based on this novel", AuthorId = 1 },
                     new Book { Title = "Echoes of the Desert", Genre = Genre.Romance, Description = "A heart-touching tale set in the Egyptian deserts", AuthorId = 2 },
                     new Book { Title = "Beneath the Nile", Genre = Genre.History, Description = "Historical fiction following ancient Egyptian pharaohs", AuthorId = 3 },
                     new Book { Title = "Starlight Protocol", Genre = Genre.SciFi, Description = "A futuristic adventure in a galaxy torn by war", AuthorId = 4 },
                     new Book { Title = "Whispers in the Kasbah", Genre = Genre.Mystery, Description = "A suspenseful journey through Morocco’s hidden secrets", AuthorId = 4 },
                     new Book { Title = "When Cairo Sleeps", Genre = Genre.Romance, Description = "A love story intertwined with the city's night life", AuthorId = 5 },
                     new Book { Title = "Chronicles of Tomorrow", Genre = Genre.SciFi, Description = "AI rebellion and human survival in a dystopian future", AuthorId = 4 },
                     new Book { Title = "Love in Alexandria", Genre = Genre.Romance, Description = "An emotional ride through a summer in Alexandria", AuthorId = 2 },
                     new Book { Title = "The Forgotten Scrolls", Genre = Genre.History, Description = "Ancient secrets uncovered by a modern-day archaeologist", AuthorId = 3 },
                     new Book { Title = "Digital Mirage", Genre = Genre.SciFi, Description = "Virtual realities collide with real-world dangers", AuthorId = 5 }
                     );
                context.SaveChanges();
            }
        }
    }
}
