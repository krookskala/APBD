﻿using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.DTO
{
    public class VisitUpdateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The EmployeeId Field Is Required.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "The AnimalId Field Is Required.")]
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "The EmployeeName Field Is Required.")]
        public string EmployeeName { get; set; } = null!;

        [Required(ErrorMessage = "The AnimalName Field Is Required.")]
        public string AnimalName { get; set; } = null!;

        [Required(ErrorMessage = "The Date Field Is Required.")]
        public DateTime Date { get; set; }
    }
}