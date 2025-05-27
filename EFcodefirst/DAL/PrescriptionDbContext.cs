using EFcodefirst.Model;
using Microsoft.EntityFrameworkCore;

namespace EFcodefirst.DAL;

public class PrescriptionDbContext : DbContext
{
    public DbSet<Medicament> Medicament { get; set; }
    public DbSet<Prescription_Medicament> Prescription_Medicament { get; set; }
    public DbSet<Prescription> Prescription { get; set; }
    public DbSet<Patient> Patient { get; set; }
    public DbSet<Doctor> Doctor { get; set; }

    protected PrescriptionDbContext()
    {
    }

    public PrescriptionDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prescription_Medicament>()
            .HasKey(p => new { p.IdMedicament, p.IdPrescription });
        
    }
}