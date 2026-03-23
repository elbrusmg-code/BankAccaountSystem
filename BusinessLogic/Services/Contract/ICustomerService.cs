using BusinessLogic.Dtos;

namespace BusinessLogic.Services.Contract
{
    public interface ICustomerService
    {
        CustomerDto? GetById(int id);
        CustomerDto? GetByNationalId(string nationalId);
        CustomerDto? GetByEmail(string email);
        List<CustomerDto> GetAllActive(int page, int pageSize);
        void Add(CreateCustomerDto dto);
        void Update(int id, UpdateCustomerDto dto);
        void SoftDelete(int id);
    }
}