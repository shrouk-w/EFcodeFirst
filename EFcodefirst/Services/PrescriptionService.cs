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

    public async Task<PrescriptionsForClientReplyDTO> GetPrescriptionsAsync(int idPatient, CancellationToken token)
    {
        var patient = await _dbContext.Patient.FindAsync(idPatient, token);
        if (patient == null)
            throw new NotFoundException("Patient not found");
        
        var pre = new PrescriptionsForClientReplyDTO()
        {
            IdPatient = idPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,
        };  
        List<PrescriptionDTO> prescriptions = new List<PrescriptionDTO>();

        var presriptions = await _dbContext.Prescription.Where(p => p.IdPatient == idPatient).ToListAsync(token);

        foreach (var pres in presriptions)
        {
            List<MedicamentDTO> medicaments = new List<MedicamentDTO>();
            var pres_medicament = await _dbContext.Prescription_Medicament.Where(m => m.IdPrescription == pres.IdPrescription).ToListAsync(token);
            foreach (var medi in pres_medicament)
            {
                var medicament = await _dbContext.Medicament.FindAsync(medi.IdMedicament, token);
                medicaments.Add(new MedicamentDTO()
                {
                    Description = medicament.Description,
                    Dose = medi.Dose,
                    Name = medicament.Name,
                    IdMedicament = medi.IdMedicament
                });
            }
            var doc = await _dbContext.Doctor.FindAsync(pres.IdDoctor, token);
            var presc = new PrescriptionDTO()
            {
                IdPrescription = pres.IdPrescription,
                DueDate = pres.DueDate,
                Date = pres.Date,
                Medicaments = medicaments,
                Doctor = new DoctorDTO()
                {
                    IdDoctor = doc.IdDoctor,
                    FirstName = doc.FirstName,
                }
            };
            prescriptions.Add(presc);
        }
        pre.Prescriptions = prescriptions;
        
        return pre;
    }
}