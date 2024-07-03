﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.EfModels;
public class VoltageTransformer
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string InventoryNumber { get; set; }
    [Required]
    public DateOnly Verificated { get; set; }
    [Required]
    public double Kt { get; set; }
    [Required]
    public int TypeId { get; set; }
    public VoltageTransformerType Type { get; set; }
    public MeasuringPoint MeasuringPoint { get; set; }
}
