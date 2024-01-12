using Entity.Enum;
using Entity.Models.Learning;
using Entitys.Models;

namespace Entity.DataTransferObjects.Learning;

public record QuestionInExamDto(
    long id,
    long examId,
    QuestionTypes type,
    int orderNumber,
    MultiLanguageField? content,
    string? imageLink,
    string? docLink,
    string? writtenAnswer,
    bool? checkeding,
    decimal? AccumulatedBall,
    Guid? selected,
    List<SimpleQuestionOption>? options);