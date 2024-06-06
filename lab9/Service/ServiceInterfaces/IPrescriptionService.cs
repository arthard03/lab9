using lab9.DTO;

namespace lab9.Services.ServiceInterfaces;

public interface IPrescriptionService
{
    Task<int> AddPrescriptionAsync(AddPrescriptionRequestDto request);
}
