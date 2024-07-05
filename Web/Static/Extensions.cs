using Infrastructure.Data.EfModels;
using Web.Models;

namespace Web.Static;
public static class Extensions
{
    public static MeasuringPoint GetMeasuringPoint(this MeasuringPointModel model)
    {

        var measuringPoint = new MeasuringPoint() { Name = model.Name, ConsumptionObjectId = model.ConsumptionObjectId };
        measuringPoint.ElectricMeter = GetElectricMeter(model);
        measuringPoint.CurrentTransformer = GetCurrentTransformer(model);
        measuringPoint.VoltageTransformer = GetVoltageTransformer(model);
        return measuringPoint;
    }
    private static ElectricMeter GetElectricMeter(MeasuringPointModel model)
    {
        return new ElectricMeter()
        {
            InventoryNumber = model.ElectricMeter.InventoryNumber,
            TypeId = model.ElectricMeter.TypeId,
            Verificated = DateOnly.FromDateTime(model.ElectricMeter.Verificated)
        };
    }
    private static CurrentTransformer GetCurrentTransformer(MeasuringPointModel model)
    {
        return new CurrentTransformer()
        {
            InventoryNumber = model.CurrentTransformer.InventoryNumber,
            TypeId = model.CurrentTransformer.TypeId,
            Verificated = DateOnly.FromDateTime(model.CurrentTransformer.Verificated),
            Kt = model.CurrentTransformer.Kt
        };
    }
    private static VoltageTransformer GetVoltageTransformer(MeasuringPointModel model)
    {
        return new VoltageTransformer()
        {
            InventoryNumber = model.VoltageTransformer.InventoryNumber,
            TypeId = model.VoltageTransformer.TypeId,
            Verificated = DateOnly.FromDateTime(model.VoltageTransformer.Verificated),
            Kt = model.VoltageTransformer.Kt
        };
    }
}
