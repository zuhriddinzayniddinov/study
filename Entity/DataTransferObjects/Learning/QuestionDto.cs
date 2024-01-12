using Entity.Models.Learning;
using Entitys.Models;

namespace Entity.DataTransferObjects.Learning;

public record QuestionDto(
    long? id,
    long quizId,
    int orderNumber,
    MultiLanguageField? content,
    string? imageLink,
    string? docLink,
    decimal? ball,
    List<SimpleQuestionOption> options);