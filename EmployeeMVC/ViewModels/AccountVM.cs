using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeMVC.ViewModels;

public class AccountVM
{
    [MaxLength(5, ErrorMessage = "Inputan Harus 5 Karakter ex : 10923")]
    public string NIK { get; set; }
    //Email
    [Required, Column("email"), MaxLength(50)]
    public string Email { get; set; }

    [Required, Column("password"), MaxLength(255)]
    public string Password { get; set; }
}
