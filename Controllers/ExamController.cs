using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PianoFlow.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace PianoFlow.Controllers
{
    [Authorize]
    // Контроллер для экзамена и сертификата
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ExamController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Страница допуска к экзамену
        public async Task<IActionResult> Index()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            if (user == null) return NotFound();
            var totalExercises = await _context.Exercises.CountAsync();
            var completed = await _context.ExerciseAttempts.Where(a => a.UserId == user.Id && a.IsCorrect).Select(a => a.ExerciseId).Distinct().CountAsync();
            decimal percent = totalExercises > 0 ? (decimal)completed / totalExercises * 100 : 0;
            ViewBag.Allowed = percent >= 80; // 80% для допуска
            ViewBag.Percent = percent;
            return View();
        }

        // Старт экзамена (GET)
        public async Task<IActionResult> Start()
        {
            var questions = await _context.ExamQuestions.Take(5).ToListAsync();
            ViewBag.Questions = questions;
            return View();
        }

        // Проверка экзамена (POST)
        [HttpPost]
        public async Task<IActionResult> Start(List<string> answers)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var questions = await _context.ExamQuestions.Take(5).ToListAsync();
            int score = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                if (i < answers.Count && answers[i] == questions[i].CorrectAnswer)
                    score++;
            }
            var exam = new Exam
            {
                UserId = user.Id,
                Points = score,
                DateTaken = DateTime.UtcNow
            };
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();

            // Если экзамен сдан, создаём сертификат
            if (score >= questions.Count * 0.6)
            {
                var cert = new Certificate
                {
                    UserId = user.Id,
                    ExamId = exam.Id,
                    DateIssued = DateTime.UtcNow,
                    CertificateCode = Guid.NewGuid().ToString().Substring(0, 8)
                };
                _context.Certificates.Add(cert);
                await _context.SaveChangesAsync();
                TempData["CertificateId"] = cert.Id;
            }

            TempData["ExamResult"] = score;
            TempData["ExamTotal"] = questions.Count;
            return RedirectToAction("Result");
        }

        // Страница результата экзамена
        public IActionResult Result()
        {
            ViewBag.Score = TempData["ExamResult"];
            ViewBag.Total = TempData["ExamTotal"];
            ViewBag.CertificateId = TempData["CertificateId"];
            return View();
        }

        // Страница сертификата
        public async Task<IActionResult> Certificate(int id)
        {
            var cert = await _context.Certificates.Include(c => c.User).Include(c => c.Exam).FirstOrDefaultAsync(c => c.Id == id);
            if (cert == null) return NotFound();
            return View(cert);
        }
    }
} 