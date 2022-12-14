using BlApi;
using Dal;

namespace BlImplementation;

internal class Product : IProduct
{
    private DalApi.IDal Dal = new DalList();

    #region ADD
    public void Add(BO.Product item)
    {
        checkValid(item);
        try
        {
            Dal.Product.Add(new DO.Product()
            {
                ID = item.ID,
                Name = item.Name,
                Price = item.Price,
                Category = (DO.Category?) item.Category ,
                InStock = item.InStock,
            });
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
        List<DO.OrderItem?> orderItems = (List<DO.OrderItem?>)Dal.OrderItem.GetAll(null);
        foreach (DO.OrderItem? item in orderItems)
        {
            if (item?.ProductId == id)
                throw new BO.IntegrityDamageException("cannot delete the product without hurting data integrity. There are orders for the product");
        }
        try
        {
            Dal.Product.Delete(id);
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
        BO.ProductItem product;
        try
        {
            DO.Product dalProduct = Dal.Product.Get(p => p?.ID == id);
            BO.OrderItem? orderItem = null;
            if (cart.Items != null)
                orderItem = cart.Items!.Find((x) => x?.ProductId == id);
            int amount = 0;
            if (orderItem != null)
                amount = orderItem.Amount;
            product = new BO.ProductItem()
            {
                ID = dalProduct.ID,
                Name = dalProduct.Name,
                Category = (BO.Category?) dalProduct.Category,
                Price = dalProduct.Price,
                InStock = dalProduct.InStock > 0 ? true : false,
                Amount = amount
            };
            return product;
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException("product not found", e);
        }
    }

    public BO.Product AdminGet(int id)
    {
        if (id < 0) throw new BO.NegativeNumberException("product ID property cannot be a negative number");
        BO.Product product;
        try
        {
            DO.Product dalProduct = Dal.Product.Get(p => p?.ID == id);
            product = new BO.Product()
            {
                ID = dalProduct.ID,
                Name = dalProduct.Name,
                Category = (BO.Category?)dalProduct.Category,
                Price = dalProduct.Price,
                InStock = dalProduct.InStock,
            };
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException("product not found", e);
        }
        return product;
    }

    /// <summary>
    /// gets all the products from the data layer
    /// turns them to ProductForList type and return them in a list
    /// </summary>
    /// <returns>all the products in a list</returns>
    public IEnumerable<BO.ProductForList?> GetAll()
    {
        List<DO.Product?> dalProducts = (List<DO.Product?>)Dal.Product.GetAll(null);
        List<BO.ProductForList?> blProducts = new List<BO.ProductForList?>();
        foreach (DO.Product? item in dalProducts)
        {
            
            if(item.HasValue)
                blProducts.Add(new BO.ProductForList()
                {
                    ID = item!.Value.ID,
                    Name = item!.Value.Name,
                    Price = item!.Value.Price,
                    Category = item!.Value.Category.HasValue ? (BO.Category)item!.Value.Category : null,
                });
        }
        return blProducts;
    }

    public IEnumerable<BO.ProductForList?> GetByCategory(BO.Category category)
    {
        List<DO.Product?> dalProducts = (List<DO.Product?>)Dal.Product.GetAll((product) => product?.Category == (DO.Category)category);
        List<BO.ProductForList?> blProducts = new List<BO.ProductForList?>();
        foreach (DO.Product? item in dalProducts)
        {
            if (item.HasValue)
                blProducts.Add(new BO.ProductForList()
                {
                    ID = item!.Value.ID,
                    Name = item!.Value.Name,
                    Price = item!.Value.Price,
                    Category = item!.Value.Category.HasValue ? (BO.Category)item!.Value.Category : null,
                });
        }
        return blProducts;
    }


    /// <summary>
    /// gets all the products with the category wanted from the data layer
    /// turns them to ProductForList type and return them in a list
    /// </summary>
    /// <returns>all the products in a list</returns>
    public IEnumerable<BO.ProductForList?> GetByCategory(string category)
    {
        List<DO.Product?> dalProducts = (List<DO.Product?>)Dal.Product.GetAll(null);
        List<BO.ProductForList?> blProducts = new List<BO.ProductForList?>();
        foreach (DO.Product? item in dalProducts)
        {
            if (item.HasValue && (item!.Value.Category.HasValue.ToString()==category))

                blProducts.Add(new BO.ProductForList()
                {
                    ID = item!.Value.ID,
                    Name = item!.Value.Name,
                    Price = item!.Value.Price,
                    Category = item!.Value.Category.HasValue ? (BO.Category)item!.Value.Category : null,
                });
        }
        return blProducts;
    }

    #endregion

    #region UPDATE

    public void Update(BO.Product item)
    {
        checkValid(item);
        try
        {
            Dal.Product.Update(new DO.Product()
            {
                ID = item.ID,
                Name = item.Name,
                Price = item.Price,
                Category = item.Category.HasValue ? (DO.Category)item.Category : null,
                InStock = item.InStock,
            });
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
