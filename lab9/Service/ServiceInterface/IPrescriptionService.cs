using lab9.DTO;
using lab9.Models;

namespace lab9.Service.ServiceInterface;

public interface IPrescriptionService
{
    Task AddPrescription(PrescriptionDTO prescription);
    Task<Patient> GetPatientDetails(int patientId);


}
