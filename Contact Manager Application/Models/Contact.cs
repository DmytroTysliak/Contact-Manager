using System.ComponentModel.DataAnnotations;

namespace Contact_Manager_Application.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public bool Married { get; set; }

        [Phone(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salaru must be positive")]
        public decimal Salary { get; set; }
    }
}
