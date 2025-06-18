using System;

namespace PianoFlow.Models
{
    // Модель экзамена
    public class Exam
    {
        public int Id { get; set; } // Уникальный идентификатор экзамена
        public string UserId { get; set; } // Внешний ключ пользователя
        public ApplicationUser User { get; set; } // Навигационное свойство пользователя
        public int Points { get; set; } // Количество баллов за экзамен
        public DateTime DateTaken { get; set; } // Дата сдачи экзамена
    }
} 