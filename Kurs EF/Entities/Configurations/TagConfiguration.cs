using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kurs_EF.Entities.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> eb)
        {
            eb.HasData(new Tag() { Id = 3, Value = "Desktop" },
                        new Tag() { Id = 4, Value = "Api" },
                        new Tag() { Id = 5, Value = "Service" });
        }
    }
}
