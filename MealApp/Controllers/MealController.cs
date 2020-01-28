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
        [Route("")]
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

        [HttpGet]
        [Route("")]
        public async Task<JsonResult> GetAll()
        {
            return Json(await Context.Meals.ToListAsync());
        }

        private IActionResult BadRequestFromModelState()
        {
            return BadRequest(ModelState.SelectMany(m => m.Value.Errors)
                .Select(m => m.ErrorMessage)
                .ToList());
        }
    }
}