using System;
using Lona.Data.Context;
using Lona.Data.DTOs;
using Lona.Data.Entities;

namespace Lona.Interfaces;

public interface ILoanService
{
    Task<List<LoanDto>> GetAll(LonaDbContext _dbContext);
    Task<LoanDto> Get(long id, LonaDbContext _dbContext);
    Task<bool> NewLoan(NewLoanDto car, LonaDbContext _dbContext);
}


