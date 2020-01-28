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
        
        [StringLength(5000)]
        [Required]
        public string Name { get; set; }
        
        [Required]
        public int Calories { get; set; }
        
        [Required]
        public DateTime Time { get; set; }
    }
}