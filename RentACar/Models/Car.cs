using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class Car
    {
        public enum CarMake
        {
            [Display(Name = "Audi")]
            Audi,
            [Display(Name = "Alfa Romeo")]
            AlfaRomeo,
            [Display(Name = "BMW")]
            BMW,
            [Display(Name = "Tesla")]
            Tesla,
            [Display(Name = "Tojota")]
            Toyota,
            [Display(Name = "Volksvagen")]
            Volkswagen,
            [Display(Name = "Honda")]
            Honda,
            [Display(Name = "Nisan")]
            Nissan,
            [Display(Name = "Micubisi")]
            Mitsubishi,
            [Display(Name = "Mazda")]
            Mazda,
            [Display(Name = "Ford")]
            Ford,
            [Display(Name = "Lamborgini")]
            Lamborghini,
            [Display(Name = "Sevrolet")]
            Chevrolet,
            [Display(Name = "Dodz")]
            Dodge,
            [Display(Name = "Mercedes")]
            Mercedes,
            [Display(Name = "Porse")]
            Porsche,
            [Display(Name = "Ferari")]
            Ferrari,
            [Display(Name = "Pezo")]
            Peugeot,
            [Display(Name = "Citroen")]
            Citroen,
            [Display(Name = "Reno")]
            Renault,
            [Display(Name = "Volvo")]
            Volvo,
            [Display(Name = "Opel")]
            Opel,
            [Display(Name = "Bentli")]
            Bentley,
            [Display(Name = "Skoda")]
            Skoda,

        }

        public enum FuelType
        {
            [Display(Name = "Benzin")]
            Gasoline,
            [Display(Name = "Dizel")]
            Diesel,
            [Display(Name = "Elektrik")]
            Electric,
            [Display(Name = "Hibrid")]
            Hybrid,
            [Display(Name = "Plin")]
            Lpg
        }

        public enum TransmissionType
        {
            [Display(Name = "Automatski")]
            Automatic,
            [Display(Name = "Manuelni")]
            Manual
        }

        public enum DriveType
        {
            [Display(Name = "Prednji")]
            FrontWheel,
            [Display(Name = "Zadnji")]
            RearWheel,
            [Display(Name = "4x4")]
            AllWheel
        }

        public enum CarType
        {
            [Display(Name = "Limuzina")]
            Sedan,
            [Display(Name = "Dzip")]
            SUV,
            [Display(Name = "Kupe")]
            Coupe,
            [Display(Name = "Hecbek")]
            Hatchback,
            [Display(Name = "Minivan")]
            Minivan,
            [Display(Name = "Kamion")]
            Truck,
            [Display(Name = "Pikap")]
            Pickup
        }

        public enum ColorType
        {
            [Display(Name = "Bela")]
            White,
            [Display(Name = "Crna")]
            Black,
            [Display(Name = "Siva")]
            Grey,
            [Display(Name = "Plava")]
            Blue,
            [Display(Name = "Zuta")]
            Yellow,
            [Display(Name = "Zelena")]
            Green,
            [Display(Name = "Narandzasta")]
            Orange,
            [Display(Name = "Srebrna")]
            Silver,
            [Display(Name = "Crvena")]
            Red
        }

        public enum NumberSeats
        {
            [Display(Name = "1")]
            One = 1,
            [Display(Name = "2")]
            Two = 2,
            [Display(Name = "3")]
            Three = 3,
            [Display(Name = "4")]
            Four = 4,
            [Display(Name = "5")]
            Five = 5,
            [Display(Name = "6")]
            Six = 6,
            [Display(Name = "7")]
            Seven = 7,
            [Display(Name = "8")]
            Eight = 8,
            [Display(Name = "9")]
            Nine = 9
        }

        public int CarId { get; set; }

        [Required]
        public CarMake Make { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Model ne moze imati vise od 50 karaktera!")]
        public string Model { get; set; }

        [Required]
        [Range(1900, 2025, ErrorMessage = "Godiste automobila mora biti starije od 1900. a mladje od 2025!")]
        public int Year {  get; set; }

        [Required]
        [Range(1, 5000, ErrorMessage = "Cena po danu mora biti veca od 0, a manje od 5000!")]
        public decimal PricePerDay { get; set; }
        public bool isAvailable { get; set; }

        [Required]
        public FuelType Fuel { get; set; }

        [Required]
        public TransmissionType Transmission { get; set; }

        [Required]
        public DriveType Drive { get; set; }

        [Required]
        public CarType Type { get; set; }

        [Required]
        public ColorType Color { get; set; }

        [Required]
        public NumberSeats Seats { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Mileage { get; set; }

        [Required]
        [StringLength(17), MinLength(17)]
        public string VIN {  get; set; } //Serijski broj , kombinacija simbola i brojeva

        [Required]
        [StringLength(20), MinLength(5)]
        public string LicensePlate { get; set; }
        
        public string? ImageUrl { get; set; }

    }
}
