using System.ComponentModel.DataAnnotations;

namespace EFcodefirst.Model;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    
    public ICollection<Prescription_Medicament> Prescription_Medicament { get; set; }
}