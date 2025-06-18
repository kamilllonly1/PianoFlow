using System;

namespace PianoFlow.Models
{
    // Модель сертификата
    public class Certificate
    {
        public int Id { get; set; } // Уникальный идентификатор сертификата
        public string UserId { get; set; } // Внешний ключ пользователя
        public ApplicationUser User { get; set; } // Навигационное свойство пользователя
        public int ExamId { get; set; } // Внешний ключ экзамена
        public Exam Exam { get; set; } // Навигационное свойство экзамена
        public DateTime DateIssued { get; set; } // Дата выдачи сертификата
        public string CertificateCode { get; set; } // Уникальный код сертификата
    }
} 