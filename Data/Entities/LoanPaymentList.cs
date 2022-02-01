namespace Lona.Data.Entities;

public class LoanPaymentList
{
    public long Id { get; set; }
    public long LoanId { get; set; }
    public long ApplicantId { get; set; }
    public DateTime PaymentDueDate { get; set; }
    public decimal TermlyAmountPayable { get; set; }
    public bool IsPaid { get; set; }
    

    public virtual Loan Loan { get; set; }
    public virtual Applicant ApplicantNavigation { get; set; }


  

}


