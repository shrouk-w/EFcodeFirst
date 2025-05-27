using System.ComponentModel.DataAnnotations;

namespace EFcodefirst.Model.DTOs;

public class PatientDTO
{
    [Required]
    public int IdPatient { get; set; }
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
}