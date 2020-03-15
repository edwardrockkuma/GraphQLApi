using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel.Data;

namespace DataModel.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
            
        }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer(
        //         @"data source=127.0.0.1,1433;initial catalog=GraphQL;persist security info=True;user id=sa;password=9@^Edw;MultipleActiveResultSets=True;");
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var ids = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
            modelBuilder.ApplyConfiguration(new OwnerContextConfiguration(ids));
            modelBuilder.ApplyConfiguration(new AccountContextConfiguration(ids));
        }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}