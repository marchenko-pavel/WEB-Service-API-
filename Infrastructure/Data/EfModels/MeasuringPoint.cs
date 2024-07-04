using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.EfModels;
public class MeasuringPoint
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int ConsumptionObjectId { get; set; }
    public ElectricMeter ElectricMeter { get; set; }
    public CurrentTransformer CurrentTransformer { get; set; }
    public VoltageTransformer VoltageTransformer { get; set; }
    public List<CalculationMeter> CalculationMeters { get; set; }
}
