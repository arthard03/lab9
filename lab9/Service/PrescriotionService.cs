using lab9.Exceptions;
using lab9.Models;
using lab9.Repository;
using lab9.Service.ServiceInterface;
using lab9.DTO;

namespace lab9.Service
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Medicament> _medicamentRepository;
        private readonly IRepository<Prescription> _prescriptionRepository;


        public PrescriptionService(
            IRepository<Patient> patientRepository,
            IRepository<Doctor> doctorRepository,
            IRepository<Medicament> medicamentRepository,
            IRepository<Prescription> prescriptionRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _medicamentRepository = medicamentRepository;
            _prescriptionRepository = prescriptionRepository;
        }


        public async Task AddPrescription(PrescriptionDTO prescriptionDTO)
        {
            if (prescriptionDTO.DueDate < prescriptionDTO.Date)
            {
                throw new HospitalPrescriptionException(prescriptionDTO.DueDate);
            }

            if (prescriptionDTO.PrescriptionMedicaments.Count > 10)
            {
                throw new HospitalDosageException(prescriptionDTO.PrescriptionMedicaments.Count);
            }

            var prescription = new Prescription
            {
                Date = prescriptionDTO.Date,
                DueDate = prescriptionDTO.DueDate,
                Patient = new Patient
                {
                    FirstName = prescriptionDTO.Patient.FirstName,
                    LastName = prescriptionDTO.Patient.LastName,
                    BirthDate = prescriptionDTO.Patient.BirthDate
                },
                Doctor = new Doctor
                {
                    FirstName = prescriptionDTO.Doctor.FirstName,
                    LastName = prescriptionDTO.Doctor.LastName,
                    Specialization = prescriptionDTO.Doctor.Specialization
                },
                PrescriptionMedicaments = prescriptionDTO.PrescriptionMedicaments.Select(pm => new PrescriptionMedicament
                {
                    MedicamentId = pm.MedicamentId,
                    Dose = pm.Dose,
                    Details = pm.Details
                }).ToList()
            };

            var existingPatient = await _patientRepository.GetById(prescription.Patient.Id);
            if (existingPatient == null)
            {
                await _patientRepository.Add(prescription.Patient);
            }

            foreach (var prescriptionMedicament in prescription.PrescriptionMedicaments)
            {
                var medicament = await _medicamentRepository.GetById(prescriptionMedicament.MedicamentId);
                if (medicament == null)
                {
                    throw new HospitalMedicamentException(prescriptionMedicament.MedicamentId);
                }
            }

            await _prescriptionRepository.Add(prescription);
        }

     

        public async Task<Patient> GetPatientDetails(int patientId)
        {
            var patient = await _patientRepository.GetById(patientId);
            if (patient == null)
            {
                throw new HospitalPatientException(patientId);
            }
            return patient;
        }
    }
}
