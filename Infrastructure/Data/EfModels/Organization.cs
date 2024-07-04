using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.EfModels;
public class Organization
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
    public int? ParentOrgId { get; set; }
    [ForeignKey("ParentOrgId")]
    public ICollection<Organization> Organizations { get; set; }
    public ICollection<ConsumptionObject> ConsumptionObjects { get; set; }
}
