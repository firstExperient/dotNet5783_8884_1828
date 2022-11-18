
using BO;

namespace BlApi;

public interface IProduct
{
    public IEnumerable<ProductForList> GetAll();
    public Product AdminGet(int id);
    public ProductItem Get(int id,Cart cart);
    public void Add(Product item);
    public void Update(Product item);
    public void Delete(int id);
}
