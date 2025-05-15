namespace APBD10.DTOs;


public class PatientDtos
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<PrescriptionDtos> Prescriptions { get; set; }
}



public class PrescriptionDtos
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentDtos> Medicaments { get; set; }
    public DoctorDtos Doctor { get; set; }
}


public class MedicamentDtos
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Dose { get; set; }
}


public class DoctorDtos
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
