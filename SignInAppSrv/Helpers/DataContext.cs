using Microsoft.EntityFrameworkCore;
using SignInAppSrv.Models;
using System;
using WebApp.Models;

namespace SignInAppSrv.dbContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMembership> GroupMemberships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupMembership>()
                        .HasOne<User>(sc => sc.User)
                        .WithMany(s => s.groupMemberships)
                        .HasForeignKey(sc => sc.UserId);


            modelBuilder.Entity<GroupMembership>()
                        .HasOne<Group>(sc => sc.Group)
                        .WithMany(s => s.groupMemberships)
                        .HasForeignKey(sc => sc.GroupId);

            modelBuilder.Entity<Day>()
                        .HasMany(b => b.Assignments)
                        .WithOne(d => d.Day);

            modelBuilder.Entity<User>().HasData(
                new User() { UserId = 1, FirstName = "Grzegorz", LastName = "Cieplechowicz", Email = "gcieplechowicz@gmail.com", Username = "gcieplechowicz",  Password = "atomics1", PhoneNumber = "573248012" },
                new User() { UserId = 2, FirstName = "Anna", LastName = "Cieplechowicz", Email = "gcieplechowicz2@gmail.com", Username = "acieplechowicz", Password = "atomics1", PhoneNumber = "573248012" },
                new User() { UserId = 3, FirstName = "Jan", LastName = "Cieplechowicz", Email = "gcieplechowicz3@gmail.com", Username = "jcieplechowicz", Password = "atomics1", PhoneNumber = "573248012" });

            modelBuilder.Entity<Day>().HasData(
                new Day() { Id = 1, Name = "Monday" },
                new Day() { Id = 2, Name = "Tuesday" },
                new Day() { Id = 3, Name = "Wednesday" },
                new Day() { Id = 4, Name = "Thursday" },
                new Day() { Id = 5, Name = "Friday" },
                new Day() { Id = 6, Name = "Saturday" },
                new Day() { Id = 7, Name = "Sunday" });

            modelBuilder.Entity<Group>().HasData(
                new Group() { Id = 1, Name = "Plan lekcji", Identifier = "tajnagrupa", creatorId = 1 , modifyTime = DateTime.Now});

            modelBuilder.Entity<GroupMembership>().HasData(
                new GroupMembership() { Id = 1, GroupId = 1, UserId = 1, userIdentifier = "Grzegorz Cieplechowicz" },
                new GroupMembership() { Id = 2, GroupId = 1, UserId = 2, userIdentifier = "Anna Cieplechowicz" },
                new GroupMembership() { Id = 3, GroupId = 1, UserId = 3, userIdentifier = "Jan Cieplechowicz" });

            modelBuilder.Entity<Assignment>().HasData(
                //Poniedzialek
                new Assignment() { Id = 1, Name = "Religia", GroupId = 1, Description = "200", DayId = 1, TimeFrom = new System.TimeSpan(8, 00, 00), TimeTo = new System.TimeSpan(8, 45, 00) },
                new Assignment() { Id = 2, Name = "Polski", GroupId = 1, Description = "155", DayId = 1, TimeFrom = new System.TimeSpan(8, 50, 00), TimeTo = new System.TimeSpan(9, 35, 00) },
                new Assignment() { Id = 3, Name = "Matematyka", GroupId = 1, Description = "254", DayId = 1, TimeFrom = new System.TimeSpan(9, 40, 00), TimeTo = new System.TimeSpan(10, 25, 00) },
                new Assignment() { Id = 4, Name = "Przyroda", GroupId = 1, Description = "205", DayId = 1, TimeFrom = new System.TimeSpan(10, 30, 00), TimeTo = new System.TimeSpan(11, 15, 00) },
                //Wtorek
                new Assignment() { Id = 5, Name = "W-F", GroupId = 1, Description = "200", DayId = 2, TimeFrom = new System.TimeSpan(8, 00, 00), TimeTo = new System.TimeSpan(8, 45, 00) },
                new Assignment() { Id = 6, Name = "Muzyka", GroupId = 1, Description = "155", DayId = 2, TimeFrom = new System.TimeSpan(8, 50, 00), TimeTo = new System.TimeSpan(9, 35, 00) },
                new Assignment() { Id = 7, Name = "Matematyka", GroupId = 1, Description = "254", DayId = 2, TimeFrom = new System.TimeSpan(9, 40, 00), TimeTo = new System.TimeSpan(10, 25, 00) },
                new Assignment() { Id = 8, Name = "Biologia", GroupId = 1, Description = "205", DayId = 2, TimeFrom = new System.TimeSpan(10, 30, 00), TimeTo = new System.TimeSpan(11, 15, 00) },
                new Assignment() { Id = 9, Name = "Polski", GroupId = 1, Description = "205", DayId = 2, TimeFrom = new System.TimeSpan(11, 20, 00), TimeTo = new System.TimeSpan(12, 5, 00) },
                //Sroda
                new Assignment() { Id = 10, Name = "W-F", GroupId = 1, Description = "200", DayId = 3, TimeFrom = new System.TimeSpan(8, 00, 00), TimeTo = new System.TimeSpan(8, 45, 00) },
                new Assignment() { Id = 12, Name = "Chemia", GroupId = 1, Description = "155", DayId = 3, TimeFrom = new System.TimeSpan(8, 50, 00), TimeTo = new System.TimeSpan(9, 35, 00) },
                new Assignment() { Id = 13, Name = "Matematyka", GroupId = 1, Description = "254", DayId = 3, TimeFrom = new System.TimeSpan(9, 40, 00), TimeTo = new System.TimeSpan(10, 25, 00) },
                new Assignment() { Id = 14, Name = "Matematyka", GroupId = 1, Description = "205", DayId = 3, TimeFrom = new System.TimeSpan(10, 30, 00), TimeTo = new System.TimeSpan(11, 15, 00) },
                new Assignment() { Id = 15, Name = "WDŻ", GroupId = 1, Description = "205", DayId = 3, TimeFrom = new System.TimeSpan(11, 20, 00), TimeTo = new System.TimeSpan(12, 5, 00) },
                //Czwartek
                new Assignment() { Id = 16, Name = "Polski", GroupId = 1, Description = "200", DayId = 4, TimeFrom = new System.TimeSpan(8, 00, 00), TimeTo = new System.TimeSpan(8, 45, 00) },
                new Assignment() { Id = 17, Name = "Historia", GroupId = 1, Description = "155", DayId = 4, TimeFrom = new System.TimeSpan(8, 50, 00), TimeTo = new System.TimeSpan(9, 35, 00) },
                new Assignment() { Id = 18, Name = "WOS", GroupId = 1, Description = "254", DayId = 4, TimeFrom = new System.TimeSpan(9, 40, 00), TimeTo = new System.TimeSpan(10, 25, 00) },
                new Assignment() { Id = 19, Name = "W-F", GroupId = 1, Description = "205", DayId = 4, TimeFrom = new System.TimeSpan(10, 30, 00), TimeTo = new System.TimeSpan(11, 15, 00) },
                //Piatek
                new Assignment() { Id = 20, Name = "Religia", GroupId = 1, Description = "200", DayId = 5, TimeFrom = new System.TimeSpan(8, 00, 00), TimeTo = new System.TimeSpan(8, 45, 00) },
                new Assignment() { Id = 21, Name = "Chemia", GroupId = 1, Description = "155", DayId = 5, TimeFrom = new System.TimeSpan(8, 50, 00), TimeTo = new System.TimeSpan(9, 35, 00) },
                new Assignment() { Id = 22, Name = "Polski", GroupId = 1, Description = "254", DayId = 5, TimeFrom = new System.TimeSpan(9, 40, 00), TimeTo = new System.TimeSpan(10, 25, 00) },
                new Assignment() { Id = 23, Name = "Przyroda", GroupId = 1, Description = "205", DayId = 5, TimeFrom = new System.TimeSpan(10, 30, 00), TimeTo = new System.TimeSpan(11, 15, 00) },
                new Assignment() { Id = 24, Name = "WOS", GroupId = 1, Description = "205", DayId = 5, TimeFrom = new System.TimeSpan(11, 20, 00), TimeTo = new System.TimeSpan(12, 5, 00) },
                new Assignment() { Id = 25, Name = "Angielski", GroupId = 1, Description = "205", DayId = 5, TimeFrom = new System.TimeSpan(12, 10, 00), TimeTo = new System.TimeSpan(12, 55, 00) });

        }
    }

}