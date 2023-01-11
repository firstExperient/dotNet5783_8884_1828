using BlApi;
namespace BlImplementation;

internal class Product : IProduct
{
    private DalApi.IDal? dal =  DalApi.Factory.Get();


    #region ADD
    public void Add(BO.Product item)
    {
        checkValid(item);
        try
        {
            //I couldn't use the operator ? here. because i wanted to throw error if dal is null, and (?? throw...)
            //works only in assignment like i did in the rest of the project
            if (dal == null) throw new BO.AccessToDataFailedException("cannot access the data layer");
            dal?.Product.Add(Tools.Copy(item, new DO.Product()
            {
                Category = (DO.Category?)item.Category,
            }));
        }
        catch (DO.AlreadyExistsException e)
        {
            throw new BO.AlreadyExistsException(e.Message, e);
        }
    }

    #endregion

    #region DELETE

    public void Delete(int id)
    {
        List<DO.OrderItem?> orderItems = (List<DO.OrderItem?>)(dal?.OrderItem.GetAll(null) ?? throw new BO.AccessToDataFailedException("cannot access the data layer"));
        foreach (DO.OrderItem? item in orderItems)
        {
            if (item?.ProductId == id)
                throw new BO.IntegrityDamageException("cannot delete the product without hurting data integrity. There are orders for the product");
        }
        try
        {
            dal.Product.Delete(id);

        }
        catch (DO.NotFoundException e)
        { 
            throw new BO.NotFoundException("product not found",e);
        }
    }

    #endregion

    #region GET
    public BO.ProductItem Get(int id, BO.Cart cart)
    {
        if (id < 0) throw new BO.NegativeNumberException("product ID property cannot be a negative number");
        try
        {
            DO.Product dalProduct = dal?.Product.Get(p => p?.ID == id) ?? throw new BO.AccessToDataFailedException("cannot access the data layer");
            BO.OrderItem? orderItem = cart.Items?.Where((x) => x?.ProductId == id).FirstOrDefault();//.Find((x) => x?.ProductId == id);
            int amount = orderItem?.Amount ?? 0;
            return Tools.Copy(dalProduct, new BO.ProductItem()
            {
                Category = (BO.Category?)dalProduct.Category,
                InStock = dalProduct.InStock - amount > 0 ? true : false,
                Amount = amount
            });
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException("product not found", e);
        }
    }

    public BO.Product AdminGet(int id)
    {
        if (id < 0) throw new BO.NegativeNumberException("product ID property cannot be a negative number");
        try
        {
            DO.Product dalProduct = dal?.Product.Get(p => p?.ID == id) ?? throw new BO.AccessToDataFailedException("cannot access the data layer");
            return Tools.Copy(dalProduct, new BO.Product()
            {
                Category = (BO.Category?)dalProduct.Category,
            });
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException("product not found", e);
        }
        
    }

    /// <summary>
    /// gets all the products from the data layer
    /// turns them to ProductForList type and return them in a list
    /// </summary>
    /// <returns>all the products in a list</returns>
    public IEnumerable<BO.ProductForList?> GetAll()
    {
        List<DO.Product?> dalProducts = (List<DO.Product?>)(dal?.Product.GetAll(null) ?? throw new BO.AccessToDataFailedException("cannot access the data layer"));
        List<BO.ProductForList?> blProducts = new List<BO.ProductForList?>();
        foreach (DO.Product? item in dalProducts)
        {
            //didnt find a way to do this without checking if item is null, because i want to make comletely different thi each time
            //since tha category is a different type, I had to copy it manually
            blProducts.Add(item != null ? Tools.Copy(item, new BO.ProductForList() { Category = (BO.Category?)item?.Category }) : null);
        }
        return blProducts;
    }


    /// <summary>
    /// gets all the products with the category wanted from the data layer
    /// turns them to ProductForList type and return them in a list
    /// </summary>
    /// <returns>all the products in a list</returns>
    public IEnumerable<BO.ProductForList?> GetByCategory(BO.Category category)
    {
        List<DO.Product?> dalProducts = (List<DO.Product?>)(dal?.Product.GetAll((product) => product?.Category == (DO.Category)category) ?? throw new BO.AccessToDataFailedException("cannot access the data layer"));
        List<BO.ProductForList?> blProducts = new List<BO.ProductForList?>();
        foreach (DO.Product? item in dalProducts)
        {
            blProducts.Add(item != null ? Tools.Copy(item, new BO.ProductForList() { Category = (BO.Category?)item?.Category }) : null);
        }
        return blProducts;
    }

    public IEnumerable<BO.ProductItem?> GetCatalog(BO.Cart cart)
    {
        IEnumerable<DO.Product?> products =   dal?.Product.GetAll(null) ?? throw new BO.AccessToDataFailedException("cannot access the data layer");
        List<BO.ProductItem?> productItems = new List<BO.ProductItem?>();
        foreach (DO.Product? product in products)
        {
            BO.OrderItem? orderItem = cart.Items?.Where((x) => x?.ProductId == product?.ID).FirstOrDefault();
            int amount = orderItem?.Amount ?? 0;
            productItems.Add(Tools.Copy(product, new BO.ProductItem()
            {
                Category = (BO.Category?)product?.Category,
                InStock = product?.InStock - amount > 0 ? true : false,
                Amount = amount
            }));
        }
        return productItems;
    }

    #endregion

    #region UPDATE

    public void Update(BO.Product item)
    {
        checkValid(item);
        try
        {
            if (dal == null) throw new BO.AccessToDataFailedException("cannot access the data layer");
            dal.Product.Update(Tools.Copy(item, new DO.Product() { Category =  (DO.Category?)item.Category  }));
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException("product not found", e);
        }
    }

  
    #endregion

    #region Helpers
    /// <summary>
    /// check validation of a BO.Product object, throw if there are unvalid values
    /// </summary>
    /// <param name="product">a product to validate</param>
    /// <exception cref="BO.NegativeNumberException">ID,Price or InStock are negative number</exception>
    /// <exception cref="BO.NullValueException">Name is null</exception>
    private void checkValid(BO.Product product)
    {
        if (product.ID < 0) throw new BO.NegativeNumberException("product ID property cannot be a negative number");
        if (product.Name == null || product.Name == "") throw new BO.NullValueException("product Name property cannot be null or an empty string");
        if(product.Price < 0) throw new BO.NegativeNumberException("product Price property cannot be a negative number");
        if (product.InStock < 0) throw new BO.NegativeNumberException("product InStock property cannot be a negative number");
    }

    

    #endregion
}
