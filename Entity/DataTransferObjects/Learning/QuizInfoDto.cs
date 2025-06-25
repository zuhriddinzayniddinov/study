using Entitys.Models;

namespace Entity.DataTransferObjects.Learning;

public record QuizInfoDto(
    long id,
    decimal totalScore,
    decimal? passingScore,
    int durationMinutes,
    int countQuestion,
    int restHeart);