using Microsoft.EntityFrameworkCore;
using MJC.CoreAPI.Template.WebAPI.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MJC.CoreAPI.Template.WebAPI.Data
{
    public class DummyApiContext : DbContext
    {
        public DummyApiContext(DbContextOptions<DummyApiContext> options)
                : base(options)
        {
            //Database.Migrate();
        }

        public DbSet<Dummy> Dummies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dummy>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Dummy>().Property(b => b.UpdatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Dummy>().Property(b => b.UpdatedAt).HasComputedColumnSql("getdate()");
        }


        public void Seed(string JsonPath)
        {
            this.Database.EnsureCreated();

            if (!this.Dummies.Any())
            {
                // Need to create sample data
                var filepath = Path.Combine(JsonPath, "dummies.json");
                var json = File.ReadAllText(filepath);
                var dummies = JsonConvert.DeserializeObject<IEnumerable<Dummy>>(json);
                this.Dummies.AddRange(dummies);

                this.SaveChanges();
            }
        }
    }
}
