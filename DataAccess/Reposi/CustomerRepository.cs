using DataAccess.Data;
using DataAccess.Models;
using DataAccess.Reposi.Contracts;
using System;
using System.Collections.Generic;
namespace DataAccess.Reposi;

public class CustomerRepository : EfCoreRepository<Customer>, ICustomerRepos
{
    public CustomerRepository(BankContext context) : base(context) { }

    public Customer? GetByNationalId(string nationalId)
        => AppDbContext.Customers
            .FirstOrDefault(c => c.NationalId == nationalId && !c.IsDeleted);

    public Customer? GetByEmail(string email)
        => AppDbContext.Customers
            .FirstOrDefault(c => c.Email == email && !c.IsDeleted);

    public List<Customer> GetAllActive(int page, int pageSize)
        => AppDbContext.Customers
            .Where(c => !c.IsDeleted)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
}