namespace lab9.DTO;

public class PatientDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<PrescriptionDTO> Prescriptions { get; set; }
}