
using DO;

namespace BO;

public class OrderItem
{
    /// <summary>
    /// Unique ID of the order-item
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Unique ID of the product ID in the order-item
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// the name of this order-item
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// the price of the item (watch) specific order
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// the amount of items (wathes) in this order-item
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// the total price of this order-item
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// a string of the order-item details
    /// </summary>
    public override string ToString()
    {
        return $@"
        ID: {ID}
        Product Id: {ProductId}
        Name: {Name}
        Price: {Price}
        Amount: {Amount}
        Total Price: {TotalPrice}
        ";
    }
}
