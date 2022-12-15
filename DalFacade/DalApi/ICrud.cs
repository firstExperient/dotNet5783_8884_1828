using DO;
namespace DalApi;

public interface ICrud<T> where T : struct
{
    int Add(T value);
    T Get(Predicate<T?> match);
    IEnumerable<T?> GetAll(Predicate<T?>? match);
    void Update(T value);
    void Delete(int id);
}