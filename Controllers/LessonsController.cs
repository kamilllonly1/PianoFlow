using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PianoFlow.Models;
using System.Threading.Tasks;
using System.Linq;

namespace PianoFlow.Controllers
{
    [Authorize]
    // Контроллер для работы с уроками
    public class LessonsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LessonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Список всех уроков
        public async Task<IActionResult> Index()
        {
            var lessons = await _context.Lessons.Include(l => l.LessonType).ToListAsync();
            return View(lessons);
        }

        // Детали выбранного урока
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var lesson = await _context.Lessons.Include(l => l.LessonType)
                                               .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
                return NotFound();
            return View(lesson);
        }
    }
} 