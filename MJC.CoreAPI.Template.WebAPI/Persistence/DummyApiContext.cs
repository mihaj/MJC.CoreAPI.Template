using Microsoft.EntityFrameworkCore;
using MJC.CoreAPI.Template.WebAPI.Core.Entities;
using MJC.CoreAPI.Template.WebAPI.Persistence.EntityConfigurations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MJC.CoreAPI.Template.WebAPI.Persistence
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
            modelBuilder.ApplyConfiguration(new DummyConfiguration());
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
