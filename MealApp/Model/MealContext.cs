using Microsoft.EntityFrameworkCore;

namespace MealApp.Model
{
    public class MealContext: DbContext
    {
        public DbSet<Meal> meals;
        
    }
}