using FluentValidation;
using Lona.Data.Constants;
using Lona.Data.DTOs;

namespace Lona.Data.Validations;


public class LoanValidator : AbstractValidator<NewLoanDto>
{
    public LoanValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(MigrationConstants.NAME_MAXLENGTH);

        RuleFor(x => x.Address).NotEmpty().MinimumLength(1).MaximumLength(MigrationConstants.ADDRESS_MAXLENGTH).WithMessage("Invalid {PropertyName}");

        RuleFor(x => x.Email).EmailAddress().MaximumLength(MigrationConstants.EMAIL_MAXLENGTH).WithMessage("Invalid {PropertyName}");

        RuleFor(x => x.DateOfBirth).Must(BeAValidAge).WithMessage("Invalid {PropertyName}. You must be 18 years and above.");

        RuleFor(x => x.Amount).NotNull().GreaterThanOrEqualTo(MigrationConstants.MINIMUM_LOAN_AMOUNT).LessThanOrEqualTo(MigrationConstants.MAXIMUM_LOAN_AMOUNT).WithMessage($"Amount has to be between {MigrationConstants.MAXIMUM_LOAN_AMOUNT} and {MigrationConstants.MAXIMUM_LOAN_AMOUNT} .");

        RuleFor(x => x.HomeOwner).Must(x => x == false || x == true);


        static bool BeAValidAge(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Year;

            if (dobYear <= currentYear && (currentYear - dobYear) >= 18)
            {
                return true;
            }

            return false;
        }
    }
}



