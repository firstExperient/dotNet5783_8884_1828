using DO;

namespace Dal;

public class DalProduct
{
    #region Add
    /// <summary>
    /// this function is used when there is a new watch
    /// </summary>
    /// <param name="product">the new watch to add</param>
    /// <returns>ID of the added watch</returns>
    public int Add(Product product)
    {
        if (DataSource.Config.ProductsIndex == 49) throw new Exception("Erorr! Products array is full");
        product.ID = DataSource.Random.Next(100000, 1000000);
        //add check to see if exist
        DataSource.Products[DataSource.Config.ProductsIndex] = product;
        DataSource.Config.ProductsIndex++;
        return product.ID;
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
        for (int i = 0; i < DataSource.Config.ProductsIndex; i++)
        {
            if (DataSource.Products[i].ID == id) return DataSource.Products[i];
        }
        throw new Exception("Product not found");
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
        for (int i = 0; i < DataSource.Config.ProductsIndex; i++)
        {
            if (DataSource.Products[i].ID == product.ID)
            {
                DataSource.Products[i] = product;
                flag = true;
                break;
            }
        }
        if (!flag) throw new Exception("Product not found");
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
        for (int i = 0; i < DataSource.Config.ProductsIndex; i++)
        {
            if (DataSource.Products[i].ID == id)
            {
                flag = true;
                for (; i < DataSource.Config.ProductsIndex - 1; i++)
                {
                    DataSource.Products[i] = DataSource.Products[i + 1];
                }
                DataSource.Config.ProductsIndex--;
                break;
            }
        }
        if (!flag) throw new Exception("Product not found");
    }

    #endregion

}
