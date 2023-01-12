using DO;
namespace DalApi;

public interface ICrud<T> where T : struct
{
    int Add(T value);
    T Get(Func<T?,bool> match);
    IEnumerable<T?> GetAll(Func<T?,bool>? match);
    void Update(T value);
    void Delete(int id);
}