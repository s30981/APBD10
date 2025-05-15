using APBD10.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD10.Data;

public class PrescriptionDbContext : DbContext
{
    protected PrescriptionDbContext() {}
    public PrescriptionDbContext(DbContextOptions options) : base(options) {}

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
}
