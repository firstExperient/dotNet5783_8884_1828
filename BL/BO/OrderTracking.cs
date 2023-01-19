using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;

namespace BO;

public class OrderTracking:INotifyPropertyChanged
{
    /// <summary>
    /// order id
    /// </summary>
    private int id;
    public int ID
    {
        get { return id; }
        set
        {
            id = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ID"));
        }
    }

    /// <summary>
    /// the status of this order tracking
    /// </summary>
    private OrderStatus? status;
    public OrderStatus? Status
    {
        get { return status; }
        set
        {
            status = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Status"));
        }
    }

    /// <summary>
    /// a list of dates and a string represent the order status at that date
    /// </summary>

    private Dictionary<DateTime, string?>? trackingList = new();
    public Dictionary<DateTime, string?>? TrackingList
    {
        get { return trackingList; }
        set
        {
            trackingList = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TrackingList"));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// a string of the order-tracking details
    /// </summary>
    public override string ToString()
    {
        string tracking = "";
        if(TrackingList != null)
            foreach (var pair in TrackingList) {
                    tracking += "\n\t\tDate: " + pair.Key + "\n\t\tDescreption: " + pair.Value + "\n";
            } 
        return $@"
        Order ID: {ID}
        status: {Status}
        Tracking: 
        {tracking}
        ";

    }
}
