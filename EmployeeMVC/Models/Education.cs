﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeMVC.Models;

[Table("tb_m_educations")]
public class Education
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Required, Column("major"), MaxLength(100)]
    public string Major { get; set; }
    [Required, Column("degree", TypeName = "nchar(2)")]
    public string Degree { get; set; }
    [Required, Column("gpa")]
    public float Gpa { get; set; }
    [Required, Column("university_id")]
    public int UniversityId { get; set; }

    // Relasi & Kardinalitas
    [ForeignKey(nameof(UniversityId))]
    public University? University { get; set; }
    public ICollection<Profiling>? Profilings { get; set; }
}
