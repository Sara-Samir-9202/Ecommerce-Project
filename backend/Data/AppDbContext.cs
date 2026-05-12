using e_commerceAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_commerceAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product -> Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            // CartItem -> Cart
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            // CartItem -> Product
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId);


            modelBuilder.Entity<WishlistItem>()
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<WishlistItem>()
                .HasOne(w => w.Product)
                .WithMany()
                .HasForeignKey(w => w.ProductId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany()
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId);

            // Seller relationship with Product
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Seller)
                .WithMany()
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.SetNull);



            // Decimal Precision
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CartItem>()
                .Property(c => c.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(o => o.UnitPrice)
                .HasPrecision(18, 2);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "men's clothing" },
                new Category { Id = 2, Name = "jewelery" },
                new Category { Id = 3, Name = "electronics" },
                new Category { Id = 4, Name = "women's clothing" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(

                new Product
                {
                    Id = 1,
                    Title = "Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops",
                    Price = 109.95m,
                    Description = "Your perfect pack for everyday use and walks in the forest.",
                    Image = "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_t.png",
                    Stock = 25,
                    Rate = 3.9,
                    RatingCount = 120,
                    CategoryId = 1
                },

                new Product
                {
                    Id = 2,
                    Title = "Mens Casual Premium Slim Fit T-Shirts",
                    Price = 22.3m,
                    Description = "Slim-fitting style, contrast raglan long sleeve.",
                    Image = "https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_t.png",
                    Stock = 40,
                    Rate = 4.1,
                    RatingCount = 259,
                    CategoryId = 1
                },

                new Product
                {
                    Id = 3,
                    Title = "Mens Cotton Jacket",
                    Price = 55.99m,
                    Description = "Great outerwear jacket for seasonal use.",
                    Image = "https://fakestoreapi.com/img/71li-ujtlUL._AC_UX679_t.png",
                    Stock = 18,
                    Rate = 4.7,
                    RatingCount = 500,
                    CategoryId = 1
                },

                new Product
                {
                    Id = 4,
                    Title = "Mens Casual Slim Fit",
                    Price = 15.99m,
                    Description = "Casual slim fit shirt.",
                    Image = "https://fakestoreapi.com/img/71YXzeOuslL._AC_UY879_t.png",
                    Stock = 33,
                    Rate = 2.1,
                    RatingCount = 430,
                    CategoryId = 1
                },

                new Product
                {
                    Id = 5,
                    Title = "John Hardy Women's Bracelet",
                    Price = 695m,
                    Description = "Gold and silver dragon chain bracelet.",
                    Image = "https://fakestoreapi.com/img/71pWzhdJNwL._AC_UL640_QL65_ML3_t.png",
                    Stock = 10,
                    Rate = 4.6,
                    RatingCount = 400,
                    CategoryId = 2
                },

                new Product
                {
                    Id = 6,
                    Title = "Solid Gold Petite Micropave",
                    Price = 168m,
                    Description = "Elegant jewelry piece.",
                    Image = "https://fakestoreapi.com/img/61sbMiUnoGL._AC_UL640_QL65_ML3_t.png",
                    Stock = 15,
                    Rate = 3.9,
                    RatingCount = 70,
                    CategoryId = 2
                },

                new Product
                {
                    Id = 7,
                    Title = "White Gold Plated Princess Ring",
                    Price = 9.99m,
                    Description = "Engagement ring for women.",
                    Image = "https://fakestoreapi.com/img/71YAIFU48IL._AC_UL640_QL65_ML3_t.png",
                    Stock = 60,
                    Rate = 3.0,
                    RatingCount = 400,
                    CategoryId = 2
                },

                new Product
                {
                    Id = 8,
                    Title = "Rose Gold Plated Earrings",
                    Price = 10.99m,
                    Description = "Stainless steel earrings.",
                    Image = "https://fakestoreapi.com/img/51UDEzMJVpL._AC_UL640_QL65_ML3_t.png",
                    Stock = 55,
                    Rate = 1.9,
                    RatingCount = 100,
                    CategoryId = 2
                },

                new Product
                {
                    Id = 9,
                    Title = "WD 2TB External Hard Drive",
                    Price = 64m,
                    Description = "USB 3.0 portable hard drive.",
                    Image = "https://fakestoreapi.com/img/61IBBVJvSDL._AC_SY879_t.png",
                    Stock = 20,
                    Rate = 3.3,
                    RatingCount = 203,
                    CategoryId = 3
                },

                new Product
                {
                    Id = 10,
                    Title = "SanDisk SSD PLUS 1TB",
                    Price = 109m,
                    Description = "Fast SSD storage drive.",
                    Image = "https://fakestoreapi.com/img/61U7T1koQqL._AC_SX679_t.png",
                    Stock = 12,
                    Rate = 2.9,
                    RatingCount = 470,
                    CategoryId = 3
                },

                new Product
                {
                    Id = 11,
                    Title = "Silicon Power 256GB SSD",
                    Price = 109m,
                    Description = "High performance SSD.",
                    Image = "https://fakestoreapi.com/img/71kWymZ+c+L._AC_SX679_t.png",
                    Stock = 30,
                    Rate = 4.8,
                    RatingCount = 319,
                    CategoryId = 3
                },

                new Product
                {
                    Id = 12,
                    Title = "WD 4TB Gaming Drive",
                    Price = 114m,
                    Description = "Gaming external drive.",
                    Image = "https://fakestoreapi.com/img/61mtL65D4cL._AC_SX679_t.png",
                    Stock = 22,
                    Rate = 4.8,
                    RatingCount = 400,
                    CategoryId = 3
                },

                new Product
                {
                    Id = 13,
                    Title = "Acer 21.5 Full HD Monitor",
                    Price = 599m,
                    Description = "IPS ultra-thin display.",
                    Image = "https://fakestoreapi.com/img/81QpkIctqPL._AC_SX679_t.png",
                    Stock = 8,
                    Rate = 2.9,
                    RatingCount = 250,
                    CategoryId = 3
                },

                new Product
                {
                    Id = 14,
                    Title = "Samsung Curved Gaming Monitor",
                    Price = 999.99m,
                    Description = "Ultra wide curved monitor.",
                    Image = "https://fakestoreapi.com/img/81Zt42ioCgL._AC_SX679_t.png",
                    Stock = 5,
                    Rate = 2.2,
                    RatingCount = 140,
                    CategoryId = 3
                },

                new Product
                {
                    Id = 15,
                    Title = "Women's Snowboard Jacket",
                    Price = 56.99m,
                    Description = "Winter waterproof jacket.",
                    Image = "https://fakestoreapi.com/img/51Y5NI-I5jL._AC_UX679_t.png",
                    Stock = 14,
                    Rate = 2.6,
                    RatingCount = 235,
                    CategoryId = 4
                },

                new Product
                {
                    Id = 16,
                    Title = "Women's Faux Leather Jacket",
                    Price = 29.95m,
                    Description = "Stylish biker jacket.",
                    Image = "https://fakestoreapi.com/img/81XH0e8fefL._AC_UY879_t.png",
                    Stock = 27,
                    Rate = 2.9,
                    RatingCount = 340,
                    CategoryId = 4
                },

                new Product
                {
                    Id = 17,
                    Title = "Women's Rain Jacket",
                    Price = 39.99m,
                    Description = "Lightweight raincoat.",
                    Image = "https://fakestoreapi.com/img/71HblAHs5xL._AC_UY879_-2t.png",
                    Stock = 35,
                    Rate = 3.8,
                    RatingCount = 679,
                    CategoryId = 4
                },

                new Product
                {
                    Id = 18,
                    Title = "Women's Boat Neck T-Shirt",
                    Price = 9.85m,
                    Description = "Soft stretch top.",
                    Image = "https://fakestoreapi.com/img/71z3kpMAYsL._AC_UY879_t.png",
                    Stock = 50,
                    Rate = 4.7,
                    RatingCount = 130,
                    CategoryId = 4
                },

                new Product
                {
                    Id = 19,
                    Title = "Women's Moisture Shirt",
                    Price = 7.95m,
                    Description = "Breathable sports shirt.",
                    Image = "https://fakestoreapi.com/img/51eg55uWmdL._AC_UX679_t.png",
                    Stock = 60,
                    Rate = 4.5,
                    RatingCount = 146,
                    CategoryId = 4
                },

                new Product
                {
                    Id = 20,
                    Title = "Women's Cotton T-Shirt",
                    Price = 12.99m,
                    Description = "Casual cotton V-neck shirt.",
                    Image = "https://fakestoreapi.com/img/61pHAEJ4NML._AC_UX679_t.png",
                    Stock = 45,
                    Rate = 3.6,
                    RatingCount = 145,
                    CategoryId = 4
                }
            );
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Notification> Notifications { get; set; }
     
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }
    }
}