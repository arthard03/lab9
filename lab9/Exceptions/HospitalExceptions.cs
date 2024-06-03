namespace lab9.Exceptions;

public abstract class NotFoundException(string message) : Exception(message);

public class HospitalPrescriptionException(DateTime prescription) : NotFoundException($" DueDate : {prescription} must be greater than or equal to Date");
public class HospitalMedicamentException(int medicament) : NotFoundException($"Medicament : {medicament} does not exist");
public class HospitalPatientException(int patientId) : NotFoundException($"Patient with ID : {patientId} not found");

public class HospitalDosageException(int count) : NotFoundException($"Exceeded maximum of 10 medications per patient:  {count} ");
