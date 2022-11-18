

using DO;

namespace BO;

public class Cart
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    List<OrderItem> Items { get; set; }
    public double TotalPrice { get; set; }
}
