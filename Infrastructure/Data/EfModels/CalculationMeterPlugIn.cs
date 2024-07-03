using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.EfModels;
public class CalculationMeterPlugIn
{
    public int CalculationMeterId { get; set; }
    public int MeasuringPointId { get; set; }
    [Required]
    public DateTime PlugedIn { get; set; }
    public DateTime? PlugedOut { get; set; }
}
