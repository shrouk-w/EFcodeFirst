using EFcodefirst.Model.DTOs;

namespace EFcodefirst.Services;

public interface IPrescriptionService
{
    public Task InsertNewPresriptionsAsync(PrescrptionInsertRequestDTO prescription, CancellationToken token);
    public Task<PrescriptionsForClientReplyDTO> GetPrescriptionsAsync(int idPatient, CancellationToken token);
}