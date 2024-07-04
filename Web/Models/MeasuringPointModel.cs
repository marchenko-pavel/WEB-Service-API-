namespace Web.Models;
public class MeasuringPointModel
{
    public string Name { get; set; }
    public int ConsumptionObjectId { get; set; }
    public MeasureItem ElectricMeter { get; set; }
    public MeasureItemExt CurrentTransformer { get; set; }
    public MeasureItemExt VoltageTransformer { get; set; }
}
