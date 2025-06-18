namespace PianoFlow.Models
{
    // Модель комментария к уроку
    public class Comment
    {
        public int Id { get; set; } // Уникальный идентификатор комментария
        public string UserId { get; set; } // Внешний ключ пользователя
        public ApplicationUser User { get; set; } // Навигационное свойство пользователя
        public int LessonId { get; set; } // Внешний ключ урока
        public Lesson Lesson { get; set; } // Навигационное свойство урока
        public string Text { get; set; } // Текст комментария
    }
} 