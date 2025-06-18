namespace PianoFlow.Models
{
    // Модель урока
    public class Lesson
    {
        public int Id { get; set; } // Уникальный идентификатор урока
        public string Title { get; set; } // Название урока
        public string Content { get; set; } // HTML-содержимое урока
        public int LessonTypeId { get; set; } // Внешний ключ типа урока
        public LessonType LessonType { get; set; } // Навигационное свойство типа урока
    }
} 