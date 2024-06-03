using Microsoft.EntityFrameworkCore;
using RestaurantReservationAPI.Entities;
using RestaurantReservationAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestaurantReservationAPI.DbContexts
{
    public class RestaurantContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies()
                          .UseSqlite("Data Source = Restaurant.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FirstName = "Alice", LastName = "Johnson" },
                new Customer { CustomerId = 2, FirstName = "Bob", LastName = "Brown" },
                new Customer { CustomerId = 3, FirstName = "Charlie", LastName = "Davis" },
                new Customer { CustomerId = 4, FirstName = "David", LastName = "Miller" },
                new Customer { CustomerId = 5, FirstName = "Eva", LastName = "Wilson" },
                new Customer { CustomerId = 6, FirstName = "Frank", LastName = "Moore" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, FirstName = "John", LastName = "Doe", Role = EmployeeRole.Waiter },
                new Employee { EmployeeId = 2, FirstName = "Jane", LastName = "Smith", Role = EmployeeRole.Host },
                new Employee { EmployeeId = 3, FirstName = "Alice", LastName = "Johnson", Role = EmployeeRole.Waiter }
            );

            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem { MenuItemId = 1, Name = "Cheeseburger", Price = 9.99m, Description = "Juicy beef patty with melted cheese, lettuce, and tomato, served on a sesame seed bun." },
                new MenuItem { MenuItemId = 2, Name = "Margherita Pizza", Price = 12.99m, Description = "Classic Italian pizza topped with tomato sauce, mozzarella, and fresh basil leaves." },
                new MenuItem { MenuItemId = 3, Name = "Chicken Caesar Salad", Price = 8.49m, Description = "Crisp romaine lettuce topped with grilled chicken breast strips, croutons, and Caesar dressing." },
                new MenuItem { MenuItemId = 4, Name = "Spaghetti Bolognese", Price = 11.99m, Description = "Spaghetti pasta served with rich Bolognese sauce made with ground beef, tomatoes, and herbs." },
                new MenuItem { MenuItemId = 5, Name = "Vegetable Stir-Fry", Price = 10.49m, Description = "Assorted fresh vegetables stir-fried in a savory sauce served with steamed rice." },
                new MenuItem { MenuItemId = 6, Name = "Chocolate Brownie Sundae", Price = 6.99m, Description = "Warm chocolate brownie topped with vanilla ice cream, chocolate syrup, whipped cream, and a cherry." },
                new MenuItem { MenuItemId = 7, Name = "Grilled Salmon", Price = 14.99m, Description = "Fresh salmon fillet grilled to perfection, served with roasted vegetables and lemon wedges." },
                new MenuItem { MenuItemId = 8, Name = "Mushroom Risotto", Price = 13.49m, Description = "Creamy Arborio rice cooked with mushrooms, onions, garlic, and Parmesan cheese." },
                new MenuItem { MenuItemId = 10, Name = "BBQ Ribs", Price = 16.99m, Description = "Tender pork ribs basted in barbecue sauce, served with coleslaw and fries." }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, EmployeeId = 1, ReservationId = 1 },
                new Order { OrderId = 2, EmployeeId = 2, ReservationId = 2 },
                new Order { OrderId = 3, EmployeeId = 3, ReservationId = 3 },
                new Order { OrderId = 4, EmployeeId = 1, ReservationId = 4 },
                new Order { OrderId = 5, EmployeeId = 2, ReservationId = 5 },
                new Order { OrderId = 6, EmployeeId = 3, ReservationId = 6 }
            );

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { OrderItemId = 1, OrderId = 1, MenuItemId = 5, Quantity = 1 },
                new OrderItem { OrderItemId = 2, OrderId = 2, MenuItemId = 1, Quantity = 2 },
                new OrderItem { OrderItemId = 3, OrderId = 2, MenuItemId = 10, Quantity = 1 },
                new OrderItem { OrderItemId = 4, OrderId = 3, MenuItemId = 2, Quantity = 1 },
                new OrderItem { OrderItemId = 5, OrderId = 3, MenuItemId = 7, Quantity = 1 },
                new OrderItem { OrderItemId = 6, OrderId = 4, MenuItemId = 4, Quantity = 2 },
                new OrderItem { OrderItemId = 7, OrderId = 5, MenuItemId = 6, Quantity = 1 },
                new OrderItem { OrderItemId = 8, OrderId = 6, MenuItemId = 3, Quantity = 1 },
                new OrderItem { OrderItemId = 9, OrderId = 6, MenuItemId = 8, Quantity = 1 }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { ReservationId = 1, Date = DateTime.Now.AddDays(1), CustomerId = 1, NumberOfGuests = 2 },
                new Reservation { ReservationId = 2, Date = DateTime.Now.AddDays(2), CustomerId = 2, NumberOfGuests = 4 },
                new Reservation { ReservationId = 3, Date = DateTime.Now.AddDays(3), CustomerId = 3, NumberOfGuests = 3 },
                new Reservation { ReservationId = 4, Date = DateTime.Now.AddDays(4), CustomerId = 4, NumberOfGuests = 5 },
                new Reservation { ReservationId = 5, Date = DateTime.Now.AddDays(5), CustomerId = 5, NumberOfGuests = 1 },
                new Reservation { ReservationId = 6, Date = DateTime.Now.AddDays(6), CustomerId = 6, NumberOfGuests = 2 }
            );



        }
    }
}
