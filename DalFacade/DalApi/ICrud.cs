

using DO;

namespace DalApi;

internal interface ICrud<T>
{
    int Add(T value);
    T Get(int id);
    IEnumerable<T> GetAll();
    void Update(T value);
    void Delete(int id);
}
 