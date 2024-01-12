using Entitys.Models;

namespace Entity.DataTransferObjects.Learning;

public record QuizDto(
    long? id,
    int courseId,
    MultiLanguageField title,
    MultiLanguageField description, 
    decimal totalScore,
    decimal passingScore,
    int durationMinutes);
