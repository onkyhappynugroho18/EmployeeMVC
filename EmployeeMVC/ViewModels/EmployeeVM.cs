using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace EmployeeMVC.ViewModels;

public class EmployeeVM
{
    [MaxLength(5, ErrorMessage ="Inputan Harus 5 Karakter ex : 10923")]
    public string NIK { get; set; }

    //[Display(Name = "Full Name")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    [Display(Name = "Birth Date")]
    public DateTime Birthdate { get; set; }
    public GenderEnum Gender { get; set; }

    [Display(Name = "Hiring Date")]
    public DateTime HiringDate { get; set; }

    public string Email { get; set; }

    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }
}

public enum GenderEnum
{
    Male,
    Female
}
