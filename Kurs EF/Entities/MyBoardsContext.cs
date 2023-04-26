using Microsoft.EntityFrameworkCore;

namespace Kurs_EF.Entities
{
    public class MyBoardsContext : DbContext
    {
        public MyBoardsContext(DbContextOptions<MyBoardsContext> options) : base(options) { }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Epic> Epics { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<WorkItemState> WorkItemStates { get; set; }
        public DbSet<WorkitemTag> WorkItemTag { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItem>()
                .Property(x => x.Area)
                .HasColumnType("varchar(200)");

            modelBuilder.Entity<Epic>()
                .Property(wi => wi.EndDate)
                .HasPrecision(3);

            modelBuilder.Entity<Issue>()
                .Property(wi => wi.Effort)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<WorkItem>(eb =>
            {
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
            });


            modelBuilder.Entity<User>(eb =>
            {
                //relacja jeden do jednego
                eb.HasOne(u => u.Adress).WithOne(a => a.User).HasForeignKey<Adress>(a => a.UserId);

                //eb.HasMany(c=>c.Comment).WithOne().HasForeignKey<Comment>(c => c.CommentId);
            });

            modelBuilder.Entity<Comment>(eb =>
            {
                eb.Property(co => co.CreatedDate).HasDefaultValueSql("getutcdate()");
                eb.Property(co => co.UpdatedDate).ValueGeneratedOnUpdate();

                eb.HasOne(c => c.Author)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);
            });



            modelBuilder.Entity<WorkItemState>(eb =>
            {
                eb.Property(s => s.Value)
                .IsRequired()
                .HasMaxLength(60);
                //eb.HasMany(s => s.WorkItems).WithOne(wi=>wi.State).HasForeignKey(s => s.State.Id);
            });

            //dodawanie danych do tabeli (encji) - seed-owanie danych - sposób 1 - model data seed 
            modelBuilder.Entity<WorkItemState>()
                .HasData(new WorkItemState() { Id=1, Value = "To do" },
                        new WorkItemState() { Id=2, Value = "Doing" },
                        new WorkItemState() { Id=3, Value = "Done" });

            modelBuilder.Entity<Tag>()
                .HasData(new Tag() { Id = 3, Value = "Desktop" },
                        new Tag() { Id = 4, Value = "Api" },
                        new Tag() { Id = 5, Value = "Service" });
            //modelBuilder.Entity<WorkitemTag>().HasKey(c => new { c.TagId, c.WorkItemId });

        }
    }
}
