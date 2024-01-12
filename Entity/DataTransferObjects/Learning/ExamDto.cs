using Entity.Enum;
using Entitys.Models;

namespace Entity.DataTransferObjects.Learning;

public record ExamDto(
    long id,
    long quizId,
    MultiLanguageField title,
    long? createAt,
    long? finishidAt,
    ExamStatus? status,
    int?  remainedHeart,
    List<QuestionInExamDto> questions);