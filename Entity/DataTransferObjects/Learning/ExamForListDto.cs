using Entity.Enum;

namespace Entity.DataTransferObjects.Learning;

public record ExamForListDto(
    long id,
    DateTime createAt,
    DateTime? closeAt,
    ExamStatus status,
    int  remainedHeart,
    decimal totalScore,
    decimal collectedScore);