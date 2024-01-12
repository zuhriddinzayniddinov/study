using Entitys.Models;

namespace Entity.Models.Learning;

public class SimpleQuestionOption
{
    public MultiLanguageField AnswerContent { get; set; }
    public Guid AnswerId { get; set; } = Guid.NewGuid();
    public decimal Ball { get; set; } = 0;
}