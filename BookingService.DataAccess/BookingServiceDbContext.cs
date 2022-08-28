using System;
using System.Collections.Generic;
using System.Text;
using BookingService.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BookingService.DataAccess
{

    public class BookingServiceDbContext : DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=psql-mock-database-cloud.postgres.database.azure.com;Database=booking1661678861778msjijmdzuhimkuzz;Port=5432;Trust Server Certificate=true;Ssl Mode=Require;User Id=wthrndmdcjbjchnwqiwhfpdm@psql-mock-database-cloud;Password=hubzsrqeepttqzdaqjnplsob");

        public DbSet<bookings> bookings { get; set; }
        public DbSet<appartments> appartments { get; set; }
        public DbSet<company> company { get; set; }
        public DbSet<users> users { get; set; }

    }
}
