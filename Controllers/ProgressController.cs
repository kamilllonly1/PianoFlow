using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PianoFlow.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PianoFlow.Controllers
{
    [Authorize]
    // Контроллер для отображения прогресса пользователя
    public class ProgressController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProgressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Страница прогресса пользователя
        public async Task<IActionResult> Index()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            if (user == null) return NotFound();

            var totalExercises = await _context.Exercises.CountAsync();
            var completedExercises = await _context.ExerciseAttempts
                .Where(a => a.UserId == user.Id && a.IsCorrect)
                .Select(a => a.ExerciseId)
                .Distinct()
                .CountAsync();
            var points = await _context.ExerciseAttempts
                .Where(a => a.UserId == user.Id && a.IsCorrect)
                .CountAsync();
            decimal percent = totalExercises > 0 ? (decimal)completedExercises / totalExercises * 100 : 0;

            ViewBag.Completed = completedExercises;
            ViewBag.Total = totalExercises;
            ViewBag.Points = points;
            ViewBag.Percent = percent;

            return View();
        }
    }
} 