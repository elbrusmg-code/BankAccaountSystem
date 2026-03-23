using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Reposi.Contracts
{
    public interface ICustomerRepos :IRepository<Customer>
    {
        Customer? GetByNationalId(string nationalId);
        Customer? GetByEmail(string email);
        List<Customer> GetAllActive(int page, int pageSize);
    }
}
