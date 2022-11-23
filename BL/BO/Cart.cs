using DO;

namespace BO;

public class Cart
{
    /// <summary>
    /// customer's name
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// customer's email
    /// </summary>
    public string CustomerEmail { get; set; }

    /// <summary>
    /// customer's address
    /// </summary>
    public string CustomerAdress { get; set; }

    /// <summary>
    /// list of items in cart
    /// </summary>
    public List<OrderItem> Items { get; set; }

    /// <summary>
    /// the total price of all items in cart
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// a string of the cart details
    /// </summary>
    public override string ToString()
    {
        string temp = "";
        foreach (OrderItem item in Items)
        {
            temp = temp + item;
        }
        return $@"
        Customer name: {CustomerName}
        Customer email: {CustomerEmail}
        Customer address: {CustomerAdress}
        Items list: {temp}
        Total Price: {TotalPrice}
        ";
    }
}
