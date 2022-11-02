using DO;

namespace Dal;

public class DalProduct
{
    #region Add
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
    public Product Get(int id)
    {
        for (int i = 0; i < DataSource.Config.ProductsIndex; i++)
        {
            if (DataSource.Products[i].ID == id) return DataSource.Products[i];
        }
        throw new Exception("Product not found");
    }

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
