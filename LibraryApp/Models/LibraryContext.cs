using Microsoft.EntityFrameworkCore;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Categories> Categories { get; set; }
    public DbSet<Borrowing> Borrowings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Library;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ModelBuilder.Entity<Categories>()
            .HasMany(c => c.Books)
            .WithOne(b => b.Category)
            .HasForeignKey(b => b.Category_Id);

        ModelBuilder.Entity<Book>()
            .HasMany(b => b.Borrowings)
            .WithOne(br => br.Book)
            .HasForeignKey(br => br.Book_Id);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Borrowings)
            .WithOne(br => br.User)
            .HasForeignKey(br => br.UserId);
    }
}

