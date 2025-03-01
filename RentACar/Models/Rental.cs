using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RentACar.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        [ForeignKey("Car")]
        public int CarId { get; set; }

        public Car Car { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
