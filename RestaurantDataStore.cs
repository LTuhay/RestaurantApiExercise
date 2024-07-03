
//using RestaurantReservationAPI.Entities;
//using RestaurantReservationAPI.Models;

//namespace RestaurantReservationAPI
//{
//    public class RestaurantDataStore
//    {
//        public List<EmployeeDto> Employee { get; set; }
//        public List<CustomerDto> Customers { get; set; }
//        public List<MenuItemDto> MenuItems { get; set; }
//        public List<OrderItemDto> OrderItems { get; set; }

//        public List<ReservationDto> Reservations { get; set; }

//        public List<OrderDto> Orders { get; set; }

//        public static RestaurantDataStore Current { get; } = new RestaurantDataStore();


//        public RestaurantDataStore()
//        {
//            Employee = new List<EmployeeDto>()
//            {
//                new EmployeeDto(1, "John", "Doe", EmployeeRole.Manager),
//                new EmployeeDto(2, "Jane", "Smith", EmployeeRole.Host),
//                new EmployeeDto(3, "Alice", "Johnson", EmployeeRole.Waiter),
//                new EmployeeDto(4, "Bob", "Brown", EmployeeRole.Chef),
//                new EmployeeDto(5, "Charlie", "Davis", EmployeeRole.Waiter),
//                new EmployeeDto(6, "David", "Miller", EmployeeRole.Manager),
//                new EmployeeDto(7, "Eva", "Wilson", EmployeeRole.Waiter),
//                new EmployeeDto(8, "Frank", "Moore", EmployeeRole.Chef),
//                new EmployeeDto(9, "Grace", "Taylor", EmployeeRole.Host),
//                new EmployeeDto(10, "Hank", "Anderson", EmployeeRole.Waiter)
//            };

//            Customers = new List<CustomerDto>()
//            {
//                new CustomerDto(1, "Alice", "Johnson"),
//                new CustomerDto(2, "Bob", "Brown"),
//                new CustomerDto(3, "Charlie", "Davis"),
//                new CustomerDto(4, "David", "Miller"),
//                new CustomerDto(5, "Eva", "Wilson"),
//                new CustomerDto(6, "Frank", "Moore"),
//                new CustomerDto(7, "Grace", "Taylor"),
//                new CustomerDto(8, "Hank", "Anderson"),
//                new CustomerDto(9, "Ivy", "Clark"),
//                new CustomerDto(10, "Jack", "White")
//            };

//            MenuItems = new List<MenuItemDto>()
//            {
//                new MenuItemDto(1, "Cheeseburger", 9.99m, "Juicy beef patty with melted cheese, lettuce, and tomato, served on a sesame seed bun."),
//                new MenuItemDto(2, "Margherita Pizza", 12.99m, "Classic Italian pizza topped with tomato sauce, mozzarella cheese, and fresh basil leaves."),
//                new MenuItemDto(3, "Chicken Caesar Salad", 8.49m, "Crisp romaine lettuce topped with grilled chicken breast strips, croutons, and Caesar dressing."),
//                new MenuItemDto(4, "Spaghetti Bolognese", 11.99m, "Spaghetti pasta served with rich Bolognese sauce made with ground beef, tomatoes, and herbs."),
//                new MenuItemDto(5, "Vegetable Stir-Fry", 10.49m, "Assorted fresh vegetables stir-fried in a savory sauce, served with steamed rice."),
//                new MenuItemDto(6, "Chocolate Brownie Sundae", 6.99m, "Warm chocolate brownie topped with vanilla ice cream, chocolate syrup, whipped cream, and a cherry."),
//                new MenuItemDto(7, "Grilled Salmon", 14.99m, "Fresh salmon fillet grilled to perfection, served with roasted vegetables and lemon wedges."),
//                new MenuItemDto(8, "Mushroom Risotto", 13.49m, "Creamy Arborio rice cooked with mushrooms, onions, garlic, and Parmesan cheese."),
//                new MenuItemDto(9, "Caesar Wrap", 7.99m, "Grilled chicken, romaine lettuce, Parmesan cheese, and Caesar dressing wrapped in a soft tortilla."),
//                new MenuItemDto(10, "BBQ Ribs", 16.99m, "Tender pork ribs basted in barbecue sauce, served with coleslaw and fries.")
//            };

//            OrderItems = new List<OrderItemDto>()
//            {
//                new OrderItemDto(1, MenuItems[0], 2),
//                new OrderItemDto(2, MenuItems[1], 1),
//                new OrderItemDto(3, MenuItems[2], 3),
//                new OrderItemDto(4, MenuItems[3], 1),
//                new OrderItemDto(5, MenuItems[4], 2),
//                new OrderItemDto(6, MenuItems[5], 1),
//                new OrderItemDto(7, MenuItems[6], 2),
//                new OrderItemDto(8, MenuItems[7], 1),
//                new OrderItemDto(9, MenuItems[8], 3),
//                new OrderItemDto(10, MenuItems[9], 1)

//            };

//            Orders = new List<OrderDto>()
//            {

//                    new OrderDto(1, new List<OrderItemDto>()
//                    {
//                        OrderItems[0],
//                        OrderItems[1],
//                        OrderItems[2]
//                    }, Employee[0]),

//                    new OrderDto(2, new List<OrderItemDto>()
//                    {
//                        OrderItems[3],
//                        OrderItems[4],
//                        OrderItems[5]
//                    }, Employee[1]),

//                    new OrderDto(3, new List<OrderItemDto>()
//                    {
//                        OrderItems[6],
//                        OrderItems[7],
//                        OrderItems[8],
//                        OrderItems[9],
//                    }, Employee[2])

//            };

//            Reservations = new List<ReservationDto>()
//            {
//                new ReservationDto()
//                {
//                    ReservationId = 1,
//                    Date = DateTime.Today.AddDays(1),
//                    Customer = Customers[0],
//                    Orders = new List<OrderDto>() { Orders[0] },
//                    NumberOfGuests = 4,
//                    Notes = "Please seat us at a table near the window."
//                },
//                new ReservationDto()
//                {
//                    ReservationId = 2,
//                    Date = DateTime.Today.AddDays(5),
//                    Customer = Customers[2],
//                    Orders = new List<OrderDto>() { Orders[1] },
//                    NumberOfGuests = 6,
//                    Notes = "Birthday celebration. Prefer a table in a quiet area."
//                },
//                new ReservationDto()
//                {
//                    ReservationId = 3,
//                    Date = DateTime.Today.AddDays(3),
//                    Customer = Customers[3],
//                    Orders = new List<OrderDto>() { Orders[2] },
//                    NumberOfGuests = 2,
//                    Notes = ""
//                }
//            };


//        }



//    }
//}
