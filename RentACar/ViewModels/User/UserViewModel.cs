using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RentACar.ViewModels.User
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Ime je obavezno")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Duzina mora biti veca od 2, a manja od 50 karaktera!")]
        //[StringLengthRangeAttribute(2,50, ErrorMessage = "Ime mora da ima izmedju 2 i 50 karaktera")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Duzina mora biti veca od 2, a manja od 50 karaktera!")]
        //[StringLengthRangeAttribute(2, 50, ErrorMessage = "Ime mora da ima izmedju 2 i 50 karaktera")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email je obavezan")]
        [EmailAddress(ErrorMessage = "Pogresna forma, email potreban!")]
        [DataType(DataType.EmailAddress)]
        [EmailValidationAttribute]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password je obavezan")]
        [DataType(DataType.Password)]
        [StringLength(45, MinimumLength = 6, ErrorMessage = "Lozinka mora imati izmedju 6 i 45 karaktera!")]
        [PasswordStrengthAttribute(6, 45)]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Potvrda passworda je obavezna!")]
        [DataType(DataType.Password)]
        [PasswordMatchAttribute("PasswordHash", ErrorMessage = "Lozinke se ne poklapaju.")]
        public string ConfirmPasswordHash { get; set; }

        [Required(ErrorMessage = "Broj telefona je obavezan")]
        [PhoneNumberValidation(8, 15)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Adresa je obavezna")]
        [StringLength(100, ErrorMessage = "Adresa ne moze imati vise od 100 karaktera!")]
        public string Address { get; set; }

        [StringLength(50, ErrorMessage = "Duzina mora biti manja od 50 karaktera!")]
        public string Username { get; set; }
    }

    public class StringLengthRangeAttribute : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public StringLengthRangeAttribute(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is string stringUsername)
            {
                if(stringUsername.Length <  _minLength || stringUsername.Length > _maxLength)
                {
                    return new ValidationResult(ErrorMessage ?? $"Duzina mora da bude veca od {_minLength} i manja od {_maxLength}");
                }

            }

            return ValidationResult.Success;
        }

    }

    public class PasswordStrengthAttribute : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public PasswordStrengthAttribute(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is string stringPassword)
            {
                if(stringPassword.Length < _minLength || stringPassword.Length > _maxLength)
                {
                    return new ValidationResult(ErrorMessage ?? $"Duzina mora da bude veca od {_minLength} i manja od {_maxLength}");
                }

                if (!Regex.IsMatch(stringPassword, @"[!@#$%^&*()_+]"))
                {
                    return new ValidationResult(ErrorMessage ?? "Potreban barem 1 specijalan karakter.");
                }

                if (!Regex.IsMatch(stringPassword, @"[A-Z]"))
                {
                    return new ValidationResult(ErrorMessage ?? "Potrebno jedno veliko slovo!");
                }
            }

            return ValidationResult.Success;
        }
    }

    public class EmailValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
           if (value is string email)
           {
                var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

                if(!regex.IsMatch(email))
                {
                    return new ValidationResult(ErrorMessage ?? "Neispravan email.");
                }
           }

           return ValidationResult.Success;
        }
    }

    public class PhoneNumberValidationAttribute : ValidationAttribute
    {
        protected int _minLength;
        protected int _maxLength;

        public PhoneNumberValidationAttribute(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is string number)
            {
                var regex = new Regex(@"^(\+?[0-9]{1,3})?[-.\s]?([0-9]{2,4})[-.\s]?([0-9]{3,4})[-.\s]?([0-9]{3,4})$");

                if(number.Length <  _minLength || number.Length > _maxLength)
                {
                    return new ValidationResult(ErrorMessage ?? $"Broj telefona mora imati vise od {_minLength} cifara, a manje od {_maxLength} cifara.");
                }

                if (!regex.IsMatch(number))
                {
                    return new ValidationResult(ErrorMessage ?? "Neispravan broj telefona.");
                }
            }
            return ValidationResult.Success;
        }
    }

    public class PasswordMatchAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public PasswordMatchAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var currentValue = value as string;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                return new ValidationResult(ErrorMessage ?? $"Property {property} ne postoji.");
            }

            var comparisonValue = property.GetValue(validationContext.ObjectInstance) as string;
            if(!string.Equals(currentValue, comparisonValue))
            {
                return new ValidationResult(ErrorMessage ?? $"Lozinke se ne poklapaju");
            }

            return ValidationResult.Success;
        }
    }
}