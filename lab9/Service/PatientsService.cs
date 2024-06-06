using lab9.Context;
using lab9.DTO;


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace lab9.Services
{
    public class PatientsService
    {
        private readonly AppDbContext _context;

        public PatientsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PatientDTO> GetPatientData(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Doctor)
                .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.PrescriptionMedicaments)
                .ThenInclude(pm => pm.Medicament)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
                return null;

            var patientDto = new PatientDTO
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Prescriptions = patient.Prescriptions.OrderBy(pr => pr.DueDate).Select(pr => new PrescriptionDTO
                {
                    Id = pr.Id,
                    Date = pr.Date,
                    DueDate = pr.DueDate,
                    Medicaments = pr.PrescriptionMedicaments.Select(pm => new PrescriptionMedicamentDTO
                    {
                        MedicamentId = pm.MedicamentId,
                        Dose = pm.Dose,
                        Details = pm.Details
                    }).ToList(),
                    Doctor = new DoctorDTO
                    {
                        Id = pr.Doctor.Id,
                        FirstName = pr.Doctor.FirstName,
                        LastName = pr.Doctor.LastName
                    }
                }).ToList()
            };

            return patientDto;
        }
    }
}
