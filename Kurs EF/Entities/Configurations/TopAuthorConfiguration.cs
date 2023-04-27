using Kurs_EF.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kurs_EF.Entities.Configurations
{
    public class TopAuthorConfiguration : IEntityTypeConfiguration<TopAuthor>
    {
        public void Configure(EntityTypeBuilder<TopAuthor> eb)
        {
            eb.ToView("View_TopAuthors");
            eb.HasNoKey();
        }
    }
}
