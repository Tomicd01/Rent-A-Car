using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Models
{
    public class Payment
    {
        public enum PaymentType
        {
            Cash,
            Card,
            PayPal,
            Crypto,
            Invoice
        }
        [Required]
        public int PaymentId { get; set; }
        [Required]
        [ForeignKey("Rental")]
        public int RentalId { get; set; }

        public Rental Rental { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public PaymentType PaymentT { get; set; }
    }
}
