using DO;
using System.ComponentModel;

namespace BO;

public class OrderItem:INotifyPropertyChanged
{
    /// <summary>
    /// Unique ID of the order-item
    /// </summary>
    //public int ID { get; set; }
    private int id;
    public int ID { 
        get { return id; } 
        set { id = value; 
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ID")); 
        } }

    /// <summary>
    /// Unique ID of the product ID in the order-item
    /// </summary>
    //public int ProductId { get; set; }
    private int productId;
    public int ProductId
    {
        get { return productId; }
        set
        {
            productId = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ProductId"));
        }
    }

    /// <summary>
    /// the name of the product
    /// </summary>
    //public string? Name { get; set; }

    private string? name;
    public string? Name
    {
        get { return name; }
        set
        {
            name = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Name"));
        }
    }

    /// <summary>
    /// the price of the item (watch) in a specific order
    /// </summary>
    //public double Price { get; set; }

    private double price;
    public double Price
    {
        get { return price; }
        set
        {
            price = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Price"));
        }
    }

    /// <summary>
    /// the amount of items (wathes) 
    /// </summary>
   // public int Amount { get; set; }

    private int amount;
    public int Amount
    {
        get { return amount; }
        set
        {
            amount = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
        }
    }

    /// <summary>
    /// the total price of this order-item
    /// </summary>
    //public double TotalPrice { get; set; }

    private double totalPrice;
    public double TotalPrice
    {
        get { return totalPrice; }
        set
        {
            totalPrice = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TotalPrice"));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

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
