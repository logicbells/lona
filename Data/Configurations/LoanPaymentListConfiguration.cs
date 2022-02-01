using Lona.Data.Constants;
using Lona.Data.Entities;
using Lona.Data.Seed;

namespace Lona.Data.Configurations;

public class LoanPaymentListConfiguration : IEntityTypeConfiguration<LoanPaymentList>
{
    public void Configure(EntityTypeBuilder<LoanPaymentList> entity)
    {
        
        entity.Property(b => b.TermlyAmountPayable).IsRequired().HasPrecision(18, 2);
        entity.Property(e => e.PaymentDueDate).HasColumnType("date").IsRequired();
        entity.HasOne(d => d.ApplicantNavigation).WithMany().HasForeignKey(d => d.ApplicantId).OnDelete(DeleteBehavior.Restrict);

        entity.HasData(SeedHelper.SeedData<LoanPaymentList>());
    }

}

