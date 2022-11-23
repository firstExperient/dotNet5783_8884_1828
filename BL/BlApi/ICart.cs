using BO;

namespace BlApi;

public interface ICart
{
    public Cart AddItem(int id, Cart cart);
    public Cart UpdateItemAmount(int id, Cart cart, int newAmount);
    public void ConfirmOrder(Cart cart, string customerName, string email, string adress);
}
