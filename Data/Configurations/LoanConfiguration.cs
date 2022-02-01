using Lona.Data.Constants;
using Lona.Data.Entities;
using Lona.Data.Seed;

namespace Lona.Data.Configurations;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> entity)
    {
        entity.Property(e => e.Amount).IsRequired();
        entity.Property(e => e.PaymentPlan).IsRequired().HasMaxLength(16).IsUnicode(false);
        entity.Property(e => e.DateCreated).IsRequired();
        entity.Property(e => e.ApprovalStatus).HasMaxLength(MigrationConstants.STATUS_MAXLENGTH).IsUnicode(false);
        entity.Property(b => b.Amount).IsRequired().HasPrecision(18, 2);
        entity.Property(b => b.InterestRate).IsRequired().HasPrecision(18, 2);
        entity.Property(b => b.TermlyAmountPayable).IsRequired().HasPrecision(18, 2);
        entity.Property(b => b.TotalAmountPayable).IsRequired().HasPrecision(18, 2);
        entity.HasOne(d => d.ApplicantNavigation).WithMany().HasForeignKey(d => d.ApplicantId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(d => d.StaffNavigation).WithMany().HasForeignKey(d => d.ApprovalStatusActionByStaffId);

        entity.HasData(SeedHelper.SeedData<Loan>());
    }

}

