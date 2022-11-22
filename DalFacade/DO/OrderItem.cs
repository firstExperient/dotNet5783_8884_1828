using System.Xml.Linq;

namespace DO;

    /// <summary>
    /// Structure for watch order-items
    /// </summary>
public struct OrderItem
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
    /// Unique ID of the order ID in the order-item
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// the price of the item (watch) specific order
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// the amount of items (wathes) in this order-item
    /// </summary>
    public int Amount { get; set; }


    /// <summary>
    /// a string of the order-item details
    /// </summary>
    public override string ToString()
    {
        return $@"
        ID: {ID}
        Product Id: {ProductId}
        Order Id: {OrderId}
        Price: {Price}
        Amount: {Amount}
        ";
    }
}