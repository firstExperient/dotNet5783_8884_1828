using DO;

namespace Dal;

public struct DataSource
{
    internal static Product[] Products;
    internal static Order[] Orders;
    internal static OrderItem[] OrderItems;
    internal struct Config
    {
        static internal int ProductsIndex = 0;
        static internal int OrdersIndex = 0;
        static internal int OrderItemsIndex = 0;

    }

    internal static void Initialize() { }
}
