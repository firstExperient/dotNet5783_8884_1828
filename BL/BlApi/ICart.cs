using BO;

namespace BlApi;

public interface ICart
{

    /// <summary>
    /// this function adds to cart a new item
    /// </summary>
    /// <param name="id">ID of the cart</param>
    /// <param name="cart">the cart to add</param>
    /// <returns>a BO cart</returns>
    public Cart AddItem(int id, Cart cart);

    /// <summary>
    /// this function updates the item amount to the given cart
    /// </summary>
    /// <param name="id">id of the cart</param>
    /// <param name="cart">the cart itself</param>
    /// <param name="newAmount">the amount to </param>
    /// <returns>a BO cart after the item amount update</returns>
    public Cart UpdateItemAmount(int id, Cart cart, int newAmount);

    /// <summary>
    /// this function confirmes the order and cart's details before shipping it
    /// </summary>
    /// <param name="cart">the cart</param>
    /// <param name="customerName">name of the customer</param>
    /// <param name="email">the customer's email</param>
    /// <param name="adress">the addredd to deluver the order to</param>
    public void ConfirmOrder(Cart cart, string customerName, string email, string adress);
}
