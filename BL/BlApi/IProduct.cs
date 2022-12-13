using BO;

namespace BlApi;

public interface IProduct
{
    /// <summary>
    /// get all the products from the database, return a list of ProductForList 
    /// </summary>
    /// <returns>a list of BO.ProductForList objects</returns>
    public IEnumerable<ProductForList?> GetAll();


    /// <summary>
    /// get all the products with the category wanted from the database, return a list of ProductForList 
    /// </summary>
    /// <returns>a list of BO.ProductForList filtered by category</returns>
    public IEnumerable<ProductForList?> GetByCategory(BO.Category category);

    /// <summary>
    /// get a product from the database using id, returns a Product.
    /// for the administrator view
    /// </summary>
    /// <param name="id">id of the product</param>
    /// <returns>BO.Product object</returns>
    public Product AdminGet(int id);

    /// <summary>
    /// get a product from the database using id, returns a ProductItem.
    /// for the customer view
    /// </summary>
    /// <param name="id">id of the product</param>
    /// <param name="cart">the customer cart</param>
    /// <returns>BO.ProductItem object</returns>
    public ProductItem Get(int id, Cart cart);

    /// <summary>
    /// add a new product to database
    /// </summary>
    /// <param name="item">the product to add</param>
    public void Add(Product item);

    /// <summary>
    /// update a product in the database
    /// </summary>
    /// <param name="item">the product to update</param>
    public void Update(Product item);
   
    /// <summary>
    /// delete a product from the database
    /// </summary>
    /// <param name="id">the product id</param>
    public void Delete(int id);
}