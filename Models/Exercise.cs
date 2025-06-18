namespace PianoFlow.Models
{
    // Модель упражнения
    public class Exercise
    {
        public int Id { get; set; } // Уникальный идентификатор упражнения
        public int LessonId { get; set; } // Внешний ключ урока
        public Lesson Lesson { get; set; } // Навигационное свойство урока
        public string Question { get; set; } // Вопрос (HTML)
        public string Options { get; set; } // Варианты ответа (JSON-массив)
        public string CorrectAnswer { get; set; } // Правильный ответ
        public string Explanation { get; set; } // Пояснение к ответу
        public string AudioPath { get; set; } // Путь к аудиофайлу (если есть)
    }
} 