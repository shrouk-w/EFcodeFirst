using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFcodefirst.Model;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
    public int IdPatient { get; set; }
    
    [ForeignKey("IdDoctor")]
    public Doctor Doctor { get; set; }
    
    [ForeignKey("IdPatient")]
    public Patient Patient { get; set; }
    
    public ICollection<Prescription_Medicament> Prescription_Medicament { get; set; }
}