using DO;
using DalApi;
using System.Diagnostics;
using System.Xml.Linq;

namespace Dal;

internal class DalProduct : IProduct
{
    #region Add
    /// <summary>
    /// this function is used when there is a new watch
    /// </summary>
    /// <param name="product">the new watch to add</param>
    /// <returns>ID of the added watch</returns>
    public int Add(Product product)
    {
       
        int id = DataSource.Random.Next(100000, 1000000);
        for (int i = 0; i < DataSource.Products.Count; i++)
        {
            if (DataSource.Products[i].ID == id)
            {
                id = DataSource.Random.Next(100000, 1000000);
                i = 0;
            }
        }
        product.ID = id;   
        DataSource.Products.Add(product);
        return id;
    }

    #endregion

    #region Get

    /// <summary>
    /// a function that returns the specific watch that was asked
    /// </summary>
    /// <param name="id">ID of watch to get</param>
    /// <returns>the watch that has the given ID</returns>
    public Product Get(int id)
    {
        for (int i = 0; i < DataSource.Products.Count; i++)
        {
            if (DataSource.Products[i].ID == id) return DataSource.Products[i];
        }
        throw new AlreadyExistsException("Product not found");
    }

    /// <summary>
    /// a function that returns all the watches
    /// </summary>
    /// <returns>an array of all watches</returns>
    public Product[] GetAll()
    {
        Product[] products = new Product[DataSource.Config.ProductsIndex];
        for (int i = 0; i < DataSource.Config.ProductsIndex; i++)
        {
            products[i] = DataSource.Products[i];
        }
        return products;
    }

    #endregion

    #region Update

    /// <summary>
    /// this function gets a watch to update it's details
    /// </summary>
    /// <param name="product">the watch to update</param>
    public void Update(Product product)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Products.Count; i++)
        {
            if (DataSource.Products[i].ID == product.ID)
            {
                DataSource.Products[i] = product;
                flag = true;
                break;
            }
        }
        if (!flag) throw new AlreadyExistsException("Product not found");
    }

    #endregion

    #region Delete

    /// <summary>
    /// this fuction delete's a watch by the given ID
    /// </summary>
    /// <param name="id">the ID of the watch to delete</param>
    public void Delete(int id)
    {
        bool flag = false;
        int i = 0;
        for (; i < DataSource.Products.Count; i++)
        {
            if (DataSource.Products[i].ID == id)
            {
                flag = true;
                break;
            }
        }
        if (!flag) throw new NotFoundException("Product not found");
        else
            DataSource.Products.RemoveAt(i);
    }

    #endregion
}
