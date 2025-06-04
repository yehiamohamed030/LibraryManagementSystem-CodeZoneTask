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
                    new Author { FullName = "Dummy Name Dummy Name", Email = "dummyname@gmail.com", Website = "dummyname.com", Bio = "Dummy Bio" },
                    new Author { FullName = "Name Dummy Name Dummy", Email = "namedummy@gmail.com", Website = "namedummy.com", Bio = "Dummy Bio" }
                    );
                context.SaveChanges();
            }
        }
    }
}
