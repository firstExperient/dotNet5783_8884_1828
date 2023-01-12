using BO;

namespace BlApi;

public interface ICart
{

    /// <summary>
    /// adds a new item to cart (if item already exists - add one to the amount)
    /// </summary>
    /// <param name="id">ID of the product to add</param>
    /// <param name="cart">the cart to add to</param>
    /// <returns>a BO updated cart</returns>
    public Cart AddItem(int id, Cart cart);

    /// <summary>
    /// updates the product item amount to the given cart
    /// </summary>
    /// <param name="id">id of the product</param>
    /// <param name="cart">the cart to update</param>
    /// <param name="newAmount">the new amount</param>
    /// <returns>a BO cart after the update</returns>
    public Cart UpdateItemAmount(int id, Cart cart, int newAmount);

    /// <summary>
    /// confirmes the order - creates an order from the cart details and save to database
    /// </summary>
    /// <param name="cart">the cart</param>
    /// <returns>the id of the new order</returns>
    public int ConfirmOrder(Cart cart);
}
