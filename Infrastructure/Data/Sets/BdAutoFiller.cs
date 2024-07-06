using Infrastructure.DAL;
using Infrastructure.Data.EfModels;

namespace Infrastructure.Data.Sets;
public class BdAutoFiller
{
    private readonly IRepository _repository;
    private bool isFilledBd;
    public bool IsFilledBd { get { return isFilledBd; } }
    public BdAutoFiller(IRepository repository) { _repository = repository; }
    public async Task FillAsync()
    {
        await FillOrgsAsync();
        await FillConsumptionObjectsAsync();
        await FillDeliveryPointsAsync();
        await FillMeasuringTypesAsync();
        await FillMeasuringPointAsync();
        await FillCalculationMeterPlugInAsync();

        isFilledBd = true;
    }
    private async Task FillOrgsAsync()
    {
        foreach (var org in DataSet.Organizations) { await _repository.AddObjectAsync<Organization>(org); }
    }
    private async Task FillConsumptionObjectsAsync()
    {
        foreach (var consObj in DataSet.ConsumptionObjects) { await _repository.AddObjectAsync<ConsumptionObject>(consObj); }
    }
    private async Task FillDeliveryPointsAsync()
    {
        foreach (var deliveryPoint in DataSet.DeliveryPoints) { await _repository.AddObjectAsync<DeliveryPoint>(deliveryPoint); }
    }
    private async Task FillMeasuringTypesAsync()
    {
        foreach (var type in DataSet.ElectricMeterTypes) { await _repository.AddObjectAsync<ElectricMeterType>(type); }
        foreach (var type in DataSet.CurrentTransformerTypes) { await _repository.AddObjectAsync<CurrentTransformerType>(type); }
        foreach (var type in DataSet.VoltageTransformerTypes) { await _repository.AddObjectAsync<VoltageTransformerType>(type); }
    }
    private async Task FillMeasuringPointAsync()
    {
        foreach (var point in DataSet.MeasuringPoints) { await _repository.AddObjectAsync<MeasuringPoint>(point); }
    }
    private async Task FillCalculationMeterPlugInAsync()
    {
        foreach (var plugIn in DataSet.CalculationMeterPlugIns) { await _repository.AddObjectAsync<CalculationMeterPlugIn>(plugIn); }
    }
}
