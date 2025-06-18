using System;
using System.Linq;
using PianoFlow.Models;

namespace PianoFlow.Models
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Удаляем старые данные
            if (context.ExerciseAttempts.Any()) context.ExerciseAttempts.RemoveRange(context.ExerciseAttempts);
            if (context.Exercises.Any()) context.Exercises.RemoveRange(context.Exercises);
            if (context.ExamQuestions.Any()) context.ExamQuestions.RemoveRange(context.ExamQuestions);
            if (context.Lessons.Any()) context.Lessons.RemoveRange(context.Lessons);
            if (context.LessonTypes.Any()) context.LessonTypes.RemoveRange(context.LessonTypes);
            context.SaveChanges();

            // Добавляем тип урока
            var type = new LessonType { Name = "Базовые ноты" };
            context.LessonTypes.Add(type);
            context.SaveChanges();

            // Один урок
            var lesson = new Lesson {
                Title = "Узнай ноту по картинке",
                Content = "<b>Цель:</b> Научиться узнавать ноты по их положению на нотном стане.<br>На экране показывается изображение одной ноты на нотном стане. Ниже — 7 вариантов ответа: До, Ре, Ми, Фа, Соль, Ля, Си.",
                LessonTypeId = type.Id
            };
            context.Lessons.Add(lesson);
            context.SaveChanges();

            // 7 упражнений (по одной ноте, только картинка)
            var notes = new[] {
                ("До", "/img/notes/do.jpg"),
                ("Ре", "/img/notes/re.jpg"),
                ("Ми", "/img/notes/mi.jpg"),
                ("Фа", "/img/notes/fa.jpg"),
                ("Соль", "/img/notes/sol.jpg"),
                ("Ля", "/img/notes/la.jpg"),
                ("Си", "/img/notes/si.jpg")
            };
            var options = "[\"До\",\"Ре\",\"Ми\",\"Фа\",\"Соль\",\"Ля\",\"Си\"]";
            foreach (var (note, img) in notes)
            {
                var ex = new Exercise
                {
                    LessonId = lesson.Id,
                    Question = $"<b>Какая это нота?</b><br><img src='{img}' alt='Нота {note}' style='max-width:200px;'>",
                    Options = options,
                    CorrectAnswer = note,
                    Explanation = $"Это была нота {note}.",
                    AudioPath = ""
                };
                context.Exercises.Add(ex);
            }
            context.SaveChanges();

            // Экзаменационные вопросы (только текстовые, без картинок)
            if (!context.ExamQuestions.Any())
            {
                var examOptions = "[\"До\",\"Ре\",\"Ми\",\"Фа\",\"Соль\",\"Ля\",\"Си\"]";
                var q1 = new ExamQuestion
                {
                    Question = "Какая 2-я нота?",
                    Options = examOptions,
                    CorrectAnswer = "Ре",
                    Explanation = "Вторая нота — Ре."
                };
                var q2 = new ExamQuestion
                {
                    Question = "Что идет после ноты Фа?",
                    Options = examOptions,
                    CorrectAnswer = "Соль",
                    Explanation = "После Фа идет Соль."
                };
                var q3 = new ExamQuestion
                {
                    Question = "Какая последняя нота?",
                    Options = examOptions,
                    CorrectAnswer = "Си",
                    Explanation = "Последняя нота — Си."
                };
                var q4 = new ExamQuestion
                {
                    Question = "Что идет перед нотой До?",
                    Options = examOptions,
                    CorrectAnswer = "Си",
                    Explanation = "Перед До — Си (если считать по кругу)."
                };
                context.ExamQuestions.AddRange(q1, q2, q3, q4);
                context.SaveChanges();
            }
        }
    }
} 