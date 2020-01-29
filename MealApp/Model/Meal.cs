using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        // Non-primitive type is required for model binding schema validation.
        public int? Calories { get; set; }
        
        [Required]
        // Non-primitive type is required for model binding schema validation.
        public DateTime? Time { get; set; }
    }
}