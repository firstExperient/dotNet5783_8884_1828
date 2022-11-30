using DO;

namespace BO;

public class ProductForList
{
    /// <summary>
    /// Unique ID of each watch
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// the name of the watch
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// the price of the watch
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// the category of the watch
    /// </summary>
    public Category? Category { get; set; }

    /// <summary>
    /// a string of each product details in the list 
    /// </summary>
    public override string ToString()
    {
        return $@"
        Product ID: {ID}
        name: {Name}
        category: {Category}
        Price: {Price}
        ";
    }
}