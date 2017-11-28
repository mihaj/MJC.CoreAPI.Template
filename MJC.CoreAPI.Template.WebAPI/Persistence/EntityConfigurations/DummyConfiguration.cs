using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MJC.CoreAPI.Template.WebAPI.Core.Entities;

namespace MJC.CoreAPI.Template.WebAPI.Persistence.EntityConfigurations
{
    public class DummyConfiguration : IEntityTypeConfiguration<Dummy>
    {
        public void Configure(EntityTypeBuilder<Dummy> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name)
                .HasMaxLength(75)
                .IsRequired();

            builder.Property(b => b.CreatedAt)
                .HasDefaultValueSql("getdate()")
                .HasColumnType("datetime2(7)")
                .IsRequired();

            builder.Property(b => b.UpdatedAt)
                .HasDefaultValueSql("getdate()")
                .HasColumnType("datetime2(7)")
                .HasComputedColumnSql("getdate()")
                .IsRequired();
        }
    }
}
