using Microsoft.EntityFrameworkCore;
using UserNotebook.Domain.Models.Entities;
using UserNotebook.Domain.Models.Enums;

namespace UserNotebook.Dal.Context
{
    public class UserNotebookDbContext : DbContext
    {
        public UserNotebookDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            // data
            var user1 = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Anna",
                LastName = "Nowak",
                BirthDate = new DateTime(1995, 1, 1),
                Gender = Gender.female
            };

            var user2 = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Jan",
                LastName = "Kowalski",
                BirthDate = new DateTime(1996, 3, 2),
                Gender = Gender.male
            };

            modelBuilder.Entity<User>().HasData(user1, user2);
        }
    }
}
