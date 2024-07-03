using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.EfModels;
public class CalculationMeter
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DeliveryPoint DeliveryPoint { get; set; }
    public List<MeasuringPoint> MeasuringPoints { get; set; }
}
