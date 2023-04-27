using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Kurs_EF.Entities.Configurations
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> eb)
        {
            eb.Property(wi => wi.Effort).HasColumnType("decimal(5,2)");
        }
    }
}
