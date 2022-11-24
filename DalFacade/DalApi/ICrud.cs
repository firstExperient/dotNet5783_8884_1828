using DO;

namespace DalApi;

public interface ICrud<T>
{
    int Add(T value);
    T Get(int id);
    IEnumerable<T> GetAll();
    void Update(T value);
    void Delete(int id);
}
 