using LibraryManagementSystem.DAL.Data;
using LibraryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.DataSeeders
{
    public static class AuthorSeeder
    {
        public static void SeedAuthorsData(AppDbContext context)
        {
            if (!context.Authors.Any())
            {
                context.Authors.AddRange(
                     new Author { FullName = "Yehia Mohamed Ibrahim Mohamed", Email = "yehiamohamed030@gmail.com", Website = "yehiamohamed.com", Bio = "Classic author" },
                     new Author { FullName = "Sarah Mostafa Elbaz Ahmed", Email = "sarah.elbaz@example.com", Website = "sarahelbazwrites.com", Bio = "Award-winning novelist and poet" },
                     new Author { FullName = "Ahmed Tarek Mansour Khaled", Email = "ahmed.mansour@example.com", Website = "ahmedwrites.net", Bio = "History enthusiast and fiction writer" },
                     new Author { FullName = "Layla Hassan Fouad Gamal", Email = "layla.hassan@example.com", Website = "laylahassanbooks.com", Bio = "Science fiction and mystery writer" },
                     new Author { FullName = "Mohamed Zaki Mostafa Nour", Email = "m.zaki@example.com", Website = "mohamedzakiauthor.org", Bio = "Romance novelist and literary speaker" },
                     new Author { FullName = "Nourhan Salah Ezz Eldin", Email = "nourhan.salah@example.com", Website = "nourhaneldinbooks.com", Bio = "Young adult fiction specialist" }
                     );
                context.SaveChanges();
            }
        }
    }
}
