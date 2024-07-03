using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.EfModels;
public class ElectricMeter
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string InventoryNumber { get; set; }
    [Required]
    public int Type { get; set; }
    [Required]
    public DateOnly Verificated { get; set; }
    public MeasuringPoint MeasuringPoint { get; set; }
}
