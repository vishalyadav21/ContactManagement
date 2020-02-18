using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Contact.WebApp.Models
{
    public class ContactModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "The First Name field is required.")]
        [DisplayName("First Name")]
        [StringLength(50)]
        [RegularExpression(@"^([a-zA-Z_ ]*)$", ErrorMessage = "The First Name field must be alphabet.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The Last Name field is required.")]
        [DisplayName("Last Name")]
        [StringLength(50)]
        [RegularExpression(@"^([a-zA-Z_ ]*)$", ErrorMessage = "The Last Name field must be alphabet.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The Email Address field is required.")]
        [EmailAddress]
        [StringLength(100)]
        [DisplayName("Email Address")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email.")]
        public string Email { get; set; }
        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; }
    }
    public enum ContactStatus
    {
        Inactive = 0,
        Active = 1
    }
}

