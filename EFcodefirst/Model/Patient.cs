﻿using System.ComponentModel.DataAnnotations;

namespace EFcodefirst.Model;

public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    
    public ICollection<Prescription> Prescription { get; set; }
}