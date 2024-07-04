using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DAL;
public class Repository : IRepository
{
    private readonly IDbContextFactory<AppDbContext> _factory;
    private readonly ILogger<Repository> _logger;
    public Repository(IDbContextFactory<AppDbContext> factory, ILogger<Repository> logger)
    {
        _factory = factory;
        _logger = logger;
    }
    public async Task<bool> AddObjectAsync<T>(T obj)
    {
        if (obj is not null)
        {
            using (var _context = _factory.CreateDbContext())
            {
                try
                {
                    await _context.AddAsync(obj);
                    await SaveContextAsync(_context);
                    return true;
                }
                catch (Exception ex)
                {
                    _context.ChangeTracker.Clear();
                    _logger.LogError($"При добавлении в БД объекта класса '{obj.ToString()}' произошла ошибка - {ex.Message}");
                    return false;
                }
            }
        }
        else return false;
    }
    private async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
    {
        using (var _context = _factory.CreateDbContext())
        {
            try
            {
                _context.Remove<TEntity>(entity);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"При удалении объекта из БД произошла ошибка: {ex}");
            }
        }
    }
    private static async Task SaveContextAsync(AppDbContext context)
    {
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException exUpdate)
        {
            throw exUpdate;
        }
        catch (Exception ex)
        {
            throw new Exception($"При сохранении контекста в базе данных произошла ошибка: {ex}");
        }
    }
}
