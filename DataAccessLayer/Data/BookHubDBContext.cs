using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Data
{
    public class BookHubDBContext : DbContext
    {
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<PurchaseHistory> PurchaseHistories { get; set; }

    public BookHubDBContext(DbContextOptions options) : base(options)
    {
    }

    // https://docs.microsoft.com/en-us/ef/core/modeling/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Setup the delete method for all of the entities using reflexion
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.SetNull;
        }

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Genre)
            .WithMany(g => g.Books)
            .HasForeignKey(b => b.GenreId);


        modelBuilder.Entity<Book>()
            .HasOne(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Book)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.BookId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.CustomerId);

        modelBuilder.Entity<Wishlist>()
            .HasOne(w => w.Book)
            .WithMany(b => b.Wishlists)
            .HasForeignKey(w => w.BookId);

        modelBuilder.Entity<Wishlist>()
            .HasOne(w => w.Customer)
            .WithMany(c => c.Wishlists)
            .HasForeignKey(w => w.CustomerId);

        modelBuilder.Entity<PurchaseHistory>()
            .HasOne(p => p.Book)
            .WithMany(b => b.PurchaseHistories)
            .HasForeignKey(p => p.BookId);

        modelBuilder.Entity<PurchaseHistory>()
            .HasOne(p => p.Customer)
            .WithMany(c => c.PurchaseHistories)
            .HasForeignKey(p => p.CustomerId);

        /* run the DB seeding */
        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
    }
}

