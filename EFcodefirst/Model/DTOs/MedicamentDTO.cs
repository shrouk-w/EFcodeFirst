using System.ComponentModel.DataAnnotations;

namespace EFcodefirst.Model.DTOs;

public class MedicamentDTO
{
    [Required]
    public int IdMedicament { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public int? Dose { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
}