using System.ComponentModel.DataAnnotations;

namespace attendance_system_backend.Models
{
    public class Address
    {
        [Required(ErrorMessage = "Street address is required.")]
        [StringLength(100, ErrorMessage = "Street address can't be longer than 100 characters.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City can't be longer than 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State/Province is required.")]
        [StringLength(50, ErrorMessage = "State/Province can't be longer than 50 characters.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal code is required.")]
        [StringLength(20, ErrorMessage = "Postal code can't be longer than 20 characters.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(50, ErrorMessage = "Country can't be longer than 50 characters.")]
        public string Country { get; set; }
    }
}