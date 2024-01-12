using Entity.Enum;
using Entity.Models.Learning;

namespace Entity.DataTransferObjects.Learning;

public record ExamResultDto(
    long id,
    Quiz quiz,
    DateTime createAt,
    DateTime closeAt,
    ExamStatus status,
    TimeSpan duration,
    int  remainedHeart,
    decimal collectedScore,
    List<QuestionInExamDto> questions);