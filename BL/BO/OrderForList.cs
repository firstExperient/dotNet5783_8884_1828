namespace BO;

public class OrderForList
{
    /// <summary>
    /// Unique ID of the order
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// the name of the customer who ordered the order
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// the status of the order
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// Amount of items in the order 
    /// </summary>
    public int AmountOfItems { get; set; }

    /// <summary>
    /// the order's total price
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// a string of each order details in list
    /// </summary>
    public override string ToString()
    {
        return $@"
        Order ID: {ID}
        Customer name: {CustomerName}
        Order status: {Status}
        Amount of items ordered: {AmountOfItems}
        Total Price: {TotalPrice}
        ";
    }
}