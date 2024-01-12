using Entitys.Models;

namespace Entity.DataTransferObjects.Learning;

public record QuizInfoDto(
    long id,
    MultiLanguageField title,
    MultiLanguageField description, 
    decimal totalScore,
    decimal? passingScore,
    int durationMinutes,
    int countQuestion,
    int restHeart);