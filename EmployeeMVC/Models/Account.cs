﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeMVC.Models;

[Table("tb_m_accounts")]

public class Account
{
    [Key, Column("employee_nik", TypeName = "nchar(5)")]
    public string EmployeeNIK { get; set; }

    [Required, Column("password"), MaxLength(255)]
    public string Password { get; set; }

    // Kardinalitas
    public ICollection<AccountRole>? AccountRoles { get; set; }
    public Employee? Employee { get; set; }
}
