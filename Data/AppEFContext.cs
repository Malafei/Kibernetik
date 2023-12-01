using Kibernetik.Data.DataNews;
using Kibernetik.Data.DataShedule;
using Kibernetik.Data.DataUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Kibernetik.Data
{
    public class AppEFContext : DbContext
    {
        public AppEFContext(DbContextOptions<AppEFContext> options)
            : base(options)
        {

        }



        public DbSet<User> users { get; set; }
        public DbSet<News> news { get; set; }
        public DbSet<Lesson> lesson { get; set; }
        public DbSet<Shedule> shedule { get; set; }
        public DbSet<Group> group { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lesson>()
                .HasOne(p => p.shedule)
                .WithMany(b => b.lessons);
        }
    }
}
