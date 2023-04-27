using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Kurs_EF.Entities.Configurations
{
    public class AdressConfiguration : IEntityTypeConfiguration<Adress>
    {
        public void Configure(EntityTypeBuilder<Adress> eb)
        {
            eb.OwnsOne(a => a.Coordinates, cmb =>
            {
                cmb.Property(c => c.Latitude).HasPrecision(18, 7);
                cmb.Property(c => c.Longitude).HasPrecision(18, 7);
            });
        }
    }
}
