namespace Lona.Data.Entities;

public class Applicant
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    //[DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool HomeOwner { get; set; }
}


