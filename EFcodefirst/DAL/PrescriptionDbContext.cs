using EFcodefirst.Model;
using Microsoft.EntityFrameworkCore;

namespace EFcodefirst.DAL;

public class PrescriptionDbContext : DbContext
{
    public DbSet<Medicament> Medicaments { get; set; }

    protected PrescriptionDbContext()
    {
    }

    public PrescriptionDbContext(DbContextOptions options) : base(options)
    {
    }
}