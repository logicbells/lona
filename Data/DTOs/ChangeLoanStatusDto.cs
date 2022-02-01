namespace Lona.Data.DTOs;

public record ChangeLoanStatusDto
{
    public long Id { get; set; }
    public string ApprovalStatus { get; set; }
    public int? StaffId { get; set; }
}


