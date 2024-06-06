namespace lab9.Exceptions;

public abstract class NotFoundException(string message) : Exception(message);

public class HospitalPrescriptionException(DateTime prescription) : NotFoundException($" DueDate : {prescription} must be greater than or equal to Date");
public class HospitalMedicamentException(int medicament) : NotFoundException($"Medicament : {medicament} does not exist");

public class HospitalDoctorException(int doctodId) : NotFoundException($"Doctor {doctodId} : does not  exsists  ");

public class HospitalMedicamentDoesNotExsits(int medicamnentId): NotFoundException($"Medicament : {medicamnentId} does not exsits");