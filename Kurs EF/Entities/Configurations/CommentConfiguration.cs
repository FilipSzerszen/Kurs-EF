using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kurs_EF.Entities.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> eb)
        {
            eb.Property(co => co.CreatedDate).HasDefaultValueSql("getutcdate()");
            eb.Property(co => co.UpdatedDate).ValueGeneratedOnUpdate();

            eb.HasOne(c => c.Author)
            .WithMany(a => a.Comments)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
