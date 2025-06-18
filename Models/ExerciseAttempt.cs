namespace PianoFlow.Models
{
    // Модель попытки выполнения упражнения
    public class ExerciseAttempt
    {
        public int Id { get; set; } // Уникальный идентификатор попытки
        public string UserId { get; set; } // Внешний ключ пользователя
        public ApplicationUser User { get; set; } // Навигационное свойство пользователя
        public int ExerciseId { get; set; } // Внешний ключ упражнения
        public Exercise Exercise { get; set; } // Навигационное свойство упражнения
        public bool IsCorrect { get; set; } // Был ли ответ правильным
    }
} 