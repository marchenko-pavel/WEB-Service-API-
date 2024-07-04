namespace Infrastructure.DAL;
public interface IRepository
{
    public Task<bool> AddObjectAsync<T>(T obj);
}
