using Entitys.Models;

namespace Entity.DataTransferObjects.Learning;

public record QuizDto(
    long? id, 
    decimal totalScore,
    decimal passingScore,
    int durationMinutes);
