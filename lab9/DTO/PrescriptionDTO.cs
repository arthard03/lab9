namespace lab9.DTO;

public class PrescriptionDTO
{
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public PatientDTO Patient { get; set; }
    public DoctorDTO Doctor { get; set; }
    public List<PrescriptionMedicamentDTO> PrescriptionMedicaments { get; set; }
}

