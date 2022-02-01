using Lona.Data.Constants;
using Lona.Data.Entities;
using Lona.Data.Seed;

namespace Lona.Data.Configurations;

public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> entity)
    {
        entity.Property(e => e.Name).IsRequired().HasMaxLength(MigrationConstants.NAME_MAXLENGTH).IsUnicode(false);
        entity.Property(e => e.Address).IsRequired().HasMaxLength(MigrationConstants.ADDRESS_MAXLENGTH).IsUnicode(false);
        entity.Property(e => e.Email).IsRequired().HasMaxLength(MigrationConstants.EMAIL_MAXLENGTH).IsUnicode(false);
        entity.Property(e => e.DateOfBirth).IsRequired().HasColumnType("date");

        entity.HasData(SeedHelper.SeedData<Applicant>());

    }
}

