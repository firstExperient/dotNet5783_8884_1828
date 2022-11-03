namespace DO;

/// <summary>
/// Structure for watch (the product)
/// </summary>
public struct Product
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
    /// the name of the category of the watch
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// the price of the watch
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// this is to know how many watches have left in stock
    /// </summary>
    public int InStock { get; set; }


    /// <summary>
    /// a string of the product details
    /// </summary>
    public override string ToString() {
        return $@"
        Product ID={ID}: {Name},
        category - {Category}
        Price: {Price}
        Amount in stock: {InStock}
        ";
    }
}