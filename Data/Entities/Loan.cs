namespace Lona.Data.Entities;

public class Loan
{

    public Loan()
    {
        Items = new HashSet<LoanPaymentList>();
    }
    public long Id { get; set; }
    public long ApplicantId { get; set; }
    public decimal Amount { get; set; }
    public int Term { get; set; }
    public string PaymentPlan { get; set; }
    public decimal InterestRate { get; set; }
    public decimal TermlyAmountPayable { get; set; }
    public decimal TotalAmountPayable { get; set; }
    public DateTime DateCreated { get; set; }
    public string ApprovalStatus { get; set; }
    public int? ApprovalStatusActionByStaffId { get; set; }
    //[DataType(DataType.Date)]
    public DateTime? ApprovalStatusDate { get; set; }

    public virtual Applicant ApplicantNavigation { get; set; }
    public virtual Staff StaffNavigation { get; set; }

    public virtual ICollection<LoanPaymentList> Items { get; set; }


}


