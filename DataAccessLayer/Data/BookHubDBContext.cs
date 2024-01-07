using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Data
{
    public class BookHubDBContext : IdentityDbContext
    {
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<AuthorBook> AuthorBooks { get; set; }
    public DbSet<GenreBook> GenreBooks { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<PurchaseHistory> PurchaseHistories { get; set; }

    public DbSet<LocalIdentityUser> IdentityCustomers { get; set; }

    public BookHubDBContext(DbContextOptions<BookHubDBContext> options) : base(options)
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

        // Define the many-to-many relationship.
        // Configure Author-Book many-to-many relationship
        modelBuilder.Entity<AuthorBook>()
            .HasKey(ab => new { ab.AuthorId, ab.BookId });

        modelBuilder.Entity<AuthorBook>()
            .HasOne(ab => ab.Author)
            .WithMany(a => a.AuthorBooks)
            .HasForeignKey(ab => ab.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AuthorBook>()
            .HasOne(ab => ab.Book)
            .WithMany(b => b.AuthorBooks)
            .HasForeignKey(ab => ab.BookId)
            .OnDelete(DeleteBehavior.Cascade);

            // Configure Book-Genre many-to-many relationship
        modelBuilder.Entity<GenreBook>()
            .HasKey(bg => new { bg.BookId, bg.GenreId });

        modelBuilder.Entity<GenreBook>()
            .HasOne(bg => bg.Book)
            .WithMany(b => b.GenreBooks)
            .HasForeignKey(bg => bg.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GenreBook>()
            .HasOne(bg => bg.Genre)
            .WithMany(g => g.GenreBooks)
            .HasForeignKey(bg => bg.GenreId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Book)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.BookId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Wishlist>()
            .HasOne(w => w.Customer)
            .WithMany(c => c.Wishlists)
            .HasForeignKey(w => w.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<PurchaseHistory>()
            .HasOne(p => p.Customer)
            .WithMany(c => c.PurchaseHistories)
            .HasForeignKey(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

            /* run the DB seeding */
            modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
    }
}

