using Microsoft.EntityFrameworkCore;

namespace MealApp.Model
{
    public class MealContext: DbContext
    {
        public MealContext(DbContextOptions<MealContext> options) : base(options)
        {
        }

        public DbSet<Meal> Meals { get; set; }
    }
}