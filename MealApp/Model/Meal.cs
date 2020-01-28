using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MealApp.Model
{
    [Table("Meal")]
    public class Meal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        // Non-primitive type is required for model binding.
        public int? Calories { get; set; }
        
        [Required]
        // Non-primitive type is required for model binding.
        public DateTime? Time { get; set; }
    }
}