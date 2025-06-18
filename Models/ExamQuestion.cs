namespace PianoFlow.Models
{
    // Модель вопроса экзамена
    public class ExamQuestion
    {
        public int Id { get; set; } // Уникальный идентификатор вопроса
        public int? ExamId { get; set; } // Внешний ключ экзамена (опционально)
        public Exam? Exam { get; set; } // Навигационное свойство экзамена
        public string Question { get; set; } // Текст вопроса
        public string Options { get; set; } // Варианты ответа (JSON-массив)
        public string CorrectAnswer { get; set; } // Правильный ответ
        public string Explanation { get; set; } // Пояснение к ответу
    }
} 