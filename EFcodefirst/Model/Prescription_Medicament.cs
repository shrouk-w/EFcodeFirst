using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFcodefirst.Model;

public class Prescription_Medicament
{
    
    public int IdMedicament { get; set; }
    public int IdPrescription { get; set; }
    public int? Dose { get; set; }
    [Required]
    [MaxLength(100)]
    public string Details { get; set; }
    
    [ForeignKey("IdMedicament")]
    public Medicament Medicament { get; set; }
    
    [ForeignKey("IdPrescription")]
    public Prescription Prescription { get; set; }
    
}