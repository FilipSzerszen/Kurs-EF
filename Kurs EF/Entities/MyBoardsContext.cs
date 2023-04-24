using Microsoft.EntityFrameworkCore;

namespace Kurs_EF.Entities
{
    public class MyBoardsContext : DbContext
    {
        public MyBoardsContext(DbContextOptions<MyBoardsContext> options) : base(options) { }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<WorkItem>()
            //    .Property(x => x.State)
            //    .IsRequired();
            modelBuilder.Entity<WorkItem>()
                .Property(x => x.Area)
                .HasColumnType("varchar200");
            modelBuilder.Entity<WorkItem>(eb =>
            {

                eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path");
                eb.Property(wi => wi.EndDate).HasPrecision(3);
                eb.Property(wi => wi.Effort).HasColumnType("decimal(5,2)");
                eb.Property(wi => wi.Priority).HasDefaultValue(1);

                //wiele do jednego
                eb.HasMany(c => c.Comments).WithOne(wi => wi.WorkItem).HasForeignKey(wi => wi.WorkItemId);

                //jeden do wielu 
                eb.HasOne(u => u.Author).WithMany(wi => wi.WorkItems).HasForeignKey(u => u.AuthorID);
                eb.HasOne(s => s.State).WithMany().HasForeignKey(s => s.StateId);

                //wiele do wielu z tabelą łączącą
                eb.HasMany(wi => wi.Tags).WithMany(t => t.WorkItems).UsingEntity<WorkitemTag>(
                    wi=>wi.HasOne(wit=>wit.Tag).
                    WithMany().
                    HasForeignKey(wit=>wit.TagId),

                    t=>t.HasOne(wit=>wit.WorkItem)
                    .WithMany()
                    .HasForeignKey(wit=>wit.WorkItemId),

                    wit =>
                    {
                        wit.HasKey(x => new {x.TagId, x.WorkItemId});
                        wit.Property(x => x.PublicationDate).HasDefaultValueSql("getutcdate()");
                    }
                    );
            });

            modelBuilder.Entity<Comment>(eb =>
            {
                eb.Property(co => co.CreatedDate).HasDefaultValueSql("getutcdate()");
                eb.Property(co => co.UpdatedDate).ValueGeneratedOnUpdate();
            });

            modelBuilder.Entity<User>(eb =>
            {
                //relacja jeden do jednego
                eb.HasOne(u => u.Adress).WithOne(a => a.User).HasForeignKey<Adress>(a => a.User.Id);
            });

            modelBuilder.Entity<State>(eb =>
            {
                eb.Property(s => s.Value)
                .IsRequired()
                .HasMaxLength(50);
                //eb.HasMany(s => s.WorkItems).WithOne(wi=>wi.State).HasForeignKey(s => s.State.Id);
            });

            //modelBuilder.Entity<WorkitemTag>().HasKey(c => new { c.TagId, c.WorkItemId });

        }
    }
}
