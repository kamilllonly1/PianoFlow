using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PianoFlow.Models;
using System.Threading.Tasks;

namespace PianoFlow.Controllers
{
    [Authorize]
    // Контроллер для добавления комментариев к урокам
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Добавление нового комментария
        [HttpPost]
        public async Task<IActionResult> Add(int lessonId, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                TempData["CommentError"] = "Комментарий не может быть пустым.";
                return RedirectToAction("Details", "Lessons", new { id = lessonId });
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var comment = new Comment
            {
                UserId = user.Id,
                LessonId = lessonId,
                Text = text
            };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Lessons", new { id = lessonId });
        }
    }
} 