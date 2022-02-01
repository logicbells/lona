using FluentValidation.AspNetCore;
using Lona.Data.Context;
using Lona.Data.DTOs;
using Lona.Data.Entities;
using Lona.Data.Seed;
using Lona.Interfaces;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddFluentValidation(s =>
{
    s.DisableDataAnnotationsValidation = true;
});

var provider = builder.Configuration.GetSection("Provider").Value;
builder.Services.AddDbContext<LonaDbContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseSqlServer(builder.Configuration.GetConnectionString(provider));
            });
              
//builder.Services.AddScoped<ILoanService, LoanService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<LonaDbContext>();
    context.Database.EnsureCreated();
    SeedDataInitiliazer.Initialize(services);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStatusCodePages(async statusCodeContext =>
{
    // using static System.Net.Mime.MediaTypeNames;
    statusCodeContext.HttpContext.Response.ContentType = Text.Plain;

    await statusCodeContext.HttpContext.Response.WriteAsync(
        $"Status Code Page: {statusCodeContext.HttpContext.Response.StatusCode}");
});

app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.MapRazorPages();


app.MapPost("/api/newloan", async (NewLoanDto model, LonaDbContext _dbContext) =>
{
    var existingApplicant = await _dbContext.Applicants.Where(x => x.Id == model.ApplicantId).FirstOrDefaultAsync();

    if (existingApplicant == null)
    {
        var applicant = new Applicant
        {
            Name = model.Name,
            Address = model.Address,
            DateOfBirth = model.DateOfBirth,
            Email = model.Email,
            HomeOwner = model.HomeOwner
        };

        _dbContext.Applicants.Add(applicant);

        await _dbContext.SaveChangesAsync();
        var loan = AddLoan(model, applicant.Id);
        _dbContext.Loans.Add(loan);
    }


    else
    {
        var newLoan = AddLoan(model, existingApplicant.Id);
        _dbContext.Loans.Add(newLoan);
    }
    var success = await _dbContext.SaveChangesAsync() > 0;
    return Results.Ok(success);
});


app.MapGet("/api/loans", async (LonaDbContext _dbContext) =>
    await _dbContext.Loans
    .Select(x => new
    {
        x.Id,
        x.ApplicantId,
        ApplicantName = x.ApplicantNavigation.Name,
        x.DateCreated,
        x.ApplicantNavigation.Address,
        x.ApplicantNavigation.DateOfBirth,
        x.ApplicantNavigation.Email,
        x.ApplicantNavigation.HomeOwner,
        x.Term,
        x.PaymentPlan,
        x.InterestRate,
        x.Amount,
        x.TermlyAmountPayable,
        x.TotalAmountPayable,
        x.ApprovalStatus,
        x.ApprovalStatusActionByStaffId,
        ApprovalStatusActionByStaffName = x.StaffNavigation.Name,
        x.ApprovalStatusDate,
        Items = x.Items.Select(x => new
        {
            PaymentDueDate = x.PaymentDueDate.ToString("dd-MMMM-yyyy"),
            TermlyAmountPayable = String.Format("{0:N}", x.TermlyAmountPayable),
            IsPaid = x.IsPaid == true ? "Yes" : "No"
        })
    }).ToListAsync()
    );



app.MapGet("/api/loan/{id:long}", async (long id, LonaDbContext _dbContext) =>
    await _dbContext.Loans.Where(x => x.Id == id)
    .Select(x => new LoanDto
    {
        Id = x.Id,
        ApplicantId = x.ApplicantId,
        ApplicantName = x.ApplicantNavigation.Name,
        DateCreated = x.DateCreated,
        Address = x.ApplicantNavigation.Address,
        DateOfBirth = x.ApplicantNavigation.DateOfBirth,
        Email = x.ApplicantNavigation.Email,
        HomeOwner = x.ApplicantNavigation.HomeOwner,
        Term = x.Term,
        PaymentPlan = x.PaymentPlan,
        InterestRate = x.InterestRate,
        Amount = x.Amount,
        TermlyAmountPayable = x.TermlyAmountPayable,
        TotalAmountPayable = x.TotalAmountPayable,
        ApprovalStatus = x.ApprovalStatus,
        ApprovalStatusActionByStaffId = x.ApprovalStatusActionByStaffId,
        ApprovalStatusActionByStaffName = x.StaffNavigation.Name,
        ApprovalStatusDate = x.ApprovalStatusDate
    }).FirstOrDefaultAsync());

app.MapPut("/api/updateloan", async (ChangeLoanStatusDto model, LonaDbContext _dbContext) =>
{
    var existingLoan = await _dbContext.Loans.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
    if (existingLoan != null)
    {
        existingLoan.ApprovalStatus = model.ApprovalStatus;
        existingLoan.ApprovalStatusActionByStaffId = model.StaffId;
        existingLoan.ApprovalStatusDate = DateTime.UtcNow;

        _dbContext.Update(existingLoan);
        var success = await _dbContext.SaveChangesAsync() > 0;
        return Results.Ok(existingLoan);
    }
        return Results.NoContent();
});



//app.MapGet("/api/getstatus", (long id, ILoanService service) => new()
//{
//    new(1, "Admin", 1),
//    new(2, "User", 1),
//    new(3, "Worker", 1)

static Loan AddLoan(NewLoanDto model, long applicantId)
{
    // AutoMappercould be nice here
    var paymentList = new List<LoanPaymentList>();

    if (model.Paymentlist != null)
    {
        //add items to SalaryScaleLineItem
        foreach (var item in model.Paymentlist)
        {
            paymentList.Add(new LoanPaymentList()
            {
                ApplicantId = applicantId,
                TermlyAmountPayable = model.TermlyAmountPayable,
                PaymentDueDate = DateTime.Parse(item),
                IsPaid = false
            });
        }
    }

    var loan = new Loan
    {
        ApplicantId = applicantId,
        Term = model.Term,
        Amount = model.Amount,
        PaymentPlan = model.PaymentPlan,
        InterestRate = model.InterestRate,
        TermlyAmountPayable = model.TermlyAmountPayable,
        TotalAmountPayable = model.TotalAmountPayable,
        DateCreated = DateTime.Now,
        ApprovalStatus = "In Progress",
        Items = paymentList
    };
    return loan;
}
//});

app.Run();

