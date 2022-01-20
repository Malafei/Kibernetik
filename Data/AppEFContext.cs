using Kibernetik.Data.DataNews;
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
    }
}
