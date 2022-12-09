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
    public OrderStatus? Status { get; set; }

    
    /// <summary>
    /// a list of dates and a string represent the order status at that date
    /// </summary>
    public List<(DateTime?, string?)>? TrackingList = new();

    /// <summary>
    /// a string of the order-tracking details
    /// </summary>
    public override string ToString()
    {
        string tracking = "";
        if(TrackingList != null)
            foreach ((DateTime? date, string? msg) in TrackingList) {
                if(date != null && msg != null)
                    tracking += "\n\t\tDate: " + date + "\n\t\tDescreption: " + msg + "\n";
            } 
        return $@"
        Order ID: {ID}
        status: {Status}
        Tracking: 
        {tracking}
        ";

    }
}
