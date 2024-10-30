using System.ComponentModel.DataAnnotations;

namespace StudentAdminSys.Models {
    public class Student {
        [Required(ErrorMessage = "Please enter the student's name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please choose an education")]
        public int Education { get; set; }

        [Required(ErrorMessage = "Please choose an option")]
        public int SemesterId { get; set; }

        [Required(ErrorMessage = "Please enter the student's email")]
        [EmailAddress]
        [Key]
        public string? Email {  get; set; }
    }
}
