using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.EfModels;
public class ConsumptionObject
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public int OrganizationId { get; set; }
    public ICollection<DeliveryPoint> DeliveryPoints { get; set; }
    public ICollection<MeasuringPoint> MeasuringPoints { get; set; }
}
