using System.Diagnostics;
using System.Xml.Linq;

namespace BO;

public class OrderTracking
{
    /// <summary>
    /// Unique ID of this order tracking
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// the status of this order tracking
    /// </summary>
    public OrderStatus Status { get; set; }

    //fix this add list with pairs of descreption and date

    /// <summary>
    /// a string of the order-tracking details
    /// </summary>
    public override string ToString()
    {
        return $@"
        Order-Tracking ID: {ID}
        status: {Status}
        ";
    }
}
