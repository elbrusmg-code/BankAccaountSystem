using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Services.Contract;
using DataAccess.Models;
using DataAccess.Reposi.Contracts;

namespace BusinessLogic.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepos _customerRepos;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepos customerRepos, IMapper mapper)
        {
            _customerRepos = customerRepos;
            _mapper = mapper;
        }

        public CustomerDto? GetById(int id)
        {
            var customer = _customerRepos.GetById(id);
            return customer == null ? null : _mapper.Map<CustomerDto>(customer);
        }

        public CustomerDto? GetByNationalId(string nationalId)
        {
            var customer = _customerRepos.GetByNationalId(nationalId);
            return customer == null ? null : _mapper.Map<CustomerDto>(customer);
        }

        public CustomerDto? GetByEmail(string email)
        {
            var customer = _customerRepos.GetByEmail(email);
            return customer == null ? null : _mapper.Map<CustomerDto>(customer);
        }

        public List<CustomerDto> GetAllActive(int page, int pageSize)
        {
            var customers = _customerRepos.GetAllActive(page, pageSize);
            return _mapper.Map<List<CustomerDto>>(customers);
        }

        public void Add(CreateCustomerDto dto)
        {
            if (_customerRepos.GetByNationalId(dto.NationalId) != null)
                throw new InvalidOperationException("Bu FİN ilə müştəri artıq mövcuddur.");

            if (_customerRepos.GetByEmail(dto.Email) != null)
                throw new InvalidOperationException("Bu email ilə müştəri artıq mövcuddur.");

            var customer = _mapper.Map<Customer>(dto);
            _customerRepos.Add(customer);
        }

        public void Update(int id, UpdateCustomerDto dto)
        {
            var existing = _customerRepos.GetById(id)
                ?? throw new KeyNotFoundException("Müştəri tapılmadı.");

            _mapper.Map(dto, existing);
            _customerRepos.Update(id, existing);
        }

        public void SoftDelete(int id)
        {
            var customer = _customerRepos.GetById(id)
                ?? throw new KeyNotFoundException("Müştəri tapılmadı.");

            customer.IsDeleted = true;
            _customerRepos.Update(id, customer);
        }
    }
}