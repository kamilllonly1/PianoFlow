using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PianoFlow.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace PianoFlow.Controllers
{
    [Authorize]
    // Контроллер для работы с упражнениями
    public class ExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Список упражнений по уроку
        public async Task<IActionResult> Index(int lessonId)
        {
            var lesson = await _context.Lessons.FindAsync(lessonId);
            if (lesson == null) return NotFound();
            ViewBag.LessonTitle = lesson.Title;
            var exercises = await _context.Exercises.Where(e => e.LessonId == lessonId).ToListAsync();
            return View(exercises);
        }

        // Страница выполнения упражнения
        [HttpGet]
        public async Task<IActionResult> Do(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null) return NotFound();
            ViewBag.Options = JsonSerializer.Deserialize<string[]>(exercise.Options);
            return View(exercise);
        }

        // Проверка ответа на упражнение
        [HttpPost]
        public async Task<IActionResult> Do(int id, string selectedOption)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null) return NotFound();
            bool isCorrect = selectedOption == exercise.CorrectAnswer;
            // Сохраняем попытку пользователя
            var attempt = new ExerciseAttempt
            {
                UserId = _context.Users.First(u => u.UserName == User.Identity.Name).Id,
                ExerciseId = id,
                IsCorrect = isCorrect
            };
            _context.ExerciseAttempts.Add(attempt);
            await _context.SaveChangesAsync();
            ViewBag.Options = JsonSerializer.Deserialize<string[]>(exercise.Options);
            ViewBag.Result = isCorrect ? "Верно!" : $"Неверно. Правильный ответ: {exercise.CorrectAnswer}";
            return View(exercise);
        }
    }
} 