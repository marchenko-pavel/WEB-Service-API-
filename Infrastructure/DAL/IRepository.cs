using Infrastructure.Data.EfModels;

namespace Infrastructure.DAL;
public interface IRepository
{
    public Task<bool> AddObjectAsync<T>(T obj);
    public Task<MeasuringPoint?> GetMeasuringPointAsync(string name);
    public Task<List<CalculationMeterPlugIn>> GetCalculationMeterPlugInsAsync();
}
