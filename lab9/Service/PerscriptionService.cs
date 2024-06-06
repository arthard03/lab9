using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using lab9.Context;
using lab9.DTO;
using lab9.Exceptions;
using lab9.Models;
using lab9.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace lab9.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly AppDbContext _context;

        public PrescriptionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddPrescriptionAsync(AddPrescriptionRequestDto request)
        {
            var patient = request.PatientId.HasValue
                ? await _context.Patients.FindAsync(request.PatientId.Value)
                : new Patient { FirstName = request.PatientFirstName, LastName = request.PatientLastName };

            if (patient == null)
            {
                patient = new Patient { FirstName = request.PatientFirstName, LastName = request.PatientLastName };
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }

            var doctor = await _context.Doctors.FindAsync(request.DoctorId);
            if (doctor == null)
                throw new HospitalDoctorException(request.DoctorId);

            if (request.Medicaments.Count > 10)
                throw new HospitalMedicamentException(request.Medicaments.Count);

            var medicamentIds = request.Medicaments.Select(m => m.MedicamentId).ToList();
            var medicaments = await _context.Medicaments
                .Where(m => medicamentIds.Contains(m.Id))
                .ToListAsync();

            if (medicaments.Count != request.Medicaments.Count)
                throw new HospitalMedicamentDoesNotExsits(medicaments.Count);

            if (request.DueDate < request.Date)
                throw new HospitalPrescriptionException(request.DueDate);
            

            var prescription = new Prescription
            {
                Date = request.Date,
                DueDate = request.DueDate,
                Patient = patient,
                Doctor = doctor,
                PrescriptionMedicaments = request.Medicaments.Select(m => new PrescriptionMedicament
                {
                    MedicamentId = m.MedicamentId,
                    Dose = m.Dose,
                    Details = m.Details
                }).ToList()
            };

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            return prescription.Id;
        }
    }
}