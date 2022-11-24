namespace DO;

/// <summary>
/// Structure for watch orders
/// </summary>
public struct Order
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
    /// the email of the customer who ordered the order
    /// </summary>
    public string CustomerEmail { get; set; }


    /// <summary>
    /// the address of the customer, to know where to send the order to
    /// </summary>
    public string CustomerAdress { get; set; }


    /// <summary>
    /// the date of the orderation
    /// </summary>
    public DateTime OrderDate { get; set; }


    /// <summary>
    /// the order shipping date
    /// </summary>
    public DateTime ShipDate { get; set; }


    /// <summary>
    /// the order delivering date
    /// </summary>
    public DateTime DeliveryDate { get; set; }


    /// <summary>
    /// a string of the order details
    /// </summary>
    public override string ToString()
    {
        return $@"
        Order ID={ID}
        Customer name={CustomerName}
        Customer email={CustomerEmail}
        Customer address={CustomerAdress}
        Order date={OrderDate.Day}:{OrderDate.Month}:{OrderDate.Year}
        Ship date={ShipDate.Day}:{ShipDate.Month}:{ShipDate.Year}
        Delivery date={DeliveryDate.Day}:{DeliveryDate.Month}:{DeliveryDate.Year}
        ";
    }
}