using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.EfModels;
public class DeliveryPoint
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public double MaxPower { get; set; }
    [Required]
    public int ConsumptionObjectId { get; set; }
    public CalculationMeter CalculationMeter { get; set; }
}
