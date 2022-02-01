using Lona.Data.Entities;

namespace Lona.Data.DTOs;

public record LoanDto
{
    public long Id { get; set; }
    public long ApplicantId { get; set; }
    public string ApplicantName { get; set; }
    public string Address { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool HomeOwner { get; set; }
    public int Term { get; set; }
    public string PaymentPlan { get; set; } = string.Empty;
    public decimal InterestRate { get; set; }
    public decimal Amount { get; set; }
    public decimal TermlyAmountPayable { get; set; }
    public decimal TotalAmountPayable { get; set; }
    public DateTime DateCreated { get; set; }
    public string ApprovalStatus { get; set; }
    public int? ApprovalStatusActionByStaffId { get; set; }
    public string ApprovalStatusActionByStaffName { get; set; }
    public DateTime? ApprovalStatusDate { get; set; }
    public LoanPaymentList[] Items { get; set; }
}


