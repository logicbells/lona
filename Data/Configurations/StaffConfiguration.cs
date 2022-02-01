using Lona.Data.Constants;
using Lona.Data.Entities;
using Lona.Data.Seed;

namespace Lona.Data.Configurations;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> entity)
    {
        entity.Property(e => e.Name).HasMaxLength(MigrationConstants.NAME_MAXLENGTH).IsUnicode(false);
        entity.HasData(SeedHelper.SeedData<Staff>());
    }
}

