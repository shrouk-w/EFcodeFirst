using EFcodefirst.DAL;
using EFcodefirst.Exceptions;
using EFcodefirst.Model;
using EFcodefirst.Model.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EFcodefirst.Services;

public class PrescriptionService : IPrescriptionService
{
    
    private readonly PrescriptionDbContext _dbContext;
    
    public PrescriptionService(PrescriptionDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task InsertNewPresriptionsAsync(PrescrptionInsertRequestDTO prescription, CancellationToken token)
    {
        if(prescription == null)
            throw new BadRequestException("Invalid prescription");
        if(prescription.Medicaments.Count()>10)
            throw new ConflictException("one prescription can contain up to 10 medicaments");
        if (prescription.DueDate < prescription.Date)
            throw new ConflictException("prescription is outdated");
        
        foreach (var medicament in prescription.Medicaments)
        {
            var medi = await _dbContext.Medicament.FindAsync(medicament.IdMedicament, token);
            if(medi == null)
                throw new NotFoundException("Medicament not found");
        }
        
        var patient = await _dbContext.Patient.FindAsync(prescription.Patient.IdPatient, token);
        if (patient == null)
        {
            var pat = new Patient()
            {
                IdPatient = prescription.Patient.IdPatient,
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                BirthDate = prescription.Patient.BirthDate,
            };
            await _dbContext.Patient.AddAsync(pat, token);
        }

        var nextid = await _dbContext.Prescription
            .Select(p => (int?)p.IdPrescription)
            .MaxAsync(token);
        if (nextid == null)
            nextid = 0;
        nextid++;

        var per = new Prescription()
        {
            IdPrescription = (int)nextid,
            DueDate = prescription.DueDate,
            Date = prescription.Date,
            IdDoctor = prescription.Doctor.IdDoctor,
            IdPatient = prescription.Patient.IdPatient
        };
        await _dbContext.Prescription.AddAsync(per, token);

        foreach (var medicament in prescription.Medicaments)
        {
            var per_med = new Prescription_Medicament()
            {
                IdMedicament = medicament.IdMedicament,
                IdPrescription = (int)nextid,
                Dose = medicament.Dose,
                Details = ""
            };
            await _dbContext.Prescription_Medicament.AddAsync(per_med, token);
        }

        await _dbContext.SaveChangesAsync();

    }
}