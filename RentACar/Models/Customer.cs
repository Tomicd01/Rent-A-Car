using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string DriverLicenseNumber { get; set; }
    }
}
