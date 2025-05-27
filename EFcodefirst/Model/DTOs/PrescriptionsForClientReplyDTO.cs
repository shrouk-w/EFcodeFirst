namespace EFcodefirst.Model.DTOs;

public class PrescriptionsForClientReplyDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public IEnumerable<PrescriptionDTO> Prescriptions { get; set; }
}