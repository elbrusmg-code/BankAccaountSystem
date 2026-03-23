using DataAccess.Models.Enums;

namespace BusinessLogic.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string NationalId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AccountCount { get; set; }
    }

    public class CreateCustomerDto
    {
        public string FullName { get; set; } = null!;
        public string NationalId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
    }

    public class UpdateCustomerDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}