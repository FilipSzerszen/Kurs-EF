using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Kurs_EF.Entities.Configurations
{
    public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
    {
        public void Configure(EntityTypeBuilder<WorkItem> eb)
        {
            eb.Property(x => x.Area).HasColumnType("varchar(200)");

            eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path");
            eb.Property(wi => wi.Priority).HasDefaultValue(1);

            //wiele do jednego
            eb.HasMany(c => c.Comments).WithOne(wi => wi.WorkItem).HasForeignKey(wi => wi.WorkItemId);

            //jeden do wielu 
            eb.HasOne(u => u.Author).WithMany(wi => wi.WorkItems).HasForeignKey(u => u.AuthorID);
            eb.HasOne(s => s.State).WithMany().HasForeignKey(s => s.StateId);

            //wiele do wielu z tabelą łączącą
            eb.HasMany(wi => wi.Tags).WithMany(t => t.WorkItems).UsingEntity<WorkitemTag>(
                wi => wi.HasOne(wit => wit.Tag).
                WithMany().
                HasForeignKey(wit => wit.TagId),

                t => t.HasOne(wit => wit.WorkItem)
                .WithMany()
                .HasForeignKey(wit => wit.WorkItemId),

                wit =>
                {
                    wit.HasKey(x => new { x.TagId, x.WorkItemId });
                    wit.Property(x => x.PublicationDate).HasDefaultValueSql("getutcdate()");
                }
                );
        }
    }
}
