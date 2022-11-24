using System.Diagnostics;
using System.Xml.Linq;

namespace BO;

public class OrderTracking
{
    /// <summary>
    /// order id
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// the status of this order tracking
    /// </summary>
    public OrderStatus Status { get; set; }

    
    public List<(DateTime, string)> TrackingList = new();
    /// <summary>
    /// a string of the order-tracking details
    /// </summary>
    public override string ToString()
    {
        string tracking = "";
        foreach ((DateTime date, string msg) in TrackingList) tracking += date + ": " + msg + "\n";
        return $@"
        Order ID: {ID}
        status: {Status}
        Tracking: 
        {tracking}
        ";
    }
}
