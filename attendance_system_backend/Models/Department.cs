using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace attendance_system_backend.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, ErrorMessage = "Department name can't be longer than 100 characters.")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "Description can't be longer than 250 characters.")]
        public string Description { get; set; }
    }
}