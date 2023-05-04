using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kurs_EF.Entities.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> eb)
        {
            eb.HasOne(u => u.Adress).WithOne(a => a.User).HasForeignKey<Adress>(a => a.UserId);
            //eb.HasIndex(u => u.Email);                      //index dla samej kolumny email
            eb.HasIndex(u => new { u.FullName, u.Email });  //index dla wielu kollumn
        }
    }
}
