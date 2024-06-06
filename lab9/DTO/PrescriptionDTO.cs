namespace lab9.DTO;

public class PrescriptionDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<PrescriptionMedicamentDTO> Medicaments { get; set; }
    public DoctorDTO Doctor { get; set; }
}