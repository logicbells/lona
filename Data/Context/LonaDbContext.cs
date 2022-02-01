using System.Reflection;
using Lona.Data.Entities;

namespace Lona.Data.Context
{
    public class LonaDbContext : DbContext
    {

        public LonaDbContext(DbContextOptions<LonaDbContext> options)
             : base(options)
        {
        }

        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Staff> Staffers { get; set; }
        public DbSet<LoanPaymentList> LoanPaymentList { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            //Disable change tracking
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

