using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MealApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MealApp.Controllers
{
    [Route("/meals/")]
    public class MealController: Controller
    {
        private MealContext Context { get; }
        
        public MealController(MealContext context)
        {
            Context = context;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMeal([FromBody] Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestFromModelState();
            }
            
            Context.Meals.Add(meal);
            await Context.SaveChangesAsync();
            return Created("", meal);
        }
        
        public async Task<JsonResult> GetAll()
        {
            return Json(await Context.Meals.ToListAsync());
        }
        
        [Route("{date}/")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllFromDate(string date)
        {
            var requestedDate = ParseDate(date);

            if (!ModelState.IsValid)
            {
                return BadRequestFromModelState();
            }
            
            return Json(
                await Context.Meals
                    .Where(m => (((DateTime)m.Time).Date) == requestedDate.Date)
                    .ToListAsync()
                );
        }
        
        [Route("{date}/calories")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSumCaloriesFromDate(string date)
        {
            var requestedDate = ParseDate(date);

            if (!ModelState.IsValid)
            {
                return BadRequestFromModelState();
            }
            
            return Json(new
            {
                sumCalories = (
                    await Context.Meals
                        .Where(m => (((DateTime)m.Time).Date) == requestedDate.Date)
                        .ToListAsync()
                    ).Aggregate(0, (sum, meal) => sum + (meal.Calories ?? 0))
            });
        }

        private IActionResult BadRequestFromModelState()
        {
            return BadRequest(
                ModelState
                        .SelectMany(m => m.Value.Errors)
                        .Select(m => m.ErrorMessage)
                        .ToList()
                );
        }

        private DateTime ParseDate(string date)
        {
            const string dateFormat = "yyyy-MM-dd";
            if (!DateTime.TryParseExact(date, dateFormat, CultureInfo.InvariantCulture, 
                DateTimeStyles.None,
                out var requestedDate))
            {
                ModelState.AddModelError(
                    "" ,$"Cannot parse date string. Its format should be {dateFormat}."
                    );
            }

            return requestedDate;
        }
    }
}