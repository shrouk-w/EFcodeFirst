﻿using System.ComponentModel.DataAnnotations;

namespace EFcodefirst.Model;

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    
    public ICollection<Prescription> Prescription { get; set; }
}