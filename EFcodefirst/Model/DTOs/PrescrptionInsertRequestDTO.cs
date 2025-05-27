using System.ComponentModel.DataAnnotations;

namespace EFcodefirst.Model.DTOs;

public class PrescrptionInsertRequestDTO
{
    [Required]
    public PatientDTO Patient { get; set; }
    [Required]
    public IEnumerable<MedicamentDTO> Medicaments { get; set; }
    [Required]
    public DoctorDTO Doctor { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
}