using BuySellCarsWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuySellCarsWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CarOrderItem> CarOrderItems { get; set; }
        public DbSet<CarPartOrderItem> CarPartOrderItems { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarPart> CarParts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Seed Cars
            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, Brand = "Toyota", Model = "Camry", Year = 2020 },
                new Car { Id = 2, Brand = "Honda", Model = "Civic", Year = 2021 }
            );

            // Seed CarParts
            modelBuilder.Entity<CarPart>().HasData(
                new CarPart { Id = 1, Name = "Brake Pad", Price = 50.00m },
                new CarPart { Id = 2, Name = "Oil Filter", Price = 25.00m }
            );

            // Seed Orders
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, OrderDate = DateTime.Now, UserId = 1 } // Adjust UserId to match existing users
            );

            // Seed OrderItems
            modelBuilder.Entity<CarOrderItem>().HasData(
                new CarOrderItem { Id = 1, OrderId = 1, CarId = 1 }
            );

            modelBuilder.Entity<CarPartOrderItem>().HasData(
                new CarPartOrderItem { Id = 2, OrderId = 1, CarPartId = 1 }
            );



            modelBuilder.Entity<OrderItem>()
                .HasDiscriminator<string>("OrderItemType")
                .HasValue<CarOrderItem>("CarOrderItem")
                .HasValue<CarPartOrderItem>("CarPartOrderItem");


            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);


            modelBuilder.Entity<CarOrderItem>()
                .HasOne(coi => coi.Car)
                .WithMany()
                .HasForeignKey(coi => coi.CarId);


            modelBuilder.Entity<CarPartOrderItem>()
                .HasOne(cpoi => cpoi.CarPart)
                .WithMany()
                .HasForeignKey(cpoi => cpoi.CarPartId);
        }
    }
}