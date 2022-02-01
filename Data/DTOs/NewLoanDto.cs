namespace Lona.Data.DTOs;

public record NewLoanDto
{
    public int Id { get; set; }
    public long ApplicantId { get; set; } 
    public string Name { get; set; } = string.Empty;
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
    public string[] Paymentlist { get; set; }
}


