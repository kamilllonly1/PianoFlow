namespace PianoFlow.Models
{
    // Модель прогресса пользователя
    public class Progress
    {
        public int Id { get; set; } // Уникальный идентификатор прогресса
        public string UserId { get; set; } // Внешний ключ пользователя
        public ApplicationUser User { get; set; } // Навигационное свойство пользователя
        public int CompletedExercises { get; set; } // Количество выполненных упражнений
        public int Points { get; set; } // Количество баллов
        public decimal? CourseCompletion { get; set; } // Процент завершения курса
    }
} 