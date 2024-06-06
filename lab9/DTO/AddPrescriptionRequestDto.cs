namespace lab9.DTO;

public class AddPrescriptionRequestDto
{
    public int? PatientId { get; set; }
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }
    public int DoctorId { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<PrescriptionMedicamentDTO> Medicaments { get; set; }
}