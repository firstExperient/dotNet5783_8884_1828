using DO;

namespace BO;

public class ProductItem
{
    /// <summary>
    /// Unique ID of the watch
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// the name of the watch
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// the price of the watch
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// the name of the category of the watch
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// Amount of items ordered
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// this is to know how many watches have left in stock
    /// </summary>
    public bool InStock { get; set; }

    /// <summary>
    /// a string of the product-item details
    /// </summary>
    public override string ToString()
    {
        return $@"
        Product ID: {ID}
        Name: {Name}
        Category:  {Category}
        Price: {Price}
        Product is in stock: {InStock}
        Amount of items ordered: {Amount}
        ";
    }
}
